using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.SysStats;

namespace HDIMS.Services.SysStats
{
    public interface IUserStatsService
    {
        IList<UserStatsModel> Select(Hashtable param);
        IList<UserStatsModel> SelectTotCnt(Hashtable param);
    }
}