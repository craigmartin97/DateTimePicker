using System;

namespace DateTimePicker.DateStrategies
{
    internal class IncreaseYearStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddYears(1);
    }
}