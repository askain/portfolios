using System;
using System.Windows.Data;
using HDIMSAPP.Models.Statistics;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Common.Converter
{
    public class AutoStatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            AutoStatModel row = value as AutoStatModel;
            if (row != null)
            {
                string ret = "";
                string obsdt = row.OBSDT;
                obsdt = DateUtil.convStrToDateKor(obsdt, row.STATTP, false);
                ret = row.MGTNM + " " + obsdt + " " + "수문자료 신뢰도 분석";
                return ret;

                //return DateUtil.convStrToDateKor(value.ToString(), row.STATTP, false);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class AutoStatTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string ret = "월별";
                if (value.ToString().Equals("Q"))
                {
                    ret = "분기별";
                }
                else
                {
                    ret = "월별";
                }
                return ret;

                //return DateUtil.convStrToDateKor(value.ToString(), row.STATTP, false);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
