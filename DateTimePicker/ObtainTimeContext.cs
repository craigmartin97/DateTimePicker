using DateTimePicker.IntegerTimeStrategies;
using DateTimePicker.Interfaces;
using DateTimePicker.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace DateTimePicker
{
    internal class ObtainTimeContext : IObtainTimeContext
    {
        public TimeContext Apply(TextBox textBox, TimeFormatSpecifier[] formatSpecifiers, out int start, out int length)
        {
            // No value or no text selected then prevent increment
            if (string.IsNullOrWhiteSpace(textBox.SelectedText))
            {
                start = -1;
                length = -1;
                return null;
            }

            string text = textBox.SelectedText.TrimEnd();
            if (!Regex.IsMatch(text, "^[0-9]*$"))
            {
                start = -1;
                length = -1;
                return null;
            }

            start = textBox.SelectionStart;
            length = text.Length;

            int numberOfPreviousNumbers = 0;
            for (int i = 0; i < textBox.Text.Length; i++)
            {
                if (i == start) // Found start index
                    break;

                if (char.IsDigit(textBox.Text[i]))
                    continue;

                numberOfPreviousNumbers++; // Must have encountered as seperator. Inc
            }

            TimeFormatSpecifier formatSpecifier = formatSpecifiers
                .FirstOrDefault(x => x.Index == numberOfPreviousNumbers);
            TimeContext dateTimeContext = new TimeContext(formatSpecifier);
            return dateTimeContext;
        }
    }
}