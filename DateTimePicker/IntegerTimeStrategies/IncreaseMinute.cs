namespace DateTimePicker.IntegerTimeStrategies
{
    public class IncreaseMinute : IIntegerTimeStrategy
    {
        public void UpdateTime(int hour, int minute, int second, out int updatedHour,
            out int updatedMinute, out int updatedSecond)
        {
            if (hour == 24 && minute == 0) // Special case to make control wrap around to start as end it met
            {
                updatedHour = 0;
                updatedMinute = 0;
                updatedSecond = 0;
            }
            else if (minute == 59) // This is the end
            {
                updatedMinute = 0;
                updatedSecond = 0;
                updatedHour = hour + 1;
            }
            else
            {
                int m = minute + 1;
                updatedMinute = m;
                updatedHour = hour;
                updatedSecond = second;
            }
        }
    }
}