using System.Collections.Generic;

namespace HDIMS.Models
{
    public class GridModel<T>
    {
        public string Failure { get; set; }
        public string Success { get; set; }
        public IList<T> Data { get; set; }
    }
}