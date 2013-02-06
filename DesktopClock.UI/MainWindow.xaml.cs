using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
        private bool _display24Hour;
        private Brush _textBrush;

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
            _clockOpacity = .2;
            _closeClock = new CloseClock(this);            
            _showSettings = new ShowSettings(this);
            _clockText = getCurrentTimeString();
            _textBrush = Brushes.White;
            
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
        /// The color used to render the clock.
        /// </summary>
        public Brush TextBrush
        {
            get { return _textBrush; }
            set
            {
                _textBrush = value;
                RaisePropertyChanged("TextBrush");
            }
        }

        /// <summary>
        /// True if we are to display 24 hour format;
        /// otherwise false.
        /// </summary>
        public bool Display24HourFormat
        {
            get { return _display24Hour; }
            set
            {
                _display24Hour = value;
                RaisePropertyChanged("Display24HourFormat");
                ClockText = getCurrentTimeString();
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
            ClockText = getCurrentTimeString();
        }

        /// <summary>
        /// Returns the current time, formatted as 12/24 hour format.
        /// </summary>
        private string getCurrentTimeString()
        {
            if (_display24Hour)
            {
                return DateTime.Now.ToString("H:mm");
            }
            else
            {
                return DateTime.Now.ToString("h:mm tt");
            }
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
}
