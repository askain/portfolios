using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.ManAuthMng;

namespace HDIMS.Services.ManAuthMng
{
    public interface IAuthService
    {
        IList<AuthModel> Select();
        int InsertUpdate(IList<AuthModel> param);
        int Delete(IList<AuthModel> param);

        IList<MenuMngModel> SelectMenuMng(Hashtable param);
        IList<MenuMngModel> SelectRegMenuMng(Hashtable param);

        int InsertAuthMenu(IList<MenuMngModel> param);
        int DeleteAuthMenu(IList<MenuMngModel> param);
    }
}