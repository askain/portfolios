using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.DamBoObsMng;

namespace HDIMS.Services.DamBoObsMng
{
    public interface IWKMgtService
    {
        IList<DamBoModel> Select(Hashtable param);
        int Insert(IList<DamBoModel> param);
        int Update(IList<DamBoModel> param);
        int Delete(IList<DamBoModel> param);
    }
}