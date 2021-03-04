using DateTimePicker.DateStrategies;
using DateTimePicker.DateStrategies.ManualUpdates;
using DateTimePicker.Interfaces;
using DateTimePicker.Models;
using System;

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
                    new SubControlDecreaseHourStrategy(),
                    new ChangeHour()),
                // Minute
                "mm" => new FormatSpecifier(specifier, index,
                    new SubControlIncreaseMinuteStrategy(),
                    new SubControlDecreaseMinuteStrategy(),
                    new ChangeMinute()),
                // Second
                "ss" => new FormatSpecifier(specifier, index,
                    new SubControlIncreaseSecondStrategy(),
                    new SubControlDecreaseSecondStrategy(),
                    new ChangeSecond()),
                _ => throw new ArgumentOutOfRangeException("FormatSpecifier", "Could not a format specifier")
            };
        }
    }
}