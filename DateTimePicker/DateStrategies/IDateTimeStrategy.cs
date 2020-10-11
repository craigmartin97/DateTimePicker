using System;

namespace DateTimePicker.DateStrategies
{
    internal interface IDateTimeStrategy
    {
        DateTime UpdateDateTime(DateTime dateTime);
    }
}