using System;
using System.Windows.Data;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Common.Converter
{
    public class EquipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return "-";

            string _key = parameter.ToString();
            string _data = value.ToString();
             
            if (_key.Equals("OBSDT")) {
                return DateUtil.formatDate(_data);
            }
            else if (_key.Equals("BATTVOLT") || _key.Equals("SNR") || _key.Equals("DAMCD") || _key.Equals("DAMNM") || _key.Equals("OBSCD") || _key.Equals("OBSNM"))
            {
                return value;
            } 
            else if (_key.Equals("SECONDARY_CALL"))
            {
                if (_data == null||_data.Equals("0"))
                {
                    return "-";
                }
                else if (_data.Equals("1"))
                {
                    return "위성망(TCP)";
                }
                else if (_data.Equals("2"))
                {
                    return "유선망";
                }
                else if (_data.Equals("3"))
                {
                    return "위성망(UDP)";
                }
                else if (_data.Equals("4"))
                {
                    return "CDMA";
                }
            } else {
                double _val;
                bool _res = Double.TryParse(_data, out _val);
                if (_res)
                {
                    return (_val > 0) ? "이상" : "정상";
                }
                else
                {
                    return "-";
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
