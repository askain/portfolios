using System;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public class AlarmManagementService : IAlarmManagementService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(AlarmManagementService));
        #endregion

        #region == Select() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.26
        /// 3. 설  명 : 댐운영자료 알람관리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<AlarmManagementModel> Select()
        {
            IList<AlarmManagementModel> list = new List<AlarmManagementModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<AlarmManagementModel>("Code.GetAlarmList", null);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == Insert() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.26
        /// 3. 설  명 : 댐운영자료 알람관리 Row 추가
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Insert(IList<AlarmManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (AlarmManagementModel item in param)
                {
                   DaoFactory.Instance.Insert("Code.InsertAlarm", item);
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

        #region == Update() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.26
        /// 3. 설  명 : 댐운영자료 알람관리 Row 수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Update(IList<AlarmManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (AlarmManagementModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Update("Code.UpdateAlarm", item);
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

        #region == Delete() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.26
        /// 3. 설  명 : 댐운영자료 알람관리 Row 삭제
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Delete(IList<AlarmManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (AlarmManagementModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Delete("Code.DeleteAlarm", item);
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