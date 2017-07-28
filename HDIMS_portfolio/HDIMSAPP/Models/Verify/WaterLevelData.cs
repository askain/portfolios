using System;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Models.Verify
{
    //public class ExCodeObject
    //{
    //    public string EXCD { get; set; }
    //    public string EXCOLOR { get; set; }
    //    public Style EXCOLORSTYLE { get; set; }
    //}

    public class WaterLevelData : Data
    {

        //private ExCodeObject _EXCDOBJ;
        //public ExCodeObject EXCDOBJ
        //{
        //    get
        //    {
        //        if (_EXCDOBJ == null)
        //        {
        //            Style s = new Style(typeof(StackPanel));
        //            Color c = Infragistics.ColorConverter.FromString("#" + this.EXCOLOR);
        //            s.Setters.Add(new Setter(StackPanel.BackgroundProperty, new SolidColorBrush(c)));
        //            _EXCDOBJ = new ExCodeObject() { EXCD = this.EXCD, EXCOLOR = "#"+this.EXCOLOR, EXCOLORSTYLE = s };
        //        }
        //        return _EXCDOBJ;
        //    }
        //}


        //private string[] EditableColumns = { "RWL", "OSPILWL", "ETCIQTY2", "EDQTY", "ETCEDQTY", "SPDQTY", "ETCDQTY1", "ETCDQTY2", "ETCDQTY3", "OTLTDQTY", "ITQTY1", "ITQTY2", "ITQTY3" };
        //private bool _IsDirty = false;

        private string _ID ;
        //private new string _OBSDT ;
        private string _OBSCD;
        private string _OBSNM;
        //private new string _TRMDV ;
        private string _WL ;
        private string _FLW ;
        private string _EDEXWAYCONT;
        private string _EXVL;
        private string _EXCD;
        private string _CGEMPNM;
        private string _CGDT;
        private string _CHKEMPNM;
        private string _CHKDT;



        public string DAMNM { get; set; }
        public string DAMCD { get; set; }
        public string CGEMPNO { get; set; }
        public string EDEXWAY { get; set; }
        public string EDEXLVL { get; set; }
        public string EXRSN { get; set; }
        public string CNRSN { get; set; }
        public string CNDS { get; set; }
        public string EDEXLVLCONT { get; set; }
        //public string EDEXWAYCONT { get; set; }
        public string _EXCOLOR;
        public string EXCOLOR 
        { 
            get {return _EXCOLOR;}
            set { _EXCOLOR = "#" + value; }
        }


        public string ID    { get { return _ID; } set { if (_ID == value) return; _ID = value; OnPropertyChanged("ID"); } }
        //public new string OBSDT { get { return _OBSDT; } set { if (_OBSDT == value) return; _OBSDT = value; OnPropertyChanged("OBSDT"); } }
        public string OBSCD { get { return _OBSCD; } set { if (_OBSCD == value) return; _OBSCD = value; OnPropertyChanged("OBSCD"); } }
        public string OBSNM { get { return _OBSNM; } set { if (_OBSNM == value) return; _OBSNM = value; OnPropertyChanged("OBSNM"); } }
        //public new string TRMDV { get { return _TRMDV; } set { if (_TRMDV == value) return; _TRMDV = value; OnPropertyChanged("TRMDV"); } }


        

        //public string OBSCD { get { return _OBSCD; } set { if (_OBSCD == value) return; _OBSCD = value; OnPropertyChanged("OBSCD"); } }
        //public string DAMNM { get { return _DAMNM; } set { if (_DAMNM == value) return; _DAMNM = value; OnPropertyChanged("DAMNM"); } }

        public string WL            { get { return _WL; }           set { DoubleValidationCheck(value); IsDirty = true; _WL = value; OnPropertyChanged("WL"); } }
        public string FLW           { get { return _FLW; }          set { if (_FLW == value)            return; _FLW = value;           OnPropertyChanged("FLW"); } }
        public string EDEXWAYCONT   { get { return _EDEXWAYCONT; }  set { if (_EDEXWAYCONT == value)    return; _EDEXWAYCONT = value;   OnPropertyChanged("EDEXWAYCONT"); } }
        public string EXVL          { get { return _EXVL; }         set { if (_EXVL == value)           return; _EXVL = value;          OnPropertyChanged("EXVL"); } }
        public string EXCD          { get { return _EXCD; }         set { if (_EXCD == value)           return; _EXCD = value;          OnPropertyChanged("EXCD"); } }
        public string CGEMPNM       { get { return _CGEMPNM; }      set { if (_CGEMPNM == value)        return; _CGEMPNM = value;       OnPropertyChanged("CGEMPNM"); } }
        public string CGDT          { get { return _CGDT; }         set { if (_CGDT == value)           return; _CGDT = value;          OnPropertyChanged("CGDT"); } }
        public string CHKEMPNM      { get { return _CHKEMPNM; }     set { if (_CHKEMPNM == value)       return; _CHKEMPNM = value;      OnPropertyChanged("CHKEMPNM"); } }
        public string CHKDT         { get { return _CHKDT; }        set { if (_CHKDT == value)          return; _CHKDT = value;         OnPropertyChanged("CHKDT"); } }


        #region -- 출력용 변환 필드 목록 --
        public string P_OBSDT { get { return DateUtil.formatDate(_OBSDT); } }
        public string P_PRESENTER { get { return _OBSNM + ":" + P_OBSDT; } }
        #endregion

        #region -- 유효성 검사 --
        private void DoubleValidationCheck(string value)
        {
            try
            {   // " " 공백문자는 허가함. 결측 자료가 " "으로 표현되기 때문
                if (value != null && " ".Equals(value) == false)
                {
                    double.Parse(value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("숫자만을 입력 할 수 있습니다.", ex);
            }
        }
        #endregion
    }
}
