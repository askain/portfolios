using HDIMSAPP.Models.Common;

namespace HDIMSAPP.Models.Main
{
    public class EquipMonitor :ObservableModel
    {
        public string OBSDT { get; set; }
        public string OBSCD { get; set; }
        public string OBSNM { get; set; }
        public string OBSTP { get; set; }

        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string WLVL { get; set; }
        public string RFVL { get; set; }
        public decimal BATTVOLT { get; set; }
        public int BATTSTS { get; set; }
        public int DOORSTS { get; set; }
        public int PWRSTS { get; set; }
        //public decimal WSPD { get; set; }
        public string SELDV { get; set; }

        public int WL_SEN { get; set; }
        public int RF_SEN { get; set; }
        public int WQ_SEN { get; set; }
        public int ETC_SEN { get; set; }
        public int RTU_MEMORY { get; set; }
        public int RTU_RESET { get; set; }
        public int WDT_RESET { get; set; }
        public int LAN_PORT { get; set; }
        public int CDMA_MODEM { get; set; }
        public int VSAT_PORT { get; set; }
        public int CDMA_PORT { get; set; }
        public int WIRE_PORT { get; set; }
        public int MULTICAST_SOCKET { get; set; }
        public int VSAT_EVENT { get; set; }
        public int CDMA_EVENT { get; set; }
        public int WIRE_EVENT { get; set; }
        public int UDP_EVENT { get; set; }
        public int IDU_PING { get; set; }
        public decimal SNR { get; set; }
        public int ETC1 { get; set; }
        public int ETC2 { get; set; }
        public int ETC3 { get; set; }
        public int PRIMARY_DATA { get; set; }
        public int SECONDARY_CALL { get; set; }
        public int DATA_STATUS { get; set; }
        public string GATHERING_TIME { get; set; }
    }
}
