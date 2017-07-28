using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models.Domain.Main;

namespace HDIMS.Models.Dao.Main
{
    public class MainDao : IMainDao
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainDao));

        public IList<MainTimeModel> gettimeList()
        {
            IList<MainTimeModel> list = new List<MainTimeModel>();
            try
            {
                list = DaoFactory.Instance.QueryForList<MainTimeModel>("Main.getTime", null);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }

        public void insertMain(MainModel param)
        {
            DaoFactory.Instance.Insert("Main.insertMain", param);
        }

        public MainModel getMain(MainModel param)
        {
            return (MainModel)DaoFactory.Instance.QueryForObject("Main.getMain", param);
        }
       
        public void updateMain(MainModel param)
        {
            DaoFactory.Instance.Update("Main.updateMain", param);
        }

        public void deleteMain(MainModel param)
        {
            DaoFactory.Instance.Delete("Main.deleteMain", param);
        }
    }
}