using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.DamBoObsMng;

namespace HDIMS.Services.DamBoObsMng
{
    public interface IDamMgtService
    {
        IList<DamMgtModel> Select(Hashtable param);
        IList<DamMgtModel> GetDamMgtTreeList(Hashtable param);
        int Insert(IList<DamMgtModel> param);
        int Update(IList<DamMgtModel> param);
        int Delete(IList<DamMgtModel> param);
        int GetDamMgtNodeCount(Hashtable param);
    }
}