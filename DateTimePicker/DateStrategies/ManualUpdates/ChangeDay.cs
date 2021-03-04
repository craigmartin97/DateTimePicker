using DateTimePicker.Interfaces;
using System;

namespace DateTimePicker.DateStrategies.ManualUpdates
{
    public class ChangeDay : IManuallyUpdateDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime, char number, bool previouslyEnteredNumber)
        {
            if (char.IsWhiteSpace(number))
                return dateTime;

            if (!char.IsDigit(number))
                return dateTime;

            int day = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber)
            {
                string s = dateTime.Day.ToString();
                string str = $"{s[0]}{day}";
                day = int.Parse(str);
                if (day < 0 || day > 31)
                    day = 0;
            }

            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, day,
                dateTime.Hour, dateTime.Minute, dateTime.Second);
            return dt;
        }
    }
}