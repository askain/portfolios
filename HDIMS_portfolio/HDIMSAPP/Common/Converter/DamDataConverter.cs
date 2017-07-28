using System;
using System.Windows.Data;
using HDIMSAPP.Models;
using HDIMSAPP.Models.Verify;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Common.Converter
{

    public class DamDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DamDataColumn dc = parameter as DamDataColumn;
                if (dc.DataType==GridDataType.DATE)
                {
                    return DateUtil.convStrToDate(value.ToString());
                }
                else if (dc.DataType==GridDataType.NUMBER && dc.Digit > 0)
                {
                    try
                    {
                        return string.Format(CommonUtil.GetNumberFormat(dc.Digit), Double.Parse(value.ToString()));
                    }
                    catch (Exception e)
                    {
                        return value;
                    }
                }
                return value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }



}
