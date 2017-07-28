
namespace HDIMS.Models.Domain.DataSearch
{
    public class DamDataHistoryModel
    {
        public string ID { get; set; }
        public string OBSDT { get; set; }
        public string DAMCD { get; set; }
        public string DAMNM { get; set; }
        public string TRMDV { get; set; }

        public string RWL { get; set; } //저수위
        public string OSPILWL { get; set; } //방수로수위
        public string RSQTY { get; set; } //저수량
        public string RSRT { get; set; } //저수율
        public string IQTY { get; set; } //유입량
        public string ETCIQTY1 { get; set; } //기타유입량1
        public string ETCIQTY2 { get; set; } //기타유입량2
        public string ETQTY { get; set; } //계획홍수위공용량
        public string TDQTY { get; set; } //총방류량
        public string EDQTY { get; set; } //발전방류량
        public string ETCEDQTY { get; set; } //기타발전방류량
        public string SPDQTY { get; set; } //여수로방류량
        public string ETCDQTY1 { get; set; } //기타방류량1
        public string ETCDQTY2 { get; set; } //기타방류량2
        public string ETCDQTY3 { get; set; } //기타방류량3
        public string OTLTDQTY { get; set; } //아울렛방류량
        public string ITQTY1 { get; set; } //취수량1
        public string ITQTY2 { get; set; } //취수량2
        public string ITQTY3 { get; set; } //취수량3
        public string DAMBSARF { get; set; } //댐유역평균우량
        
        public string CGDT { get; set; }
        public string CGEMPNO { get; set; }
        public string CGEMPNM { get; set; }
        public string CHKEMPNO { get; set; }
        public string CHKEMPNM { get; set; }
        public string CHKDT { get; set; }
        public string EDEXLVL { get; set; }
        public string EDEXWAY { get; set; }
        public string EDEXLVLCONT { get; set; }
        public string EDEXWAYCONT { get; set; }
        public string CNRSN { get; set; }
        public string CNDS { get; set; }
        public string PRWL { get; set; } //이전저수위 값
    }
}