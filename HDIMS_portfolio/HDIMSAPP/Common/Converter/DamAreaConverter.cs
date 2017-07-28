using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Infragistics;

namespace HDIMSAPP.Common.Converter
{
    public class DamAreaFontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                SummaryResult _result = value as SummaryResult;
                
                double _cval;
                bool _res = Double.TryParse(_result.Value.ToString(), out _cval);
                if (_res && _cval > 0)
                {
                    return FontWeights.Bold;
                }
            }
            return FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return FontWeights.Normal;
        }
    }

    public class DamAreaForegroundConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                SummaryResult _result = value as SummaryResult;
                double _cval;
                bool _res = Double.TryParse(_result.Value.ToString(), out _cval);
                if (_res)
                {
                    if(_cval > 0 && _cval < 10)
                        return new SolidColorBrush(Color.FromArgb(255, 244, 124, 39));
                    else if(_cval >= 10)
                        return new SolidColorBrush(Colors.Red);
                }
            }
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new SolidColorBrush(Colors.Black);
        }
    }
}
