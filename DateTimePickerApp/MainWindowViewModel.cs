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
    }
}