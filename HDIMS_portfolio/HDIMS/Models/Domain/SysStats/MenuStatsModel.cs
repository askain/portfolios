
namespace HDIMS.Models.Domain.SysStats
{
    public class MenuStatsModel
    {
        public int MENU_ID { get; set; }
        public string ACC_DATE { get; set; }
        public string IP { get; set; }
        public string FULL_PATH { get; set; }
        public string MENU_NAME { get; set; }
        public string MENU_URI { get; set; }
        public int PARENT_ID { get; set; }
        public int ORD { get; set; }
        public int CNT { get; set; }
        public int TOT_CNT { get; set; }
    }
}