using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Browser;
using HDIMSAPP.Models.Common;
using Infragistics.Controls.Editors;

namespace HDIMSAPP.Models
{
    public class EmpData
    {
                //var glUserInfo = {EmpNo: 'Admin',EmpNm: '관리자',authCd: '01',authNm: 'MASTER',LoginDt: '20120223113638',MgtCd: '0000',MgtDamcd: 'MAIN'};
        private string _EMPNO;
        private string _EMPNM;
        private string _AUTHCD;
        private string _AUTHNM;
        private string _LOGINDT;
        private string _MGTCD;
        private string _MGTDAMCD;
        private string _DAMTP;
        private string _DEPT;
        private string _HOME;
        private string _EMAIL;
        private string _HPTEL;
        public string EMPNO { get { return _EMPNO; } }
        public string EMPNM { get { return _EMPNM; } }
        public string AUTHCD { get{return _AUTHCD;} }
        public string AUTHNM { get{return _AUTHNM;} }
        public string LOGINDT { get{return _LOGINDT;} }
        public string MGTCD { get{return _MGTCD;} }
        public string MGTDAMCD { get { return _MGTDAMCD; } }
        public string DAMTP { get { return _DAMTP; } }
        public string DEPT { get { return _DEPT; } }
        public string HOME { get { return _HOME; } }
        public string EMAIL { get { return _EMAIL; } }
        public string HPTEL { get { return _HPTEL; } }

        
        //--------------------------------------------
        private int _INDEX_OF_DEFAULT_DAMTYPE = -1;
        public int INDEX_OF_DEFAULT_DAMTYPE
        {
            get
            {
                if (_INDEX_OF_DEFAULT_DAMTYPE == -1) setIndexOfDamType();

                return _INDEX_OF_DEFAULT_DAMTYPE;
            }
        }
        private int _INDEX_OF_DEFAULT_DAMCD = -1;
        public int INDEX_OF_DEFAULT_DAMCD
        {
            get
            {
                if (_INDEX_OF_DEFAULT_DAMCD == -1) setIndexOfDamCd();

                return _INDEX_OF_DEFAULT_DAMCD;
            }
        }

        private string _DEFAULT_DAMTYPE;
        public string DEFAULT_DAMTYPE
        {
            get
            {
                if (_DEFAULT_DAMTYPE != null) return _DEFAULT_DAMTYPE;


                if ("MAIN".Equals(DEFAULT_DAMCD))
                {
                    _DEFAULT_DAMTYPE = "D";
                }
                else
                {
                    _DEFAULT_DAMTYPE = _DAMTP;
                }

                return _DEFAULT_DAMTYPE;
            }
        }
        public string[] MGTDAM_ARRAY
        {
            get
            {
                return new string[0];
            }
        }
        public string DEFAULT_DAMCD
        {     //glGetDefaultDamCd
            get
            {
                return "";
            }
        }
        //----------------------------------------------


        //public IList<DamType> DamTypeList { get; set; }
        public XamComboEditor DamTpCombo { get; set; }
        public XamComboEditor DamCdCombo { get; set; }

        private void setIndexOfDamType()
        {
            if(DamTpCombo.AllowMultipleSelection == true) _INDEX_OF_DEFAULT_DAMTYPE = -1;
            else _INDEX_OF_DEFAULT_DAMTYPE = 0;

            IList<DamType> DamTypeList = (IList<DamType>)DamTpCombo.ItemsSource;

            if (DamTpCombo.ItemsSource == null) throw new NullReferenceException("댐타입리스트를 먼저 등록해야 합니다.");

            foreach (DamType dt in DamTypeList)
            {

                if (dt.DAMTYPE.Equals(DEFAULT_DAMTYPE))
                {
                    _INDEX_OF_DEFAULT_DAMTYPE = DamTypeList.IndexOf(dt);
                    return;
                }
            }
        }
        private void setIndexOfDamCd()
        {
            // 1. 기복적으로 멀티체크박스가 아닌 경우에는 첫번째 아이템을 선택하게 한다.
            // 2. 멅티체크박스인 경우 노체크상태
            // 3. 담당댐이 본사인경우 첫번째 아이템 또는 노체크
            // 4. 당담댐이 있는 경우 당담댐의 index
            // 5. 당담댐이 없는 경우 첫번째 아이템 또는 노첵크
            if (DamCdCombo.AllowMultipleSelection == true) _INDEX_OF_DEFAULT_DAMCD = -1;
            else _INDEX_OF_DEFAULT_DAMCD = 0;

            if ("MAIN".Equals(DEFAULT_DAMCD))
            {
                return;
            }

            IList<DamCode> DamCdList = (IList<DamCode>)DamCdCombo.ItemsSource;

            if (DamCdCombo.ItemsSource == null) throw new NullReferenceException("댐코드리스트를 먼저 등록해야 합니다.");

            foreach (DamCode dc in DamCdList)
            {
                if (dc.DAMCD == this.DEFAULT_DAMCD)
                {
                    _INDEX_OF_DEFAULT_DAMCD = DamCdList.IndexOf(dc);
                    break;
                }
            }
        }



        public EmpData()
        {
            LoadEmpData();
        }
        
        private void LoadEmpData()
        {
        }
        
        public bool IsAbleToChangeData(string TargetDamCd) {
            // 마스터:01,  품질관리담당자:03

            if("01".Equals(_AUTHCD) || "MAIN".Equals(_MGTDAMCD)) {
                return true;
            }
            else if("03".Equals(_AUTHCD) && _MGTDAMCD.IndexOf(TargetDamCd) != -1) {
                return true;
            }
            return false;
        }
    }
}