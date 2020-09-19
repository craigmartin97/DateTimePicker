using System;

namespace DateTimePicker.DateStrategies
{
    public class IncreaseHourStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddHours(1);
    }
}