using System;

namespace DateTimePicker.DateStrategies
{
    internal class DecreaseYearStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddYears(-1);
    }
}