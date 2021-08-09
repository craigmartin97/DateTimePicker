using System.Diagnostics;
using System.Windows;

namespace DateTimePickerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Pressed!!!");
            Debug.WriteLine(TwentyFourHourTimeTextBox.Hour + ":" + TwentyFourHourTimeTextBox.Minute);
        }
    }
}
