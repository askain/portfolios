﻿using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Web;
using log4net;

namespace HDIMS.HttpHandlers
{
    public class UploaderControlHandler : IHttpAsyncHandler
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UploaderControlHandler));

        #region IHttpAsyncHandler Members

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            log.Info("== 파일업로드 시작 ===");
            UploaderControl2AsyncResult result = new UploaderControl2AsyncResult(context, cb, extraData);

            // parse the parameters sent to the request and bundle them up to be passed to the worker thread
            UploaderControl2ProcessFileState processFileState = new UploaderControl2ProcessFileState(context, result);

            // do all of the work on a custom thread pool so that the asp.net thread pool doesn't get blocked up

            // TODO: This example uses the built-in ThreadPool, but you should use a custom thread pool!  The built-in thread pool
            // draws from the same threads that the web server uses to service web requests, so you will see no benefit.
            // See the codeplex project for some links to open source custom thread pool implementations.

            // change this line to queue ProcessFile onto your custom thread pool
            ThreadPool.QueueUserWorkItem(ProcessFile, processFileState);

            return result;
        }

        /// <summary>
        /// Process the file.
        /// </summary>
        private void ProcessFile(object State)
        {
            UploaderControl2ProcessFileState processFileState = State as UploaderControl2ProcessFileState;

            log.Info("BoardCd:" + processFileState.BoardCd + " / ContentCd : " + processFileState.ContentCd + " / Guid : " + processFileState.Guid + " / FileName : " + processFileState.Name);

            try
            {
                // the path to where this file will be written on disk
                string sandboxPath = Path.Combine(Sandbox.GetSandboxPathCombinWith(processFileState.BoardCd, processFileState.ContentCd), processFileState.Guid);

                log.Info(sandboxPath);

                // Is it the first chunk or is the upload for this file being canceled? Create the file on the server
                if (processFileState.Cancel || processFileState.FirstChunk)
                {
                    // if the file already exists in the sandbox, delete it
                    if (File.Exists(sandboxPath))
                        File.Delete(sandboxPath);
                }                

                if (!processFileState.Cancel)
                {
                    // if the process flag is set to true, the entire file has been sent, so call the custom file processor
                    if (processFileState.Process && !string.IsNullOrEmpty(processFileState.UploadedFileProcessorType))
                    {
                        string typeName = processFileState.UploadedFileProcessorType;
                        string className = typeName.Substring(0, typeName.IndexOf(",")).Trim();
                        IUploadedFileProcessor processor = Type.GetType(typeName, true).Assembly.CreateInstance(className) as IUploadedFileProcessor;
                        processor.ProcessFile(processFileState.AsyncResult.Context, processFileState.Guid, processFileState.Name, processFileState.ContextParam);
                    }
                    else
                    {
                        // this is another chunk of data
                        byte[] buffer = new byte[4096];
                        int bytesRead;
                        using (FileStream fs = File.Open(sandboxPath, FileMode.Append))
                        {
                            while ((bytesRead = processFileState.AsyncResult.Context.Request.InputStream.Read(buffer, 0, buffer.Length)) != 0)
                                fs.Write(buffer, 0, bytesRead);
                        }
                    }
                }
                else log.Info("== 업로드 취소 ==");

                processFileState.AsyncResult.CompleteCall();
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                processFileState.AsyncResult.Exception = ex;
                processFileState.AsyncResult.CompleteCall();
            }
        }

        /// <summary>
        /// EndProcessRequest only does exception handling for this handler.  There is no other information currently sent
        /// back to the file uploader control.
        /// </summary>
        /// <param name="result"></param>
        public void EndProcessRequest(IAsyncResult result)
        {
            log.Info("== 파일업로드 종료 ==");
            UploaderControl2AsyncResult asyncOp = (UploaderControl2AsyncResult)result;
            if (asyncOp.Exception != null)
                asyncOp.Context.Response.Write(asyncOp.Exception.ToString());
        }

        #endregion

        /// <summary>
        /// State required by the asynchronous ProcessFile method.
        /// </summary>
        private class UploaderControl2ProcessFileState
        {
            public UploaderControl2AsyncResult AsyncResult { get; set; }
            public bool Cancel { get; set; }
            public bool Process { get; set; }
            public bool FirstChunk { get; set; }
            public bool LastChunk { get; set; }
            public string Guid { get; set; }
            public string Name { get; set; }
            public string UploadedFileProcessorType { get; set; }
            public string ContextParam { get; set; }
            public string BoardCd { get; set; }
            public string ContentCd { get; set; }

            public UploaderControl2ProcessFileState(HttpContext context, UploaderControl2AsyncResult result)
            {
                AsyncResult = result;
                Guid = context.Request.QueryString["guid"];
                FirstChunk = string.IsNullOrEmpty(context.Request.QueryString["first"]) ? false : bool.Parse(context.Request.QueryString["first"]);
                LastChunk = string.IsNullOrEmpty(context.Request.QueryString["last"]) ? false : bool.Parse(context.Request.QueryString["last"]);
                Cancel = string.IsNullOrEmpty(context.Request.QueryString["cancel"]) ? false : bool.Parse(context.Request.QueryString["cancel"]);
                Process = string.IsNullOrEmpty(context.Request.QueryString["process"]) ? false : bool.Parse(context.Request.QueryString["process"]);
                Name = context.Request.QueryString["name"];
                UploadedFileProcessorType = context.Request.QueryString["processor"];
                ContextParam = context.Request.QueryString["context"];

                BoardCd = context.Request.QueryString["BoardCd"];
                ContentCd = context.Request.QueryString["ContentCd"];
            }
        }

        private class UploaderControl2AsyncResult : IAsyncResult
        {
            private HttpContext _context;
            private AsyncCallback _cb;
            private object _state;
            private ManualResetEvent _event;
            private bool _completed = false;
            private object _lock = new object();

            public UploaderControl2AsyncResult(HttpContext context, AsyncCallback cb, object state)
            {
                _context = context;
                _cb = cb;
                _state = state;
            }

            #region IAsyncResult Members

            public object AsyncState { get { return _state; } }

            public WaitHandle AsyncWaitHandle
            {
                get
                {
                    lock (_lock)
                    {
                        if (_event == null)
                            _event = new ManualResetEvent(IsCompleted);
                        return _event;
                    }
                }
            }

            public bool CompletedSynchronously { get { return false; } }

            public bool IsCompleted { get { return _completed; } }

            #endregion

            public HttpContext Context { get { return _context; } }

            /// <summary>
            /// If an exception occurs in the worker thread, a reference is set here, and then EndProcessRequest will
            /// write it to the output.
            /// </summary>
            public Exception Exception { get; set; }

            /// <summary>
            /// Call this when work is done.
            /// </summary>
            public void CompleteCall()
            {
                lock (_lock)
                {
                    _completed = true;
                    if (_event != null) _event.Set();
                }

                if (_cb != null) _cb(this);
            }
        }

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            // never called
        }

        #endregion
    }

    public class Sandbox
    {
        /// <summary>
        /// This folder must have the proper permissions for a web application to write to it.  Either ASPNET or NETWORK SERVICE
        /// user needs write access, depending on your OS/IIS version.
        /// </summary>
        public static string UploaderControlSandboxPath
        {
            get
            {
                string path = Path.Combine(ConfigurationSettings.AppSettings["FileUploadPath"], "UploaderControl");

                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while trying to create a directory on disk for storing your uploaded files. " +
                            "Make sure that you have set the app setting 'SandboxPath' in web.config appropriately, and that the sandbox " +
                            "path has the appropriate permissions.", ex);
                    }
                }

                return path;
            }
        }

        public static string GetSandboxPathCombinWith(params string[] folderName) 
        {
            string path = Path.Combine(ConfigurationSettings.AppSettings["FileUploadPath"], "UploaderControl");

            foreach (string folder in folderName)
            {
                if (folder == null) throw new ArgumentNullException();

                path = Path.Combine(path, folder);
            }

            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while trying to create a directory on disk for storing your uploaded files. " +
                        "Make sure that you have set the app setting 'SandboxPath' in web.config appropriately, and that the sandbox " +
                        "path has the appropriate permissions.", ex);
                }
            }

            return path;
        }
    }
}