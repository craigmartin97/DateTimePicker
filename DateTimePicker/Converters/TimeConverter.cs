using System;
using System.Globalization;
using System.Windows.Data;

namespace DateTimePicker.Converters
{
    internal class TimeConverter : IMultiValueConverter
    {
        private DateTime _date;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2)
                return null;

            if (!(values[0] is DateTime dateTime) || !(values[1] is string formatString))
                return null;

            _date = dateTime;
            return dateTime.ToString(formatString, culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (!(value is string)) // Not a string invalid data
                return null;

            if (string.IsNullOrWhiteSpace((string)value)) // No data give. Invalid
                return null;

            string date = _date.Year + "/" + _date.Month + "/" + _date.Day;
            string time = (string)value;
            string fullDateTime = date + " " + time;
            DateTime dateTime = DateTime.Parse(fullDateTime);
            return new object []{ dateTime };
        }
    }
}