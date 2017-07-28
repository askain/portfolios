using System.Collections;
using System.Collections.Generic;

namespace HDIMSAPP.Utils
{
    public class DataSourceUtil
    {
        public static IEnumerable GetDataSource(IList<IDictionary<string, string>> _conv, string convName)
        {

            IList<IDictionary> list = new List<IDictionary>();
            foreach (IDictionary<string, string> _item in _conv)
            {
                IDictionary _dc = new Dictionary<string, string>();
                foreach (string _key in _item.Keys)
                {
                    _dc.Add(_key, _item[_key]);
                }
                list.Add(_dc);
            }
            return list.ToDataSource(convName);
        }

        public static IEnumerable GetDataSource(IList<IDictionary<string, string>> _conv)
        {

            IList<IDictionary> list = new List<IDictionary>();
            foreach (IDictionary<string, string> _item in _conv)
            {
                IDictionary _dc = new Dictionary<string, string>();
                foreach (string _key in _item.Keys)
                {
                    _dc.Add(_key, _item[_key]);
                }
                list.Add(_dc);
            }
            return list.ToDataSource();
        }

        public static IEnumerable GetDataSource(IList<IDictionary> _conv)
        {
            return _conv.ToDataSource();
        }
    }
}
