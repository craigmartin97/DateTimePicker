using DateTimePicker.Interfaces;
using System;

namespace DateTimePicker.DateStrategies.ManualUpdates
{
    public class ChangeMonth : IManuallyUpdateDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime, char number, bool previouslyEnteredNumber)
        {
            if (char.IsWhiteSpace(number))
                return dateTime;

            if (!char.IsDigit(number))
                return dateTime;

            int month = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber)
            {
                string s = dateTime.Month.ToString();
                string str = $"{s[0]}{month}";
                month = int.Parse(str);
                if (month < 0 || month > 12)
                    month = 0;
            }

            DateTime dt = new DateTime(dateTime.Year, month, dateTime.Day,
                dateTime.Hour, dateTime.Minute, dateTime.Second);
            return dt;
        }
    }
}