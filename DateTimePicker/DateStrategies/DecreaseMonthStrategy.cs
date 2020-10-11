using System;

namespace DateTimePicker.DateStrategies
{
    internal class DecreaseMonthStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddMonths(-1);
    }
}