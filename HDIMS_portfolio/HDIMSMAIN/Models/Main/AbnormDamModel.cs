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
    public class AbnormDamModel : ObservableModel
    {
        private bool _CHKYN;

        public string ID { get; set; }
        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string EXCOLUMN { get; set; }
        public string EXCOLUMNNM { get; set; }
        public string OBSDT { get; set; }
        public string OBVL { get; set; }
        public string EXVL { get; set; }
        public string EXCONT { get; set; }
        public string EXNOTE { get; set; }
        public string EXCD { get; set; }
        public string EXCOLOR { get; set; }
        public bool CHKYN { 
            get { return _CHKYN; } 
            set { 
                if(object.ReferenceEquals(this._CHKYN,value)!=true) {
                    this._CHKYN = value;
                    OnPropertyChanged("CHKYN"); 
                }
            } 
        }
        public string P_OBSDT { get { return DateUtil.formatDate(OBSDT); } }
    }
}
