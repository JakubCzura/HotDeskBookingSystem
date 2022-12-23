using CommunityToolkit.Mvvm.Input;
using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Model;
using HotDeskBookingSystem.Validators;
using HotDeskBookingSystem.Views.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace HotDeskBookingSystem.ViewModels
{
    public class RegistrationWindowVM : BaseViewModel
    {
        public RegistrationWindowVM()
        {
            Person = new();
            RegisterCommand = new RelayCommand(Register);
        }

        public ICommand RegisterCommand { get; private set; }

        public List<string> roles = WorkersRoles.RolesList;

        public List<string> Roles
        {
            get { return roles; }
            set { roles = value; OnPropertyChanged(); }
        }

        private Person person;

        public Person Person
        {
            get { return person; }
            set { person = value; OnPropertyChanged(); }
        }

        public string Role
        {
            get { return Person.Role; }
            set { Person.Role = value; OnPropertyChanged(); }
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

        /// <summary>
        /// Register user, save data to database, close RegistrationWindow
        /// </summary>
        private void Register()
        {
            try
            {
                PersonValidator Validator = new();
                if (RegistrationWindow.Instance != null)
                {
                    Person.Password = Hasher.HashPassword(RegistrationWindow.Instance.UserPasswordPasswordBox.Password);
                    if (Validator.Validate(Person))
                    {
                        if (Registration.Register(Person))
                        {
                            RegistrationWindow.Instance?.Close();
                            MessageBox.Show("You can sign in", "You've been just registered");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Registration error");
            }
        }
    }
}