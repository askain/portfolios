
namespace HDIMS.Models.Domain.Statistics
{
    public class StatAbnorm
    { //ID, DAMCD, OBSDT, EXCD, ECNT
        public string ID { get; set; }
        public string DAMCD { get; set; }
        public string OBSDT { get; set; }
        public string EXCD { get; set; }
        public int ECNT { get; set; }
    }
}