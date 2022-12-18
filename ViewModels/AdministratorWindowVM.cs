using CommunityToolkit.Mvvm.Input;
using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Models;
using HotDeskBookingSystem.Validators;
using HotDeskBookingSystem.Views.Windows;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace HotDeskBookingSystem.ViewModels
{
    public class AdministratorWindowVM : MainWindowVM
    {
        public static AdministratorWindowVM? Instance { get; private set; }

        public AdministratorWindowVM()
        {
            Location = new();
            Locations = LocationsGetter.GetAllLocations();
            ShowAddLocationWindowCommand = new RelayCommand(() =>
            {
                AddLocationWindow AddLocationWindow = new();
                AddLocationWindow.Show();
            });
            AddNewLocationCommand = new RelayCommand(AddNewLocation);
            Instance = this;
        }

        private ObservableCollection<Location> locations;

        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set { locations = value; OnPropertyChanged(); }
        }

        private Location selectedLocation;

        public Location SelectedLocation
        {
            get { return selectedLocation; }
            set { selectedLocation = value; OnPropertyChanged(); }
        }

        private Location location;

        public Location Location
        {
            get { return location; }
            set { location = value; OnPropertyChanged(); }
        }

        public string LocationName
        {
            get { return Location.Name; }
            set { Location.Name = value; OnPropertyChanged(); }
        }

        public int Floor
        {
            get { return Location.Floor; }
            set { Location.Floor = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get { return Location.Description; }
            set { Location.Description = value; OnPropertyChanged(); }
        }

        public ICommand AddNewLocationCommand { get; private set; }
        public ICommand ShowAddLocationWindowCommand { get; private set; }

        //private void AddNewLocation()
        //{
        //    try
        //    {
        //        LocationValidator Validator = new();
        //        if (Validator.Validate(Location))
        //        {
        //            if (DataInsertion.AddData(Location))
        //            {
        //                Locations = LocationsGetter.GetAllLocations();
        //                OnPropertyChanged(nameof(Locations));
        //                MessageBox.Show("New location added");
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message);
        //    }
        //}

        private void AddNewLocation()
        {
            try
            {
                LocationValidator Validator = new();
                if (Validator.Validate(Location))
                {
                    if (DataInsertion.AddData(Location))
                    {
                        Locations = LocationsGetter.GetAllLocations();
                        if (AdministratorWindow.Instance != null)
                        {
                            //Despite using ObservableCollection and INotifyPropertyChanged(), 'Locations' aren't refreshed 
                            //by UI, so I need to assign Locations to LocationsDatGrid.ItemsSource manually
                            //and it works, it refreshes list of Locations in UI
                            AdministratorWindow.Instance.LocationsDatGrid.ItemsSource = Locations;
                        }
                        MessageBox.Show("New location added");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}