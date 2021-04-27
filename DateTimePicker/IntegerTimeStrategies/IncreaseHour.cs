namespace DateTimePicker.IntegerTimeStrategies
{
    public class IncreaseHour : IIntegerTimeStrategy
    {
        public void UpdateTime(int hour, int minute, int second, 
            out int updatedHour, out int updatedMinute, out int updatedSecond)
        {
            int h = hour + 1;
            if (h == 24)
            {
                updatedHour = h;
                updatedMinute = 0;
                updatedSecond = 0;
            }
            else if (hour == 24) // At the end, so wrap around back to the start
            {
                updatedHour = 0;
                updatedMinute = 0;
                updatedSecond = 0;
            }
            else
            {
                updatedHour = h;
                updatedMinute = minute;
                updatedSecond = second;
            }
        }
    }
}