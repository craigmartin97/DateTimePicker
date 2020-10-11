using DateTimePicker.DateStrategies;

namespace DateTimePicker.Models
{
    internal class FormatSpecifier
    {
        #region Properties

        public string Specifier { get; set; }
        public int Index { get; set; }
        public IDateTimeStrategy IncrementStrategy { get; set; }
        public IDateTimeStrategy DecrementStrategy { get; set; }
        #endregion

        #region Constructors

        public FormatSpecifier(string specifier, int index, IDateTimeStrategy incrementStrategy, IDateTimeStrategy decrementStrategy)
        {
            Specifier = specifier;
            Index = index;
            IncrementStrategy = incrementStrategy;
            DecrementStrategy = decrementStrategy;
        }
        #endregion
    }
}