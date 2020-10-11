using System;

namespace DateTimePicker.DateStrategies
{
    internal class IncreaseMonthStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddMonths(1);
    }
}