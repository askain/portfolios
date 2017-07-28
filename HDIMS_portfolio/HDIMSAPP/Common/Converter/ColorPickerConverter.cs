using System;
using System.Windows.Data;
using HDIMSAPP.Models;
using HDIMSAPP.Models.Codes;

namespace HDIMSAPP.Common.Converter
{
    public class ColorPickerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                ExamManagementModel ne = value as ExamManagementModel;
                string value_test = null;
                value_test += ne.EXCOLOR.ToString();

                return Constants.GetColorFromString(value_test);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
