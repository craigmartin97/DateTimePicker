using DateTimePicker.Interfaces;
using System;

namespace DateTimePicker.DateStrategies.ManualUpdates
{
    public class ChangeHour : IManuallyUpdateDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime, char number, bool previouslyEnteredNumber)
        {
            if (char.IsWhiteSpace(number))
                return dateTime;

            if (!char.IsDigit(number))
                return dateTime;

            int hour = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber)
            {
                string s = dateTime.Hour.ToString();
                string str = $"{s[0]}{hour}";
                hour = int.Parse(str);
                if (hour < 0 || hour > 23)
                    hour = 0;
            }

            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                hour, dateTime.Minute, dateTime.Second);
            return dt;
        }
    }
}