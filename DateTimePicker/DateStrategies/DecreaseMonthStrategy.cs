using System;

namespace DateTimePicker.DateStrategies
{
    public class DecreaseMonthStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddMonths(-1);
    }
}