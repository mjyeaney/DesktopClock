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
    public partial class MainWindow : Window, IMainViewModel, INotifyPropertyChanged
    {
        private Timer _timer;
        private string _clockText;
        private double _clockOpacity;
        private ICommand _closeClock;
        private ICommand _showSettings;

        // For INCP
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Main window constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // we are our own view model
            this.DataContext = this;
            this._clockOpacity = .3;
            this._closeClock = new CloseClock(this);            
            this._showSettings = new ShowSettings(this);
            this._clockText = DateTime.Now.ToShortTimeString();           
            
            // position the clock at top / right, primary screen
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 5.0;

            // enable dragging
            this.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;

            // setup our timer to...well...keep time
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Enabled = true;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        /// <summary>
        /// Shuts down the clock.
        /// </summary>
        public void Shutdown()
        {
            Application.Current.Shutdown();
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
        /// Command binding used to show settings dialog
        /// </summary>
        public ICommand ShowSettings
        {
            get { return _showSettings; }
        }

        /// <summary>
        /// The desired opacity of the clock;
        /// </summary>
        public double ClockOpacity
        {
            get { return _clockOpacity; }
            set
            {
                _clockOpacity = value;
                RaisePropertyChanged("ClockOpacity");
            }
        }

        /// <summary>
        /// Used to support dragging
        /// </summary>
        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
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
        private IMainViewModel _host;

        /// <summary>
        /// Creates a new CloseClock instance.
        /// </summary>
        public CloseClock(IMainViewModel host)
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
            _host.Shutdown();
        }
    }

    /// <summary>
    /// Command used to handle 'gear' clicks
    /// </summary>
    public class ShowSettings : ICommand
    {
        private IMainViewModel _host;

        /// <summary>
        /// Creates a new ShowSettings instance using the specified host.
        /// </summary>
        public ShowSettings(IMainViewModel host)
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
            // show settings window
            var settingsWindow = new Settings(_host, _host.ClockOpacity);
            settingsWindow.ShowDialog();
        }
    }
}
