using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public interface IObsManagementService
    {
        IList<ObsManagementModel> Select(Hashtable param);
        IList<ObsManagementModel> SelectObsMngCodeList(Hashtable param);
        IList<ObsManagementModel> SelectObsMngDamCodeList(Hashtable param);
        IList<ObsManagementModel> SelectDownLeftList(Hashtable param);
        IList<ObsManagementModel> SelectDownRightList(Hashtable param);
        int Delete(IList<ObsManagementModel> param);
        int InsertUpdate(IList<ObsManagementModel> param);
    }
}