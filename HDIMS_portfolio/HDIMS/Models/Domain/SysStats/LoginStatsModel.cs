
namespace HDIMS.Models.Domain.SysStats
{
    public class LoginStatsModel
    {
        public string DAY { get; set; }
        public string MONTH { get; set; }
        public int CNT { get; set; }
        public int TOT_CNT { get; set; }
        public string STARTDT { get; set; }
        public string ENDDT { get; set; }
    }
}