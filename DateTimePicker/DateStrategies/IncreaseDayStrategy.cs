using System;

namespace DateTimePicker.DateStrategies
{
    internal class IncreaseDayStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddDays(1);
    }
}