using System.Windows.Input;

namespace DesktopClock
{
    /// <summary>
    /// View model used for settings UI.
    /// </summary>
    public interface ISettingsViewModel
    {
        /// <summary>
        /// Closes this view.
        /// </summary>
        void CloseSettings();

        /// <summary>
        /// The desired opacity of the 
        /// </summary>
        double ClockOpacity { get; set; }

        /// <summary>
        /// True if we are to display 24-hour time format; 
        /// otherwise false.
        /// </summary>
        bool Display24HourFormat { get; set; }

        /// <summary>
        /// True if we are to use white text to render;
        /// otherwise, false.
        /// </summary>
        bool UseWhiteText { get; set; }

        /// <summary>
        /// Command invoked when user clicks 'Done'
        /// </summary>
        ICommand SaveSettings { get; }
    }
}
