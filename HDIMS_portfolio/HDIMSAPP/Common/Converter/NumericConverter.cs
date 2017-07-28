using System;
using System.Windows.Data;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Common.Converter
{

    public class NumericConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int DigitsUnderDot = 0;
                if (parameter != null && int.TryParse(parameter.ToString(), out DigitsUnderDot) == false) 
                {
                    DigitsUnderDot = 0;    
                }
                
                return string.Format(CommonUtil.GetNumberFormat(DigitsUnderDot), Double.Parse(value.ToString()));
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }



}
