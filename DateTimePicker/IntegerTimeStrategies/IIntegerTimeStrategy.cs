namespace DateTimePicker.IntegerTimeStrategies
{
    public interface IIntegerTimeStrategy
    {
        void UpdateTime(int hour, int minute, int second, 
            out int updatedHour, out int updatedMinute, out int updatedSecond);
    }
}