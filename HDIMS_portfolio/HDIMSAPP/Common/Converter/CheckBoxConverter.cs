using System;
using System.Windows.Data;

namespace HDIMSAPP.Common.Converter
{
    public class CheckBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string _val = value as string;

                if (_val != null)
                {
                    return _val.Contains("Y");
                }
            }
            return value;


        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            bool _val = (bool)value;

            if (_val == true) return "Y";

            return "N";

//            return value;
            
        }
    }
}
