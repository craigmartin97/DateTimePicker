using System;

namespace DateTimePicker.DateStrategies
{
    internal class DecreaseMinuteStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddMinutes(-1);
    }
}