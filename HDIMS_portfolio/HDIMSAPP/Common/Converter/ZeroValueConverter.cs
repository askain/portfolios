using System;
using System.Windows.Data;

namespace HDIMSAPP.Common.Converter
{
    public class ZeroValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                string _val = value as string;
                bool _isInvisible = (bool)parameter;
                if (_isInvisible && (_val.Equals("0")||_val.Equals("999")||_val.Equals("-99.9"))) return "";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
