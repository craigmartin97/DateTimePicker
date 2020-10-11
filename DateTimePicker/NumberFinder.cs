using System.Windows.Controls;
using DateTimePicker.Interfaces;

namespace DateTimePicker
{
    /// <summary>
    /// Find the next or previous number in the text box
    /// </summary>
    internal class NumberFinder : INumberFinder
    {
        public void FindNext(TextBox textBox, out int startSelectIndex, out int length)
        {
            startSelectIndex = 0;
            length = 0;

            int start = textBox.SelectionStart + textBox.SelectionLength;

            for (int i = start; i < textBox.Text.Length; i++)
            {
                char c = textBox.Text[i];

                if (!char.IsDigit(c)) // Must be a seperator
                    continue;

                startSelectIndex = i;
                for (int j = startSelectIndex; j <= textBox.Text.Length; j++)
                {
                    if (j == textBox.Text.Length)
                    {
                        return;
                    }

                    char nextNumber = textBox.Text[j];
                    if (!char.IsDigit(nextNumber))
                        return;

                    length++;
                }
            }
        }

        public void FindPrevious(TextBox textBox, out int startSelectIndex, out int length)
        {
            startSelectIndex = 0;
            length = 0;

            for (int i = textBox.SelectionStart - 1; i >= 0; i--)
            {
                char c = textBox.Text[i];

                if (!char.IsDigit(c)) // Must be a seperator
                    continue;

                startSelectIndex = i;
                for (int j = startSelectIndex; j >= 0; j--)
                {
                    if (j == 0)
                    {
                        startSelectIndex = 0;
                        length++;
                        return;
                    }

                    char nextNumber = textBox.Text[j];
                    if (!char.IsDigit(nextNumber))
                    {
                        startSelectIndex -= 1;
                        return;
                    }

                    length++;
                }
            }
        }
    }
}