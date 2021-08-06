using DateTimePicker.Exceptions;
using DateTimePicker.IntegerTimeStrategies;
using DateTimePicker.Interfaces;
using DateTimePicker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    ///     <MyNamespace:TwentyFourHourTimeTextBox/>
    ///
    /// </summary>
    public class TwentyFourHourTimeTextBox : Control
    {
        #region Dependency Properties

        /// <summary>
        /// Dependency property for the hour dependency property
        /// </summary>
        public static readonly DependencyProperty SetHourProperty =
            DependencyProperty.Register("Hour", typeof(int?),
                typeof(TwentyFourHourTimeTextBox),
                new FrameworkPropertyMetadata(default(int?))
                {
                    BindsTwoWayByDefault = true
                });

        /// <summary>
        /// Hour value
        /// </summary>
        public int? Hour
        {
            get => (int?)GetValue(SetHourProperty);
            set => SetValue(SetHourProperty, value);
        }

        /// <summary>
        /// Dependency property for the hour dependency property
        /// </summary>
        public static readonly DependencyProperty SetMinuteProperty =
            DependencyProperty.Register("Minute", typeof(int?),
                typeof(TwentyFourHourTimeTextBox),
                new FrameworkPropertyMetadata(default(int?))
                {
                    BindsTwoWayByDefault = true
                });

        /// <summary>
        /// Hour value
        /// </summary>
        public int? Minute
        {
            get => (int?)GetValue(SetMinuteProperty);
            set => SetValue(SetMinuteProperty, value);
        }

        /// <summary>
        /// Dependency property for the hour dependency property
        /// </summary>
        public static readonly DependencyProperty SetSecondProperty =
            DependencyProperty.Register("Second", typeof(int?),
                typeof(TwentyFourHourTimeTextBox),
                new FrameworkPropertyMetadata(default(int?))
                {
                    BindsTwoWayByDefault = true
                });

        /// <summary>
        /// Hour value
        /// </summary>
        public int? Second
        {
            get => (int?)GetValue(SetSecondProperty);
            set => SetValue(SetSecondProperty, value);
        }

        /// <summary>
        /// Dependency property for the hour dependency property
        /// </summary>
        public static readonly DependencyProperty SetDisplayValueProperty =
            DependencyProperty.Register("DisplayValue", typeof(string),
                typeof(TwentyFourHourTimeTextBox),
                new FrameworkPropertyMetadata(default(string))
                {
                    BindsTwoWayByDefault = true
                });

        /// <summary>
        /// Hour value
        /// </summary>
        public string DisplayValue
        {
            get => (string)GetValue(SetDisplayValueProperty);
            set => SetValue(SetDisplayValueProperty, value);
        }

        /// <summary>
        /// Dependency property for The image source for the up button's.
        /// Default to the image Images/arrowup.ico
        /// </summary>
        public static readonly DependencyProperty SetUpSourceProperty =
            DependencyProperty.Register("UpSource", typeof(ImageSource),
                typeof(TwentyFourHourTimeTextBox),
                new FrameworkPropertyMetadata(new BitmapImage(new Uri("pack://application:,,,/DateTimePicker;component/Images/arrowup.ico"))));

        /// <summary>
        /// Up property for the Up buttons image
        /// </summary>
        public ImageSource UpSource
        {
            get => (ImageSource)GetValue(SetUpSourceProperty);
            set => SetValue(SetUpSourceProperty, value);
        }

        /// <summary>
        /// Dependency property for the image source for the down button's
        /// Default to the image Images/arrowdn.ico
        /// </summary>
        public static readonly DependencyProperty SetDownSourceProperty =
            DependencyProperty.Register("DownSource", typeof(ImageSource),
                typeof(TwentyFourHourTimeTextBox),
                new FrameworkPropertyMetadata(new BitmapImage(new Uri("pack://application:,,,/DateTimePicker;component/Images/arrowdn.ico"))));

        /// <summary>
        /// Path to the image to be used for the down button content
        /// </summary>
        public ImageSource DownSource
        {
            get => (ImageSource)GetValue(SetDownSourceProperty);
            set => SetValue(SetDownSourceProperty, value);
        }

        /// <summary>
        /// Dependency property for the custom format string
        /// </summary>
        public static readonly DependencyProperty SetFormatStringProperty =
            DependencyProperty.Register("FormatString", typeof(string),
                typeof(TwentyFourHourTimeTextBox),
                new FrameworkPropertyMetadata(default));

        /// <summary>
        /// Custom string format property
        /// </summary>
        public string FormatString
        {
            get => (string)GetValue(SetFormatStringProperty);
            set => SetValue(SetFormatStringProperty, value);
        }

        #region Times Drop Down DP Properties

        /// <summary>
        /// Dependency property for setting the collection of times in the drop down
        /// </summary>
        public static readonly DependencyProperty SetTimesProperty =
            DependencyProperty.Register("Times",
                typeof(IEnumerable<Time>),
                typeof(TwentyFourHourTimeTextBox),
                new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Times drop down options
        /// </summary>
        public IEnumerable<Time> Times
        {
            get => (IEnumerable<Time>)GetValue(SetTimesProperty);
            set => SetValue(SetTimesProperty, value);
        }

        /// <summary>
        /// Dependency property for the selected time property
        /// </summary>
        public static readonly DependencyProperty SetSelectedTimeProperty =
            DependencyProperty.Register("SelectedTime",
                typeof(object),
                typeof(TwentyFourHourTimeTextBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, SelectedTimeChanged));

        private static void SelectedTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Is the calling function a DateTimePicker
            if (!(d is TwentyFourHourTimeTextBox dateTimePicker))
                return;

            if (!(e.NewValue is Time selectedTime))
                return;

            dateTimePicker.Hour = selectedTime.Hour;
            dateTimePicker.Minute = selectedTime.Minute;
            dateTimePicker.Second = selectedTime.Second;
        }

        /// <summary>
        /// Selected time property
        /// </summary>
        public object SelectedTime
        {
            get => GetValue(SetSelectedTimeProperty);
            set => SetValue(SetSelectedTimeProperty, value);
        }

        /// <summary>
        /// Dependency property to allow the user to set preset selection options for the available times
        /// </summary>
        public static readonly DependencyProperty SetShowTimesDropDownProperty =
            DependencyProperty.Register("ShowTimesDropDown",
                typeof(Visibility),
                typeof(TwentyFourHourTimeTextBox),
                new FrameworkPropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Property to toggle the visibility of the times drop down selections
        /// </summary>
        public Visibility ShowTimesDropDown
        {
            get => (Visibility)GetValue(SetShowTimesDropDownProperty);
            set => SetValue(SetShowTimesDropDownProperty, value);
        }
        #endregion
        #endregion

        #region Fields

        /// <summary>
        /// The main controls up button to increment the DateTime
        /// </summary>
        private RepeatButton _mainUpButton;

        /// <summary>
        /// The main controls down button to decrement the DateTime
        /// </summary>
        private RepeatButton _mainDownButton;

        /// <summary>
        /// The main controls text box
        /// </summary>
        private TextBox _mainTextBox;

        /// <summary>
        /// DateTimeContext to get the current DateTime and correct strategy to apply
        /// based on the StringFormat and the position of the selected text
        /// </summary>
        private readonly IObtainTimeContext _obtainContext = new ObtainTimeContext();

        /// <summary>
        /// The sub controls format specifier options
        /// </summary>
        private TimeFormatSpecifier[] _timeFormatSpecifiers;

        /// <summary>
        /// Find the next or previous number to jump to on left right key down movements
        /// </summary>
        private readonly INumberFinder _numberFinder = new NumberFinder();

        private bool _previouslyEnteredNumber = false;
        #endregion

        #region Constructors

        static TwentyFourHourTimeTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TwentyFourHourTimeTextBox), new FrameworkPropertyMetadata(typeof(TwentyFourHourTimeTextBox)));
        }
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Set event handlers for buttons and text boxes on view.
            _mainUpButton = Template.FindName("PART_MAIN_UP_BUTTON", this) as RepeatButton;
            _mainDownButton = Template.FindName("PART_MAIN_DOWN_BUTTON", this) as RepeatButton;

            if (_mainUpButton == null || _mainDownButton == null)
                throw new NullResourceException("Unable to find the main spinner buttons");

            _mainUpButton.Click += MainControlButtonOnClick;
            _mainDownButton.Click += MainControlButtonOnClick;

            _mainTextBox = Template.FindName("PART_MAIN_TEXT_BOX", this) as TextBox;

            if (_mainTextBox == null)
                throw new NullResourceException("Unable to find the text boxes");

            _mainTextBox.PreviewKeyDown += MainTextBoxOnPreviewKeyDown;
            _mainTextBox.PreviewMouseLeftButtonUp += MainTextBoxOnPreviewMouseLeftButtonDown;

            // The user has specified a custom string format calculate the FormatSpecifiers to use
            if (string.IsNullOrWhiteSpace(FormatString))
            {
                FormatString = "HH:mm";
            }

            // The user has specified some time format specifiers.
            ITimeFormatStringFormatter formatter = new TimeFormatStringFormatter();
            IEnumerable<TimeFormatSpecifier> specifiers = formatter.CalculateTimeFormatSpecifiers(FormatString);
            _timeFormatSpecifiers = specifiers.ToArray();

            // Toggle the visibility and pre-load options of the Time options combo box
            if (Times == null && ShowTimesDropDown == Visibility.Visible && !string.IsNullOrWhiteSpace(FormatString))
            {
                List<Time> times = new List<Time>();
                // The user hasn't set the times options but it is visible. Set with pre-set options
                IEnumerable<Time> allTimes = PreLoadTimeOptions.GetPreLoadTimes(FormatString);

                times.AddRange(allTimes);
                // Represents the start of the next day
                times.Add(new Time(24, 0, FormatString, 0)); // Special date time for this control
                Times = new List<Time>(times);

                times.Clear();
                SelectedTime = null;
            }

            _mainTextBox.TextChanged += (sender, args) =>
            {
                TextBox textBox = (TextBox) sender;
                DisplayValue = textBox.Text;
            };
        }

        private static void MainTextBoxOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                return;

            if (!(sender is TextBox textBox))
                return;

            int caretIndex = textBox.CaretIndex;
            int start = 0;
            int end = 0;

            if (caretIndex == 0) // At the start
            {
                start = 0;
                for (int i = 0; i < textBox.Text.Length; i++)
                {
                    if(char.IsNumber(textBox.Text[i]))
                        continue;
                    end = i;
                    break;
                }
            }
            else if (textBox.Text.Length == caretIndex) // At the end
            {
                end = textBox.Text.Length;
                for (int i = textBox.Text.Length - 1; i >= 0; i--)
                {
                    if (char.IsNumber(textBox.Text[i]))
                        continue;
                    start = i+1;
                    break;
                }
            }
            else // Somewhere in between
            {
                // Find the end
                for (int i = caretIndex; i < textBox.Text.Length; i++)
                {
                    if (char.IsNumber(textBox.Text[i]))
                        continue;
                    end = i;
                    break;
                }

                if (end == 0)
                    end = caretIndex;

                // Find the start
                for (int i = caretIndex; i >= 0; i--)
                {
                    if (char.IsNumber(textBox.Text[i]))
                        continue;
                    start = i+1;
                    break;
                }
            }

            textBox.Select(start, end);
        }

        #region Button Events

        /// <summary>
        /// Main control up button event handler/
        /// If has text selected, increment the selected text by one.
        /// Else increment the day by one
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Event arguments</param>
        private void MainControlButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is FrameworkElement frameworkElement))
                return;

            DateTime now = DateTime.Now;

            Hour ??= now.Hour;
            Minute ??= 0;
            Second ??= 0;

            if (string.IsNullOrWhiteSpace(_mainTextBox.SelectedText)) // Nothing selected. Change hour only
            {
                if (frameworkElement.Name.Equals("PART_MAIN_UP_BUTTON"))
                {
                    if (Hour == 24) // At the end, so wrap around back to the start
                    {
                        Hour = 0;
                        Minute = 0;
                        Second = 0;
                    }
                    else
                    {
                        Hour++;
                    }
                }
                else if (frameworkElement.Name.Equals("PART_MAIN_DOWN_BUTTON"))
                {
                    if (Hour == 0) // At the start, so wrap around back to the end
                    {
                        Hour = 24;
                        Minute = 0;
                        Second = 0;
                    }
                    else
                    {
                        Hour--;
                    }
                }
            }
            else // The user has something selected
            {
                int hour = (int)Hour;
                int minute = (int)Minute;
                int second = (int)Second;

                int updatedHour = hour;
                int updatedMinute = minute;
                int updatedSecond = second;

                TimeContext context = _obtainContext.Apply(_mainTextBox, _timeFormatSpecifiers, out int start, out int length);

                if (frameworkElement.Name.Equals("PART_MAIN_UP_BUTTON"))
                {
                    context.ExecuteIncrease(hour, minute, second, out updatedHour, out updatedMinute, out updatedSecond);
                }
                else if (frameworkElement.Name.Equals("PART_MAIN_DOWN_BUTTON"))
                {
                    context.ExecuteDecrease(hour, minute, second, out updatedHour, out updatedMinute, out updatedSecond);
                }

                Hour = updatedHour;
                Minute = updatedMinute;
                Second = updatedSecond;

                // Select the same portion in the text box
                _mainTextBox.Select(start, length);
            }
        }
        #endregion

        #region Key Events

        /// <summary>
        /// Event handler for when the user presses the arrow keys inside main controls text box
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Event arguments</param>
        private void MainTextBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_timeFormatSpecifiers == null)
                return;

            DateTime now = DateTime.Now;
            Hour ??= now.Hour;
            Minute ??= 0;
            Second ??= 0;

            int hour = (int)Hour;
            int minute = (int)Minute;
            int second = (int)Second;

            int updatedHour;
            int updatedMinute;
            int updatedSecond;

            if (e.Key == Key.Down) // Increase or decrease the selected values
            {
                TimeContext context = _obtainContext.Apply(_mainTextBox, _timeFormatSpecifiers, out int start, out int length);
                if (context == null || start < 0 || length < 0)
                    return;

                context.ExecuteDecrease(hour, minute, second, out updatedHour, out updatedMinute, out updatedSecond);

                Hour = updatedHour;
                Minute = updatedMinute;
                Second = updatedSecond;

                _mainTextBox.Select(start, length);
                _previouslyEnteredNumber = false;
            }
            else if (e.Key == Key.Up)
            {
                TimeContext context = _obtainContext.Apply(_mainTextBox, _timeFormatSpecifiers, out int start, out int length);
                if (context == null || start < 0 || length < 0)
                    return;

                context.ExecuteIncrease(hour, minute, second, out updatedHour, out updatedMinute, out updatedSecond);

                Hour = updatedHour;
                Minute = updatedMinute;
                Second = updatedSecond;

                _mainTextBox.Select(start, length);
                _previouslyEnteredNumber = false;
            }
            else if (e.Key == Key.Left) // Find the next left number
            {
                _numberFinder.FindPrevious(_mainTextBox, out int startSelectIndex, out int length);
                _mainTextBox.Select(startSelectIndex, length);
                _previouslyEnteredNumber = false;
            }
            else if (e.Key == Key.Right) // Find the next right number
            {
                _numberFinder.FindNext(_mainTextBox, out int startSelectIndex, out int length);
                _mainTextBox.Select(startSelectIndex, length);
                _previouslyEnteredNumber = false;
            }
            // The user pressed a number
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                if (string.IsNullOrWhiteSpace(_mainTextBox.SelectedText))
                    return;

                TimeContext context = _obtainContext.Apply(_mainTextBox, _timeFormatSpecifiers, out int start, out int length);
                if (context == null || start < 0 || length < 0)
                    return;

                char i = e.Key.ToString()[1];
                context.UpdateTime(hour, minute, second, i, _previouslyEnteredNumber, out updatedHour,
                    out updatedMinute, out updatedSecond);

                Hour = updatedHour;
                Minute = updatedMinute;
                Second = updatedSecond;

                _mainTextBox.Select(start, length);
                _previouslyEnteredNumber = !_previouslyEnteredNumber;
            }
            //else if (e.Key == Key.Back) // The user has pressed the backspace key
            //{
            //    if(string.IsNullOrWhiteSpace(_mainTextBox.Text))
            //        return;
            //    int caretIndex = _mainTextBox.CaretIndex;
            //    string s = _mainTextBox.Text.Remove(caretIndex - 1, 1);
            //    _mainTextBox.Text = s;
            //    _mainTextBox.CaretIndex = caretIndex - 1;
            //}
            else if (e.Key == Key.End) // The user has pressed the end key
            {
                _mainTextBox.Select(0,0);; // Deselect all test
                _mainTextBox.CaretIndex = _mainTextBox.Text.Length; // Put cursor at end
            }

            e.Handled = true;
        }
        #endregion
    }
}
