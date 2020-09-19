using System;

namespace DateTimePicker.DateStrategies
{
    public class IncreaseMonthStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddMonths(1);
    }
}