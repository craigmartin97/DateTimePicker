namespace DateTimePicker.IntegerTimeStrategies
{
    public class DecreaseSecond : IIntegerTimeStrategy
    {
        public void UpdateTime(int hour, int minute, int second, out int updatedHour, 
            out int updatedMinute, out int updatedSecond)
        {
            if (hour == 0 && minute == 0) // Special case to make control wrap around to start as end it met
            {
                updatedHour = 24;
                updatedMinute = 0;
                updatedSecond = 0;
            }
            else if (second == 0) // This is the end
            {
                int m = minute - 1;
                int h = hour;
                const int s = 59;
                if (m == -1)
                {
                    m = 59;
                    h--;
                }

                if (h == 24 && minute == 0)
                {
                    h = 0;
                    m = 0;
                }

                updatedMinute = m;
                updatedSecond = s;
                updatedHour = h;
            }
            else
            {
                updatedMinute = minute;
                updatedHour = hour;
                updatedSecond = second - 1;
            }
        }
    }
}