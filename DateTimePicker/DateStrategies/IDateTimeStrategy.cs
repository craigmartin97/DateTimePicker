using System;

namespace DateTimePicker.DateStrategies
{
    public interface IDateTimeStrategy
    {
        DateTime UpdateDateTime(DateTime dateTime);
    }
}