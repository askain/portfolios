
namespace HDIMSAPP.Models.Common
{
    public class AlarmCode
    {
        public decimal ALARMCD { get; set; }
        public string ALARMCONT { get; set; }
        public string ALARMNM { get; set; }
        public string ALARMCD_STR { get { return "V_" + ALARMCD.ToString().Replace("-", "M"); } }
    }
}
