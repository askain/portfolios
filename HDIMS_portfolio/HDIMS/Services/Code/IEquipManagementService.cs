using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.Code;


namespace HDIMS.Services.Code
{
    public interface IEquipManagementService
    {
        IList<EquipGroupComboModel> SelectCombo();
        IList<EquipManagementModel> Select(Hashtable param);
        int Insert(IList<EquipManagementModel> param);
        int Update(IList<EquipManagementModel> param);
        int Delete(IList<EquipManagementModel> param);
    }
}