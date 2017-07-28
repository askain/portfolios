using System.Collections.Generic;

namespace HDIMSAPP.Models
{
    public class JsonModel<T>
    {
        public string Result { get; set; }
        public string msg { get; set; }
        public bool success { get; set; }
        public int total { get; set; }
        public int totalCount { get; set; }
        public IList<T> Data { get; set; }
        public IList<T> Head { get; set; }
        public IList<T> Model { get; set; }
        public IList<T> rows { get; set; }
        public string identifier { get; set; }
        public string label { get; set; }
        public IList<T> items { get; set; }
    }
}