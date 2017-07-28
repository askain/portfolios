using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Logging;
using HDIMS.HttpHandlers;
using HDIMS.Models.Domain.Board;

namespace HDIMS.Services.File
{
    public class FileService : IFileService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(FileService));
        #endregion

        #region == 파일 목록() ==
        public IList<IDictionary<string, string>> GetFileList(string BoardCd, string ContentCd)
        {
            IList<IDictionary<string, string>> UserFiles = new List<IDictionary<string, string>>();
            try
            {
                if (BoardCd == null || ContentCd == null)
                {
                    throw new ArgumentNullException(string.Format("BoardCd={0} / ContentCd={1}", BoardCd, ContentCd));
                }

                string theFolder = Sandbox.GetSandboxPathCombinWith(BoardCd, ContentCd);

                string[] filePaths = Directory.GetFiles(theFolder);

                log.Fatal(theFolder);
                log.Fatal(filePaths.Count());

                foreach (string filePath in filePaths)
                {
                    FileInfo fi = new FileInfo(filePath);
                    IDictionary<string, string> UserFile = new Dictionary<string, string>();

                    UserFile.Add("Guid", fi.Name);
                    UserFile.Add("FileSize", fi.Length.ToString());
                    //FileStream = file.OpenRead();
                    //UIDispatcher = this.Dispatcher;
                    //HttpUploader = true;
                    //UploadHandlerName = UploadHandlerPath;

                    UserFiles.Add(UserFile);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }

            return UserFiles;
        }
        #endregion

        #region == 파일 존재확인 ==
        public string IsFileExist(string BoardCd, string ContentCd)
        {
            string cnt = "0";
            try
            {
                if (BoardCd == null || ContentCd == null)
                {
                    throw new ArgumentNullException(string.Format("BoardCd={0} / ContentCd={1}", BoardCd, ContentCd));
                }

                string theFolder = Sandbox.GetSandboxPathCombinWith(BoardCd, ContentCd);

                string[] filePaths = Directory.GetFiles(theFolder);

                cnt = filePaths.Length.ToString();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }

            return cnt;
        }
        #endregion

        #region == 게시판 파일 다 삭제 ==
        public void DeleteFiles(IEnumerable<BoardModel> boards)
        {
            try
            {
                foreach (BoardModel b in boards)
                {
                    this.DeleteFiles(b);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public void DeleteFiles(BoardModel board)
        {
            if (board.BOARDCD == null)
            {
                throw new ArgumentNullException(string.Format("BoardCd={0}", board.BOARDCD));
            }

            string theFolder = Sandbox.GetSandboxPathCombinWith(board.BOARDCD);

            DirectoryInfo di = new DirectoryInfo(theFolder);
            di.Delete(true);
        }
        #endregion

        #region == 게시물 파일 다 삭제 ==
        public void DeleteFiles(BoardContentModel boardContent)
        {
            this.DeleteFiles(boardContent.BOARDCD, boardContent.CONTENTCD);
        }
        #endregion

        #region == 파일 다 삭제 ==
        public void DeleteFiles(string BoardCd, string ContentCd)
        {
            try
            {
                if (BoardCd == null || ContentCd == null)
                {
                    throw new ArgumentNullException(string.Format("BoardCd={0} / ContentCd={1}", BoardCd, ContentCd));
                }

                string theFolder = Sandbox.GetSandboxPathCombinWith(BoardCd, ContentCd);

                DirectoryInfo di = new DirectoryInfo(theFolder);

                foreach(FileInfo fi in di.GetFiles()) 
                {
                    fi.Delete();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        #endregion

        #region == 파일 삭제 ==
        public void DeleteFile(string BoardCd, string ContentCd, string Guid)
        {
            try
            {
                if (BoardCd == null || ContentCd == null) 
                {
                    throw new ArgumentNullException(string.Format("BoardCd={0} / ContentCd={1} / Guid={2}", BoardCd, ContentCd, Guid));
                }

                string theFolder = Sandbox.GetSandboxPathCombinWith(BoardCd, ContentCd);
                string filePath = Path.Combine(theFolder, Guid);

                FileInfo fi = new FileInfo(filePath);
                if (fi.Exists == true)
                {
                    fi.Delete();
                }
                else
                {
                    log.Warn("파일을 삭제하려고 하였으나 파일이 이미 존재하지 않습니다. : " + filePath);
                }

            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        #endregion

    }
}