namespace DateTimePicker.IntegerTimeStrategies
{
    public class DecreaseMinute : IIntegerTimeStrategy
    {
        public void UpdateTime(int hour, int minute, int second, out int updatedHour,
            out int updatedMinute, out int updatedSecond)
        {
            if (hour == 0 && minute == 0) // Special case to make control wrap around to end as the start is met
            {
                updatedHour = 24;
                updatedMinute = 0;
                updatedSecond = 0;
            }
            else if (minute == 0) // This is the end
            {
                updatedMinute = 59;
                updatedSecond = second;
                updatedHour = hour - 1;
            }
            else
            {
                int m = minute - 1;
                updatedMinute = m;
                updatedHour = hour;
                updatedSecond = second;
            }
        }
    }
}