using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.SysStats;

namespace HDIMS.Services.SysStats
{
    public interface ILoginStatsService
    {
        IList<LoginStatsModel> SelectMonth(Hashtable param);
        IList<LoginStatsModel> SelectDay(Hashtable param);

        IList<LoginStatsModel> SelectTotCnt(Hashtable param);
    }
}