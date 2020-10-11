using DateTimePicker.DateStrategies;
using DateTimePicker.Interfaces;
using DateTimePicker.Models;

namespace DateTimePicker.Factories
{
    internal class MainControlFormatSpecifierFactory : IFormatSpecifierFactory
    {
        public FormatSpecifier CreateFormatSpecifier(string specifier, int index)
        {
            return specifier switch
            {
                // Day
                "dd" =>
                new FormatSpecifier(specifier, index,
                    new IncreaseDayStrategy(),
                    new DecreaseDayStrategy()),
                // Month
                "MM" =>
                new FormatSpecifier(specifier, index,
                    new IncreaseMonthStrategy(),
                    new DecreaseMonthStrategy()),
                // Year
                "yyyy" =>
                new FormatSpecifier(specifier, index,
                    new IncreaseYearStrategy(),
                    new DecreaseYearStrategy()),
                // Hour
                "HH" =>
                new FormatSpecifier(specifier, index,
                    new IncreaseHourStrategy(),
                    new DecreaseHourStrategy()),
                // Minute
                "mm" =>
                new FormatSpecifier(specifier, index,
                    new IncreaseMinuteStrategy(),
                    new DecreaseMinuteStrategy()),
                // Second
                "ss" =>
                new FormatSpecifier(specifier, index,
                    new IncreaseSecondStrategy(),
                    new DecreaseSecondStrategy()),
                _ => null
            };
        }
    }
}