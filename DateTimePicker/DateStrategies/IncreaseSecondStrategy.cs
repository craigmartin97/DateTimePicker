using System;

namespace DateTimePicker.DateStrategies
{
    internal class IncreaseSecondStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddSeconds(1);
    }
}