using HDIMSAPP.Utils;

namespace HDIMSAPP.Models.Verify
{
    public class WaterLevelDataForVerifying// : ObservableModel
    {
        public string OBSDT { get; set; }
        public string WL { get; set; }
        public string EXVL { get; set; }
        public string EXVL2 { get; set; }
        public string SPLINE { get; set; }

        #region -- 출력용 변환 필드 목록 --
        public string P_OBSDT { get { return DateUtil.formatDate(OBSDT); } }
        //public string P_PRESENTER { get { return _OBSNM + ":" + P_OBSDT; } }
        #endregion
    }
}
