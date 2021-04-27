using DateTimePicker.IntegerTimeStrategies;
using DateTimePicker.Interfaces;

namespace DateTimePicker.Models
{
    internal readonly struct TimeFormatSpecifier
    {
        #region Properties

        public string Specifier { get; }
        public int Index { get; }
        public IIntegerTimeStrategy IncrementStrategy { get; }
        public IIntegerTimeStrategy DecrementStrategy { get; }
        public IManuallyUpdateTimeStrategy ManuallyUpdateTimeStrategy { get; }
        #endregion

        #region Constructors

        public TimeFormatSpecifier(string specifier, 
            int index,
            IIntegerTimeStrategy incrementStrategy,
            IIntegerTimeStrategy decrementStrategy,
            IManuallyUpdateTimeStrategy manuallyUpdateTimeStrategy)
        {
            Specifier = specifier;
            Index = index;
            IncrementStrategy = incrementStrategy;
            DecrementStrategy = decrementStrategy;
            ManuallyUpdateTimeStrategy = manuallyUpdateTimeStrategy;
        }
        #endregion
    }
}