using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DesktopClock
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window, INotifyPropertyChanged
    {
        private ICommand _saveSettings;

        // INPC support
        public event PropertyChangedEventHandler PropertyChanged;

        public Settings()
        {
            InitializeComponent();
            this.DataContext = this;

            _saveSettings = new SaveSettings(this);
        }

        public ICommand SaveSettings
        {
            get { return _saveSettings; }
        }
    }

    public class SaveSettings : ICommand
    {
        private Window _host;

        public SaveSettings(Window host)
        {
            _host = host;
        }

        /// <summary>
        /// Returns true if the command can execute; otherwise false.
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Raised when the CanExecute value changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute(object parameter)
        {
            _host.Close();
        }
    }
}
