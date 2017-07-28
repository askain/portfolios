using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.SysStats;

namespace HDIMS.Services.SysStats
{
    public interface IMenuStatsService
    {
        IList<MenuStatsModel> Select(Hashtable param); 
        IList<MenuStatsModel> SelectTotCnt(Hashtable param);
        int Insert(Hashtable param);
    }
}