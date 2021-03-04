using System;
using DateTimePicker.Interfaces;

namespace DateTimePicker.DateStrategies.ManualUpdates
{
    public class ChangeMinute : IManuallyUpdateDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime, char number, bool previouslyEnteredNumber)
        {
            if (char.IsWhiteSpace(number))
                return dateTime;

            if (!char.IsDigit(number))
                return dateTime;

            int minute = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber)
            {
                string s = dateTime.Minute.ToString();
                string str = $"{s[0]}{minute}";
                minute = int.Parse(str);
                if (minute < 0 || minute > 60)
                    minute = 0;
            }

            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                dateTime.Hour, minute, dateTime.Second);
            return dt;
        }
    }
}