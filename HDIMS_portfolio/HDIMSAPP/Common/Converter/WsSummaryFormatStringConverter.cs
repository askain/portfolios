﻿using System;
using System.Windows.Data;

namespace HDIMSAPP.Common.Converter
{
    public class WsSummaryFormatStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
