using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.DataSearch;

namespace HDIMS.Services.DataSearch
{
    public class DataSearchService : IDataSearchService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(DataSearchService));
        #endregion

        #region == getList() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.06.23
        /// 3. 설  명 : 설비상태 이력조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<DataSearchModel> getList(Hashtable param)
        {
            IList<DataSearchModel> list = new List<DataSearchModel>();
            JsonModel<DataSearchModel> model = new JsonModel<DataSearchModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DataSearchModel>("DataSearch.getList", param);
                model.success = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == getListCount() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.03
        /// 3. 설  명 : 설비상태 이력조회 카운트
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int getListCount(Hashtable param)
        {
            IList<WLDataSearchModel> list = new List<WLDataSearchModel>();
            JsonModel<WLDataSearchModel> model = new JsonModel<WLDataSearchModel>();
            int totalCount = 0;

            try
            {
                totalCount = DaoFactory.Instance.QueryForObject<int>("DataSearch.getListCount", param);
                model.success = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return totalCount;
        }
        #endregion


        public IList<Hashtable> GetNetworkSearchList(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();

            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("DataSearch.GetNetworkSearchList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        public IList<Hashtable> GetWaterQualitySearchList(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();

            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("DataSearch.GetWaterQualitySearchList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
    }
}