using System;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Models.Verify
{
    public class RainFallData : Data
    {
        //[{"ID":"9000239_201203031430","DAMCD":"1021701","DAMNM":null,"OBSCD":"9000239","OBSNM":"군남조절지","OBSDT":"201203031430","RF":"0","ACURF":"4","PEXCD":null,"EXRF":"0","EXVL":"4","EXCD":"0100","ETCCD":null,"CHKEMPNO":null,"CHKEMPNM":null,"CHKDT":null,"CGEMPNO":null,"CGEMPNM":null,"CGDT":null,"PEXCOLOR":null,"EXCOLOR":"FFFFFF","ETCTITLE":null,"StartPage":null,"EndPage":null,"PTACURF":null,"PDACURF":null,"PPDACURF":null,"DBSNTSN":null,"EDEXWAY":null,"EDEXLVL":" ","TRMDV":"10","EDEXWAYCONT":null},

        private string _ID ;
        private string _OBSCD;
        private string _OBSNM;
        private string _ACURF ;
        private string _RF ;
        private string _EDEXWAYCONT;
        private string _EXVL;
        private string _EXRF;
        private string _EXCD;
        private string _CGEMPNM;
        private string _CGDT;
        private string _CHKEMPNM;
        private string _CHKDT;

        public string DAMNM { get; set; }
        public string DAMCD { get; set; }
        public string PYACURF { get; set; }    // 이력조회용
        public string CGEMPNO { get; set; }
        public string EDEXWAY { get; set; }
        public string EDEXLVL { get; set; }
        public string EXRSN { get; set; }
        public string CNRSN { get; set; }
        public string CNDS { get; set; }
        public string EDEXLVLCONT { get; set; }
        public string _EXCOLOR;
        public string EXCOLOR 
        { 
            get {return _EXCOLOR;}
            set { _EXCOLOR = "#" + value; }
        }
        //public string ACURF_CK { get; set; }

        public string EDRF { get; set; }


        public string ID            { get { return _ID; }           set { if (_ID == value)             return;         _ID = value;            OnPropertyChanged("ID"); } }
        public string OBSCD         { get { return _OBSCD; }        set { if (_OBSCD == value)          return;         _OBSCD = value;         OnPropertyChanged("OBSCD"); } }
        public string OBSNM         { get { return _OBSNM; }        set { if (_OBSNM == value)          return;         _OBSNM = value;         OnPropertyChanged("OBSNM"); } }
        public string ACURF         { get { return _ACURF; }        set { DoubleValidationCheck(value); IsDirty = true; _ACURF = value;         OnPropertyChanged("ACURF"); OnPropertyChanged("ACURF_C");} }
        public string RF            { get { return _RF; }           set { DoubleValidationCheck(value); IsDirty = true; _RF = value;            OnPropertyChanged("RF"); OnPropertyChanged("RF_C");} }
        public string EDEXWAYCONT   { get { return _EDEXWAYCONT; }  set { if (_EDEXWAYCONT == value)    return;         _EDEXWAYCONT = value;   OnPropertyChanged("EDEXWAYCONT"); } }
        public string EXVL          { get { return _EXVL; }         set { if (_EXVL == value)           return;         _EXVL = value;          OnPropertyChanged("EXVL"); } }
        public string EXRF          { get { return _EXRF; }         set { if (_EXRF == value)           return;         _EXRF = value;          OnPropertyChanged("EXRF"); } }
        public string EXCD          { get { return _EXCD; }         set { if (_EXCD == value)           return;         _EXCD = value;          OnPropertyChanged("EXCD"); } }
        public string CGEMPNM       { get { return _CGEMPNM; }      set { if (_CGEMPNM == value)        return;         _CGEMPNM = value;       OnPropertyChanged("CGEMPNM"); } }
        public string CGDT          { get { return _CGDT; }         set { if (_CGDT == value)           return;         _CGDT = value;          OnPropertyChanged("CGDT"); } }
        public string CHKEMPNM      { get { return _CHKEMPNM; }     set { if (_CHKEMPNM == value)       return;         _CHKEMPNM = value;      OnPropertyChanged("CHKEMPNM"); } }
        public string CHKDT         { get { return _CHKDT; }        set { if (_CHKDT == value)          return;         _CHKDT = value;         OnPropertyChanged("CHKDT"); } }

        #region == 차트 출력용 ==
        public string ACURF_C { 
            get 
            { 
                if ("-99.9".Equals(_ACURF) == true) return null; 
                return _ACURF; 
            } 
        }
        public string RF_C { 
            get 
            { 
                if ("999".Equals(_RF) == true) return null; 
                return _RF; 
            } 
        }
        public string EXVL_C
        {
            get
            {
                if ("-99.9".Equals(_EXVL) == true) return null;
                return _EXVL;
            }
        }
        #endregion

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
