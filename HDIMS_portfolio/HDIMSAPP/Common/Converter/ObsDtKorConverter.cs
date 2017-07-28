using System;
using System.Windows.Data;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Common.Converter
{
    public class ObsDtKorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string obsdt = value as string;
            string stattp = parameter as string;
            if (obsdt != null)
            {
                return DateUtil.convStrToDateKor(obsdt, stattp, false);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
