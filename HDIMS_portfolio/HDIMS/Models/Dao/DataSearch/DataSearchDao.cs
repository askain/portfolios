using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models.Domain.DataSearch;

namespace HDIMS.Models.Dao.DataSearch
{
    public class DataSearchDao : IDataSearchDao
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DataSearchDao));

        public IList<DataSearchModel> getList(Hashtable param)
        {
            IList<DataSearchModel> list = new List<DataSearchModel>();
            try
            {
                list = DaoFactory.Instance.QueryForList<DataSearchModel>("DataSearch.getList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }
    }
}