using System;

namespace DateTimePicker.DateStrategies
{
    public class DecreaseDayStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddDays(-1);
    }
}