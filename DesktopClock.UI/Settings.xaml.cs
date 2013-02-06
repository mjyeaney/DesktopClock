﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

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
        private bool _useWhiteText;

        // INPC support
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Window constructor.
        /// </summary>
        public Settings(IMainViewModel host, double currentOpacity, Brush currentTextColor)
        {
            InitializeComponent();
            this.DataContext = this;
            _host = host;            
            _useWhiteText = true;
            _clockOpacity = currentOpacity;
            _saveSettings = new SaveSettings(this);
            _useWhiteText = (currentTextColor == Brushes.White);
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
        /// True if we are to use white text to render;
        /// otherwise, false.
        /// </summary>
        public bool UseWhiteText
        {
            get { return _useWhiteText; }
            set
            {
                _useWhiteText = value;
                _host.TextBrush = (_useWhiteText ? Brushes.White : Brushes.Black);
                RaisePropertyChanged("UseWhiteText");
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

        /// <summary>
        /// Handles a user click on the navigation link.
        /// TODO: Should this be a command?
        /// </summary>
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(((Hyperlink)sender).NavigateUri.ToString());
        }
    }
}
