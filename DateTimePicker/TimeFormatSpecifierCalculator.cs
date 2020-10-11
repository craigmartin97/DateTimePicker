using DateTimePicker.Interfaces;
using System;
using System.Linq;

namespace DateTimePicker
{
    internal class TimeFormatSpecifierCalculator : ITimeFormatSpecifierCalculator
    {
        public string CalculateTimeFormatString(string formatString)
        {

            string timeFormatString = null;
            string[] specifiers = formatString.Split(FormatStringSeperators.Seperators);

            foreach (string specifier in specifiers)
            {
                if (specifier.Equals("HH") ||
                    specifier.Equals("mm") ||
                    specifier.Equals("ss"))
                {
                    timeFormatString += specifier;

                    // Find the next char after this specifier
                    int indexOf = formatString.IndexOf(specifier, StringComparison.Ordinal);
                    char sep = formatString.ElementAtOrDefault(indexOf + specifier.Length);

                    if(sep == default(char))
                        continue;

                    if (FormatStringSeperators.Seperators.Contains(sep))
                        timeFormatString += sep;
                    else
                        timeFormatString += ':';
                }
            }

            return timeFormatString;
        }
    }
}