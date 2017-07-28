using HDIMSAPP.Models.Common;

namespace HDIMSAPP.Models.Verify
{
    public abstract class Data : ObservableModel
    {
        public bool IsDirty { get; set; }

        protected string _OBSDT;
        protected string _TRMDV;
        //private string _TRMDV;
        public string OBSDT { get { return _OBSDT; } set { if (_OBSDT == value) return; _OBSDT = value; } }
        public string TRMDV { get { return _TRMDV; } set { if (_TRMDV == value) return; _TRMDV = value; } }
    }
}
