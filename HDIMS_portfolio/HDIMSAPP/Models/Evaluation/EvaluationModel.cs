
namespace HDIMSAPP.Models.Evaluation
{
    public class EvaluationModel
    {
        public string OBSDT { get; set; }
        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string DATATP { get; set; }
        public string CHKYN { get; set; }
        public string CHKOBSDT { get; set; }
        public string EMPNO { get; set; }
        public string EMPNM { get; set; }
        public string NOTE { get; set; }

        #region == 표시용 ==
        public string DATATP_SHOW
        {
            get
            {
                switch (DATATP)
                {
                    case "D":
                        return "댐운영자료";
                    case "R":
                        return "우량자료";
                    case "W":
                        return "수위자료";
                    default:
                        return "-";
                }
            }
        }
        
        public string CHKYN_SHOW {
            get
            {
                switch(CHKYN) 
                {
                    case "Y":
                        return "이상 없음";
                    case "N":
                        return "이상 있음";
                    default:
                        break;
                }

                return "미확인";
            }
        }
        #endregion
    }
}
