using System;
using DateTimePicker.Interfaces;

namespace DateTimePicker.DateStrategies.ManualUpdates
{
    public class ChangeMinute : IManuallyUpdateDateTimeStrategy, IManuallyUpdateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime, char number, int previouslyEnteredNumber)
        {
            if (char.IsWhiteSpace(number))
                return dateTime;

            if (!char.IsDigit(number))
                return dateTime;

            int minute = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber == 1)
            {
                string s = dateTime.Minute.ToString();
                string str = $"{s[0]}{minute}";
                minute = int.Parse(str);
                if (minute < 0 || minute > 59)
                    minute = 0;
            }

            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                dateTime.Hour, minute, dateTime.Second);
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

            int tempMinute = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber)
            {
                string s = minute.ToString();
                string str = $"{s[0]}{tempMinute}";
                tempMinute = int.Parse(str);
                if (tempMinute < 0 || tempMinute >= 60)
                    tempMinute = 0;
            }

            int tempSecond = second;
            int tempHour = hour;

            if (tempHour == 24 && tempMinute > 0) // This would case an invalid time
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