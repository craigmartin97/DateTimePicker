using System.Windows.Controls;

namespace DateTimePicker.Interfaces
{
    internal interface INumberFinder
    {
        void FindNext(TextBox textBox, out int startSelectIndex, out int length);
        void FindPrevious(TextBox textBox, out int startSelectIndex, out int length);
    }
}