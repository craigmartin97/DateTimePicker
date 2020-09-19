using System;

namespace DateTimePicker.DateStrategies
{
    public class DateTimeContext
    {
        #region Fields

        private readonly IDateTimeStrategy _strategy;
        #endregion

        #region Constructors

        public DateTimeContext(IDateTimeStrategy strategy)
        {
            _strategy = strategy;
        }
        #endregion

        public DateTime Execute(DateTime dateTime)
        {
            return _strategy.UpdateDateTime(dateTime);
        }
    }
}