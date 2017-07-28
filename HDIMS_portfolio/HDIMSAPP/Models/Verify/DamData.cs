using System;
using System.Collections.ObjectModel;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Models.Verify
{
    public class DamData : Data
    {
        private string _ID ;
        private string _DAMCD ;
        private string _DAMNM ;

        private string _PRWL ;
        private string _RWL ;
        private string _RSQTY ;
        private string _RSRT ;
        private string _OSPILWL ;
        private string _IQTY ;
        private string _ETCIQTY1 ;
        private string _ETCIQTY2 ;
        private string _ETQTY ;
        private string _TDQTY ;
        private string _EDQTY ;
        private string _ETCEDQTY ;
        private string _SPDQTY ;
        private string _ETCDQTY1 ;
        private string _ETCDQTY2 ;
        private string _ETCDQTY3 ;
        private string _OTLTDQTY ;
        private string _ITQTY1 ;
        private string _ITQTY2 ;
        private string _ITQTY3 ;
        private string _DAMBSARF ;
        private string _CHKDT ;
        private string _CHKEMPNM ;
        private string _CGEMPNO ;
        private string _CGEMPNM ;
        private string _CHKEMPNO ;
        private string _EDEXLVL ;
        private string _EDEXWAY ;
        private string _CGDT ;
        private string _CNDS ;
        private string _CNRSN ;

        private string _RWL_CK ;
        private string _OSPILWL_CK ;
        private string _ETCIQTY2_CK ;
        private string _ETCDQTY1_CK ;
        private string _ETCDQTY2_CK ;
        private string _ETCDQTY3_CK ;
        private string _OTLTDQTY_CK ;
        private string _SPDQTY_CK ;
        private string _ITQTY1_CK ;
        private string _ITQTY2_CK ;
        private string _ITQTY3_CK ;
        private string _ETCEDQTY_CK ;
        private string _EDQTY_CK;

        public DamData()
        {
        }

        #region -- 비수정 필드 목록 --
        public string ID { get { return _ID; } set { if (_ID == value) return; _ID = value; } }
        public string DAMCD { get { return _DAMCD; } set { if (_DAMCD == value) return; _DAMCD = value; } }
        public string DAMNM { get { return _DAMNM; } set { if (_DAMNM == value) return; _DAMNM = value; } }
        

        public string PRWL { get { return _PRWL; } set { if (_PRWL == value) return; _PRWL = value; } }
        public string RSQTY { get { return _RSQTY; } set { if (_RSQTY == value) return; _RSQTY = value; } }
        public string RSRT { get { return _RSRT; } set { if (_RSRT == value) return; _RSRT = value; } }
        public string IQTY { get { return _IQTY; } set { if (_IQTY == value) return; _IQTY = value; } }
        public string ETCIQTY1 { get { return _ETCIQTY1; } set { if (_ETCIQTY1 == value) return; _ETCIQTY1 = value; } }
        public string ETQTY { get { return _ETQTY; } set { if (_ETQTY == value) return; _ETQTY = value; } }
        public string TDQTY { get { return _TDQTY; } set { if (_TDQTY == value) return; _TDQTY = value; } }
        public string DAMBSARF { get { return _DAMBSARF; } set { if (_DAMBSARF == value) return; _DAMBSARF = value; } }
        public string CHKDT { get { return _CHKDT; } set { if (_CHKDT == value) return; _CHKDT = value; } }
        public string CHKEMPNM { get { return _CHKEMPNM; } set { if (_CHKEMPNM == value) return; _CHKEMPNM = value; } }
        public string CGEMPNO { get { return _CGEMPNO; } set { if (_CGEMPNO == value) return; _CGEMPNO = value; } }
        public string CGEMPNM { get { return _CGEMPNM; } set { if (_CGEMPNM == value) return; _CGEMPNM = value; } }
        public string CHKEMPNO { get { return _CHKEMPNO; } set { if (_CHKEMPNO == value) return; _CHKEMPNO = value; } }
        public string EDEXLVL { get { return _EDEXLVL; } set { if (_EDEXLVL == value) return; _EDEXLVL = value; } }
        public string EDEXWAY { get { return _EDEXWAY; } set { if (_EDEXWAY == value) return; _EDEXWAY = value; } }
        public string CGDT { get { return _CGDT; } set { if (_CGDT == value) return; _CGDT = value; } }
        public string CNDS { get { return _CNDS; } set { if (_CNDS == value) return; _CNDS = value; } }
        public string CNRSN { get { return _CNRSN; } set { if (_CNRSN == value) return; _CNRSN = value; } }
        #endregion


        #region -- 수정가능 필드 목록 --
        public string RWL{     
            get { return _RWL; }
            set
            {
                DoubleValidationCheck(value);
                _RWL = value;
                _RWL_CK = "1";
                IsDirty = true; 
                OnPropertyChanged("RWL");
            }
        }
        public string OSPILWL { get { return _OSPILWL; } set { DoubleValidationCheck(value); _OSPILWL = value; _OSPILWL_CK = "1"; IsDirty = true; OnPropertyChanged("OSPILWL"); } }
        public string ETCIQTY2 { get { return _ETCIQTY2; } set { DoubleValidationCheck(value); _ETCIQTY2 = value; _ETCIQTY2_CK = "1"; IsDirty = true; OnPropertyChanged("ETCIQTY2"); } }
        public string EDQTY { get { return _EDQTY; } set { DoubleValidationCheck(value); _EDQTY = value; _EDQTY_CK = "1"; IsDirty = true; OnPropertyChanged("EDQTY"); } }
        public string ETCEDQTY { get { return _ETCEDQTY; } set { DoubleValidationCheck(value); _ETCEDQTY = value; _ETCEDQTY_CK = "1"; IsDirty = true; OnPropertyChanged("ETCEDQTY"); } }
        public string SPDQTY { get { return _SPDQTY; } set { DoubleValidationCheck(value); _SPDQTY = value; _SPDQTY_CK = "1"; IsDirty = true; OnPropertyChanged("SPDQTY"); } }
        public string ETCDQTY1 { get { return _ETCDQTY1; } set { DoubleValidationCheck(value); _ETCDQTY1 = value; _ETCDQTY1_CK = "1"; IsDirty = true; OnPropertyChanged("ETCDQTY1"); } }
        public string ETCDQTY2 { get { return _ETCDQTY2; } set { DoubleValidationCheck(value); _ETCDQTY2 = value; _ETCDQTY2_CK = "1"; IsDirty = true; OnPropertyChanged("ETCDQTY2"); } }
        public string ETCDQTY3 { get { return _ETCDQTY3; } set { DoubleValidationCheck(value); _ETCDQTY3 = value; _ETCDQTY3_CK = "1"; IsDirty = true; OnPropertyChanged("ETCDQTY3"); } }
        public string OTLTDQTY { get { return _OTLTDQTY; } set { DoubleValidationCheck(value); _OTLTDQTY = value; _OTLTDQTY_CK = "1"; IsDirty = true; OnPropertyChanged("OTLTDQTY"); } }
        public string ITQTY1 { get { return _ITQTY1; } set { DoubleValidationCheck(value); _ITQTY1 = value; _ITQTY1_CK = "1"; IsDirty = true; OnPropertyChanged("ITQTY1"); } }
        public string ITQTY2 { get { return _ITQTY2; } set { DoubleValidationCheck(value); _ITQTY2 = value; _ITQTY2_CK = "1"; IsDirty = true; OnPropertyChanged("ITQTY2"); } }
        public string ITQTY3 { get { return _ITQTY3; } set { DoubleValidationCheck(value); _ITQTY3 = value; _ITQTY3_CK = "1"; IsDirty = true; OnPropertyChanged("ITQTY3"); } }
        #endregion


        #region -- 보정 확인용 필드 목록 (1: 보정, Y: DB보정 ) --
        public string RWL_CK { get { return _RWL_CK; } set { if (_RWL_CK == value) return; _RWL_CK = value; OnPropertyChanged("RWL_CK"); } }
        public string OSPILWL_CK { get { return _OSPILWL_CK; } set { if (_OSPILWL_CK == value) return; _OSPILWL_CK = value; OnPropertyChanged("OSPILWL_CK"); } }
        public string ETCIQTY2_CK { get { return _ETCIQTY2_CK; } set { if (_ETCIQTY2_CK == value) return; _ETCIQTY2_CK = value; OnPropertyChanged("ETCIQTY2_CK"); } }
        public string ETCDQTY1_CK { get { return _ETCDQTY1_CK; } set { if (_ETCDQTY1_CK == value) return; _ETCDQTY1_CK = value; OnPropertyChanged("ETCDQTY1_CK"); } }
        public string ETCDQTY2_CK { get { return _ETCDQTY2_CK; } set { if (_ETCDQTY2_CK == value) return; _ETCDQTY2_CK = value; OnPropertyChanged("ETCDQTY2_CK"); } }
        public string ETCDQTY3_CK { get { return _ETCDQTY3_CK; } set { if (_ETCDQTY3_CK == value) return; _ETCDQTY3_CK = value; OnPropertyChanged("ETCDQTY3_CK"); } }
        public string OTLTDQTY_CK { get { return _OTLTDQTY_CK; } set { if (_OTLTDQTY_CK == value) return; _OTLTDQTY_CK = value; OnPropertyChanged("OTLTDQTY_CK"); } }
        public string SPDQTY_CK { get { return _SPDQTY_CK; } set { if (_SPDQTY_CK == value) return; _SPDQTY_CK = value; OnPropertyChanged("SPDQTY_CK"); } }
        public string ITQTY1_CK { get { return _ITQTY1_CK; } set { if (_ITQTY1_CK == value) return; _ITQTY1_CK = value; OnPropertyChanged("ITQTY1_CK"); } }
        public string ITQTY2_CK { get { return _ITQTY2_CK; } set { if (_ITQTY2_CK == value) return; _ITQTY2_CK = value; OnPropertyChanged("ITQTY2_CK"); } }
        public string ITQTY3_CK { get { return _ITQTY3_CK; } set { if (_ITQTY3_CK == value) return; _ITQTY3_CK = value; OnPropertyChanged("ITQTY3_CK"); } }
        public string ETCEDQTY_CK { get { return _ETCEDQTY_CK; } set { if (_ETCEDQTY_CK == value) return; _ETCEDQTY_CK = value; OnPropertyChanged("ETCEDQTY_CK"); } }
        public string EDQTY_CK { get { return _EDQTY_CK; } set { if (_EDQTY_CK == value) return; _EDQTY_CK = value; OnPropertyChanged("EDQTY_CK"); } }
        #endregion

        #region -- 출력용 변환 필드 목록 --
        public string P_OBSDT { get { return DateUtil.formatDate(_OBSDT); } }
        public string P_PRESENTER { get { return _DAMNM + ":" + P_OBSDT; } }
        #endregion

        #region -- 일자료 추가 필드 목록--
        public string VYRWL { get; set; }
        public string OYRWL { get; set; }
        public string MXRWL { get; set; }
        public string MXRWLDH { get; set; }
        public string MNRWL { get; set; }
        public string MNRWLDH { get; set; }
        public string VYRSQTY { get; set; }
        public string OYRSQTY { get; set; }
        public string VYIQTY { get; set; }
        public string OYIQTY { get; set; }
        public string MXIQTY { get; set; }
        public string MXIQTYDH { get; set; }
        public string VYTDQTY { get; set; }
        public string OYTDQTY { get; set; }
        public string MXDQTY { get; set; }
        public string MXDQTYDH { get; set; }
        public string RF { get; set; }
        public string VYRF { get; set; }
        public string OYRF { get; set; }
        public string PYACURF { get; set; }
        public string VYACURF { get; set; }
        public string OYAACURF { get; set; }
        public string MXRF { get; set; }
        public string MXRFDH { get; set; }
        public string HEDQTY { get; set; }
        public string GEDQTY { get; set; }
        public string RF07 { get; set; }
        public string VYEDQTY { get; set; }
        public string VYETCEDQTY { get; set; }
        public string VYSPDQTY { get; set; }
        public string VYETCDQTY1 { get; set; }
        public string VYETCDQTY2 { get; set; }
        public string VYETCDQTY3 { get; set; }
        public string VYOTLTDQTY1 { get; set; }
        public string VYITQTY1 { get; set; }
        public string VYITQTY2 { get; set; }
        public string VYITQTY3 { get; set; }
        public string OYEDQTY { get; set; }
        public string OYETCEDQTY { get; set; }
        public string OYSPDQTY { get; set; }
        public string OYETCDQTY1 { get; set; }
        public string OYETCDQTY2 { get; set; }
        public string OYETCDQTY3 { get; set; }
        public string OYOTLTDQTY1 { get; set; }
        public string OYITQTY1 { get; set; }
        public string OYITQTY2 { get; set; }
        public string OYITQTY3 { get; set; }
        public string ESSSQTY { get; set; }
        public string OYETCIQTY1 { get; set; }
        public string OYETCIQTY2 { get; set; }

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
                throw new Exception("숫자만을 입력 할 수 있습니다.",ex);
            }
        }
        #endregion
    }


    public class DamDataCollection : ObservableCollection<DamData>
    {

    }

}
