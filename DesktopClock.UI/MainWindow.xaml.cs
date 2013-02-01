using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace DesktopClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Timer _timer;
        private string _clockText;
        private ICommand _closeClock;

        // For INCP
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            
            // we are our own view model
            this.DataContext = this;
            this._closeClock = new CloseClock();
            
            // position the clock at top / right, primary screen
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 25.0;

            // setup our timer to...well...keep time
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Enabled = true;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }
        
        /// <summary>
        /// The text for the clock face
        /// </summary>
        public string ClockText
        {
            get { return _clockText; }
            set
            {
                _clockText = value;
                RaisePropertyChanged("ClockText");
            }
        }

        /// <summary>
        /// Command binding used to shutdown requests.
        /// </summary>
        public ICommand CloseClock
        {
            get { return _closeClock; }
        }

        /// <summary>
        /// Called when the timer fires.
        /// </summary>
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ClockText = DateTime.Now.ToShortTimeString();
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
    /// Command used to handle shutdown event (user clicked on 'x').
    /// </summary>
    public class CloseClock : ICommand
    {
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
            Application.Current.Shutdown();
        }
    }
}
