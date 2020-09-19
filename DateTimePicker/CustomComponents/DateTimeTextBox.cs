﻿using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DateTimePicker.CustomComponents
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DateTimePicker.CustomComponents"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DateTimePicker.CustomComponents;assembly=DateTimePicker.CustomComponents"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:DateTimeTextBox/>
    ///
    /// </summary>
    public class DateTimeTextBox : TextBox
    {
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            e.Handled = true;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {

            }
            else if (e.Key == Key.Up)
            {

            }
            else if (e.Key == Key.Left)
            {

            }
            else if (e.Key == Key.Right)
            {
                
            }

            int start = SelectionStart;
            int length = SelectedText.Trim().Length;

            Select(start, length);
            e.Handled = true;
        }
    }
}
