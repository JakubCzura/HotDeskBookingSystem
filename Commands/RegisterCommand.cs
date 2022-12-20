using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Model;
using HotDeskBookingSystem.Validators;
using HotDeskBookingSystem.Views.Windows;
using System;
using System.Windows;
using System.Windows.Input;

namespace HotDeskBookingSystem.Commands
{
    public class RegisterCommand : BasicCommand, ICommand
    {
        public RegisterCommand(Person person)
        {
            Person = person;
            // PasswordBox = passwordBox;
        }

        public Person Person { get; set; }
        //public PasswordBox PasswordBox { get; set; }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
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