using DateTimePicker.Models;

namespace DateTimePicker.Interfaces
{
    internal interface ITimeFormatSpecifierFactory
    {
        TimeFormatSpecifier CreateFormatSpecifier(string specifier, int index);
    }
}