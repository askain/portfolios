using HDIMSAPP.Utils;

namespace HDIMSAPP.Models.DataSearch
{
    public class WaterQuality
    {
        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string OBSCD { get; set; }
        public string OBSNM { get; set; }
        public string OBSDT { get; set; }
        public string TRMDV { get; set; }
        public string OBSELM { get; set; } //
        public string ACTOBSDT { get; set; }
        public string SRDWATITG { get; set; }
        public string SRDWATFLT { get; set; }
        public string RWL { get; set; }
        public string TBDT { get; set; }
        public string PH { get; set; }
        public string WTRTMP { get; set; }
        public string ELCCND { get; set; }
        public string DO { get; set; }
        public string CHL { get; set; }
        public string SAL { get; set; }
        public string EDEXLVL { get; set; }

        public string P_OBSDT { get { return DateUtil.formatDate(OBSDT); } }
        public string P_PRESENTER { get { return DAMNM+"["+OBSNM+"]" + " : " + DateUtil.formatDate(OBSDT); } }
    }
}
