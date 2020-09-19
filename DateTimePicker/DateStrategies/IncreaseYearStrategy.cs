using System;

namespace DateTimePicker.DateStrategies
{
    public class IncreaseYearStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddYears(1);
    }
}