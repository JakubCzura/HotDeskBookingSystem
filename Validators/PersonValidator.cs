using CommunityToolkit.Diagnostics;
using HotDeskBookingSystem.Model;
using System;
using System.Windows;

namespace HotDeskBookingSystem.Validators
{
    public class PersonValidator : IValidator<Person>
    {
        /// <summary>
        /// Validate Person's data
        /// </summary>
        /// <param name="person">Person to validate</param>
        /// <returns>True if data is correct, otherwise false</returns>
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