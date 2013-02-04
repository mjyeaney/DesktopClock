using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DesktopClock
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window, ISettingsViewModel, INotifyPropertyChanged
    {
        private IMainViewModel _host;
        private ICommand _saveSettings;
        private double _clockOpacity;
        private bool _display24Hour;

        // INPC support
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Window constructor.
        /// </summary>
        public Settings(IMainViewModel host, double currentOpacity)
        {
            InitializeComponent();
            _host = host;
            this.DataContext = this;

            _clockOpacity = currentOpacity;
            _saveSettings = new SaveSettings(this);
            MouseLeftButtonDown += Settings_MouseLeftButtonDown;
        }

        /// <summary>
        /// Used to support dragging the window around.
        /// </summary>
        private void Settings_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        /// <summary>
        /// Closes this view.
        /// </summary>
        public void CloseSettings()
        {
            this.Close();
        }

        /// <summary>
        /// The desired opacity of the 
        /// </summary>
        public double ClockOpacity
        {
            get { return _clockOpacity; }
            set
            {
                _clockOpacity = value;
                _host.ClockOpacity = value;
                RaisePropertyChanged("ClockOpacity");
            }
        }

        /// <summary>
        /// True if we are to display 24-hour time format; 
        /// otherwise false.
        /// </summary>
        public bool Display24HourFormat
        {
            get { return _display24Hour; }
            set
            {
                _display24Hour = value;
                _host.Display24HourFormat = value;
                RaisePropertyChanged("Display24HourFormat");
            }
        }

        /// <summary>
        /// Command invoked when user clicks 'Done'
        /// </summary>
        public ICommand SaveSettings
        {
            get { return _saveSettings; }
        }

        /// <summary>
        /// Used to raise change notifications to other consumers.
        /// </summary>
        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }

    /// <summary>
    /// Command used to support saving the new settings.
    /// </summary>
    public class SaveSettings : ICommand
    {
        private ISettingsViewModel _host;

        /// <summary>
        /// Creates a new instance of this command.
        /// </summary>
        /// <param name="host">The hosting Window control</param>
        public SaveSettings(ISettingsViewModel host)
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
            _host.CloseSettings();
        }
    }
}
