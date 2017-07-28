
namespace HDIMS.Models.Domain.Code
{
    public class ObsManagementModel
    {
        public string ID { get; set; }
        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string OBSCD { get; set; }
        public string OBSNM { get; set; }
        public string OBSTP { get; set; }
        public string WLOBSCD { get; set; }
        public string WLOBCD { get; set; }
        public string NROBCD { get; set; }
        public string NROBNM { get; set; }
        public string NRDIST { get; set; }
        public string NRQUAD { get; set; }
        public string UPDOWNTP { get; set; }
        public string RFOBSCD { get; set; }
        public string RFOBCD { get; set; }
        public int REGCOUNT { get; set; }
        public string KEY { get; set; }
        public string VALUE { get; set; }
        public int ORDERNUM { get; set; }
        public string PARENTKEY { get; set; }
        
        public string LATITUDE { get; set; }   //위경도가 등록되어 있는가 확인용
        
    }
}