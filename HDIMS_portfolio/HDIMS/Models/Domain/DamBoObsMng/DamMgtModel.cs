using System.Collections.Generic;

namespace HDIMS.Models.Domain.DamBoObsMng
{
    public class DamMgtModel
    {
        public string id { get; set; }
        public string MGTCD { get; set; }
        public string PARCD { get; set; }
        public string MGTNM { get; set; }
        public string USEYN { get; set; }
        public string MGTCOMMENT { get; set; }
        public int MGTLVL { get; set; }
        public int MGTORD { get; set; }
        public int CHILD_CNT { get; set; }
        public string parentId { get; set; }
        public bool root { get; set; }
        public int depth { get; set; }
        public bool expanded { get; set; }
        public bool leaf { get; set; }
        public string iconCls { get; set; }
        public IList<DamMgtModel> children { get; set; }
    }
}