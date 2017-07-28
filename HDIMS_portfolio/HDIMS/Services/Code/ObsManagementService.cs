using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public class ObsManagementService : IObsManagementService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(ObsManagementService));
        #endregion

        #region == Select() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.02
        /// 3. 설  명 : 인근관측국 관리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<ObsManagementModel> Select(Hashtable param)
        {
            IList<ObsManagementModel> list = new List<ObsManagementModel>();

            try
            {
                if (param["OBSTP"].ToString() == "WL")
                {
                    list = DaoFactory.Instance.QueryForList<ObsManagementModel>("Code.GetWLObsList", param);
                }
                else
                {
                    list = DaoFactory.Instance.QueryForList<ObsManagementModel>("Code.GetRFObsList", param);
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
        /// 3. 설  명 : 이상기준치관리 Row 추가  수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int InsertUpdate(IList<ObsManagementModel> param)
        {
            IList<ObsManagementModel> list = new List<ObsManagementModel>();
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (ObsManagementModel item in param)
                {
                    if (item.OBSTP == "WL")
                    {
                        ObsManagementModel obsMng = DaoFactory.Instance.QueryForObject<ObsManagementModel>("Code.GetDownRightObsList", item);

                        if (obsMng != null)
                        {
                            DaoFactory.Instance.Update("Code.UpdateWLObs", item);
                        }
                        else
                        {
                            DaoFactory.Instance.Insert("Code.InsertWLObs", item);
                        }
                    }
                    else
                    {
                        ObsManagementModel obsMng = DaoFactory.Instance.QueryForObject<ObsManagementModel>("Code.GetDownRightObsList", item);

                        if (obsMng != null)
                        {
                            DaoFactory.Instance.Update("Code.UpdateRFObs", item);
                        }
                        else
                        {
                            DaoFactory.Instance.Insert("Code.InsertRFObs", item);
                        }
                    }
                    iRetVal++;
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

        #region == SelectDownLeftList() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.02
        /// 3. 설  명 : 수위 우량 인근관측국 왼쪽 하단 리스트 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<ObsManagementModel> SelectDownLeftList(Hashtable param)
        {
            IList<ObsManagementModel> list = new List<ObsManagementModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<ObsManagementModel>("Code.GetDownLeftObsList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == SelectObsMngDamCodeList() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.11
        /// 3. 설  명 : 인근관측국관리 등록/수정 댐콤보 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<ObsManagementModel> SelectObsMngDamCodeList(Hashtable param)
        {
            IList<ObsManagementModel> list = new List<ObsManagementModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<ObsManagementModel>("Code.GetObsMngDamCodeList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == SelectObsMngCodeList() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.11
        /// 3. 설  명 : 수위 우량 인근관측국 관리 관측국 콤보 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<ObsManagementModel> SelectObsMngCodeList(Hashtable param)
        {
            IList<ObsManagementModel> list = new List<ObsManagementModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<ObsManagementModel>("Code.GetObsMngCodeList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == SelectDownRightList() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.02
        /// 3. 설  명 : 수위 우량 인근관측국 오른쪽 하단 등록/수정 리스트 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<ObsManagementModel> SelectDownRightList(Hashtable param)
        {
            IList<ObsManagementModel> list = new List<ObsManagementModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<ObsManagementModel>("Code.GetDownRightObsList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == Delete() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.26
        /// 3. 설  명 : 인근관측국관리 Row 삭제
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Delete(IList<ObsManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (ObsManagementModel item in param)
                {
                   iRetVal += DaoFactory.Instance.Delete("Code.DeleteObs", item);
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