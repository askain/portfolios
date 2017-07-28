
namespace HDIMS.Models.Domain.Board
{
    public class BoardModel
    {
        public string CRUD { get; set; }
        public string BOARDCD { get; set; }
        public string BOARDNM { get; set; }
        public string BOARDNOTE { get; set; }
        public string FILEYN { get; set; }
        public string FILEEXTENTION { get; set; }
        public int MAXFILESIZE { get; set; }
        public string MAILYN { get; set; }
        public string MAILADDRESS { get; set; }
        public string READAUTHCODE { get; set; }
        public string WRITEAUTHCODE { get; set; }
        public int ORD { get; set; }
        public string USEYN { get; set; }
        public string HOMEYN { get; set; }
        public string OASISYN { get; set; }

        #region == 메뉴 등록용 ==
        public string MENU_ID { get{ return "999" + BOARDCD; } }
        public string MENU_URI { get { return "/R/#/Board/BoardTemplate?BoardCd=" + BOARDCD; } }
        #endregion
    }
}