using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.DataSearch;

namespace HDIMS.Services.DataSearch
{
    public class RFDataSearchService : IRFDataSearchService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(RFDataSearchService));
        #endregion

        #region == getList() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.03
        /// 3. 설  명 : 우량자료 이력조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<RFDataSearchModel> getList(Hashtable param)
        {
            IList<RFDataSearchModel> list = new List<RFDataSearchModel>();
            JsonModel<RFDataSearchModel> model = new JsonModel<RFDataSearchModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<RFDataSearchModel>("DataSearch.RFgetList", param);
                model.success = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == getSearchList() ==
        public IList<RFDataSearchModel> getSearchList(Hashtable param)
        {
            IList<RFDataSearchModel> list = new List<RFDataSearchModel>();
            JsonModel<RFDataSearchModel> model = new JsonModel<RFDataSearchModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<RFDataSearchModel>("DataSearch.RFgetSearchList", param);
                model.success = true;
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
        /// 3. 설  명 : 우량자료 이력조회(chart)
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<RFDataSearchModel> getChartList(Hashtable param)
        {
            IList<RFDataSearchModel> list = new List<RFDataSearchModel>();
            JsonModel<RFDataSearchModel> model = new JsonModel<RFDataSearchModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<RFDataSearchModel>("DataSearch.RFgetChartList", param);
                model.success = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == getRfDataTotalCount() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.09.01
        /// 3. 설  명 : 우량자료 이력조회 카운트
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int getRfDataTotalCount(Hashtable param)
        {
            IList<RFDataSearchModel> list = new List<RFDataSearchModel>();
            JsonModel<RFDataSearchModel> model = new JsonModel<RFDataSearchModel>();
            int totalCount = 0;

            try
            {
                totalCount = DaoFactory.Instance.QueryForObject<int>("DataSearch.getRfDataTotalCount", param);
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
        public IList<RFDataHistoryModel> getHistory(Hashtable param)
        {
            IList<RFDataHistoryModel> list = new List<RFDataHistoryModel>();
            JsonModel<RFDataHistoryModel> model = new JsonModel<RFDataHistoryModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<RFDataHistoryModel>("DataSearch.RFgetHistory", param);
                model.success = true;
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