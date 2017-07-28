using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using HDIMSMAIN.Models.Common;
using HDIMSMAIN.Utils;

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
        public string P_OBSDT { get { return DateUtil.formatDate(OBSDT); } }
        public string LONGITUDE { get; set; }
        public string LATITUDE { get; set; }
    }
}
