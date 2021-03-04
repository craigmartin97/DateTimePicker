using System;
using DateTimePicker.DateStrategies;
using DateTimePicker.DateStrategies.ManualUpdates;
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
                    new DecreaseDayStrategy(),
                    new ChangeDay()),
                // Month
                "MM" =>
                new FormatSpecifier(specifier, index,
                    new IncreaseMonthStrategy(),
                    new DecreaseMonthStrategy(),
                    new ChangeMonth()),
                // Year
                "yyyy" =>
                new FormatSpecifier(specifier, index,
                    new IncreaseYearStrategy(),
                    new DecreaseYearStrategy(),
                    new ChangeYear()),
                // Hour
                "HH" =>
                new FormatSpecifier(specifier, index,
                    new IncreaseHourStrategy(),
                    new DecreaseHourStrategy(),
                    new ChangeHour()),
                // Minute
                "mm" =>
                new FormatSpecifier(specifier, index,
                    new IncreaseMinuteStrategy(),
                    new DecreaseMinuteStrategy(),
                    new ChangeMinute()),
                // Second
                "ss" =>
                new FormatSpecifier(specifier, index,
                    new IncreaseSecondStrategy(),
                    new DecreaseSecondStrategy(),
                    new ChangeSecond()),
                _ => throw new ArgumentOutOfRangeException("FormatString", "The FormatSpecifier could not be found, for the FormatString provided")
            };
        }
    }
}