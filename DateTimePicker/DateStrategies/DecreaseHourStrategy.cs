using System;

namespace DateTimePicker.DateStrategies
{
    public class DecreaseHourStrategy : IDateTimeStrategy
    {
        public DateTime UpdateDateTime(DateTime dateTime) => dateTime.AddHours(-1);
    }
}