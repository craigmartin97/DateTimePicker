using DateTimePicker.Interfaces;
using System;

namespace DateTimePicker.DateStrategies.ManualUpdates
{
    public class ChangeSecond : IManuallyUpdateDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime, char number, bool previouslyEnteredNumber)
        {
            if (char.IsWhiteSpace(number))
                return dateTime;

            if (!char.IsDigit(number))
                return dateTime;

            int second = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber)
            {
                string s = dateTime.Second.ToString();
                string str = $"{s[0]}{second}";
                second = int.Parse(str);
                if (second < 0 || second > 59)
                    second = 0;
            }

            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                dateTime.Hour, dateTime.Minute, second);
            return dt;
        }
    }
}