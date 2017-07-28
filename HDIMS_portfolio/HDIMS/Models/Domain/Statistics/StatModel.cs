
namespace HDIMS.Models.Domain.Statistics
{
    public class StatModel
    {  //DD.ID, DD.DAMCD, DC.DAMNM, DD.OBSDT, DD.ACNT, DD.TCNT
        public string ID { get; set; }
        public string DAMTYPE { get; set; }
        public string DAMTPNM { get; set; }
        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string OBSCD { get; set; }
        public string OBSNM { get; set; }
        public string OBSDT { get; set; }
        public int ACNT { get; set; }
        public int TCNT { get; set; }
        public int NCNT { get; set; }
        public string TPERC { get; set; }
    }
}