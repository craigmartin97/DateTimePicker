using DateTimePicker.Interfaces;
using System;

namespace DateTimePicker.DateStrategies.ManualUpdates
{
    public class ChangeHour : IManuallyUpdateDateTimeStrategy, IManuallyUpdateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime, char number, int previouslyEnteredNumber)
        {
            if (char.IsWhiteSpace(number))
                return dateTime;

            if (!char.IsDigit(number))
                return dateTime;

            int hour = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber == 1)
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

            int tempHour = (int)char.GetNumericValue(number);

            if (previouslyEnteredNumber)
            {
                string s = hour.ToString();
                string str = $"{s[0]}{tempHour}";
                tempHour = int.Parse(str);
                if (tempHour < 0 || tempHour > 24)
                    tempHour = 0;
            }

            int tempSecond = second;
            int tempMinute = minute;

            if (tempHour == 24) // This would case an invalid time
            {
                tempSecond = 0;
                tempMinute = 0;
            }

            updatedHour = tempHour;
            updatedMinute = tempMinute;
            updatedSecond = tempSecond;
        }
    }
}