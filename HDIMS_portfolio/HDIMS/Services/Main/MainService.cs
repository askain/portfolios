using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Dao.Main;
using HDIMS.Models.Domain.Main;

namespace HDIMS.Services.Main
{
    public class MainService : IMainService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainService));
        public IMainDao  MainDao { get; set; }

        public IList<Hashtable> GetRadarTimes(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();
            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Main.GetRadarTimes", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }

        public IList<Hashtable> GetPopBalloonDate(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();
            log.Debug(param);
            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Main.GetPopBalloonDate", param);
            }
            catch (Exception e)
            {
                
                log.Error(e);
            }
            return list;
        }

        public IList<Hashtable> GetGoogleEarthDate(Hashtable param)
        {
            IList<Hashtable> data = new List<Hashtable>();
            log.Debug(param);
            try
            {
                data = DaoFactory.Instance.QueryForList<Hashtable>("Main.GetGoogleEarthDate", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return data;
        }

        #region 실시간 모니터링(이상자료, 알림현황, 댐운영자료, 설비상태)
        public int getabnormalListCount(Hashtable param)
        {
            int cnt = 0;
            try
            {
                cnt = DaoFactory.Instance.QueryForObject<int>("Main.getabnormalListCount", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return cnt;
        }
        public IList<MainAbnormalModel> getabnormalList(Hashtable param)
        {
            IList<MainAbnormalModel> list = new List<MainAbnormalModel>();
            try
            {
                list = DaoFactory.Instance.QueryForList<MainAbnormalModel>("Main.getabnormalList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }
        public void updateabnormalData(Hashtable param)
        {
            IList<MainAbnormalModel> list = new List<MainAbnormalModel>();
            try
            {
                //log.Debug("============ " + param + " ===============");
                DaoFactory.Instance.QueryForList<MainAbnormalModel>("Main.updateabnormalData", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }

        public int getabnormdamListCount(Hashtable param)
        {
            int cnt = 0;
            try
            {
                cnt = DaoFactory.Instance.QueryForObject<int>("Main.getabnormdamListCount", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return cnt;
        }
        public IList<MainAbnormdamModel> getabnormdamList(Hashtable param)
        {
            IList<MainAbnormdamModel> list = new List<MainAbnormdamModel>();
            try
            {
                list = DaoFactory.Instance.QueryForList<MainAbnormdamModel>("Main.getabnormdamList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }
        public void updateabnormdamData(Hashtable param)
        {
            IList<MainAbnormdamModel> list = new List<MainAbnormdamModel>();
            try
            {
                //log.Debug("============ " + param + " ===============");
                DaoFactory.Instance.QueryForList<MainAbnormdamModel>("Main.updateabnormdamData", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }

        public int getdamoperListCount(Hashtable param)
        {
            int cnt = 0;
            try
            {
                cnt = DaoFactory.Instance.QueryForObject<int>("Main.getdamoperListCount", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return cnt;
        }
        public IList<MainDamoperModel> getdamoperList(Hashtable param)
        {
            IList<MainDamoperModel> list = new List<MainDamoperModel>();
            try
            {
                list = DaoFactory.Instance.QueryForList<MainDamoperModel>("Main.getdamoperList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }
        public void updatedamoperData(Hashtable param)
        {
            IList<MainDamoperModel> list = new List<MainDamoperModel>();
            try
            {
                Hashtable item = DaoFactory.Instance.QueryForObject<Hashtable>("Main.GetDamOper", param);
                if (item == null)
                {
                    DaoFactory.Instance.Insert("Main.InsertDamOperData", param);
                }
                else
                {
                    DaoFactory.Instance.Update("Main.UpdateDamOperData", param);
                }
                
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }

        public int getequipListCount(Hashtable param)
        {

            int cnt = 0;
            try
            {
                cnt = DaoFactory.Instance.QueryForObject<int>("Main.getequipListCount", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return cnt;
        }
        public IList<MainEquipModel> getequipList(Hashtable param)
        {

            IList<MainEquipModel> list = new List<MainEquipModel>();
            try
            {
                list = DaoFactory.Instance.QueryForList<MainEquipModel>("Main.getequipList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }
        public void updateequipData(Hashtable param)
        {
            IList<MainEquipModel> list = new List<MainEquipModel>();
            try
            {
                //log.Debug("============ " + param + " ===============");
                DaoFactory.Instance.Update("Main.updateequipData", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }
        #endregion

        public IList<ObsCode> getObsCodeList(Hashtable param)
        {

            IList<ObsCode> list = new List<ObsCode>();
            try
            {
                list = DaoFactory.Instance.QueryForList<ObsCode>("Main.getObsCodeList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }

        public void saveMain(MainModel param)
        {
            MainDao.insertMain(param);
        }

        public MainModel getMain(MainModel param)
        {
            return MainDao.getMain(param);
        }

        public void updateMain(MainModel param)
        {
            MainDao.updateMain(param);
        }
        public void deleteMain(MainModel param)
        {
            MainDao.deleteMain(param);
        }
    }
}