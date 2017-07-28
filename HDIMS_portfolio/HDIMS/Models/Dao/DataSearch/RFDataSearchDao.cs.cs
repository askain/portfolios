using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models.Domain.DataSearch;

namespace HDIMS.Models.Dao.DataSearch
{
    public class RFDataSearchDao : IRFDataSearchDao
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RFDataSearchDao));

        public IList<RFDataSearchModel> getList(Hashtable param)
        {
            IList<RFDataSearchModel> list = new List<RFDataSearchModel>();
            try
            {
                if (log.IsDebugEnabled)
                {
                    foreach (string key in param.Keys)
                    {
                        log.Debug("key : " + key + ", val : " + param[key]);
                    }
                }
                list = DaoFactory.Instance.QueryForList<RFDataSearchModel>("DataSearch.RFgetList", param);

                if (log.IsDebugEnabled)
                {
                    log.Debug("list count : " + list.Count);
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }
    }
}