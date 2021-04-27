namespace DateTimePicker.Interfaces
{
    public interface IManuallyUpdateTimeStrategy
    {
        void UpdateTime(int hour, int minute, int second, char number, bool previouslyEnteredNumber,
            out int updatedHour, out int updatedMinute, out int updatedSecond);
    }
}