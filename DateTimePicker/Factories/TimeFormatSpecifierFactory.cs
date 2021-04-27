using System;
using DateTimePicker.DateStrategies.ManualUpdates;
using DateTimePicker.IntegerTimeStrategies;
using DateTimePicker.Interfaces;
using DateTimePicker.Models;

namespace DateTimePicker.Factories
{
    internal class TimeFormatSpecifierFactory : ITimeFormatSpecifierFactory
    {
        public TimeFormatSpecifier CreateFormatSpecifier(string specifier, int index)
        {
            return specifier switch
            {
                // Hour
                "HH" => new TimeFormatSpecifier(specifier, 
                    index,
                    new IncreaseHour(),
                    new DecreaseHour(),
                    new ChangeHour()),
                // Minute
                "mm" => new TimeFormatSpecifier(specifier, 
                    index,
                    new IncreaseMinute(),
                    new DecreaseMinute(),
                    new ChangeMinute()),
                // Second
                "ss" => new TimeFormatSpecifier(specifier, index,
                    new IncreaseSecond(),
                    new DecreaseSecond(),
                    new ChangeSecond()),
                _ => throw new ArgumentOutOfRangeException("FormatSpecifier", "Could not a format specifier")
            };
        }
    }
}