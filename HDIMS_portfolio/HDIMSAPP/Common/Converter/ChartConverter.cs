using System;
using System.Windows;
using System.Windows.Data;

namespace HDIMSAPP.Common.Converter
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return (Visibility)value == Visibility.Visible ? true : false;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            return Visibility.Collapsed;
        }
    }
}
