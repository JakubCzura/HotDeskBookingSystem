﻿using CommunityToolkit.Mvvm.Input;
using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Model;
using HotDeskBookingSystem.Views.Windows;
using System;
using System.Windows;
using System.Windows.Input;

namespace HotDeskBookingSystem.ViewModels
{
    public class LoginWindowVM : BaseViewModel
    {
        public LoginWindowVM()
        {
            Person = new Person();
            ShowRegistrationWindowCommand = new RelayCommand(() =>
            {
                RegistrationWindow RegistrationWindow = new();
                RegistrationWindow.ShowDialog();
            });
            LoginCommand = new RelayCommand(Login);
        }

        public ICommand ShowRegistrationWindowCommand { get; private set; }

        public ICommand LoginCommand { get; private set; }

        private Person person;

        public Person Person
        {
            get { return person; }
            set { person = value; OnPropertyChanged(); }
        }

        private string email = null!;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Login person and show new instance of MainWindow which is bound with person's role
        /// </summary>
        private void Login()
        {
            try
            {
                if (LoginWindow.Instance != null)
                {
                    //Person.Password = Hasher.HashPassword(LoginWindow.Instance.UserPasswordPasswordBox.Password);

                    if ((Person = LoggingIn.Login(Email, LoginWindow.Instance.UserPasswordPasswordBox.Password)) != null)
                    {
                        LoggedPersonData.Id = Person.Id;
                        if (Person.Role == Enum.GetName(WorkersRoles.Roles.Administrator))
                        {
                            AdministratorWindow AdministratorWindow = new();
                            AdministratorWindow.Show();
                        }
                        else if (Person.Role == Enum.GetName(WorkersRoles.Roles.Employee))
                        {
                            EmployeeWindow EmployeeWindow = new();
                            EmployeeWindow.Show();
                        }
                        LoginWindow.Instance?.Close();
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