using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public class RevisionManagementService : IRevisionManagementService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(RevisionManagementService));
        #endregion

        #region == GetRevisionManagementList() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.11.24
        /// 3. 설  명 : 보정코드관리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <return></return>
        public IList<RevisionManagementModel> GetRevisionManagementList()
        {
            IList<RevisionManagementModel> list = new List<RevisionManagementModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<RevisionManagementModel>("Code.GetRevisionManagementList", null);
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }
        #endregion

        #region == InsertUpdateRevisionManagement() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.11.24
        /// 3. 설  명 : 보정코드관리 Row 추가  수정
        /// 4. 이  력 :
        /// </summary>
        /// <param name="param">추가 및 수정된 각 row의 컬럼값 </param>
        /// <returns></returns>
        public int InsertUpdateRevisionManagement(IList<RevisionManagementModel> param)
        {
            IList<RevisionManagementModel> list = new List<RevisionManagementModel>();
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (RevisionManagementModel item in param)
                {
                    RevisionManagementModel obsMng = DaoFactory.Instance.QueryForObject<RevisionManagementModel>("Code.GetRevisionManagementList", item);

                    if (obsMng != null)
                    {
                        DaoFactory.Instance.Update("Code.UpdateRevisionManagement", item);
                    }
                    else
                    {
                        DaoFactory.Instance.Insert("Code.InsertRevisionManagement", item);
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

        #region == DeleteRevisionManagement() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.11.24
        /// 3. 설  명 : 보정코드관리 Row 삭제
        /// 4. 이  력 :
        /// </summary>
        /// <param name="param">삭제된 각 row의 컬럼값 </param>
        /// <returns></returns>
        public int DeleteRevisionManagement(IList<RevisionManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (RevisionManagementModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Delete("Code.DeleteRevisionManagement", item);
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