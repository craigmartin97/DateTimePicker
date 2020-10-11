using System;

namespace DateTimePicker.DateStrategies
{
    internal class DecreaseHourStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddHours(-1);
    }
}