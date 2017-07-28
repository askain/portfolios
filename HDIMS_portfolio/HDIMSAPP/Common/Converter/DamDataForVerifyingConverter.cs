using System;
using System.Windows.Data;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Common.Converter
{
    public class DamDataForVerifyingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string KEY = (string)parameter;
            //MessageBox.Show(KEY);
            //DamDataConst dc = parameter as DamDataConst;
            //MessageBox.Show("2 " + dc.KEY);
            if (KEY.Equals("SDATE") || "EDATE".Equals(KEY))
            {
                return DateUtil.convStrToDate(value.ToString());
            }

            // EditMode 이벤트가 안먹으면 EditValueConverter도 작동 안함. 아하하 시발.
            //else if (KEY.Equals("ACT"))
            //{
            //    MessageBox.Show("V : " + targetType.ToString());
            //    if(targetType == typeof(IList<DAMLinearInterpolationResultModel>)) 
            //    {
            //        return "{ ... }";
            //    } 

            //    return value;
            //}
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
