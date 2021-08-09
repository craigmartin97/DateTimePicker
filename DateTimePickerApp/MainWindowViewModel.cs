using System;
using System.Diagnostics;

namespace DateTimePickerApp
{
    public class MainWindowViewModel : ViewModelBase
    {
        private DateTime? _value;
        public DateTime? Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        private int? _hour = 23;
        public int? Hour
        {
            get => _hour;
            set
            {
                _hour = value;
                Debug.WriteLine("Hour Update: " + _hour);
                OnPropertyChanged(nameof(Hour));
            }
        }

        private int? _minute = 10;
        public int? Minute
        {
            get => _minute;
            set
            {
                _minute = value;
                Debug.WriteLine("Minute Update: " + _minute);
                OnPropertyChanged(nameof(Minute));
            }
        }

        private int? _second;
        public int? Second
        {
            get => _second;
            set
            {
                _second = value;
                OnPropertyChanged(nameof(Second));
            }
        }

        private string _displayValue;
        public string DisplayValue
        {
            get => _displayValue;
            set
            {
                _displayValue = value;
                OnPropertyChanged(nameof(DisplayValue));
            }
        }
    }
}