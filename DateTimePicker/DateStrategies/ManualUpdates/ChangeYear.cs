using System;
using DateTimePicker.Interfaces;

namespace DateTimePicker.DateStrategies.ManualUpdates
{
    public class ChangeYear : IManuallyUpdateDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime, char number, int previouslyEnteredNumber)
        {
            if (char.IsWhiteSpace(number))
                return dateTime;

            if (!char.IsDigit(number))
                return dateTime;

            int year = (int)char.GetNumericValue(number);

            string s = dateTime.Year.ToString();
            string str;
            switch (previouslyEnteredNumber)
            {
                case 3:
                    str = $"{s[0]}{s[1]}{s[2]}{year}";
                    break;
                case 2:
                    str = $"{s[0]}{s[1]}{year}{s[3]}";
                    break;
                case 1:
                    str = $"{s[0]}{year}{s[2]}{s[3]}";
                    break;
                case 0:
                    str = $"{year}{s[1]}{s[2]}{s[3]}";
                    break;
                default:
                    str = dateTime.Year.ToString();
                    break;
            }
            year = int.Parse(str);

            if (year < 1)
                year = 1;

            DateTime dt = new DateTime(year, dateTime.Month, dateTime.Day,
                dateTime.Hour, dateTime.Minute, dateTime.Second);
            return dt;
        }
    }
}