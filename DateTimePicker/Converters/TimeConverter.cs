using System;
using System.Globalization;
using System.Windows.Data;

namespace DateTimePicker.Converters
{
    public class TimeConverter : IValueConverter
    {
        private DateTime _date;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            _date = (DateTime)value;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string)) // Not a string invalid data
                return null;

            if (string.IsNullOrWhiteSpace((string)value)) // No data give. Invalid
                return null;

            string date = _date.Year + "/" + _date.Month + "/" + _date.Day;
            string time = (string)value;
            string fullDateTime = date + " " + time;
            DateTime dateTime = DateTime.Parse(fullDateTime);
            return dateTime;
        }
    }
}