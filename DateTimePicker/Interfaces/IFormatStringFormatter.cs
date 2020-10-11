using DateTimePicker.Models;
using System.Collections.Generic;

namespace DateTimePicker.Interfaces
{
    internal interface IFormatStringFormatter
    {
        IEnumerable<FormatSpecifier> CalculateMainFormatSpecifiers(string formatString);
        IEnumerable<FormatSpecifier> CalculateTimeFormatSpecifiers(string formatString);
    }
}