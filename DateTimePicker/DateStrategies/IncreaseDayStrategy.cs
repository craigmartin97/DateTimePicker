using System;

namespace DateTimePicker.DateStrategies
{
    public class IncreaseDayStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddDays(1);
    }
}