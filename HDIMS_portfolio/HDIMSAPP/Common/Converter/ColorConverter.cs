using System;
using System.Windows.Data;
using System.Windows.Media;
using HDIMSAPP.Models;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Common.Converter
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null) 
            {
                if(value.ToString().Length == 6) 
                {
                    return new SolidColorBrush(Constants.GetColorFromString(value.ToString()));
                }
                else 
                {
                    //글자에 근거하여 반 랜덤색 생성
                    char[] charArray = value.ToString().ToCharArray();
                    int result = 0;
                    foreach(char c in charArray) {
                        result += StringUtil.Asc(c);
                    }

                    result = result % 80;
                    return new SolidColorBrush(Constants.GetColorFromString(Constants.ChartColors[result]));
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
