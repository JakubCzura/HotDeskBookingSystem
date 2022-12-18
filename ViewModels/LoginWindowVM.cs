﻿using HotDeskBookingSystem.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using HotDeskBookingSystem.Model;
using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Validators;
using HotDeskBookingSystem.Views.Windows;
using System.Windows;
using HotDeskBookingSystem.Models;

namespace HotDeskBookingSystem.ViewModels
{
    public class LoginWindowVM : BaseViewModel
    {
        public LoginWindowVM()
        {
            Person = new Person();
            ShowRegistrationWindowCommand = new ShowRegistrationWindowCommand();
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

        private void Login()
        {
            try
            {
                if (LoginWindow.Instance != null)
                {
                    //Person.Password = Hasher.HashPassword(LoginWindow.Instance.UserPasswordPasswordBox.Password);

                    if ((Person = LoggingIn.Login(Email, LoginWindow.Instance.UserPasswordPasswordBox.Password)) != null)
                    {
                        if(Person.Role == Enum.GetName(WorkersRoles.Roles.Administrator)) 
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

