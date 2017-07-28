using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models.Domain.DataSearch;

namespace HDIMS.Models.Dao.DataSearch
{
    public class WLDataSearchDao : IWLDataSearchDao
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WLDataSearchDao));

        public IList<WLDataSearchModel> getList(Hashtable param)
        {
            IList<WLDataSearchModel> list = new List<WLDataSearchModel>();
            try
            {
                if (log.IsDebugEnabled)
                {
                    foreach (string key in param.Keys)
                    {
                        log.Debug("key : " + key + ", val : " + param[key]);
                    }
                }
                list = DaoFactory.Instance.QueryForList<WLDataSearchModel>("DataSearch.WLgetList", param);

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