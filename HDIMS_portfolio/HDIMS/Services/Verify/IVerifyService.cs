using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.DataSearch;
using HDIMS.Models.Domain.Verify;

namespace HDIMS.Services.Verify
{
    public interface IVerifyService
    {
        IList<Hashtable> getDamDataList(Hashtable param);
        IList<Hashtable> getDamDataListDay(Hashtable param);
        int getDamDataTotalCount(Hashtable param);
        IList<WaterLevelModel> getWaterLevelList(Hashtable param);
        int getWaterLevelTotalCount(Hashtable param);
        IList<RainFallModel> getRainFallList(Hashtable param);
        int getRainFallTotalCount(Hashtable param);
        void mergeAbnormStat(Hashtable param);
        void insertRainfallVerifyHist(Hashtable param);
        void insertWaterLevelVerifyHist(Hashtable param);
        void insertDamVerifyHist(Hashtable param);
        void updateRainfallDataACURF(Hashtable param);
        void updateRainfallDataRF(Hashtable param);
        void updateWaterLevelData(Hashtable param);
        void updateDamData(Hashtable param);
        DamDataModel getDamDataItem(Hashtable param);
        IList<Hashtable> getRfRds(Hashtable param);
        IList<WLLinearInterpolationResultModel> getWLLinearInterpolationResult(Hashtable param);
        IList<RFLinearInterpolationResultModel> getRFLinearInterpolationResult(Hashtable param);
        IList<DAMLinearInterpolationResultModel> getDAMLinearInterpolationResult(Hashtable param);
        string getDamDataAvg(Hashtable param);
        IList<DamDataHistoryModel> getDamDataHistory(Hashtable param);
        //void updateDamStat(Hashtable param);
        void updateRfStat(Hashtable param);
        void updateWlStat(Hashtable param);
    }
}