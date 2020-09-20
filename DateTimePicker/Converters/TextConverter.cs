using System;
using System.Globalization;
using System.Windows.Data;

namespace DateTimePicker.Converters
{
    public class TextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? dateTime = (DateTime?)values[0];
            string format = (string)values[1];

            return dateTime?.ToString(format, culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}