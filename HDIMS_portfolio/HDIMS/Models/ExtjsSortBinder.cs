using Common.Logging;
using HDIMS.Models.Domain.Common;

namespace HDIMS.Models
{
    public class ExtjsSortBinder
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ExtjsSortBinder));
        public static Sort BindSort(string sort)
        {
            //sort = [{"property":"KORNAME","direction":"ASC"}]

            Sort result = new Sort();


            //property
            string[] attrs = sort.Split(',');
            string[] attrs2 = attrs[0].Split(':');
            string value1 = attrs2[1];
            result.property = value1.Replace("\"", "");

            //direction
            if (attrs[1].IndexOf("\"ASC\"") == -1)
            {
                result.direction = "DESC";
            }
            else
            {
                result.direction = "ASC";
            }

            return result;
        }
    }
}