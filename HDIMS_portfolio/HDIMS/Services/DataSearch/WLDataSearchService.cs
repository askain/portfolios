using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.DataSearch;

namespace HDIMS.Services.DataSearch
{
    public class WLDataSearchService : IWLDataSearchService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(WLDataSearchService));
        #endregion

        #region == getList() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.03
        /// 3. 설  명 : 수위자료 이력조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<WLDataSearchModel> getList(Hashtable param)
        {
            IList<WLDataSearchModel> list = new List<WLDataSearchModel>();
            //JsonModel<WLDataSearchModel> model = new JsonModel<WLDataSearchModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<WLDataSearchModel>("DataSearch.WLgetList", param);
                //model.success = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == getSearchList() ==
        public IList<WLDataSearchModel> getSearchList(Hashtable param)
        {
            IList<WLDataSearchModel> list = new List<WLDataSearchModel>();
            //JsonModel<WLDataSearchModel> model = new JsonModel<WLDataSearchModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<WLDataSearchModel>("DataSearch.WLgetSearchList", param);
                //model.success = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == getChartList() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.09.01
        /// 3. 설  명 : 수위자료 이력조회(chart)
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<WLDataSearchModel> getChartList(Hashtable param)
        {
            IList<WLDataSearchModel> list = new List<WLDataSearchModel>();
            JsonModel<WLDataSearchModel> model = new JsonModel<WLDataSearchModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<WLDataSearchModel>("DataSearch.WLgetChartList", param);
                model.success = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == getWlDataTotalCount() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.09.01
        /// 3. 설  명 : 수위자료 이력조회 카운트
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int getWlDataTotalCount(Hashtable param)
        {
            IList<WLDataSearchModel> list = new List<WLDataSearchModel>();
            JsonModel<WLDataSearchModel> model = new JsonModel<WLDataSearchModel>();
            int totalCount = 0;

            try
            {
                totalCount = DaoFactory.Instance.QueryForObject<int>("DataSearch.getWlDataTotalCount", param);
                model.success = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return totalCount;
        }
        #endregion

        #region == getHistory() ==
        public IList<WLDataHistoryModel> getHistory(Hashtable param)
        {
            IList<WLDataHistoryModel> list = new List<WLDataHistoryModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<WLDataHistoryModel>("DataSearch.WLgetHistory", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion
    }
}