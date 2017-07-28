using System;
using System.Collections.Generic;
using System.Windows.Data;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Common.Converter
{
    public class DictionaryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && parameter!=null)
            {
                string key = parameter.ToString();
                IDictionary<string, string> conv = (IDictionary<string, string>)value;
                if (conv.ContainsKey(key))
                {
                    if (key.Equals("OBSDT"))
                        return DateUtil.convStrToDate(conv[key]);
                    return conv[key];
                }
                return "";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
