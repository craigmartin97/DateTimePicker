using DateTimePicker.DateStrategies;
using DateTimePicker.Exceptions;
using DateTimePicker.Interfaces;
using DateTimePicker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class DateTimePicker_OLD : Control, INotifyPropertyChanged
    {
        #region Notify PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #region Notify Property Changed
        /// <summary>
        /// Inform the observers that the property has updated
        /// </summary>
        /// <param name="propertyName">The name of the property that has been updated</param>
        protected void OnPropertyChanged(string propertyName)
            => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
            => PropertyChanged?.Invoke(this, e);
        #endregion
        #endregion

        #region Dependency Properties

        /// <summary>
        /// Dependency property for the DateTime Value which is data bound
        /// </summary>
        public static readonly DependencyProperty SetValueProperty =
            DependencyProperty.Register("Value", typeof(DateTime?),
                typeof(DateTimePicker_OLD),
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
                typeof(DateTimePicker_OLD),
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
                typeof(DateTimePicker_OLD),
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
                typeof(DateTimePicker_OLD),
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
                typeof(DateTimePicker_OLD), new FrameworkPropertyMetadata(null));

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
                typeof(DateTimePicker_OLD),
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
                typeof(DateTimePicker_OLD),
                new FrameworkPropertyMetadata(default));

        /// <summary>
        /// Custom string format property
        /// </summary>
        public string FormatString
        {
            get => (string)GetValue(SetFormatStringProperty);
            set => SetValue(SetFormatStringProperty, value);
        }
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
        /// The sub controls text box
        /// </summary>
        private TimeTextBox _timeTextBox;

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

        private int _previousEntered;
        #endregion

        /// <summary>
        /// Apply the default style for the DateTime Picker. This style can be found in Themes/Generics.xaml
        /// </summary>
        static DateTimePicker_OLD()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateTimePicker_OLD), new FrameworkPropertyMetadata(typeof(DateTimePicker_OLD)));
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

            _mainTextBox = Template.FindName("PART_MAIN_TEXT_BOX", this) as TextBox;
            _timeTextBox = Template.FindName("PART_TIME_TEXT_BOX", this) as TimeTextBox;

            if (_mainTextBox == null || _timeTextBox == null)
                throw new NullResourceException("Unable to find the text boxes");

            _mainTextBox.PreviewKeyDown += MainTextBoxOnPreviewKeyDown;

            // The user has specified a custom string format calculate the FormatSpecifiers to use
            IFormatStringFormatter formatStringFormatter = new FormatStringFormatter();

            // The user hasn't specified a format type. Set a default
            if (Format == null && string.IsNullOrWhiteSpace(FormatString))
            {
                FormatString = string.Format(CultureInfo.CurrentCulture, "dd/MM/yyyy HH:mm");
            }
            // The user has specified a preset format
            else if (Format != null)
            {
                FormatString = null; // Make the FormatString invalid and not usable
                return;
            }

            // Get the main controls format specifiers
            IEnumerable<FormatSpecifier> mainFormatSpecifiers = formatStringFormatter.CalculateMainFormatSpecifiers(FormatString);
            _mainFormatSpecifiers = mainFormatSpecifiers.ToArray();

            // The format string doesn't contain any time formats, hide the time text box update box
            if (!(FormatString.Contains("HH") || FormatString.Contains("mm") || FormatString.Contains("ss")))
            {
                TimeTextBoxVisibility = Visibility.Collapsed;
                _timeTextBox.FormatString = null;
                return;
            }
            // The format string contains time format specifiers
            ITimeFormatSpecifierCalculator timeFormatSpecifier = new TimeFormatSpecifierCalculator();
            string timeFormatString = timeFormatSpecifier.CalculateTimeFormatString(FormatString);
            _timeTextBox.FormatString = timeFormatString;

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
            if (string.IsNullOrWhiteSpace(_timeTextBox.FormatString))
                return;

            // The user hasn't set the times options but it is visible. Set with pre-set options
            _timeTextBox.Times = PreLoadTimeOptions.GetPreLoadTimes(_timeTextBox.FormatString);
            _timeTextBox.SelectedTime = null;
        }

        /// <summary>
        /// Event handler for when the user presses the arrow keys inside main controls text box
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Event arguments</param>
        private void MainTextBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            DateTime? dateTime = GetDateTime.GetDateTimeFromString(_mainTextBox.Text);
            if (dateTime == null)
            {
                Value = DateTime.Now;
                return;
            }

            if (e.Key == Key.Down) // Decrease
            {
                DateTimeContext context = _obtainContext.Apply(_mainTextBox, Value, _mainFormatSpecifiers, out int start, out int length);
                if (context == null || start < 0 || length < 0)
                    return;

                Value = context.ExecuteDecrease((DateTime)dateTime);
                _mainTextBox.Select(start, length);
                _previousEntered = 0;
            }
            else if (e.Key == Key.Up) // Increase
            {
                DateTimeContext context = _obtainContext.Apply(_mainTextBox, Value, _mainFormatSpecifiers, out int start, out int length);
                if (context == null || start < 0 || length < 0)
                    return;

                Value = context.ExecuteIncrease((DateTime)dateTime);
                _mainTextBox.Select(start, length);
                _previousEntered = 0;
            }
            else if (e.Key == Key.Left) // Select the next left number
            {
                _numberFinder.FindPrevious(_mainTextBox, out int startSelectIndex, out int length);
                _mainTextBox.Select(startSelectIndex, length);
                _previousEntered = 0;
            }
            else if (e.Key == Key.Right) // Select the next right number
            {
                _numberFinder.FindNext(_mainTextBox, out int startSelectIndex, out int length);
                _mainTextBox.Select(startSelectIndex, length);
                _previousEntered = 0;
            }
            // The user pressed a number
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                if (string.IsNullOrWhiteSpace(_mainTextBox.SelectedText))
                    return;

                DateTimeContext context = _obtainContext.Apply(_mainTextBox, Value, _mainFormatSpecifiers, out int start, out int length);
                if (context == null || start < 0 || length < 0)
                    return;

                char i = e.Key.ToString()[1];
                Value = context.UpdateDateTime((DateTime)dateTime, i, _previousEntered);

                _mainTextBox.Select(start, length);

                int reset = context.GetPreviousReset();
                if (_previousEntered == reset)
                {
                    _previousEntered = 0;
                }
                else
                {
                    _previousEntered++;
                }
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
        #endregion
    }
}
