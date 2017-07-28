using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public class ExamManagementService : IExamManagementService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(ExamManagementService));
        #endregion

        #region == Select() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.12
        /// 3. 설  명 : 품질등급관리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<ExamManagementModel> Select(Hashtable param)
        {
            IList<ExamManagementModel> list = new List<ExamManagementModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<ExamManagementModel>("Code.GetExamList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == DAMSelect() ==
        public IList<ExamManagementModel> DAMSelect(Hashtable param)
        {
            IList<ExamManagementModel> list = new List<ExamManagementModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<ExamManagementModel>("Code.GetDAMExamList", param);
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
        /// 3. 설  명 : 품질등급관리 Row 추가
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Insert(IList<ExamManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (ExamManagementModel item in param)
                {
                    if (item.PROCINT == null) item.PROCINT = 10;
                    DaoFactory.Instance.Insert("Code.InsertExam", item);
                    iRetVal++;
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;   //실패
                throw e;
            }

            return iRetVal;
        }
        #endregion

        #region == DAMInsert() ==
        public int DAMInsert(IList<ExamManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (ExamManagementModel item in param)
                {
                    if (item.PROCINT == null) item.PROCINT = 10;
                    DaoFactory.Instance.Insert("Code.InsertDAMExam", item);
                    iRetVal++;
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;   //실패
                throw e;
            }

            return iRetVal;
        }
        #endregion

        #region == Update() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.12
        /// 3. 설  명 : 품질등급관리 Row 수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Update(IList<ExamManagementModel> param)
        {
            int iRetVal = 0;

            try
            {                
                DaoFactory.Instance.BeginTransaction();
                foreach (ExamManagementModel item in param)
                {
                    if (item.PROCINT == null) item.PROCINT = 10;
                    iRetVal += DaoFactory.Instance.Update("Code.UpdateExam", item);
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

        #region == DAMUpdate() ==
        public int DAMUpdate(IList<ExamManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (ExamManagementModel item in param)
                {
                    if (item.PROCINT == null) item.PROCINT = 10;
                    iRetVal += DaoFactory.Instance.Update("Code.UpdateDAMExam", item);
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
        /// 3. 설  명 : 품질등급관리 Row 삭제
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Delete(IList<ExamManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (ExamManagementModel wl in param)
                {
                   iRetVal += DaoFactory.Instance.Delete("Code.DeleteExam", wl);
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


        #region == DAMDelete() ==
        public int DAMDelete(IList<ExamManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (ExamManagementModel wl in param)
                {
                    iRetVal += DaoFactory.Instance.Delete("Code.DeleteDAMExam", wl);
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