using System;

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

        private int? _hour;
        public int? Hour
        {
            get => _hour;
            set
            {
                _hour = value;
                OnPropertyChanged(nameof(Hour));
            }
        }

        private int? _minute;
        public int? Minute
        {
            get => _minute;
            set
            {
                _minute = value;
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
    }
}