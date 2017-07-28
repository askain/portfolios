using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public interface IExamManagementService
    {
        IList<ExamManagementModel> Select(Hashtable param);
        int Insert(IList<ExamManagementModel> param);
        int Update(IList<ExamManagementModel> param);
        int Delete(IList<ExamManagementModel> param);
        IList<ExamManagementModel> DAMSelect(Hashtable param);
        int DAMInsert(IList<ExamManagementModel> param);
        int DAMUpdate(IList<ExamManagementModel> param);
        int DAMDelete(IList<ExamManagementModel> param);
    }
}