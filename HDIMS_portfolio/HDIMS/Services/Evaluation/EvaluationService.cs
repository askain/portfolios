using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.Evaluation;

namespace HDIMS.Services.Evaluation
{
    public class EvaluationService : IEvaluationService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(EvaluationService));
        #endregion

        #region == GetEvaluationList() ==
        public IList<EvaluationModel> GetEvaluationList(Hashtable param)
        {
            IList<EvaluationModel> list = new List<EvaluationModel>();
            try
            {
                list = DaoFactory.Instance.QueryForList<EvaluationModel>("Evaluation.GetEvaluationList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == GetEvaluationListForMgt() ==
        public IList<EvaluationModel> GetEvaluationListForMgt(Hashtable param)
        {
            IList<EvaluationModel> list = new List<EvaluationModel>();
            try
            {
                list = DaoFactory.Instance.QueryForList<EvaluationModel>("Evaluation.GetEvaluationListForMgt", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == InsertEvaluation() ==
        public int InsertEvaluation(Hashtable param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                DaoFactory.Instance.Insert("Evaluation.InsertEvaluation", param);
                DaoFactory.Instance.CommitTransaction();
                iRetVal++;
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                log.Error(e);
                throw e;
            }

            return iRetVal;
        }
        #endregion

        #region == UpdateEvaluation() ==
        public int UpdateEvaluation(Hashtable param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                iRetVal += DaoFactory.Instance.Update("Evaluation.UpdateEvaluation", param);
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

        #region == DeleteEvaluation() ==
        public int DeleteEvaluation(Hashtable param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                iRetVal += DaoFactory.Instance.Delete("Evaluation.DeleteEvaluation", param);
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