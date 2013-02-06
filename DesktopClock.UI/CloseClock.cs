using System;
using System.Windows.Input;

namespace DesktopClock
{
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
}
