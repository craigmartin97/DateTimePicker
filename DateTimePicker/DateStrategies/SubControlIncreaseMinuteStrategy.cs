using System;

namespace DateTimePicker.DateStrategies
{
    public class SubControlIncreaseMinuteStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime)
        {
            DateTime increaseDateTime = dateTime.AddMinutes(1);
            if (increaseDateTime.Day <= dateTime.Day)
                return increaseDateTime;

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                increaseDateTime.Hour, increaseDateTime.Minute, increaseDateTime.Second);
        }
    }
}