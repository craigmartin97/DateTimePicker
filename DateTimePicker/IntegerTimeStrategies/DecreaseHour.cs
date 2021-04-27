namespace DateTimePicker.IntegerTimeStrategies
{
    public class DecreaseHour : IIntegerTimeStrategy
    {
        public void UpdateTime(int hour, int minute, int second,
            out int updatedHour, out int updatedMinute, out int updatedSecond)
        {
            if (hour == 0) // At the start, so wrap around back to the end
            {
                updatedHour = 24;
                updatedMinute = 0;
                updatedSecond = 0;
            }
            else
            {
                int h = hour - 1;
                updatedHour = h;
                updatedMinute = minute;
                updatedSecond = second;
            }
        }
    }
}