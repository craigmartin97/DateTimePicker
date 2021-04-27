using DateTimePicker.IntegerTimeStrategies;
using DateTimePicker.Models;
using System.Windows.Controls;

namespace DateTimePicker.Interfaces
{
    internal interface IObtainTimeContext
    {
        TimeContext Apply(TextBox textBox, TimeFormatSpecifier[] formatSpecifiers, out int start, out int length);
    }
}