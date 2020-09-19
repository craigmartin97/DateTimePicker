using System;

namespace DateTimePicker.DateStrategies
{
    public class DecreaseYearStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddYears(-1);
    }
}