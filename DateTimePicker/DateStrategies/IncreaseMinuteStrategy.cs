using System;

namespace DateTimePicker.DateStrategies
{
    public class IncreaseMinuteStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddMinutes(1);
    }
}