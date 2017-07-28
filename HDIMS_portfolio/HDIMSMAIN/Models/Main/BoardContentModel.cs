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
using HDIMSMAIN.Utils;

namespace HDIMSMAIN.Models.Main
{
    public class BoardContentModel
    {
        public string CRUD { get; set; }

        //private BoardModel _BoardInfo;
        //public BoardModel BoardInfo { get { return _BoardInfo; } set { this.BOARDCD = value.BOARDCD; this.BOARDNM = value.BOARDNM; _BoardInfo = value; } }
        //public BoardContentModel()
        //{

        //}
        //public BoardContentModel(BoardModel BoardInfo)
        //{
        //    this.BoardInfo = BoardInfo;
        //}

        public string BOARDCD { get; set; }
        public string BOARDNM { get; set; }
        public string CONTENTCD { get; set; }
        public string TITLE { get; set; }
        public string CONTENT { get; set; }
        public string EMPNO { get; set; }
        public string EMPNM { get; set; }
        public string INDT { get; set; }
        public string CGDT { get; set; }
        //private string _READCNT;
        //public string READCNT { get { return _READCNT; } set { _READCNT = value; OnPropertyChanged("READCNT"); } }
        public string ROW_NUM { get; set; }
        public string OASISSTATUS { get; set; }

        //#region 오아시스 결재 상태
        //public string OASISSTATUS_KOR
        //{
        //    get
        //    {
        //        switch (OASISSTATUS)
        //        {
        //            case "GD":
        //                return "기안";
        //            case "SD":
        //                return "결재중";
        //            case "ED":
        //                return "결재완료";
        //            case "RD":
        //                return "반송";
        //            default:
        //                return "";
        //        }
        //    }
        //}
        //#endregion
        public string P_INDT { get { return DateUtil.formatDate(INDT); } }
        public string P_CGDT { get { return DateUtil.formatDate(CGDT); } }
    }
}
