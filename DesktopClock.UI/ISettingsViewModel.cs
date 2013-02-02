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
        /// Command invoked when user clicks 'Done'
        /// </summary>
        ICommand SaveSettings { get; }
    }
}
