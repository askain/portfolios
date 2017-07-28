using System;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public class EtcManagementService : IEtcManagementService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(EtcManagementService));
        #endregion

        #region == Select() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.12
        /// 3. 설  명 : 기타범례관리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<EtcManagementModel> Select()
        {
            IList<EtcManagementModel> list = new List<EtcManagementModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<EtcManagementModel>("Code.GetEtcList", null);
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
        /// 2. 작성일 : 2011.07.12
        /// 3. 설  명 : 기타범례관리 Row 추가
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Insert(IList<EtcManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (EtcManagementModel item in param)
                {
                    DaoFactory.Instance.Insert("Code.InsertEtc", item);
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
        /// 2. 작성일 : 2011.07.12
        /// 3. 설  명 : 기타범례관리 Row 수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Update(IList<EtcManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (EtcManagementModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Update("Code.UpdateEtc", item);
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
        /// 2. 작성일 : 2011.07.12
        /// 3. 설  명 : 기타범례관리 Row 삭제
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Delete(IList<EtcManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (EtcManagementModel wl in param)
                {
                    iRetVal += DaoFactory.Instance.Delete("Code.DeleteEtc", wl);
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