using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.ManAuthMng;

namespace HDIMS.Services.ManAuthMng
{
    public interface IMenuService
    {
        int Update(Hashtable param);
        int Insert(Hashtable param);
        IList<MenuNodeCountModel> SelectNodeCount(Hashtable param);
    }
}