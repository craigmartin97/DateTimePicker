using System;

namespace DateTimePicker.DateStrategies
{
    internal class IncreaseMinuteStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddMinutes(1);
    }
}