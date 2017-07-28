using System;
using System.Text.RegularExpressions;
using System.Windows.Data;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Common.Converter
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string _val = value.ToString();
                if (_val.Equals("TSACU"))
                {
                    return "금회 누계";
                }
                else if (_val.Equals("TYACU"))
                {
                    return "금일 누계";
                } else if(_val.Equals("YYACU")) 
                {
                    return "전일 누계";
                } else if (Regex.IsMatch(_val, "[0-9]", RegexOptions.IgnoreCase)) 
                {
                    bool second = false;
                    if (parameter != null)
                    {
                        if (parameter is string)
                        {
                            if ("true".Equals(parameter.ToString().ToLower()))
                            {
                                second = true;
                            }
                        }
                        else if (parameter is bool)
                        {
                            second = (bool)parameter;
                        }
                    }
                    return DateUtil.convStrToDate(value.ToString(), second);
                }
                return value;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
