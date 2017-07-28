using HDIMSAPP.Models.Common;
using HDIMSAPP.Utils;

namespace HDIMSMAIN.Models.Main
{
    public class AbnormEquipModel : ObservableModel
    {
        private bool _CHKYN;
        public string ID { get; set; }
        public string BFOBSDT { get; set; }
        public string OBSTP { get; set; }
        public string ABCOLUMN { get; set; }
        public string OBSCD { get; set; }
        public string OBSDT { get; set; }
        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string OBSNM { get; set; }
        public string ABCONT { get; set; }
        public bool CHKYN
        {
            get { return _CHKYN; }
            set
            {
                if (object.ReferenceEquals(this._CHKYN, value) != true)
                {
                    this._CHKYN = value;
                    OnPropertyChanged("CHKYN");
                }
            }
        }
        public string P_OBSTP { 
            get {
                if (OBSTP.Equals("WL")) return "수위";
                else return "우량";
            } 
        }
        
        #region -- 출력용 변환 필드 목록 --
        public string P_OBSDT { get { return DateUtil.formatDate(OBSDT); } }
        public string P_PRESENTER { get { return DAMNM + ":" + P_OBSDT; } }
        #endregion

        public string LONGITUDE { get; set; }
        public string LATITUDE { get; set; }
    }
}
