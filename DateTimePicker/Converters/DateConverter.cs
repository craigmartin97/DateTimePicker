using System;
using System.Globalization;
using System.Windows.Data;

namespace DateTimePicker.Converters
{
    internal class DateConverter : IValueConverter
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
            if (value == null)
                return null;

            DateTime dateTime = (DateTime)value;
            DateTime fullDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                _date.Hour, _date.Minute, 0);
            return fullDateTime;
        }
    }
}