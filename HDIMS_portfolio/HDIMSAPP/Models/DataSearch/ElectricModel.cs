using HDIMSAPP.Models.Common;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Models.DataSearch
{
    public class ElectricModel : ObservableModel
    {
        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string OBSDT { get; set; }
        public string TRMDV { get; set; }
        public string DT1 { get; set; } 
        public string DT2 { get; set; }
        public string DT3 { get; set; } 
        public string DT4 { get; set; }
        public string DT5 { get; set; }
        public string DT6 { get; set; }
        public string DT7 { get; set; }
        public string DT8 { get; set; }
        public string DT9 { get; set; }
        public string DT10 { get; set; }
        public string DT11 { get; set; }
        public string DT12 { get; set; }
        public string DT13 { get; set; }
        public string DT14 { get; set; }
        public string DT15 { get; set; }
        public string DT16 { get; set; }
        public string DT17 { get; set; }
        public string DT18 { get; set; }
        public string DT19 { get; set; }
        public string DT20 { get; set; } 
        public string P_OBSDT { get { return DateUtil.formatDate(OBSDT); } }
        public string P_PRESENTER { get { return DAMNM + " : " + DateUtil.formatDate(OBSDT); } }
    }
}
