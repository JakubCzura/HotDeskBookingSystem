using HotDeskBookingSystem.Commands;
using HotDeskBookingSystem.Model;
using HotDeskBookingSystem.Models;
using HotDeskBookingSystem.Models.Interfaces;
using HotDeskBookingSystem.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace HotDeskBookingSystem.ViewModels
{
    public class RegistrationWindowVM : BaseViewModel
    {
        public RegistrationWindowVM()
        {
            Person = new Person();
            try
            {
                //if(RegistrationWindow.Instance != null)
                RegisterCommand = new RegisterCommand(Person);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ICommand RegisterCommand { get; private set; }

        public List<string> roles = WorkersRoles.RolesList;
        public List<string> Roles
        {
            get { return roles; }
            set { roles = value; OnPropertyChanged(); }
        }

        public string Role
        {
            get { return Person.Role; }
            set { Person.Role = value; OnPropertyChanged(); }
        }

        private Person person;

        public Person Person
        {
            get { return person; }
            set { person = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return Person.Name; }
            set { Person.Name = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get { return Person.LastName; }
            set { Person.LastName = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get { return Person.Email; }
            set { Person.Email = value; OnPropertyChanged(); }
        }
    }
}
