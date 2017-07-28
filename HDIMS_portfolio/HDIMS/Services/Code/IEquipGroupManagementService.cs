using System.Collections.Generic;

using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public interface IEquipGroupManagementService
    {
        IList<EquipGroupManagementModel> Select();
        int InsertUpdate(IList<EquipGroupManagementModel> param);
        int Delete(IList<EquipGroupManagementModel> param);
    }
}