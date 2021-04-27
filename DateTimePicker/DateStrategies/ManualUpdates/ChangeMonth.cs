using DateTimePicker.Interfaces;
using System;

namespace DateTimePicker.DateStrategies.ManualUpdates
{
    public class ChangeMonth : IManuallyUpdateDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime, char number, int previouslyEnteredNumber)
        {
            if (char.IsWhiteSpace(number))
                return dateTime;

            if (!char.IsDigit(number))
                return dateTime;

            int month = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber == 1)
            {
                string s = dateTime.Month.ToString();
                string str = $"{s[0]}{month}";
                month = int.Parse(str);
            }

            if (month < 1 || month > 12)
                month = 1;

            DateTime dt = new DateTime(dateTime.Year, month, dateTime.Day,
                dateTime.Hour, dateTime.Minute, dateTime.Second);
            return dt;
        }
    }
}