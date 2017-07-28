using HDIMSAPP.Models.Common;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Models.DataSearch
{
    public class NetworkModel : ObservableModel
    {
        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string OBSCD { get; set; }
        public string OBSNM { get; set; }
        public string OBSDT { get; set; }
        public string CHVAL { get; set; } //데이터
        public string SATVAL { get; set; } //위성
        public string CDMAVAL { get; set; } //CDMA
        public string WIREVAL { get; set; } //유선
        public string P_OBSDT { get { return DateUtil.formatDate(OBSDT); } }
        public string P_PRESENTER { get { return DAMNM + " - " + OBSNM + " : " + DateUtil.formatDate(OBSDT); } }
    }
}
