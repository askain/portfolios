using System.Collections.Generic;

using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public interface IAlarmManagementService
    {
        IList<AlarmManagementModel> Select();
        int Insert(IList<AlarmManagementModel> param);
        int Update(IList<AlarmManagementModel> param);
        int Delete(IList<AlarmManagementModel> param);
    }
}