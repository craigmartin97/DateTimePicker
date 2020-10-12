using System;
using System.Globalization;
using System.Windows.Data;

namespace DateTimePicker.Converters
{
    /// <summary>
    /// Convert the date time to the UI and back to the data source
    /// </summary>
    internal class DateConverter : IValueConverter
    {
        #region Fields

        /// <summary>
        /// Stored DateTime from the UI
        /// </summary>
        private DateTime _date;
        #endregion

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
                _date.Hour, _date.Minute, _date.Second);
            return fullDateTime;
        }
    }
}