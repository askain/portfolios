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
    public class AbnormOperModel : ObservableModel
    {
        private bool _CHKYN;
        public string ID { get; set; }
        public string OBSDT { get; set; }
        public string OBSCD { get; set; }
        public string OBSNM { get; set; }
        public string ERR_CD { get; set; }
        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string EVENTTIME { get; set; }
        public string POPUP_SEE { get; set; }
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
        public string LONGITUDE { get; set; }
        public string LATITUDE { get; set; }
        public string CHKEMPNO { get; set; }
        public string CHKDT { get; set; }
        public string CHKEMPNOMAIN { get; set; }
        public string CHKDTMAIN { get; set; }
    }
}
