﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace DateTimePicker.Converters
{
    internal class NotNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
                return string.IsNullOrWhiteSpace(str);
            
            return value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}