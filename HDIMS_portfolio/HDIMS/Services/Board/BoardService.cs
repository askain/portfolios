using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.Board;

namespace HDIMS.Services.Board
{
    public class BoardService : IBoardService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(BoardService));
        #endregion


        #region == 게시    판     정보 ==
        #region == Select() ==
        public IList<BoardModel> SelectBoard(Hashtable param)
        {
            IList<BoardModel> list = new List<BoardModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<BoardModel>("Board.SelectBoard", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == Insert() ==
        public int InsertBoard(IEnumerable<BoardModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (BoardModel item in param)
                {
                    //게시판을 DB에 등록
                    DaoFactory.Instance.Insert("Board.InsertBoard", item);
                    
                    //게시판을 메뉴에 등록
                    DaoFactory.Instance.Insert("Board.InsertBoardInMenu", item);

                    //권한-메뉴 설정
                    if (item.READAUTHCODE != null)
                    {
                        string[] AuthCdList = item.READAUTHCODE.Split(new char[] { ',' });
                        foreach (string AuthCd in AuthCdList)
                        {
                            if (string.IsNullOrEmpty(AuthCd)) continue;

                            Hashtable ht = new Hashtable();
                            ht.Add("MENU_ID", item.MENU_ID);
                            ht.Add("AUTHCODE", AuthCd);
                            DaoFactory.Instance.Insert("Board.InsertAuthMenu", ht);
                        }
                    }

                    iRetVal++;
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                log.Error(e);
                throw e;
            }

            return iRetVal;
        }
        #endregion

        #region == Update() ==
        public int UpdateBoard(IEnumerable<BoardModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (BoardModel item in param)
                {
                    //게시판을 DB에 등록
                    iRetVal += DaoFactory.Instance.Update("Board.UpdateBoard", item);

                    //게시판을 메뉴에 등록
                    DaoFactory.Instance.Update("Board.UpdateBoardInMenu", item);

                    //권한-메뉴 설정
                    DaoFactory.Instance.Delete("Board.DeleteAuthMenu", item);   //모두 삭제 후 새로 입력
                    if (item.READAUTHCODE != null)
                    {
                        string[] AuthCdList = item.READAUTHCODE.Split(new char[] { ',' });
                        foreach (string AuthCd in AuthCdList)
                        {
                            if (string.IsNullOrEmpty(AuthCd)) continue;

                            Hashtable ht = new Hashtable();
                            ht.Add("MENU_ID", item.MENU_ID);
                            ht.Add("AUTHCODE", AuthCd);
                            DaoFactory.Instance.Insert("Board.InsertAuthMenu", ht);
                        }
                    }
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                throw e;
            }
            return iRetVal;
        }
        #endregion

        #region == Delete() ==
        public int DeleteBoard(IEnumerable<BoardModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (BoardModel item in param)
                {
                    //게시판을 DB에 등록
                    iRetVal += DaoFactory.Instance.Update("Board.DeleteBoard", item);

                    //게시판을 메뉴에 등록
                    DaoFactory.Instance.Update("Board.DeleteBoardInMenu", item);

                    //권한-메뉴 설정
                    DaoFactory.Instance.Delete("Board.DeleteAuthMenu", item);   //모두 삭제 후 새로 입력
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                throw e;
            }

            return iRetVal;
        }
        #endregion
        #endregion
        
        #region == 게시    물     정보 ==
        #region == SelectContent() ==
        public IList<BoardContentModel> SelectContent(Hashtable param)
        {
            IList<BoardContentModel> list = new List<BoardContentModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<BoardContentModel>("Board.SelectContent", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == GetContentCnt() ==
        public int SelectContentCnt(Hashtable param)
        {
            int cnt = 0;

            try
            {
                cnt = DaoFactory.Instance.QueryForObject<int>("Board.SelectContentCnt", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return cnt;
        }
        #endregion

        #region == Insert() ==
        public string InsertContent(BoardContentModel param)
        {
            string premaryKey;
            int iRetVal = 0;
            try
            {
                premaryKey = DaoFactory.Instance.Insert("Board.InsertContent", param) as string;
                iRetVal++;
            }
            catch (Exception e)
            {
                iRetVal = 0;
                throw e;
            }

            return premaryKey;
        }
        #endregion

        #region == Update() ==
        public int UpdateContent(BoardContentModel param)
        {
            int iRetVal = 0;

            try
            {
                iRetVal += DaoFactory.Instance.Update("Board.UpdateContent", param);

            }
            catch (Exception e)
            {
                
                iRetVal = 0;
                throw e;
            }
            return iRetVal;
        }
        #endregion

        #region == Delete() ==
        public int DeleteContent(BoardContentModel param)
        {
            int iRetVal = 0;

            try
            {
                iRetVal += DaoFactory.Instance.Update("Board.DeleteContent", param);
            }
            catch (Exception e)
            {
                iRetVal = 0;
                throw e;
            }

            return iRetVal;
        }
        #endregion

        #region == CountReadContent() ==
        public int CountReadContent(BoardContentModel param)
        {
            int iRetVal = 0;

            try
            {
                iRetVal += DaoFactory.Instance.Update("Board.CountReadContent", param);

            }
            catch (Exception e)
            {
                
                iRetVal = 0;
                throw e;
            }
            return iRetVal;
        }
        #endregion

        #endregion

        
        #region == 의견     글     정보 ==
        #region == SelectContent() ==
        public IList<BoardReplyModel> SelectReply(Hashtable param)
        {
            IList<BoardReplyModel> list = new List<BoardReplyModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<BoardReplyModel>("Board.SelectReply", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion
        
        #region == Insert() ==
        public int InsertContent(BoardReplyModel param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.Insert("Board.InsertReply", param);
                iRetVal++;
            }
            catch (Exception e)
            {
                iRetVal = 0;
                throw e;
            }

            return iRetVal;
        }
        #endregion

        #region == Delete() ==
        public int DeleteContent(BoardReplyModel param)
        {
            int iRetVal = 0;

            try
            {
                iRetVal += DaoFactory.Instance.Delete("Board.DeleteReply", param);
            }
            catch (Exception e)
            {
                iRetVal = 0;
                throw e;
            }

            return iRetVal;
        }
        #endregion
        #endregion
    }
}