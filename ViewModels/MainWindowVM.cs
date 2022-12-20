using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Model;
using System;

namespace HotDeskBookingSystem.ViewModels
{
    public class MainWindowVM : BaseViewModel
    {
        public MainWindowVM()
        {
            Person = LoggedPersonData.GetLoggedPersonData();
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

        public string Role
        {
            get { return Person.Role; }
            set { Person.Role = value; OnPropertyChanged(); }
        }

        public string WelcomeMessage
        {
            get { return $"Hello {Person.Name}!"; }
        }

        public string TodayDate
        {
            get { return $"Today is {DateTime.Today.ToShortDateString()}"; }
        }

        public string LoggedAs
        {
            get { return $"You are logged as {Person.Role}"; }
        }
    }
}