using DateTimePicker.Models;
using System.Collections.Generic;

namespace DateTimePicker
{
    internal class PreLoadTimeOptions
    {
        public static IList<Time> GetPreLoadTimes()
        {
            IList<Time> times = new List<Time>();

            for (int i = 0; i < 10; i++)
            {
                Time time = new Time(i, 0,0);
                times.Add(time);
            }

            for (int i = 10; i < 24; i++)
            {
                Time time = new Time(i, 0,0);
                times.Add(time);
            }

            return times;
        }
    }
}