using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Model;
using HotDeskBookingSystem.Models;
using System;
using System.Collections.ObjectModel;

namespace HotDeskBookingSystem.ViewModels
{
    public class MainWindowVM : BaseViewModel
    {
        public MainWindowVM()
        {
            Person = LoggedPersonData.GetLoggedPersonData();
            Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());
            Locations = new ObservableCollection<Location>(DataGetter<Location>.GetAllRows());
        }

        private ObservableCollection<Desk> desks;

        public ObservableCollection<Desk> Desks
        {
            get { return desks; }
            set { desks = value; OnPropertyChanged(); }
        }

        private Desk desk;

        public Desk Desk
        {
            get { return desk; }
            set { desk = value; OnPropertyChanged(); }
        }

        private Desk selectedDesk;

        public Desk SelectedDesk
        {
            get { return selectedDesk; }
            set { selectedDesk = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Location> locations;

        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set { locations = value; OnPropertyChanged(); }
        }

        private Location location;

        public Location Location
        {
            get { return location; }
            set { location = value; OnPropertyChanged(); }
        }

        private Person person;

        public Person Person
        {
            get { return person; }
            set { person = value; OnPropertyChanged(); }
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