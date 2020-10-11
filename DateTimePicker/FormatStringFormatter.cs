using DateTimePicker.Exceptions;
using DateTimePicker.Factories;
using DateTimePicker.Interfaces;
using DateTimePicker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DateTimePicker
{
    internal class FormatStringFormatter : IFormatStringFormatter
    {
        #region Constructors

        public FormatStringFormatter()
        {

        }
        #endregion

        public IEnumerable<FormatSpecifier> CalculateMainFormatSpecifiers(string formatString)
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
            IFormatSpecifierFactory factory = new MainControlFormatSpecifierFactory();

            string[] specifiers = formatString.Split(FormatStringSeperators.Seperators);
            FormatSpecifier[] formatSpecifiers = new FormatSpecifier[specifiers.Length];
            for (int i = 0; i < specifiers.Length; i++)
            {
                FormatSpecifier formatSpecifier = factory.CreateFormatSpecifier(specifiers[i], i);
                formatSpecifiers[i] = formatSpecifier;
            }

            return formatSpecifiers;
        }

        public IEnumerable<FormatSpecifier> CalculateTimeFormatSpecifiers(string formatString)
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
            IFormatSpecifierFactory factory = new SubControlFormatSpecifierFactory();

            int index = 0;
            string[] specifiers = formatString.Split(FormatStringSeperators.Seperators);
            IList<FormatSpecifier> formatSpecifiers = new List<FormatSpecifier>();
            foreach (string specifier in specifiers)
            {
                string s = specifier;
                if(!(s.Equals("HH") || s.Equals("mm") || s.Equals("ss")))
                    continue;

                FormatSpecifier formatSpecifier = factory.CreateFormatSpecifier(specifier, index);
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