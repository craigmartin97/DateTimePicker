using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Reflection;

namespace DateTimePicker.Converters
{
    public class IntegerTimeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 4)
                return null;

            if (!(values[3] is string formatString))
                return null;

            if (string.IsNullOrWhiteSpace(formatString))
                return null;

            if (values[0] == null || values[1] == null)
            {
                return null;
            }
            else
            {
                // Now know that the hour and minute are not null
                int hour = (int)values[0];
                int minute = (int) values[1];

                string hourStr = hour.ToString();
                string minuteStr = minute.ToString();

                if (hour <= 9)
                {
                    hourStr = $"0{hour}";
                }

                if (minute <= 9)
                {
                    minuteStr = $"0{minute}";
                }

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"{hourStr}:{minuteStr}");

                if (values[2] is int second)
                {
                    string secondStr = second.ToString();
                    if (second <= 9)
                    {
                        secondStr = $"0{second}";
                    }

                    char[] seps = { ':', ';', ',', '.', '-' };
                    string[] str = formatString.Split(seps);

                    string temp = stringBuilder.ToString();
                    string[] madeTimes = temp.Split(seps);

                    if (str.Length == 3 && madeTimes.Length == 2)
                    {
                        stringBuilder.Append($":{secondStr}");
                    }
                }
                else
                {
                    char[] seps = {':', ';', ',', '.', '-'};
                    string[] str = formatString.Split(seps);

                    string temp = stringBuilder.ToString();
                    string[] madeTimes = temp.Split(seps);

                    if (str.Length == 3 && madeTimes.Length == 2)
                    {
                        stringBuilder.Append(":00");
                    }
                }

                string s = stringBuilder.ToString();
                return s;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}