using System;
using System.Windows.Data;

namespace HDIMSAPP.Common.Converter
{
    public class RainFallConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
    public class NoRainFallConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter!=null && parameter!=null && parameter.ToString().Equals("RF"))
            {
                double _val;
                bool _res = Double.TryParse(value.ToString(), out _val);
                if (_res)
                {
                    return (_val == 0) ? "" : value;
                }
                return value;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
