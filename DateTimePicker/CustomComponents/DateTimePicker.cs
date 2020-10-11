using DateTimePicker.DateStrategies;
using DateTimePicker.Exceptions;
using DateTimePicker.Interfaces;
using DateTimePicker.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    ///     <MyNamespace:DateTimePicker/>
    ///
    /// </summary>
    public class DateTimePicker : Control
    {
        #region Dependency Properties

        /// <summary>
        /// Dependency property for the DateTime Value which is data bound
        /// </summary>
        public static readonly DependencyProperty SetValueProperty =
            DependencyProperty.Register("Value", typeof(DateTime?),
                typeof(DateTimePicker),
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
                typeof(DateTimePicker),
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

        /// <summary>
        /// Dependency property for the calendars drop down image.
        /// Default to the image Images/arrowdn.ico
        /// </summary>
        public static readonly DependencyProperty SetCalendarSourceProperty =
            DependencyProperty.Register("CalendarSource", typeof(ImageSource),
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(new BitmapImage(new Uri("pack://application:,,,/DateTimePicker;component/Images/arrowdn.ico"))));

        /// <summary>
        /// Image path to be used for the calender button content
        /// </summary>
        public ImageSource CalendarSource
        {
            get => (ImageSource)GetValue(SetCalendarSourceProperty);
            set => SetValue(SetCalendarSourceProperty, value);
        }

        /// <summary>
        /// Dependency property to allow users to use a preset DateTime Format
        /// </summary>
        public static readonly DependencyProperty SetFormatProperty =
            DependencyProperty.Register("Format", typeof(Formats?),
                typeof(DateTimePicker), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Preset image format property
        /// </summary>
        public Formats? Format
        {
            get => (Formats?)GetValue(SetFormatProperty);
            set => SetValue(SetFormatProperty, value);
        }

        /// <summary>
        /// Allow users to show or hide the Time Text Box visibility
        /// </summary>
        public static readonly DependencyProperty SetTimeTextBoxVisibilityProperty =
            DependencyProperty.Register("TimeTextBoxVisibility", typeof(Visibility),
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Visibility of the time text box inside the popup
        /// </summary>
        public Visibility TimeTextBoxVisibility
        {
            get => (Visibility)GetValue(SetTimeTextBoxVisibilityProperty);
            set => SetValue(SetTimeTextBoxVisibilityProperty, value);
        }

        /// <summary>
        /// Dependency property for the custom format string
        /// </summary>
        public static readonly DependencyProperty SetFormatStringProperty =
            DependencyProperty.Register("FormatString", typeof(string),
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(default));

        /// <summary>
        /// Custom string format property
        /// </summary>
        public string FormatString
        {
            get => (string)GetValue(SetFormatStringProperty);
            set => SetValue(SetFormatStringProperty, value);
        }

        /// <summary>
        /// Custom string format property for the time text box
        /// Calculated from the FormatString provided by the user. Otherwise null
        /// </summary>
        public string TimeFormatString { get; set; }

        #region Times Drop Down DP Properties

        /// <summary>
        /// Dependency property for setting the collection of times in the drop down
        /// </summary>
        public static readonly DependencyProperty SetTimesProperty =
            DependencyProperty.Register("Times",
                typeof(IEnumerable),
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Times drop down options
        /// </summary>
        public IEnumerable Times
        {
            get => (IEnumerable)GetValue(SetTimesProperty);
            set => SetValue(SetTimesProperty, value);
        }

        /// <summary>
        /// Dependency property for the selected time property
        /// </summary>
        public static readonly DependencyProperty SetSelectedTimeProperty =
            DependencyProperty.Register("SelectedTime",
                typeof(object),
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, SelectedTimeChanged));

        private static void SelectedTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Is the calling function a DateTimePicker
            if (!(d is DateTimePicker dateTimePicker))
                return;

            if (!(dateTimePicker.Template.FindName("PART_TIME_TEXT_BOX", dateTimePicker) is TextBox timeTextBox))
                throw new NullResourceException("Unable to find the Time Text Box");

            if (string.IsNullOrWhiteSpace(timeTextBox.Text))
                dateTimePicker.Value = DateTime.Now;

            timeTextBox.Text = e.NewValue.ToString();
            dateTimePicker.SelectedTime = e.NewValue;
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
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata());

        /// <summary>
        /// Property to toggle the visibility of the times drop down selections
        /// </summary>
        public Visibility ShowTimesDropDown
        {
            get => (Visibility)GetValue(SetShowTimesDropDownProperty);
            set => SetValue(SetShowTimesDropDownProperty, value);
        }

        /// <summary>
        /// Dependency property to allow the user to set preset selection options for the available times
        /// </summary>
        public static readonly DependencyProperty SetTimesDisplayMemberPathProperty =
            DependencyProperty.Register("TimesDisplayMemberPath",
                typeof(string),
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(default(string)));

        /// <summary>
        /// Property to toggle the visibility of the times drop down selections
        /// </summary>
        public string TimesDisplayMemberPath
        {
            get => (string)GetValue(SetTimesDisplayMemberPathProperty);
            set => SetValue(SetTimesDisplayMemberPathProperty, value);
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
        /// The sub controls up button used to increment the Time
        /// </summary>
        private RepeatButton _timeUpButton;

        /// <summary>
        /// The sub controls down button used to decrement the Time
        /// </summary>
        private RepeatButton _timeDownButton;

        /// <summary>
        /// The main controls text box
        /// </summary>
        private TextBox _mainTextBox;

        /// <summary>
        /// The sub controls text box
        /// </summary>
        private TextBox _timeTextBox;

        /// <summary>
        /// DateTimeContext to get the current DateTime and correct strategy to apply
        /// based on the StringFormat and the position of the selected text
        /// </summary>
        private readonly IObtainDateTimeContext _obtainContext = new ObtainDateTimeContext();

        /// <summary>
        /// Find the next or previous number to jump to on left right key down movements
        /// </summary>
        private readonly INumberFinder _numberFinder = new NumberFinder();

        /// <summary>
        /// The main text boxes format specifier options
        /// </summary>
        private FormatSpecifier[] _mainFormatSpecifiers;

        /// <summary>
        /// The sub controls format specifier options
        /// </summary>
        private FormatSpecifier[] _timeFormatSpecifiers;
        #endregion

        /// <summary>
        /// Apply the default style for the DateTime Picker. This style can be found in Themes/Generics.xaml
        /// </summary>
        static DateTimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateTimePicker), new FrameworkPropertyMetadata(typeof(DateTimePicker)));
        }

        /// <summary>
        /// When the template inside Themes/Generics.xaml is being constructed and applied to this
        /// object. Find the components and store them inside global variables.
        /// Validates that the users specified FormatString is valid.
        /// </summary>
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

            _timeUpButton = Template.FindName("TIME_UP_BUTTON", this) as RepeatButton;
            _timeDownButton = Template.FindName("TIME_DOWN_BUTTON", this) as RepeatButton;

            if (_timeUpButton == null || _timeDownButton == null)
                throw new NullResourceException("Unable to find the time spinner buttons");

            _timeUpButton.Click += TimeButtonOnClick;
            _timeDownButton.Click += TimeButtonOnClick;

            _mainTextBox = Template.FindName("PART_MAIN_TEXT_BOX", this) as TextBox;
            _timeTextBox = Template.FindName("PART_TIME_TEXT_BOX", this) as TextBox;

            if (_mainTextBox == null || _timeTextBox == null)
                throw new NullResourceException("Unable to find the text boxes");

            _mainTextBox.PreviewKeyDown += MainTextBoxOnPreviewKeyDown;
            _timeTextBox.PreviewKeyDown += TimeTextBoxOnPreviewKeyDown;

            // The user hasn't specified a format type. Set a default
            if (Format == null && string.IsNullOrWhiteSpace(FormatString))
            {
                FormatString = string.Format(CultureInfo.CurrentCulture, "dd/MM/yyyy HH:mm");
                return;
            }

            // The user has specified a preset format
            if (Format != null)
            {
                FormatString = null; // Make the FormatString invalid and not usable
                return;
            }

            // The user has specified a custom string format calculate the FormatSpecifiers to use
            IFormatStringFormatter formatStringFormatter = new FormatStringFormatter();

            // Get the main controls format specifiers
            IEnumerable<FormatSpecifier> mainFormatSpecifiers = formatStringFormatter.CalculateMainFormatSpecifiers(FormatString);
            _mainFormatSpecifiers = mainFormatSpecifiers.ToArray();

            // The format string doesn't contain any time formats, hide the time text box update box
            if (!(FormatString.Contains("HH") || FormatString.Contains("mm") || FormatString.Contains("ss")))
            {
                TimeTextBoxVisibility = Visibility.Collapsed;
                _timeFormatSpecifiers = null;
                TimeFormatString = null;
                return;
            }
            // The format string contains time format specifiers
            ITimeFormatSpecifierCalculator timeFormatSpecifier = new TimeFormatSpecifierCalculator();
            string timeFormatString = timeFormatSpecifier.CalculateTimeFormatString(FormatString);
            TimeFormatString = timeFormatString;

            // The user has specified some time format specifiers.
            IEnumerable<FormatSpecifier> timeFormatSpecifiers =
                formatStringFormatter.CalculateTimeFormatSpecifiers(FormatString);
            _timeFormatSpecifiers = timeFormatSpecifiers.ToArray();

            // Find the main popup to calculate it's width
            if (!(Template.FindName("PART_MAIN_POPUP", this) is FrameworkElement mainPopup))
                throw new NullResourceException("Unable to find the main drop down element");

            if (double.IsNaN(Width) || Width > 200)
            {
                /*
                 * The user hasn't specified a direct size for the control
                 * Or the control size is to large to do a direct size binding
                 */
                mainPopup.Width = 200;
            }
            else
            {
                // The width is appropriate to match the controls size as it will look reasonable.
                mainPopup.Width = Width;
            }

            // Toggle the visibility and pre-load options of the Time options combo box
            Times = new List<Time>
            {
                new Time(1, "00:00"),
                new Time(2, "01:00")
            };
            ShowTimesDropDown = Visibility.Visible;
            TimesDisplayMemberPath = "Value";
        }

        /// <summary>
        /// Event handler for when the user presses the arrow keys inside main controls text box
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Event arguments</param>
        private void MainTextBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down) // Decrease
            {
                DateTime? dateTime = GetDateTimeFromString(_mainTextBox.Text);
                if (dateTime == null)
                    return;

                DateTimeContext context = _obtainContext.Apply(_mainTextBox, Value, _mainFormatSpecifiers, out int start, out int length);
                if (context == null || start < 0 || length < 0)
                    return;

                Value = context.ExecuteDecrease((DateTime)dateTime);
                _mainTextBox.Select(start, length);
            }
            else if (e.Key == Key.Up) // Increase
            {
                DateTime? dateTime = GetDateTimeFromString(_mainTextBox.Text);
                if (dateTime == null)
                    return;

                DateTimeContext context = _obtainContext.Apply(_mainTextBox, Value, _mainFormatSpecifiers, out int start, out int length);
                if (context == null || start < 0 || length < 0)
                    return;

                Value = context.ExecuteIncrease((DateTime)dateTime);
                _mainTextBox.Select(start, length);
            }
            else if (e.Key == Key.Left) // Select the next left number
            {
                _numberFinder.FindPrevious(_mainTextBox, out int startSelectIndex, out int length);
                _mainTextBox.Select(startSelectIndex, length);
            }
            else if (e.Key == Key.Right) // Select the next right number
            {
                _numberFinder.FindNext(_mainTextBox, out int startSelectIndex, out int length);
                _mainTextBox.Select(startSelectIndex, length);
            }

            e.Handled = true;
        }

        /// <summary>
        /// Event handler for the sub control text box.
        /// When the user presses the arrow keys determine the correct action to apply.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Event arguments</param>
        private void TimeTextBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down) // Decrease
            {
                if (_timeFormatSpecifiers == null)
                    return;

                DateTime? dateTime = GetDateTimeFromString(_timeTextBox.Text);
                if (dateTime == null)
                    return;

                DateTimeContext context = _obtainContext.Apply(_timeTextBox, Value, _timeFormatSpecifiers, out int start, out int length);
                if (context == null || start < 0 || length < 0)
                    return;

                Value = context.ExecuteDecrease((DateTime)dateTime);
                _timeTextBox.Select(start, length);
            }
            else if (e.Key == Key.Up) // Increase
            {
                if (_timeFormatSpecifiers == null)
                    return;

                DateTime? dateTime = GetDateTimeFromString(_timeTextBox.Text);
                if (dateTime == null)
                    return;

                DateTimeContext context = _obtainContext.Apply(_timeTextBox, Value, _timeFormatSpecifiers, out int start, out int length);
                if (context == null || start < 0 || length < 0)
                    return;

                Value = context.ExecuteIncrease((DateTime)dateTime);
                _timeTextBox.Select(start, length);
            }
            else if (e.Key == Key.Left) // Find the next left number
            {
                _numberFinder.FindPrevious(_timeTextBox, out int startSelectIndex, out int length);
                _timeTextBox.Select(startSelectIndex, length);
            }
            else if (e.Key == Key.Right) // Find the next right number
            {
                _numberFinder.FindNext(_timeTextBox, out int startSelectIndex, out int length);
                _timeTextBox.Select(startSelectIndex, length);
            }

            e.Handled = true;
        }

        #region Events

        /// <summary>
        /// Main control up button event handler/
        /// If has text selected, increment the selected text by one.
        /// Else increment the day by one
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Event arguments</param>
        private void MainControlButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (Value == null) // Apply value
            {
                Value = DateTime.Now;
                return;
            }

            if (!(sender is FrameworkElement frameworkElement))
                return;

            if (string.IsNullOrWhiteSpace(_mainTextBox.SelectedText)) // Nothing selected
            {
                if (frameworkElement.Name.Equals("PART_MAIN_UP_BUTTON"))
                    Value = new IncreaseDayStrategy().UpdateDateTime((DateTime)Value);
                else if (frameworkElement.Name.Equals("PART_MAIN_DOWN_BUTTON"))
                    Value = new DecreaseDayStrategy().UpdateDateTime((DateTime)Value);
            }
            else // Has something selected
            {
                bool isValidDateTime = DateTime.TryParse(_mainTextBox.Text, out DateTime dateTime);
                if (!isValidDateTime)
                    return;

                DateTimeContext context = _obtainContext.Apply(_mainTextBox, Value, _mainFormatSpecifiers, out int start, out int length);

                if (frameworkElement.Name.Equals("PART_MAIN_UP_BUTTON"))
                    Value = context.ExecuteIncrease(dateTime);
                else if (frameworkElement.Name.Equals("PART_MAIN_DOWN_BUTTON"))
                    Value = context.ExecuteDecrease(dateTime);

                _mainTextBox.Select(start, length);
            }
        }

        private void TimeButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (Value == null) // Apply value
            {
                Value = DateTime.Now;
                return;
            }

            if (!(sender is FrameworkElement frameworkElement))
                return;

            if (string.IsNullOrWhiteSpace(_timeTextBox.SelectedText)) // Nothing selected
            {
                if (frameworkElement.Name.Equals("TIME_UP_BUTTON"))
                    Value = new IncreaseMinuteStrategy().UpdateDateTime((DateTime)Value);
                else if (frameworkElement.Name.Equals("TIME_DOWN_BUTTON"))
                    Value = new DecreaseMinuteStrategy().UpdateDateTime((DateTime)Value);
            }
            else // Has something selected
            {
                bool isValidDateTime = DateTime.TryParse(_timeTextBox.Text, out DateTime dateTime);
                if (!isValidDateTime)
                    return;

                DateTimeContext context = _obtainContext.Apply(_timeTextBox, Value, _timeFormatSpecifiers, out int start, out int length);

                if (frameworkElement.Name.Equals("TIME_UP_BUTTON"))
                    Value = context.ExecuteIncrease(dateTime);
                else if (frameworkElement.Name.Equals("TIME_DOWN_BUTTON"))
                    Value = context.ExecuteDecrease(dateTime);

                _timeTextBox.Select(start, length);
            }
        }
        #endregion

        private static DateTime? GetDateTimeFromString(string text)
        {
            bool isValidDateTime = DateTime.TryParse(text, out DateTime dateTime);
            return !isValidDateTime ? (DateTime?)null : dateTime;
        }
    }
}
