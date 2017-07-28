using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.ManAuthMng;

namespace HDIMS.Services.ManAuthMng
{
    public interface IManService
    {
        int Count(Hashtable param);
        IList<ManModel> Select(Hashtable param);
        IList<AuthModel> SelectCombo();
        int InsertUpdate(IList<ManModel> param);
        ManModel SelectMan(ManModel param);
        int InsertUpdateHome(Hashtable param);
    }
}