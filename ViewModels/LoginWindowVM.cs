using HotDeskBookingSystem.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotDeskBookingSystem.ViewModels
{
    public class LoginWindowVM
    {
        public LoginWindowVM() 
        {
            ShowRegistrationWindowCommand = new ShowRegistrationWindowCommand();
        }

        public ICommand ShowRegistrationWindowCommand { get; private set; }
    }
}
