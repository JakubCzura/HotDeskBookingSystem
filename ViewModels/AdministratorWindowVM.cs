using CommunityToolkit.Mvvm.Input;
using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Models;
using HotDeskBookingSystem.Validators;
using HotDeskBookingSystem.Views.Windows;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace HotDeskBookingSystem.ViewModels
{
    public class AdministratorWindowVM : MainWindowVM
    {
        public static AdministratorWindowVM? Instance { get; private set; }

        public AdministratorWindowVM()
        {
            Instance = this;
            Location = new();
            Desk = new();
            Locations = DataGetter<Location>.GetAllRows();
            Desks = DataGetter<Desk>.GetAllRows();
            AddNewLocationCommand = new RelayCommand(AddNewLocation);
            DeleteLocationCommand = new RelayCommand(DeleteLocation);
            ShowAddLocationWindowCommand = new RelayCommand(() =>
            {
                AddLocationWindow AddLocationWindow = new();
                AddLocationWindow.Show();
            });
            ShowManageDesksWindowCommand = new RelayCommand(() =>
            {
                Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == GetSelectedLocation().Id));
                ManageDesksWindow ManageDesksWindow = new();
                //ManageDesksWindow.Instance.DesksDataGrid.ItemsSource = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == GetSelectedLocation().Id));
                ManageDesksWindow.Instance.DesksDataGrid.ItemsSource = Desks;
                ManageDesksWindow.Show();
            });
            AddNewDeskCommand = new RelayCommand(AddNewDesk);
            DeleteDeskCommand = new RelayCommand(DeleteDesk);
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

        public string DeskName
        {
            get { return Desk.Name; }
            set { Desk.Name = value; OnPropertyChanged(); }
        }

        public ICommand AddNewLocationCommand { get; private set; }
        public ICommand ShowAddLocationWindowCommand { get; private set; }
        public ICommand DeleteLocationCommand { get; private set; }
        public ICommand ShowManageDesksWindowCommand { get; private set; }
        public ICommand AddNewDeskCommand { get; private set; }
        public ICommand DeleteDeskCommand { get; private set; }

        private void AssignLocationsToLocationsDataGrid()
        {
            if (AdministratorWindow.Instance != null)
            {
                //Despite using ObservableCollection and INotifyPropertyChanged(), 'Locations' aren't refreshed
                //by UI, so I need to assign Locations to LocationsDatGrid.ItemsSource manually
                //and it works, it refreshes list of Locations in UI
                AdministratorWindow.Instance.LocationsDataGrid.ItemsSource = Locations;
            }
        }

        private static Location GetSelectedLocation()
        {
            return AdministratorWindow.Instance.LocationsDataGrid.SelectedItem as Location;
        }

        private static Desk GetSelectedDesk()
        {
            return ManageDesksWindow.Instance.DesksDataGrid.SelectedItem as Desk;
        }

        public static string ManageDeskForLocation
        {
            get { return $"Manage desks for selected location: {GetSelectedLocation().Name}"; }
        }

        private void AddNewLocation()
        {
            try
            {
                LocationValidator Validator = new();
                if (Validator.Validate(Location))
                {
                    if (DataInsertion.AddData(Location))
                    {
                        Locations = DataGetter<Location>.GetAllRows();
                        AssignLocationsToLocationsDataGrid();
                        MessageBox.Show("New location added");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void DeleteLocation()
        {
            try
            {
                //Desks = DataGetter<Desk>.GetAllRows();
                if (Desks.Any(x => x.LocationId == GetSelectedLocation().Id) == false)
                {
                    if (DataDeletion.Delete(GetSelectedLocation()))
                    {
                        Locations = DataGetter<Location>.GetAllRows();
                        AssignLocationsToLocationsDataGrid();
                        MessageBox.Show("Location deleted");
                    }
                }
                else
                {
                    MessageBox.Show("Can't delete location. \nThere is a desk bound with this location");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void DeleteDesk()
        {
            try
            {
                //Desks = DataGetter<Desk>.GetAllRows();
                if (GetSelectedDesk().IsReserved == false)
                {
                    if (DataDeletion.Delete(GetSelectedDesk()))
                    {
                        //ManageDesksWindow.Instance.DesksDataGrid.ItemsSource = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == GetSelectedLocation().Id));                     
                        Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == GetSelectedLocation().Id));
                        ManageDesksWindow.Instance.DesksDataGrid.ItemsSource = Desks;
                        MessageBox.Show("Desk deleted");
                    }
                }
                else
                {
                    MessageBox.Show("Can't delete desk. \nThis desk is reserved");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void AddNewDesk()
        {
            try
            {
                DeskValidator Validator = new();
                Desk.LocationId = GetSelectedLocation().Id;
                if (Validator.Validate(Desk))
                {
                    if (DataInsertion.AddData(Desk))
                    {
                        Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == GetSelectedLocation().Id));
                        //ManageDesksWindow.Instance.DesksDataGrid.ItemsSource = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == GetSelectedLocation().Id));
                        MessageBox.Show("New desk added");
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