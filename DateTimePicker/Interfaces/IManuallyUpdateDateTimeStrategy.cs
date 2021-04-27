using System;

namespace DateTimePicker.Interfaces
{
    public interface IManuallyUpdateDateTimeStrategy
    {
        DateTime UpdateDateTime(DateTime dateTime, char number, int previouslyEnteredNumber);
    }
}