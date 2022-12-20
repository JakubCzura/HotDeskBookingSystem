using System;
using System.Windows.Input;

namespace HotDeskBookingSystem.Commands
{
    public abstract class BasicCommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}