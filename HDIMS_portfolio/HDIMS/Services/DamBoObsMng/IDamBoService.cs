using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.DamBoObsMng;

namespace HDIMS.Services.DamBoObsMng
{
    public interface IDamBoService
    {
        IList<DamBoModel> Select(Hashtable param);
        IList<DamBoModel> SelectWk();
        IList<DamBoModel> SelectDamType(Hashtable param);
        IList<DamBoModel> SelectWkCombo();
        IList<DamBoModel> SelectDamTypeCombo();

        int InsertUpdateDamBo(IList<DamBoModel> param);
        int DeleteDamBo(IList<DamBoModel> param);

        IList<DamColMgtModel> GetDamColMgtList(Hashtable param);
        int SaveDamColMgt(IList<DamColMgtModel> param);

        IList<DamTypeModel> GetDamTypeList();
        int InsertDamType(IList<DamTypeModel> param);
        int UpdateDamType(IList<DamTypeModel> param);
        int DeleteDamType(IList<DamTypeModel> param);

        IList<HDIMS.Models.Domain.Common.Code> GetDamCodeList(Hashtable param);
    }
}