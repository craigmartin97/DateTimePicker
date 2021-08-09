using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DateTimePicker.Converters
{
    public class BorderThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Use the left value on the right as Thickness.
            var originalThickness = value as Thickness?;
            if (originalThickness.HasValue)
                return new Thickness(originalThickness.Value.Left, originalThickness.Value.Top, originalThickness.Value.Left, originalThickness.Value.Bottom);

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}