using HotDeskBookingSystem.Views.Windows;
using System.Windows.Input;

namespace HotDeskBookingSystem.Commands
{
    public class ShowRegistrationWindowCommand : BasicCommand, ICommand
    {
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            RegistrationWindow RegistrationWindow = new();
            RegistrationWindow.ShowDialog();
        }
    }
}