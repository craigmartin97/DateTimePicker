using System;

namespace DateTimePicker.DateStrategies
{
    public class SubControlIncreaseHourStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime)
        {
            DateTime increaseDateTime = dateTime.AddHours(1);
            if (increaseDateTime.Day <= dateTime.Day) 
                return increaseDateTime;

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 
                increaseDateTime.Hour, increaseDateTime.Minute, increaseDateTime.Second);
        }
    }
}