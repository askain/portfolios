using System;
using System.Collections.Generic;
using System.Windows.Browser;
using System.Windows;
using HDIMSMAIN.Models.Common;
using System.Collections;

namespace HDIMSMAIN.Models
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
        public string EMPNO { get { return _EMPNO; } }
        public string EMPNM { get { return _EMPNM; } }
        public string AUTHCD { get{return _AUTHCD;} }
        public string AUTHNM { get{return _AUTHNM;} }
        public string LOGINDT { get{return _LOGINDT;} }
        public string MGTCD { get{return _MGTCD;} }
        public string MGTDAMCD { get { return _MGTDAMCD; } }
        public string DAMTP { get { return _DAMTP; } }

        
        //--------------------------------------------
        private int _INDEX_OF_DEFAULT_DAMTYPE = -1;

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
                return _MGTDAMCD.Split(',');
            }
        }
        public string DEFAULT_DAMCD
        {     //glGetDefaultDamCd
            get
            {
                return MGTDAM_ARRAY[0];
            }
        }
        //----------------------------------------------

        public EmpData()
        {
            LoadEmpData();
        }
        
        private void LoadEmpData()
        {
            ScriptObject empdata = (ScriptObject)HtmlPage.Window.Invoke("getEmpData");

            _EMPNO = empdata.GetProperty("EmpNo").ToString();
            _EMPNM = empdata.GetProperty("EmpNm").ToString();
            _AUTHCD = empdata.GetProperty("authCd").ToString();
            _AUTHNM = empdata.GetProperty("authNm").ToString();
            _LOGINDT = empdata.GetProperty("LoginDt").ToString();
            _MGTCD = empdata.GetProperty("MgtCd").ToString();
            _MGTDAMCD = empdata.GetProperty("MgtDamcd").ToString();
            _DAMTP = empdata.GetProperty("DamTp").ToString();
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