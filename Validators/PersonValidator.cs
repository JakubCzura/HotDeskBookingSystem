using CommunityToolkit.Diagnostics;
using HotDeskBookingSystem.Model;
using System;
using System.Windows;

namespace HotDeskBookingSystem.Validators
{
    public class PersonValidator : IValidator<Person>
    {
        public bool Validate(Person person)
        {
            try
            {
                Guard.IsNotNullOrWhiteSpace(person.Name);
                Guard.IsNotNullOrWhiteSpace(person.LastName);
                Guard.IsNotNullOrWhiteSpace(person.Email);
                Guard.IsNotNullOrWhiteSpace(person.Password);
                Guard.IsNotNullOrWhiteSpace(person.Role);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "An exception in Person's data");
                return false;
            }
        }
    }
}