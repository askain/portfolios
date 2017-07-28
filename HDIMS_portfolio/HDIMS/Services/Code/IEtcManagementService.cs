using System.Collections.Generic;
using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public interface IEtcManagementService
    {
        IList<EtcManagementModel> Select();
        int Insert(IList<EtcManagementModel> param);
        int Update(IList<EtcManagementModel> param);
        int Delete(IList<EtcManagementModel> param);
    }
}