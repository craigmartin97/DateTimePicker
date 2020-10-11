using System;

namespace DateTimePicker.DateStrategies
{
    internal class DecreaseDayStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddDays(-1);
    }
}