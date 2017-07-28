using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.DataSearch;
using HDIMS.Models.Domain.Main;
using HDIMS.Models.Domain.Statistics;
using HDIMS.Models.Domain.Verify;

namespace HDIMS.Services.Statistics
{
    public interface IStatisticsService
    {
        IList<Hashtable> GetWaterLevelStatList(Hashtable param);
        IList<StatAbnorm> GetWaterLevelAbnormList(Hashtable param);
        IList<WLDataSearchModel> GetWlModifiedDataList(Hashtable param);
        IList<Hashtable> GetRainFallStatList(Hashtable param);
        IList<StatAbnorm> GetRainFallAbnormList(Hashtable param);
        IList<RFDataSearchModel> GetRfModifiedDataList(Hashtable param);

        IList<Hashtable> GetWaterLevelAndRainFallStatList(Hashtable param);

        IList<Hashtable> GetVerifyStatList(Hashtable param);
        IList<Hashtable> GetEquipStatList(Hashtable param);
        IList<EquipModel> GetEquipAbnormList(Hashtable param);
        IList<Hashtable> GetDamDataStatList(Hashtable param);
        IList<DamDataModel> GetDamModifiedDataList(Hashtable param);

        IList<Hashtable> GetDam1mStatList(Hashtable param);
        IList<WaterLevelModel> GetWL30MissList(Hashtable param);
        IList<RainFallModel> GetRF30MissList(Hashtable param);
        

        IList<Hashtable> GetAlarmStatList(Hashtable param);
        IList<MainDamoperModel> GetAlarmList(Hashtable param);

        IList<AutoStatModel> GetAutoStatList(Hashtable param);
        void InsertSchedulerMonitor(Hashtable param);
        void UpdateSchedulerMonitor(Hashtable param);
    }
}