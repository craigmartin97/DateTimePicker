using DateTimePicker.Interfaces;
using System.Linq;

namespace DateTimePicker
{
    internal class TimeFormatSpecifierCalculator : ITimeFormatSpecifierCalculator
    {
        public string CalculateTimeFormatString(string formatString)
        {
            string[] specifiers = formatString.Split(FormatStringSeperators.Seperators);

            return specifiers.Where(specifier => 
                specifier.Equals("HH") || 
                specifier.Equals("mm") || 
                specifier.Equals("ss"))
                .Aggregate<string, string>(null, (current, specifier) => current + specifier);
        }
    }
}