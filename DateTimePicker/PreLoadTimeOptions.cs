using DateTimePicker.Models;
using System.Collections.Generic;

namespace DateTimePicker
{
    internal class PreLoadTimeOptions
    {
        public static IList<Time> GetPreLoadTimes(string formatString)
        {
            IList<Time> times = new List<Time>();

            for (int i = 0; i < 24; i++)
            {
                Time time = new Time(i, 0, formatString, 0);
                times.Add(time);
            }

            return times;
        }
    }
}