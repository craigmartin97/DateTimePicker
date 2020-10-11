using System.Collections.Generic;
using DateTimePicker.Models;

namespace DateTimePicker
{
    internal class PreLoadTimeOptions
    {
        public static IList<Time> GetPreLoadTimes()
        {
            IList<Time> times = new List<Time>();

            for (int i = 0; i < 10; i++)
            {
                string s = $"0{i}:00";
                Time time = new Time(i, s);
                times.Add(time);
            }

            for (int i = 10; i < 24; i++)
            {
                string s = $"{i}:00";
                Time time = new Time(i, s);
                times.Add(time);
            }

            return times;
        }
    }
}