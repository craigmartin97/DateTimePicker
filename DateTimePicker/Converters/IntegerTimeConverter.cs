using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Reflection;

namespace DateTimePicker.Converters
{
    public class IntegerTimeConverter : IMultiValueConverter
    {
        private DateTime _date;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 4)
                return null;

            if (!(values[0] is int hour) || 
                !(values[1] is int minute) || 
                !(values[2] is int second) || 
                !(values[3] is string formatString))
                return null;

            string hourStr = hour.ToString();
            string minuteStr = minute.ToString();
            string secondStr = second.ToString();

            if (hour <= 9)
            {
                hourStr = $"0{hour}";
            }

            if (minute <= 9)
            {
                minuteStr = $"0{minute}";
            }

            if (second <= 9)
            {
                secondStr = $"0{second}";
            }

            StringBuilder stringBuilder = new StringBuilder();

            char[] separators = {':', '-', ',', '.'};
            string[] fs = formatString.Split(separators);
            for(int i = 0; i < fs.Length; i++)
            {
                switch (fs[i])
                {
                    case "HH":
                    case "hh":
                        stringBuilder.Append(hourStr);
                        break;
                    case "mm":
                        stringBuilder.Append(minuteStr);
                        break;
                    case "ss":
                        stringBuilder.Append(secondStr);
                        break;
                }

                if(fs.Length == i + 1)
                    break;
                stringBuilder.Append(':');
            }

            string displayString = stringBuilder.ToString();
            return displayString;
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
            return new object[] { dateTime };
        }
    }
}