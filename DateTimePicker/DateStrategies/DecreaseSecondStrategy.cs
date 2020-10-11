using System;

namespace DateTimePicker.DateStrategies
{
    internal class DecreaseSecondStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddSeconds(-1);
    }
}