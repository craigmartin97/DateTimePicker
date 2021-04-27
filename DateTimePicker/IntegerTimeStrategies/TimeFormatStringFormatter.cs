using DateTimePicker.Exceptions;
using DateTimePicker.Factories;
using DateTimePicker.Interfaces;
using DateTimePicker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DateTimePicker.IntegerTimeStrategies
{
    internal class TimeFormatStringFormatter : ITimeFormatStringFormatter
    {
        public IEnumerable<TimeFormatSpecifier> CalculateTimeFormatSpecifiers(string formatString)
        {
            // The user has specified a custom string
            if (string.IsNullOrWhiteSpace(formatString))
                return null;

            if (!ValidStringFormat(formatString))
                return null;

            /*
             * Validated a valid DateTime Format from the user.
             * Calculate the positions of each data type.
             */
            ITimeFormatSpecifierFactory factory = new TimeFormatSpecifierFactory();

            int index = 0;
            string[] specifiers = formatString.Split(FormatStringSeperators.Seperators);
            IList<TimeFormatSpecifier> formatSpecifiers = new List<TimeFormatSpecifier>();
            foreach (string specifier in specifiers)
            {
                string s = specifier;
                if (!(s.Equals("HH") || s.Equals("mm") || s.Equals("ss")))
                    continue;

                TimeFormatSpecifier formatSpecifier = factory.CreateFormatSpecifier(specifier, index);
                formatSpecifiers.Add(formatSpecifier);
                index++;
            }

            return formatSpecifiers;
        }

        private bool ValidStringFormat(string formatString)
        {
            try
            {
                bool parsed = DateTime.TryParse(DateTime.Now.ToString(formatString), out _);
                if (!parsed)
                    throw new InvalidDateTimeFormatException(
                        $"The FormatString provided '{formatString}' is invalid");

                return true;
            }
            catch (FormatException)
            {
                Debug.WriteLine("The Format String was invalid", "Error");
                throw;
            }
        }
    }
}