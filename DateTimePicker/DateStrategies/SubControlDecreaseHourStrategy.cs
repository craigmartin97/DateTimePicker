using System;

namespace DateTimePicker.DateStrategies
{
    internal class SubControlDecreaseHourStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime)
        {
            DateTime decreaseDateTime = dateTime.AddHours(-1);
            if (decreaseDateTime.Day < dateTime.Day)
            {
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 
                    decreaseDateTime.Hour, decreaseDateTime.Minute, decreaseDateTime.Second);
            }

            return decreaseDateTime;
        }
    }
}