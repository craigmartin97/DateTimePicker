using System;

namespace DateTimePicker.DateStrategies
{
    public class DecreaseMinuteStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddMinutes(-1);
    }
}