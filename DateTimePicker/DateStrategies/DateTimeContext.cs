using DateTimePicker.Models;
using System;

namespace DateTimePicker.DateStrategies
{
    internal class DateTimeContext
    {
        #region Fields

        private readonly FormatSpecifier _formatSpecifier;
        #endregion

        #region Constructors

        public DateTimeContext(FormatSpecifier formatSpecifier)
        {
            _formatSpecifier = formatSpecifier;
        }
        #endregion

        public DateTime ExecuteIncrease(DateTime dateTime)
            => _formatSpecifier.IncrementStrategy.UpdateDateTime(dateTime);

        public DateTime ExecuteDecrease(DateTime dateTime)
            => _formatSpecifier.DecrementStrategy.UpdateDateTime(dateTime);

        public DateTime UpdateDateTime(DateTime dateTime, char number, int previouslyEnteredNumber)
            => _formatSpecifier.ManuallyUpdateDateTimeStrategy.UpdateDateTime(dateTime, number, previouslyEnteredNumber);

        public string GetSpecifier() => _formatSpecifier.Specifier;

        public int GetPreviousReset()
        {
            string s = GetSpecifier();
            return s switch
            {
                // Day
                "dd" => 1,
                // Month
                "MM" => 1,
                // Year
                "yyyy" => 3,
                // Hour
                "HH" => 1,
                // Minute
                "mm" => 1,
                // Second
                "ss" => 1,
                _ => 2
            };
        }
    }
}