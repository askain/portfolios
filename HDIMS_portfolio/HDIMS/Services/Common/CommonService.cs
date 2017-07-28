using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.Common;

namespace HDIMS.Services.Common
{
    public class CommonService : ICommonService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CommonService));
        private static IList<ObsCode> ObsCodeList = null;

        public IList<string> GetStaticObsCodeList(Hashtable param) {
            
            if (ObsCodeList == null)
            {
                Hashtable GetAllParam = new Hashtable();
                log.Debug("------------------------------------관측국-1");
                ObsCodeList = DaoFactory.Instance.QueryForList<ObsCode>("Common.GetStaticObsCodeList", param);
                log.Debug("------------------------------------관측국-2");
            }

            IList<string> result = new List<string>();
            foreach (ObsCode c in ObsCodeList)
            {
                if (param["damcd"].Equals(c.DAMCD))
                {
                    
                    if ("WL".Equals(param["obstp"]))
                    {
                        if ("WL".Equals(c.OBSTP) || "WR".Equals(c.OBSTP)) {
                            result.Add(c.WLOBCD);
                        }
                    }
                    else if ("RF".Equals(param["obstp"]))
                    {
                        if ("RF".Equals(c.OBSTP) || "WR".Equals(c.OBSTP))
                        {
                            result.Add(c.RFOBCD);
                        }
                    }
                }
            }
            
            return result;
        }

        public IList<HDIMS.Models.Domain.Common.Code> GetDamCodeList(Hashtable param)
        {
            IList<HDIMS.Models.Domain.Common.Code> list = new List<HDIMS.Models.Domain.Common.Code>();
            try
            {
                list = DaoFactory.Instance.QueryForList<HDIMS.Models.Domain.Common.Code>("Common.GetDamCodeList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public IList<Hashtable> GetMgtCodeList(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();
            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Common.GetDamMgtCodes", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        #region == GetObsCodeList() ==
        /// <summary>
        /// 1. 작성자 : 한장희
        /// 2. 작성일 : 2011.06.20
        /// 3. 설  명 : 조회 콤보 관측소 목록 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<HDIMS.Models.Domain.Common.Code> GetObsCodeList(Hashtable param)
        {
            IList<HDIMS.Models.Domain.Common.Code> list = new List<HDIMS.Models.Domain.Common.Code>();
            try
            {
                if (param.ContainsKey("Type") == false || "".Equals(param["Type"].ToString()))
                {
                    list = DaoFactory.Instance.QueryForList<HDIMS.Models.Domain.Common.Code>("Common.GetObsCodeList", param);
                }
                else if (param["Type"].ToString() == "includeWR") 
                {
                    list = DaoFactory.Instance.QueryForList<HDIMS.Models.Domain.Common.Code>("Common.GetObsCodeWRList", param);
                }
                //else if (param["Type"].ToString() == "wlSearch")
                //{
                //    list = DaoFactory.Instance.QueryForList<HDIMS.Models.Domain.Common.Code>("Common.GetWlSearchObsCodeList", param);
                //}
                //else if (param["Type"].ToString() == "rfSearch")
                //{
                //    list = DaoFactory.Instance.QueryForList<HDIMS.Models.Domain.Common.Code>("Common.GetRfSearchObsCodeList", param);
                //}
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion


        #region == GetRfObsCodeList() ==
        /// <summary>
        /// 1. 작성자 : 정광일
        /// 2. 작성일 : 2012.03.16
        /// 3. 설  명 : N 대 N , 댐 - 관측소 대응 (보 포함)
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<HDIMS.Models.Domain.Common.Code> GetRfObsCodeList(Hashtable param)
        {
            IList<HDIMS.Models.Domain.Common.Code> list = new List<HDIMS.Models.Domain.Common.Code>();
            try
            {
                list = DaoFactory.Instance.QueryForList<HDIMS.Models.Domain.Common.Code>("Common.GetRfObsCodeList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        public IList<ExCode> GetExCodeList(Hashtable param)
        {
            IList<ExCode> list = new List<ExCode>();
            try
            {
                list = DaoFactory.Instance.QueryForList<ExCode>("Common.GetExCodeList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }

        public IList<EtcCode> GetEtcCodeList(Hashtable param)
        {
            IList<EtcCode> list = new List<EtcCode>();
            try
            {
                list = DaoFactory.Instance.QueryForList<EtcCode>("Common.GetEtcCodeList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }


        public IList<UserInfo> GetUserInfoList(Hashtable param)
        {
            IList<UserInfo> list = new List<UserInfo>();
            try
            {
                list = DaoFactory.Instance.QueryForList<UserInfo>("Common.GetUserInfoList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }

        #region == GetMenuList() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.18
        /// 3. 설  명 : 메뉴 트리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<MenuModel> GetMenuList(Hashtable param)
        {
            IList<MenuModel> list = new List<MenuModel>();
            try
            {
                list = OraDaoFactory.Instance.QueryForList<MenuModel>("Common.GetMenuList", param);

            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }

        public IList<MenuModel> GetAdminMenuList(Hashtable param)
        {
            IList<MenuModel> list = new List<MenuModel>();
            try
            {
                list = DaoFactory.Instance.QueryForList<MenuModel>("Common.GetAdminMenuList", param);

            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }

        public IList<PairValue> GetMenuTitleList(Hashtable param)
        {
            IList<PairValue> list = new List<PairValue>();
            try
            {
                list = DaoFactory.Instance.QueryForList<PairValue>("Common.GetMenuTitleList", param);

            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }
        #endregion

        #region == 보정방법 GetEdExWayList() ==
        public IList<HDIMS.Models.Domain.Common.Code> GetEdExWayList(Hashtable param)
        {
            IList<HDIMS.Models.Domain.Common.Code> list = new List<HDIMS.Models.Domain.Common.Code>();
            try
            {
                list = DaoFactory.Instance.QueryForList<HDIMS.Models.Domain.Common.Code>("Common.GetEdExWayList", param);

            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }
        #endregion

        #region == 보정등급 GetEdExLvlList() ==
        public IList<HDIMS.Models.Domain.Common.Code> GetEdExLvlList(Hashtable param)
        {
            IList<HDIMS.Models.Domain.Common.Code> list = new List<HDIMS.Models.Domain.Common.Code>();
            try
            {
                list = DaoFactory.Instance.QueryForList<HDIMS.Models.Domain.Common.Code>("Common.GetEdExLvlList", param);

            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }
        #endregion
    }
}