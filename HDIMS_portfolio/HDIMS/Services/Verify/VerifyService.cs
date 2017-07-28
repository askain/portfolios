using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.DataSearch;
using HDIMS.Models.Domain.Verify;


namespace HDIMS.Services.Verify
{
    public class VerifyService : IVerifyService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(VerifyService));

        public IList<Hashtable> getDamDataList(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();
            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Verify.getDamDataList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }

        public IList<Hashtable> getDamDataListDay(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();
            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Verify.getDamDataListDay", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }
        public int getDamDataTotalCount(Hashtable param)
        {
            int totalCount = 0;
            try
            {
                totalCount = DaoFactory.Instance.QueryForObject<int>("Verify.getDamDataTotalCount", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return totalCount;
        }

        public int getWaterLevelTotalCount(Hashtable param)
        {
            int totalCount = 0;
            try
            {
                totalCount = DaoFactory.Instance.QueryForObject<int>("Verify.getWaterLevelTotalCount", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return totalCount;
        }

        public IList<WaterLevelModel> getWaterLevelList(Hashtable param)
        {
            IList<WaterLevelModel> list = new List<WaterLevelModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<WaterLevelModel>("Verify.getWaterLevelList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }

        public int getRainFallTotalCount(Hashtable param)
        {
            int totalCount = 0;
            try
            {
                totalCount = DaoFactory.Instance.QueryForObject<int>("Verify.getRainFallTotalCount", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return totalCount;
        }

        public IList<RainFallModel> getRainFallList(Hashtable param)
        {
            IList<RainFallModel> list = new List<RainFallModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<RainFallModel>("Verify.getRainFallList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return list;
        }

        public void mergeAbnormStat(Hashtable param)
        {
            try
            {
                int exist = DaoFactory.Instance.QueryForObject<int>("Verify.isAbnormStatExist", param);

                if (exist < 1)
                    DaoFactory.Instance.Insert("Verify.insertAbnormStat", param);
                else
                    DaoFactory.Instance.Update("Verify.updateAbnormStat", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        #region == 변경이력 입력 ==
        public void insertRainfallVerifyHist(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.BeginTransaction();

                int histcnt = DaoFactory.Instance.QueryForObject<int>("Verify.selectRainfallVerify", param);

                if (histcnt == 0)
                {   //결측일 경우 이전값이 없기 때문에 이력이 입력되지 않음. 그래서 일부러 null값을 넣어준다.
                    Hashtable tempParam = (Hashtable)param.Clone();
                    tempParam.Remove("RF");
                    tempParam.Remove("ACURF");
                    tempParam.Add("RF", "");
                    tempParam.Add("ACURF", "");
                    DaoFactory.Instance.Insert("Verify.insertRainfallVerifyHist2", tempParam);
                }
                else
                {
                    DaoFactory.Instance.Insert("Verify.insertRainfallVerifyHist", param);
                }

                int cnt = DaoFactory.Instance.QueryForObject<int>("Verify.isExistRainFallVerifyCheck", param);

                if (cnt == 0)
                {
                    DaoFactory.Instance.Insert("Verify.insertRainFallVerifyCheck", param);
                }
                else
                {
                    DaoFactory.Instance.Update("Verify.updateRainFallVerifyCheck", param);
                }

                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                log.Error(e);
                throw e;
            }
        }

        public void insertWaterLevelVerifyHist(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.BeginTransaction();

                int histcnt = DaoFactory.Instance.QueryForObject<int>("Verify.selectWaterLevelVerify", param);

                if (histcnt == 0)
                {   //결측일 경우 이전값이 없기 때문에 이력이 입력되지 않음. 그래서 일부러 null값을 넣어준다.
                    Hashtable tempParam = (Hashtable)param.Clone();
                    tempParam.Remove("WL");
                    tempParam.Remove("FLW");
                    tempParam.Add("WL", "");
                    tempParam.Add("FLW", "");
                    DaoFactory.Instance.Insert("Verify.insertWaterLevelVerifyHist2", tempParam);
                }
                else
                {
                    DaoFactory.Instance.Insert("Verify.insertWaterLevelVerifyHist", param);
                }

                

                int cnt = DaoFactory.Instance.QueryForObject<int>("Verify.isExistWaterLevelVerifyCheck", param);

                if (cnt == 0)
                {
                    DaoFactory.Instance.Insert("Verify.insertWaterLevelVerifyCheck", param);
                }
                else
                {
                    DaoFactory.Instance.Update("Verify.updateWaterLevelVerifyCheck", param);
                }

                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                log.Error(e);
                throw e;
            }
        }

        public void insertDamVerifyHist(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.BeginTransaction();

                DaoFactory.Instance.Insert("Verify.insertDamVerifyHist", param);

                int cnt = DaoFactory.Instance.QueryForObject<int>("Verify.selectDamVerifyCheck", param);

                if (cnt == 0)
                {
                    DaoFactory.Instance.Insert("Verify.insertDamVerifyCheck", param);
                }
                else
                {
                    DaoFactory.Instance.Update("Verify.updateDamVerifyCheck", param);
                }

                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                log.Error(e);
                throw e;
            }
        }
        #endregion

        public void updateRainfallDataACURF(Hashtable param)
        {
            try
            {
                //int exist = DaoFactory.Instance.QueryForObject<int>("Verify.isRainfallDataExist", param);

                log.Debug("Verify.KWMS_SPIN_HALF_RF 프로시져 실행:");
                DaoFactory.Instance.Update("Verify.KWMS_SPIN_HALF_RF", param);

                //if (exist < 1)
                //    DaoFactory.Instance.Insert("Verify.insertRainfallData", param);
                //else
                //    DaoFactory.Instance.Update("Verify.updateRainfallData", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public void updateRainfallDataRF(Hashtable param)
        {
            try
            {
                log.Debug("Verify.KWMS_SPIN_HALF_MFRF 프로시져 실행:");
                DaoFactory.Instance.Update("Verify.KWMS_SPIN_HALF_MFRF", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public void updateWaterLevelData(Hashtable param)
        {
            try
            {
                //int exist = DaoFactory.Instance.QueryForObject<int>("Verify.isWaterLevelDataExist", param);

                log.Debug("Verify.KWMS_SPIN_HALF_WL 프로시져 실행:");
                DaoFactory.Instance.Update("Verify.KWMS_SPIN_HALF_WL", param);

                //if (exist < 1)
                //    DaoFactory.Instance.Insert("Verify.insertWaterLevelData", param);
                //else
                //    DaoFactory.Instance.Update("Verify.updateWaterLevelData", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public void updateDamData(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.BeginTransaction();

                //이력 넣기
                DaoFactory.Instance.Insert("Verify.insertDamVerifyHist", param);

                int cnt = DaoFactory.Instance.QueryForObject<int>("Verify.selectDamVerifyCheck", param);

                if (cnt == 0)
                {
                    DaoFactory.Instance.Insert("Verify.insertDamVerifyCheck", param);
                }
                else
                {
                    DaoFactory.Instance.Update("Verify.updateDamVerifyCheck", param);
                }

                //원시자료 수정.
                log.Debug("Verify.KWMS_SPIN_HALF_DAM 프로시져 실행:");
                DaoFactory.Instance.Update("Verify.KWMS_SPIN_HALF_DAM", param);

                //통계용 업데이트
                DaoFactory.Instance.Update("Verify.UpdateDamStat", param);

                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public DamDataModel getDamDataItem(Hashtable param)
        {
            DamDataModel item = new DamDataModel();
            try
            {

                if (log.IsDebugEnabled)
                {
                    log.Debug("DAMCD : " + param["DAMCD"] + ", OBSDT : " + param["OBSDHM"] );
                }
                IList<DamDataModel> list = new List<DamDataModel>();
                list = DaoFactory.Instance.QueryForList<DamDataModel>("Verify.getDamDataItem", param);
                if (list.Count > 0)
                {
                    item = list[0];
                }
                if (log.IsDebugEnabled)
                {
                    log.Debug("DAMCD : " + param["DAMCD"] + ", OBSDT : " + param["OBSDHM"] + "," + item.RWL);
                }
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return item;
        }

        public IList<Hashtable> getRfRds(Hashtable param)
        {
            IList<Hashtable> list = null;

            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Verify.getRfRds", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }

        public IList<WLLinearInterpolationResultModel> getWLLinearInterpolationResult(Hashtable param)
        {
            IList<WLLinearInterpolationResultModel> list = null;

            try
            {
                list = DaoFactory.Instance.QueryForList<WLLinearInterpolationResultModel>("Verify.getWLLinearInterpolationResult", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }

        public IList<RFLinearInterpolationResultModel> getRFLinearInterpolationResult(Hashtable param)
        {
            IList<RFLinearInterpolationResultModel> list = null;

            try
            {
                list = DaoFactory.Instance.QueryForList<RFLinearInterpolationResultModel>("Verify.getRFLinearInterpolationResult", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }

        public IList<DAMLinearInterpolationResultModel> getDAMLinearInterpolationResult(Hashtable param)
        {
            IList<DAMLinearInterpolationResultModel> list = null;

            try
            {
                list = DaoFactory.Instance.QueryForList<DAMLinearInterpolationResultModel>("Verify.getDAMLinearInterpolationResult", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }

        public string getDamDataAvg(Hashtable param)
        {
            string list = null;

            try
            {
                list = DaoFactory.Instance.QueryForObject<string>("Verify.getDamDataAvg", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }

        #region == getDamDataHistory() ==
        public IList<DamDataHistoryModel> getDamDataHistory(Hashtable param)
        {
            IList<DamDataHistoryModel> list = new List<DamDataHistoryModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DamDataHistoryModel>("DataSearch.getDamDataHistory", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region "보정 통계 업데이트"
        //public void updateDamStat(Hashtable param)
        //{
        //    try
        //    {

                
        //    }
        //    catch (Exception e)
        //    {
        //        log.Error(e);
        //        throw e;
        //    }
        //}
        public void updateRfStat(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.Update("Verify.UpdateRfStat", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }
        public void updateWlStat(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.Update("Verify.UpdateWlStat", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }
        #endregion
    }
}