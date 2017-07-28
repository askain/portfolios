using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.Evaluation;

namespace HDIMS.Services.Evaluation
{
    public interface IEvaluationService
    {
        IList<EvaluationModel> GetEvaluationList(Hashtable param);
        IList<EvaluationModel> GetEvaluationListForMgt(Hashtable param);
        int InsertEvaluation(Hashtable param);
        int UpdateEvaluation(Hashtable param);
        int DeleteEvaluation(Hashtable param);
    }
}