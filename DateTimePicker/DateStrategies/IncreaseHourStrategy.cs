using System;

namespace DateTimePicker.DateStrategies
{
    internal class IncreaseHourStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) 
            => dateTime.AddHours(1);
    }
}