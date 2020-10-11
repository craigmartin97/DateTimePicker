using DateTimePicker.Models;

namespace DateTimePicker.Interfaces
{
    internal interface IFormatSpecifierFactory
    {
        FormatSpecifier CreateFormatSpecifier(string specifier, int index);
    }
}