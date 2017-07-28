using System;
using System.Collections;
using System.Collections.Generic;
using HDIMS.Models;
using HDIMS.Models.Domain.DataSearch;
using HDIMS.Models.Domain.Main;
using HDIMS.Models.Domain.Statistics;
using HDIMS.Models.Domain.Verify;

namespace HDIMS.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        #region == 수위자료 통계 ==
        public IList<Hashtable> GetWaterLevelStatList(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();

            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Statistics.GetWaterLevelStatList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public IList<StatAbnorm> GetWaterLevelAbnormList(Hashtable param)
        {
            IList<StatAbnorm> list = new List<StatAbnorm>();

            try
            {
                list = DaoFactory.Instance.QueryForList<StatAbnorm>("Statistics.GetWaterLevelAbnormList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public IList<WLDataSearchModel> GetWlModifiedDataList(Hashtable param)
        {
            IList<WLDataSearchModel> list = new List<WLDataSearchModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<WLDataSearchModel>("Statistics.GetWlModifiedDataList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == 우량자료 통계 ==
        public IList<Hashtable> GetRainFallStatList(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();

            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Statistics.GetRainFallStatList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public IList<StatAbnorm> GetRainFallAbnormList(Hashtable param)
        {
            IList<StatAbnorm> list = new List<StatAbnorm>();

            try
            {
                list = DaoFactory.Instance.QueryForList<StatAbnorm>("Statistics.GetRainFallAbnormList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public IList<RFDataSearchModel> GetRfModifiedDataList(Hashtable param)
        {
            IList<RFDataSearchModel> list = new List<RFDataSearchModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<RFDataSearchModel>("Statistics.GetRfModifiedDataList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == 수위 + 우량 ==
        public IList<Hashtable> GetWaterLevelAndRainFallStatList(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();

            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Statistics.GetWaterLevelAndRainFallStatList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == 검보정 통계  사용안함!!!!!!!!!!!!! ==
        public IList<Hashtable> GetVerifyStatList(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();

            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Statistics.GetVerifyStatList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public IList<StatAbnorm> GetVerifyAbnormList(Hashtable param)
        {
            IList<StatAbnorm> list = new List<StatAbnorm>();

            try
            {
                list = DaoFactory.Instance.QueryForList<StatAbnorm>("Statistics.GetVerifyAbnormList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == 설비상태 통계 ==
        public IList<Hashtable> GetEquipStatList(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();

            try
            {
                if ("WL".Equals(param["datatp"]))
                {
                    list = DaoFactory.Instance.QueryForList<Hashtable>("Statistics.GetEquipWLStatList", param);
                }
                else if ("RF".Equals(param["datatp"]))
                {
                    list = DaoFactory.Instance.QueryForList<Hashtable>("Statistics.GetEquipRFStatList", param);
                }
                else
                {
                    list = DaoFactory.Instance.QueryForList<Hashtable>("Statistics.GetEquipStatList", param);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public IList<EquipModel> GetEquipAbnormList(Hashtable param)
        {
            IList<EquipModel> list = new List<EquipModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<EquipModel>("Statistics.GetEquipAbnormList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == 댐운영자료 통계 ==
        public IList<Hashtable> GetDamDataStatList(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();

            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Statistics.GetDamDataStatList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public IList<DamDataModel> GetDamModifiedDataList(Hashtable param)
        {
            IList<DamDataModel> list = new List<DamDataModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DamDataModel>("Statistics.GetDamModifiedDataList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == 수신률 & 결측률 통계 ==
        public IList<Hashtable> GetDam1mStatList(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();

            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Statistics.GetDam1mStatList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public IList<WaterLevelModel> GetWL30MissList(Hashtable param)
        {
            IList<WaterLevelModel> list = new List<WaterLevelModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<WaterLevelModel>("Statistics.GetWL30MissList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public IList<RainFallModel> GetRF30MissList(Hashtable param)
        {
            IList<RainFallModel> list = new List<RainFallModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<RainFallModel>("Statistics.GetRF30MissList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == 알람 ==
        public IList<Hashtable> GetAlarmStatList(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();

            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Statistics.GetAlarmStatList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public IList<MainDamoperModel> GetAlarmList(Hashtable param)
        {
            IList<MainDamoperModel> list = new List<MainDamoperModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<MainDamoperModel>("Statistics.getAlarmList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion 

        #region == 기간별 통계 ==
        public IList<AutoStatModel> GetAutoStatList(Hashtable param)
        {
            IList<AutoStatModel> list = new List<AutoStatModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<AutoStatModel>("Statistics.GetAutoStatList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == 스케쥴러 모니터링 로그 남기기 ==
        public void InsertSchedulerMonitor(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.Insert("Statistics.InsertSchedulerMonitor", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateSchedulerMonitor(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.Update("Statistics.UpdateSchedulerMonitor", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}