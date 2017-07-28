using System.Collections.Generic;

using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public interface IRevisionManagementService
    {
        IList<RevisionManagementModel> GetRevisionManagementList();
        int InsertUpdateRevisionManagement(IList<RevisionManagementModel> param);
        int DeleteRevisionManagement(IList<RevisionManagementModel> param);
    }
}