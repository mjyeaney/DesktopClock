﻿using System.Windows.Input;
using System.Windows.Media;

namespace DesktopClock
{
    /// <summary>
    /// View model definition for the main clock.
    /// </summary>
    public interface IMainViewModel
    {
        /// <summary>
        /// Shuts down the clock.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// The text for the clock face
        /// </summary>
        string ClockText { get; set; }

        /// <summary>
        /// Command binding used to shutdown requests.
        /// </summary>
        ICommand CloseClock { get; }

        /// <summary>
        /// Command binding used to show settings dialog
        /// </summary>
        ICommand ShowSettings { get; }

        /// <summary>
        /// The desired opacity of the clock;
        /// </summary>
        double ClockOpacity { get; set;  }

        /// <summary>
        /// True if we are to display 24 hour format;
        /// otherwise false.
        /// </summary>
        bool Display24HourFormat { get; set; }

        /// <summary>
        /// The color used to render the clock.
        /// </summary>
        Brush TextBrush { get; set; }

        /// <summary>
        /// The font size used to render the clock.
        /// </summary>
        double UserFontSize { get; set; }
    }
}
