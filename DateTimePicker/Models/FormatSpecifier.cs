using DateTimePicker.DateStrategies;
using DateTimePicker.Interfaces;

namespace DateTimePicker.Models
{
    internal readonly struct FormatSpecifier
    {
        #region Properties

        public string Specifier { get; }
        public int Index { get; }
        public IDateTimeStrategy IncrementStrategy { get; }
        public IDateTimeStrategy DecrementStrategy { get; }
        public IManuallyUpdateDateTimeStrategy ManuallyUpdateDateTimeStrategy { get; }
        #endregion

        #region Constructors

        public FormatSpecifier(string specifier, int index, IDateTimeStrategy incrementStrategy, 
            IDateTimeStrategy decrementStrategy, IManuallyUpdateDateTimeStrategy manuallyUpdateDateTimeStrategy)
        {
            Specifier = specifier;
            Index = index;
            IncrementStrategy = incrementStrategy;
            DecrementStrategy = decrementStrategy;
            ManuallyUpdateDateTimeStrategy = manuallyUpdateDateTimeStrategy;
        }
        #endregion
    }
}