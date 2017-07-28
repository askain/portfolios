using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public interface IBaselineManagementService
    {
        IList<BaselineManagementModel> Select(Hashtable parm);
        int InsertUpdate(IList<BaselineManagementModel> param);
        IList<DAMBaselineManagementModel> DAMSelect(Hashtable parm);
        int DAMInsertUpdate(IList<DAMBaselineManagementModel> param);
    }
}