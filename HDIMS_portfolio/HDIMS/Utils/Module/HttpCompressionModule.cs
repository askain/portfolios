using System;
using System.IO.Compression;
using System.Web;
using Common.Logging;

namespace HDIMS.Utils.Module
{
    public class HttpCompressionModule : IHttpModule
    {
        #region IHttpModule Members
        private static readonly ILog log = LogManager.GetLogger(typeof(HttpCompressionModule));

        public HttpCompressionModule()
        {
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PostAcquireRequestState += new EventHandler(Context_PostAcquireRequestState);
            context.EndRequest += new EventHandler(Context_EndRequest);
        }

        void Context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication context = sender as HttpApplication;
            context.PostAcquireRequestState -= new EventHandler(Context_PostAcquireRequestState);
            context.EndRequest -= new EventHandler(Context_EndRequest);
        }

        void Context_PostAcquireRequestState(object sender, EventArgs e)
        {
            this.RegisterCompressFilter();
        }

        private void RegisterCompressFilter()
        {
            HttpContext context = HttpContext.Current;
            
            //log.Debug("Handler type : " + context.Handler.ToString());
            //if (context.Handler is DefaultHttpHandler) return;

            HttpRequest request = context.Request;
            if (request.FilePath != null && (
                request.FilePath.EndsWith(".xap") ||
                request.FilePath.EndsWith(".jpg") ||
                request.FilePath.EndsWith(".png") ||
                request.FilePath.EndsWith(".gif") ||
                request.FilePath.EndsWith(".swf")
                )
             ) return;

            string acceptEncoding = request.Headers["Accept-Encoding"];

            if (string.IsNullOrEmpty(acceptEncoding)) return;

            acceptEncoding = acceptEncoding.ToUpperInvariant();

            HttpResponse response = HttpContext.Current.Response;

            if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-Encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-Encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }
        #endregion
    }
}