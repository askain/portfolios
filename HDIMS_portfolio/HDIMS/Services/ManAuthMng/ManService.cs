using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.ManAuthMng;

namespace HDIMS.Services.ManAuthMng
{
    public class ManService : IManService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(ManService));
        #endregion

        #region == Count() ==
        public int Count(Hashtable param)
        {
            int cnt = 0;

            try
            {
                cnt = DaoFactory.Instance.QueryForObject<int>("ManAuthMng.GetManCount", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return cnt;
        }
        #endregion

        #region == Select() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.10
        /// 3. 설  명 : 담당자관리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<ManModel> Select(Hashtable param)
        {
            IList<ManModel> list = new List<ManModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<ManModel>("ManAuthMng.GetManList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }
        #endregion

        #region == SelectCombo() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.10
        /// 3. 설  명 : 담당자관리 권한 콤보 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<AuthModel> SelectCombo()
        {
            IList<AuthModel> list = new List<AuthModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<AuthModel>("ManAuthMng.GetAuthCombo", null);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }
        #endregion

        #region == InsertUpdate() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.10
        /// 3. 설  명 : 담당자관리 Row 추가 / 수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int InsertUpdate(IList<ManModel> ManList)
        {
            IList<ManModel> list = new List<ManModel>();
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (ManModel item in ManList)
                {
                    ManModel man = DaoFactory.Instance.QueryForObject<ManModel>("ManAuthMng.GetAuthUserList", item);
                    if (man != null)
                    {
                        DaoFactory.Instance.Update("ManAuthMng.UpdateAuthUser", item);
                    }
                    else
                    {
                        DaoFactory.Instance.Insert("ManAuthMng.InsertAuthUser", item);
                    }
                    iRetVal++;

                    man = null;
                    man = DaoFactory.Instance.QueryForObject<ManModel>("ManAuthMng.GetMan", item);
                    if (man != null)
                    {
                        DaoFactory.Instance.Update("ManAuthMng.UpdateMan", item);
                    }
                    else
                    {
                        DaoFactory.Instance.Insert("ManAuthMng.InsertMan", item);
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

        #region == SelectMan() ==
        public ManModel SelectMan(ManModel param)
        {
            ManModel man = new ManModel();

            try
            {
                man = DaoFactory.Instance.QueryForObject<ManModel>("ManAuthMng.GetMan", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return man;
        }
        #endregion

        #region == InsertUpdateHome() ==
        public int InsertUpdateHome(Hashtable param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();

                ManModel man = DaoFactory.Instance.QueryForObject<ManModel>("ManAuthMng.GetMan", param);
                if (man != null)
                {
                    DaoFactory.Instance.Update("ManAuthMng.UpdateHome", param);
                }
                else
                {
                    DaoFactory.Instance.Insert("ManAuthMng.InsertHome", param);
                }
                iRetVal++;

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
    }
}