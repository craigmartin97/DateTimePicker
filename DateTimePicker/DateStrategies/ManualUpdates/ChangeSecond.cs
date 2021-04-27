using DateTimePicker.Interfaces;
using System;

namespace DateTimePicker.DateStrategies.ManualUpdates
{
    public class ChangeSecond : IManuallyUpdateDateTimeStrategy, IManuallyUpdateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime, char number, int previouslyEnteredNumber)
        {
            if (char.IsWhiteSpace(number))
                return dateTime;

            if (!char.IsDigit(number))
                return dateTime;

            int second = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber == 1)
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

        public void UpdateTime(int hour, int minute, int second, char number, bool previouslyEnteredNumber, out int updatedHour,
            out int updatedMinute, out int updatedSecond)
        {
            if (char.IsWhiteSpace(number) || !char.IsDigit(number))
            {
                updatedHour = hour;
                updatedMinute = minute;
                updatedSecond = second;
                return;
            }

            int tempSecond = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber)
            {
                string s = second.ToString();
                string str = $"{s[0]}{tempSecond}";
                tempSecond = int.Parse(str);
                if (tempSecond < 0 || tempSecond > 59)
                    tempSecond = 0;
            }

            int tempMinute = minute;
            int tempHour = hour;

            if (tempHour == 24 && (tempMinute > 0 || tempSecond > 0)) // This would case an invalid time
            {
                // Reset
                tempSecond = 0;
                tempMinute = 0;
            }

            updatedHour = tempHour;
            updatedMinute = tempMinute;
            updatedSecond = tempSecond;
        }
    }
}