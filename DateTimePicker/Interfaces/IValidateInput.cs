using DateTimePicker.Events;

namespace DateTimePicker.Interfaces
{
    public interface IValidateInput
    {
        event InputValidationErrorEventHandler InputValidationError;
        bool CommitInput();
    }
}