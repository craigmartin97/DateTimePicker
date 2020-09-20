using DateTimePicker.DateStrategies;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
    ///     <MyNamespace:DateTimePicker/>
    ///
    /// </summary>
    public class DateTimePicker : Control
    {
        #region Dependency Properties

        public static readonly DependencyProperty SetValueProperty =
            DependencyProperty.Register("Value", typeof(DateTime?),
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(default(DateTime?))
                {
                    BindsTwoWayByDefault = true
                });

        /// <summary>
        /// Value to be inserted into the main text box
        /// </summary>
        public DateTime? Value
        {
            get => (DateTime?)GetValue(SetValueProperty);
            set => SetValue(SetValueProperty, value);
        }

        public static readonly DependencyProperty SetUpSourceProperty =
            DependencyProperty.Register("UpSource", typeof(ImageSource),
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(new BitmapImage(new Uri("pack://application:,,,/DateTimePicker;component/Images/arrowup.ico"))));

        /// <summary>
        /// Path to the image to be used for the up button content
        /// </summary>
        public ImageSource UpSource
        {
            get => (ImageSource)GetValue(SetUpSourceProperty);
            set => SetValue(SetUpSourceProperty, value);
        }

        public static readonly DependencyProperty SetDownSourceProperty =
            DependencyProperty.Register("DownSource", typeof(ImageSource),
                typeof(DateTimePicker), 
                new FrameworkPropertyMetadata(new BitmapImage(new Uri("pack://application:,,,/DateTimePicker;component/Images/arrowdn.ico"))));

        /// <summary>
        /// Path to the image to be used for the down button content
        /// </summary>
        public ImageSource DownSource
        {
            get => (ImageSource)GetValue(SetDownSourceProperty);
            set => SetValue(SetDownSourceProperty, value);
        }

        public static readonly DependencyProperty SetCalendarSourceProperty =
            DependencyProperty.Register("CalendarSource", typeof(ImageSource),
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(new BitmapImage(new Uri("pack://application:,,,/DateTimePicker;component/Images/arrowdn.ico"))));

        /// <summary>
        /// Image path to be used for the calender button content
        /// </summary>
        public ImageSource CalendarSource
        {
            get => (ImageSource)GetValue(SetDownSourceProperty);
            set => SetValue(SetDownSourceProperty, value);
        }


        public static readonly DependencyProperty SetFormatProperty =
            DependencyProperty.Register("Format", typeof(Formats?),
                typeof(DateTimePicker), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Image path to be used for the calender button content
        /// </summary>
        public Formats? Format
        {
            get => (Formats?)GetValue(SetFormatProperty);
            set => SetValue(SetFormatProperty, value);
        }

        public static readonly DependencyProperty SetTimeTextBoxVisibilityProperty =
            DependencyProperty.Register("TimeTextBoxVisibility", typeof(Visibility),
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(System.Windows.Visibility.Visible));

        /// <summary>
        /// Image path to be used for the calender button content
        /// </summary>
        public Visibility TimeTextBoxVisibility
        {
            get => (Visibility)GetValue(SetFormatProperty);
            set => SetValue(SetFormatProperty, value);
        }

        public static readonly DependencyProperty SetFormatStringProperty =
            DependencyProperty.Register("FormatString", typeof(string),
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(default));

        /// <summary>
        /// Image path to be used for the calender button content
        /// </summary>
        public string FormatString
        {
            get => (string)GetValue(SetFormatStringProperty);
            set => SetValue(SetFormatStringProperty, value);
        }
        #endregion

        #region Fields

        private RepeatButton _mainUpButton;
        private RepeatButton _mainDownButton;
        private RepeatButton _timeUpButton;
        private RepeatButton _timeDownButton;

        private TextBox _mainTextBox;
        private TextBox _timeTextBox;

        /// <summary>
        /// Increase main text box options
        /// </summary>
        private readonly IDictionary<int, IDateTimeStrategy> _incrementFullDateDictionary = new Dictionary<int, IDateTimeStrategy>
        {
            {0, new IncreaseYearStrategy()},
            {1, new IncreaseMonthStrategy()},
            {2, new IncreaseDayStrategy()},
            {3, new IncreaseHourStrategy()},
            {4, new IncreaseMinuteStrategy()},
        };

        /// <summary>
        /// Decrement main text box options
        /// </summary>
        private readonly IDictionary<int, IDateTimeStrategy> _decrementFullDateDictionary = new Dictionary<int, IDateTimeStrategy>
        {
            {0, new DecreaseYearStrategy()},
            {1, new DecreaseMonthStrategy()},
            {2, new DecreaseDayStrategy()},
            {3, new DecreaseHourStrategy()},
            {4, new DecreaseMinuteStrategy()},
        };

        /// <summary>
        /// Increment time options
        /// </summary>
        private readonly IDictionary<int, IDateTimeStrategy> _incrementTimeDictionary = new Dictionary<int, IDateTimeStrategy>
        {
            {0, new IncreaseHourStrategy()},
            {1, new IncreaseMinuteStrategy()},
        };

        /// <summary>
        /// Decrement time options
        /// </summary>
        private readonly IDictionary<int, IDateTimeStrategy> _decrementTimeDictionary = new Dictionary<int, IDateTimeStrategy>
        {
            {0, new DecreaseHourStrategy()},
            {1, new DecreaseMinuteStrategy()},
        };
        #endregion

        static DateTimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateTimePicker), new FrameworkPropertyMetadata(typeof(DateTimePicker)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _mainUpButton = Template.FindName("PART_MAIN_UP_BUTTON", this) as RepeatButton;
            _mainDownButton = Template.FindName("PART_MAIN_DOWN_BUTTON", this) as RepeatButton;

            _mainUpButton.Click += MainUpButtonOnClick;
            _mainDownButton.Click += MainDownButtonOnClick;

            _timeUpButton = Template.FindName("TIME_UP_BUTTON", this) as RepeatButton;
            _timeDownButton = Template.FindName("TIME_DOWN_BUTTON", this) as RepeatButton;

            _timeUpButton.Click += TimeUpButtonOnClick;
            _timeDownButton.Click += TimeDownButtonOnClick;

            _mainTextBox = Template.FindName("PART_MAIN_TEXT_BOX", this) as TextBox;
            _timeTextBox = Template.FindName("PART_TIME_TEXT_BOX", this) as TextBox;

            _mainTextBox.PreviewKeyDown += MainTextBoxOnPreviewKeyDown;
            _timeTextBox.PreviewKeyDown += TimeTextBoxOnPreviewKeyDown;
        }

        private void MainTextBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                Apply(_decrementFullDateDictionary, _mainTextBox, _mainTextBox.Text);
            }
            else if (e.Key == Key.Up)
            {
                Apply(_incrementFullDateDictionary, _mainTextBox, _mainTextBox.Text);
            }
            else if (e.Key == Key.Left)
            {
                FindPrevious(out int startSelectIndex, out int length);
                _mainTextBox.Select(startSelectIndex, length);
            }
            else if (e.Key == Key.Right)
            {
                FindNext(out int startSelectIndex, out int length);
                _mainTextBox.Select(startSelectIndex, length);
            }

            e.Handled = true;
        }

        private void TimeTextBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                Apply(_decrementTimeDictionary, _timeTextBox, _mainTextBox.Text);
            }
            else if (e.Key == Key.Up)
            {
                Apply(_incrementTimeDictionary, _timeTextBox, _mainTextBox.Text);
            }
            else if (e.Key == Key.Left)
            {
                FindPrevious(out int startSelectIndex, out int length);
                _mainTextBox.Select(startSelectIndex, length);
            }
            else if (e.Key == Key.Right)
            {
                FindNext(out int startSelectIndex, out int length);
                _mainTextBox.Select(startSelectIndex, length);
            }

            e.Handled = true;
        }

        #region Events

        private void MainUpButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (!(Template.FindName("PART_MAIN_TEXT_BOX", this) is TextBox textBox))
                return;

            if (NullDateTime()) // Apply value
                return;

            if (string.IsNullOrWhiteSpace(textBox.SelectedText)) // Nothing selected
                ChangeDayOnly(new IncreaseDayStrategy());
            else // Has something selected
                Apply(_incrementFullDateDictionary, textBox, textBox.Text);
        }

        private void MainDownButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (!(Template.FindName("PART_MAIN_TEXT_BOX", this) is TextBox textBox))
                return;

            if (NullDateTime()) // Apply value
                return;

            if (string.IsNullOrWhiteSpace(textBox.SelectedText)) // Nothing selected
                ChangeDayOnly(new DecreaseDayStrategy());
            else // Has something selected
                Apply(_decrementFullDateDictionary, textBox, textBox.Text);
        }

        private void TimeUpButtonOnClick(object sender, RoutedEventArgs e)
        {
            TextBox mainTextBox = Template.FindName("PART_MAIN_TEXT_BOX", this) as TextBox;
            TextBox textBox = Template.FindName("PART_TIME_TEXT_BOX", this) as TextBox;

            if (mainTextBox == null || textBox == null)
                return;

            if(!NullDateTime())
                Apply(_incrementTimeDictionary, textBox, mainTextBox.Text);
        }

        private void TimeDownButtonOnClick(object sender, RoutedEventArgs e)
        {
            TextBox mainTextBox = Template.FindName("PART_MAIN_TEXT_BOX", this) as TextBox;
            TextBox textBox = Template.FindName("PART_TIME_TEXT_BOX", this) as TextBox;

            if (mainTextBox == null || textBox == null)
                return;

            if(!NullDateTime())
                Apply(_decrementTimeDictionary, textBox, mainTextBox.Text);
        }
        #endregion

        private void Apply(IDictionary<int, IDateTimeStrategy> dictionary, TextBox textBox, string parseText)
        {
            // No value or no text selected then prevent increment
            if (!Value.HasValue || string.IsNullOrWhiteSpace(textBox.SelectedText))
                return;

            string text = textBox.SelectedText.TrimEnd();
            if (!Regex.IsMatch(text, "^[0-9]*$"))
                return;

            int start = textBox.SelectionStart;
            int length = text.Length;

            int numberOfPreviousNumbers = 0;
            for (int i = 0; i < textBox.Text.Length; i++)
            {
                if (i == start) // Found start index
                    break;

                if (char.IsDigit(textBox.Text[i]))
                    continue;

                numberOfPreviousNumbers++; // Must have encountered as seperator. Inc
            }

            DateTime dateTime = DateTime.Parse(parseText);

            IDateTimeStrategy strategy = dictionary[numberOfPreviousNumbers];
            DateTimeContext context = new DateTimeContext(strategy);

            DateTime newDateTime = context.Execute(dateTime);
            Value = newDateTime;

            textBox.Select(start, length);
        }

        private bool NullDateTime()
        {
            if (Value != null) 
                return false; // Value was not null

            Value = DateTime.Now; 
            return true; // Value was null
        }

        private void ChangeDayOnly(IDateTimeStrategy strategy)
        {
            DateTimeContext context = new DateTimeContext(strategy);
            Value = context.Execute((DateTime)Value);
        }

        private void FindNext(out int startSelectIndex, out int length)
        {
            startSelectIndex = 0;
            length = 0;

            int start = _mainTextBox.SelectionStart + _mainTextBox.SelectionLength;

            for (int i = start; i < _mainTextBox.Text.Length; i++)
            {
                char c = _mainTextBox.Text[i];

                if (!char.IsDigit(c)) // Must be a seperator
                    continue;

                startSelectIndex = i;
                for (int j = startSelectIndex; j <= _mainTextBox.Text.Length; j++)
                {
                    if (j == _mainTextBox.Text.Length)
                    {
                        return;
                    }

                    char nextNumber = _mainTextBox.Text[j];
                    if (!char.IsDigit(nextNumber))
                        return;

                    length++;
                }
            }
        }

        private void FindPrevious(out int startSelectIndex, out int length)
        {
            startSelectIndex = 0;
            length = 0;

            for (int i = _mainTextBox.SelectionStart - 1; i >= 0; i--)
            {
                char c = _mainTextBox.Text[i];

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

                    char nextNumber = _mainTextBox.Text[j];
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
