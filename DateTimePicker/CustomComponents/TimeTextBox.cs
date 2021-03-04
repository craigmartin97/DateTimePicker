using DateTimePicker.DateStrategies;
using DateTimePicker.Exceptions;
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
    ///     <MyNamespace:TimeTextBox/>
    ///
    /// </summary>
    public class TimeTextBox : Control
    {
        #region Dependency Properties

        /// <summary>
        /// Dependency property for the DateTime Value which is data bound
        /// </summary>
        public static readonly DependencyProperty SetValueProperty =
            DependencyProperty.Register("Value", typeof(DateTime?),
                typeof(TimeTextBox),
                new FrameworkPropertyMetadata(default(DateTime?))
                {
                    BindsTwoWayByDefault = true
                });

        /// <summary>
        /// DateTime Value property
        /// </summary>
        public DateTime? Value
        {
            get => (DateTime?)GetValue(SetValueProperty);
            set => SetValue(SetValueProperty, value);
        }

        /// <summary>
        /// Dependency property for The image source for the up button's.
        /// Default to the image Images/arrowup.ico
        /// </summary>
        public static readonly DependencyProperty SetUpSourceProperty =
            DependencyProperty.Register("UpSource", typeof(ImageSource),
                typeof(TimeTextBox),
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
                typeof(TimeTextBox),
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
                typeof(TimeTextBox),
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
                typeof(TimeTextBox),
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
                typeof(TimeTextBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, SelectedTimeChanged));

        private static void SelectedTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Is the calling function a DateTimePicker
            if (!(d is TimeTextBox dateTimePicker))
                return;

            if (!(e.NewValue is Time selectedTime))
                return;

            dateTimePicker.Value ??= DateTime.Now;

            DateTime dt = (DateTime)dateTimePicker.Value;

            DateTime dateTime = new DateTime(dt.Year, dt.Month, dt.Day, selectedTime.Hour, selectedTime.Minute, selectedTime.Second);
            dateTimePicker.Value = dateTime;
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
                typeof(TimeTextBox),
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
        private readonly IObtainDateTimeContext _obtainContext = new ObtainDateTimeContext();

        /// <summary>
        /// The sub controls format specifier options
        /// </summary>
        private FormatSpecifier[] _timeFormatSpecifiers;

        /// <summary>
        /// Find the next or previous number to jump to on left right key down movements
        /// </summary>
        private readonly INumberFinder _numberFinder = new NumberFinder();

        private bool _previouslyEnteredNumber = false;
        #endregion

        #region Constructors

        static TimeTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeTextBox), new FrameworkPropertyMetadata(typeof(TimeTextBox)));
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

            // The user has specified a custom string format calculate the FormatSpecifiers to use
            IFormatStringFormatter formatStringFormatter = new FormatStringFormatter();

            // The user has specified some time format specifiers.
            IEnumerable<FormatSpecifier> timeFormatSpecifiers =
                formatStringFormatter.CalculateTimeFormatSpecifiers(FormatString);
            _timeFormatSpecifiers = timeFormatSpecifiers.ToArray();

            // Toggle the visibility and pre-load options of the Time options combo box
            if (Times == null && ShowTimesDropDown == Visibility.Visible && !string.IsNullOrWhiteSpace(FormatString))
            {
                // The user hasn't set the times options but it is visible. Set with pre-set options
                Times = PreLoadTimeOptions.GetPreLoadTimes(FormatString);
                SelectedTime = null;
            }
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

            if (Value == null) // Apply value
            {
                Value = DateTime.Now;
                return;
            }

            if (string.IsNullOrWhiteSpace(_mainTextBox.SelectedText)) // Nothing selected
            {
                if (frameworkElement.Name.Equals("PART_MAIN_UP_BUTTON"))
                {
                    Value = new IncreaseMinuteStrategy().UpdateDateTime((DateTime)Value);
                }
                else if (frameworkElement.Name.Equals("PART_MAIN_DOWN_BUTTON"))
                {
                    Value = new DecreaseMinuteStrategy().UpdateDateTime((DateTime)Value);
                }
            }
            else // Has something selected
            {
                bool isValidDateTime = DateTime.TryParse(_mainTextBox.Text, out DateTime dateTime);
                if (!isValidDateTime)
                    return;

                DateTimeContext context = _obtainContext.Apply(_mainTextBox, dateTime, _timeFormatSpecifiers, out int start, out int length);

                if (frameworkElement.Name.Equals("PART_MAIN_UP_BUTTON"))
                {
                    Value = context.ExecuteIncrease(dateTime);
                }
                else if (frameworkElement.Name.Equals("PART_MAIN_DOWN_BUTTON"))
                {
                    Value = context.ExecuteDecrease(dateTime);
                }
                
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

            DateTime? dateTime = GetDateTime.GetDateTimeFromString(_mainTextBox.Text);
            if (dateTime == null)
            {
                Value = DateTime.Now;
                return;
            }

            if (e.Key == Key.Down || e.Key == Key.Up) // Decrease
            {
                DateTimeContext context = _obtainContext.Apply(_mainTextBox, Value, _timeFormatSpecifiers, out int start, out int length);
                if (context == null || start < 0 || length < 0)
                    return;

                Value = e.Key switch
                {
                    Key.Down => context.ExecuteDecrease((DateTime) dateTime),
                    Key.Up => context.ExecuteIncrease((DateTime) dateTime),
                    _ => Value
                };

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
                if(string.IsNullOrWhiteSpace(_mainTextBox.SelectedText))
                    return;

                DateTimeContext context = _obtainContext.Apply(_mainTextBox, Value, _timeFormatSpecifiers, out int start, out int length);
                if (context == null || start < 0 || length < 0)
                    return;

                char i = e.Key.ToString()[1];
                Value = context.UpdateDateTime((DateTime)dateTime, i, _previouslyEnteredNumber);
                _mainTextBox.Select(start, length);

                _previouslyEnteredNumber = !_previouslyEnteredNumber;
            }

            e.Handled = true;
        }
        #endregion
    }
}
