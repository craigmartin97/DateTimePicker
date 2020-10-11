using DateTimePicker.DateStrategies;
using DateTimePicker.Models;
using System;
using System.Windows.Controls;

namespace DateTimePicker.Interfaces
{
    internal interface IObtainDateTimeContext
    {
        DateTimeContext Apply(TextBox textBox, DateTime? value, FormatSpecifier[] formatSpecifiers, out int start,
            out int length);
    }
}