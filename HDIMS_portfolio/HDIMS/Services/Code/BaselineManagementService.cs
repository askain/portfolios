using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public class BaselineManagementService : IBaselineManagementService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(ExamManagementService));
        #endregion

        #region == Select() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.02
        /// 3. 설  명 : 이상기준치관리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<BaselineManagementModel> Select(Hashtable param)
        {
            IList<BaselineManagementModel> list = new List<BaselineManagementModel>();
            
            try
            {
                if (param["OBSTP"].ToString() == "WL")
                {
                    list = DaoFactory.Instance.QueryForList<BaselineManagementModel>("Code.GetWLBaselineList", param);
                }
                else
                {
                    list = DaoFactory.Instance.QueryForList<BaselineManagementModel>("Code.GetRFBaselineList", param);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == InsertUpdate() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.02
        /// 3. 설  명 : 이상기준치관리 Row 추가 / 수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int InsertUpdate(IList<BaselineManagementModel> param)
        {
            IList<BaselineManagementModel> list = new List<BaselineManagementModel>();
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();

                if (param[0].OBSTP == "WL")
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        list = DaoFactory.Instance.QueryForList<BaselineManagementModel>("Code.GetWLBaselineList", null);

                        for (int j = 0; j < list.Count; j++)
                        {
                            if (param[i].WLOBCD == list[j].OBSCD && list[j].OBSTP == "WL")
                            {
                                iRetVal += DaoFactory.Instance.Update("Code.UpdateWLBaseline", param[i]);
                                break;
                            }
                            else if (param[i].WLOBCD == string.Empty && list[j].OBSTP == "WL")
                            {
                                DaoFactory.Instance.Insert("Code.InsertWLBaseline", param[i]);
                                iRetVal++;
                                break;
                            }
                        }
                    }
                }
                else if (param[0].OBSTP == "RF")
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        list = DaoFactory.Instance.QueryForList<BaselineManagementModel>("Code.GetRFBaselineList", null);

                        for (int j = 0; j < list.Count; j++)
                        {
                            if (param[i].RFOBCD == list[j].OBSCD && list[j].OBSTP == "RF")
                            {
                                iRetVal += DaoFactory.Instance.Update("Code.UpdateRFBaseline", param[i]);
                                break;
                            }
                            else if (param[i].RFOBCD == string.Empty && list[j].OBSTP == "RF")
                            {
                                DaoFactory.Instance.Insert("Code.InsertRFBaseline", param[i]);
                                iRetVal++;
                                break;
                            }
                        }
                    }
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                throw e;
            }

            return iRetVal;
        }
        #endregion


        #region == DAMSelect() ==
        public IList<DAMBaselineManagementModel> DAMSelect(Hashtable param)
        {
            IList<DAMBaselineManagementModel> list = new List<DAMBaselineManagementModel>();

            try
            {
                
                list = DaoFactory.Instance.QueryForList<DAMBaselineManagementModel>("Code.GetDAMBaselineList", param);

            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == DAMInsertUpdate() ==
        public int DAMInsertUpdate(IList<DAMBaselineManagementModel> param)
        {
            IList<DAMBaselineManagementModel> list = new List<DAMBaselineManagementModel>();
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();

                for (int i = 0; i < param.Count; i++)
                {
                    //list = DaoFactory.Instance.QueryForList<DAMBaselineManagementModel>("Code.GetDAMBaselineList", param[i]);
                    //if (list.Count > 0)
                    //{
                    //    iRetVal += DaoFactory.Instance.Update("Code.UpdateDAMBaseline", param[i]);
                    //}
                    //else
                    //{
                    //    DaoFactory.Instance.Insert("Code.InsertDAMBaseline", param[i]);
                    //    iRetVal++;
                    //}
                    
                    int cnt = DaoFactory.Instance.Update("Code.UpdateDAMBaseline", param[i]);
                    iRetVal += cnt;

                    if (cnt == 0)
                    {
                        DaoFactory.Instance.Insert("Code.InsertDAMBaseline", param[i]);
                        iRetVal++;
                    }

                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                throw e;
            }

            return iRetVal;
        }
        #endregion
    }
}