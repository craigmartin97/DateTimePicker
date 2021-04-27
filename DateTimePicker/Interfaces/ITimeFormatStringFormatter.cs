using System.Collections.Generic;
using DateTimePicker.Models;

namespace DateTimePicker.Interfaces
{
    internal interface ITimeFormatStringFormatter
    {
        IEnumerable<TimeFormatSpecifier> CalculateTimeFormatSpecifiers(string formatString);
    }
}