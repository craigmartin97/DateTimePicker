using DateTimePicker.DateStrategies;
using DateTimePicker.Interfaces;
using DateTimePicker.Models;

namespace DateTimePicker.Factories
{
    internal class SubControlFormatSpecifierFactory : IFormatSpecifierFactory
    {
        public FormatSpecifier CreateFormatSpecifier(string specifier, int index)
        {
            return specifier switch
            {
                // Hour
                "HH" => new FormatSpecifier(specifier, index, 
                    new SubControlIncreaseHourStrategy(), 
                    new SubControlDecreaseHourStrategy()),
                // Minute
                "mm" => new FormatSpecifier(specifier, index, 
                    new SubControlIncreaseMinuteStrategy(), 
                    new SubControlDecreaseMinuteStrategy()),
                // Second
                "ss" => new FormatSpecifier(specifier, index, 
                    new SubControlIncreaseSecondStrategy(), 
                    new SubControlDecreaseSecondStrategy()),
                _ => null
            };
        }
    }
}