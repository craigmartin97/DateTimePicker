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
    }
}