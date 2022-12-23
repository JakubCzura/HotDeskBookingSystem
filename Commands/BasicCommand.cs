using System;
using System.Windows.Input;

namespace HotDeskBookingSystem.Commands
{
    /// <summary>
    /// Base class for all commands
    /// </summary>
    public abstract class BasicCommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}