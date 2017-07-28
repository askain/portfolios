
namespace HDIMS.Models.Domain.DataSearch
{
    public class DataSearchModel
    {
        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string OBSCD { get; set; }
        public string OBSNM { get; set; }
        public string OBSDT { get; set; }
        public string BATTVOLT { get; set; }
        public string BATTSTS { get; set; }
        public string DOORSTS { get; set; }
        public string WL_SEN { get; set; }
        public string RF_SEN { get; set; }
        public string WQ_SEN { get; set; }
        public string ETC_SEN { get; set; }
        public string RTU_MEMORY { get; set; }
        public string RTU_RESET { get; set; }
        public string WDT_RESET { get; set; }
        public string LAN_PORT { get; set; }
        public string CDMA_MODEM { get; set; }
        public string VSAT_PORT { get; set; }
        public string CDMA_PORT { get; set; }
        public string WIRE_PORT { get; set; }
        public string MULTICAST_SOCKET { get; set; }
        public string VSAT_EVENT { get; set; }
        public string CDMA_EVENT { get; set; }
        public string WIRE_EVENT { get; set; }
        public string UDP_EVENT { get; set; }
        public string IDU_PING { get; set; }
        public string SNR { get; set; }
        public string PRIMARY_DATA { get; set; }
        public string SECONDARY_CALL { get; set; }
        public string DATA_STATUS { get; set; }
        public string StartPage { get; set; }
        public string EndPage { get; set; }
        public int TOTCOUNT { get; set; }
    }
}