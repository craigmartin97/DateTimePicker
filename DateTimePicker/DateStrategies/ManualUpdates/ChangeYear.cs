using System;
using DateTimePicker.Interfaces;

namespace DateTimePicker.DateStrategies.ManualUpdates
{
    public class ChangeYear : IManuallyUpdateDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime, char number, bool previouslyEnteredNumber)
        {
            if (char.IsWhiteSpace(number))
                return dateTime;

            if (!char.IsDigit(number))
                return dateTime;

            int year = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber)
            {
                string s = dateTime.Year.ToString();
                string str = $"{s[0]}{year}";
                year = int.Parse(str);
                if (year < 1)
                    year = 1;
            }

            DateTime dt = new DateTime(year, dateTime.Month, dateTime.Day,
                dateTime.Hour, dateTime.Minute, dateTime.Second);
            return dt;
        }
    }
}