
namespace HDIMS.Models.Domain.Evaluation
{
    public class EvaluationModel
    {
        public string ID { get { return OBSDT + "_" + DAMCD + "_" + DATATP; } }
        public string OBSDT { get; set; }
        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string DATATP { get; set; }
        public string CHKYN { get; set; }
        public string CHKOBSDT { get; set; }
        public string EMPNO { get; set; }
        public string EMPNM { get; set; }
        public string NOTE { get; set; }
    }
}