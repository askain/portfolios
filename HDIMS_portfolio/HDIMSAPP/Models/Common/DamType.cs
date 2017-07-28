
namespace HDIMSAPP.Models.Common
{
    public class DamType : ObservableModel
    {
        private string _damtype;
        private string _damtpnm;

        public string DAMTYPE { 
            get { return _damtype; }
            set { if (_damtype == value) return; _damtype = value; OnPropertyChanged("_damtype"); }
        }
        public string DAMTPNM
        {
            get { return _damtpnm; }
            set { if (_damtpnm == value) return; _damtpnm = value; OnPropertyChanged("_damtpnm"); }
        }
    }
}
