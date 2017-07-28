using System.Collections.Generic;


namespace HDIMSAPP.Models.Common
{
    public class MenuModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string link { get; set; }
        public bool leaf { get; set; }
        public bool expanded { get; set; }
        public string depth { get; set; }
        public string ord { get; set; }
        public string menu_date { get; set; }
        public string flag { get; set; }
        public string parentId { get; set; }
        public IList<MenuModel> children { get; set; }
    }
}