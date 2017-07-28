using HDIMSAPP.Models.Common;

namespace HDIMSAPP.Models.Board
{
    public class BoardContentModel : ObservableModel
    {
        public string CRUD { get; set; }

        private BoardModel _BoardInfo;
        public BoardModel BoardInfo { get{return _BoardInfo;} set{ this.BOARDCD = value.BOARDCD; this.BOARDNM=value.BOARDNM; _BoardInfo=value;} }
        public BoardContentModel()
        {

        }
        public BoardContentModel(BoardModel BoardInfo)
        {
            this.BoardInfo = BoardInfo;
        }

        public string ID { get { return "Y".Equals(ALWAYSONTOP) ? "알림" : CONTENTCD; } }

        public string BOARDCD { get; set; }
        public string BOARDNM { get; set; }
        public string CONTENTCD { get; set; }
        public string TITLE { get; set; }
        public string CONTENT { get; set; }
        public string EMPNO { get; set; }
        public string EMPNM { get; set; }
        public string INDT { get; set; }
        public string CGDT { get; set; }
        private string _READCNT;
        public string READCNT { get { return _READCNT; } set { _READCNT = value; OnPropertyChanged("READCNT"); } }
        public string ROW_NUM { get; set; }
        public string OASISSTATUS { get; set; }
        public string OASISDOCSID { get; set; }
        public string OASISDOCUID { get; set; }
        public string REPLYCNT { get; set; }
        public string READAUTHCODE { get; set; }
        public string ALWAYSONTOP { get; set; }
        public string CATEGORY { get; set; }

        public string FileExist { get; set; }

        public string AuthImagePath
        {
            get
            {
                if (READAUTHCODE != null) return "/HDIMSAPP;component/Images/lock.png";

                return null;
            }
        }

        public string CategoryAndTitle
        {
            get
            {
                return CATEGORY + TITLE;
            }
        }

        #region 오아시스 결재 상태
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
        #endregion
    }
}
