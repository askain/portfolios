using System;
using System.Windows.Data;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Common.Converter
{
    public class ObsDtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return DateUtil.convStrToDate(value.ToString());
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
