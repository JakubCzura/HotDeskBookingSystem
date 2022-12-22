using CommunityToolkit.Mvvm.Input;
using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Models;
using HotDeskBookingSystem.Validators;
using HotDeskBookingSystem.Views.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
            Locations = new ObservableCollection<Location>(DataGetter<Location>.GetAllRows());
            Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());
            AddNewLocationCommand = new RelayCommand(AddNewLocation);
            DeleteLocationCommand = new RelayCommand(DeleteLocation);
            ShowAddLocationWindowCommand = new RelayCommand(() =>
            {
                AddLocationWindow AddLocationWindow = new();
                AddLocationWindow.Show();
            });
            ShowManageDesksWindowCommand = new RelayCommand(ShowManageDesksWindow);
            AddNewDeskCommand = new RelayCommand(AddNewDesk);
            DeleteDeskCommand = new RelayCommand(DeleteDesk);
            ChangeDeskAvailabilityCommand = new RelayCommand(ChangeDeskAvailability);
        }

        public ICommand AddNewLocationCommand { get; private set; }
        public ICommand ShowAddLocationWindowCommand { get; private set; }
        public ICommand DeleteLocationCommand { get; private set; }
        public ICommand ShowManageDesksWindowCommand { get; private set; }
        public ICommand AddNewDeskCommand { get; private set; }
        public ICommand DeleteDeskCommand { get; private set; }
        public ICommand ChangeDeskAvailabilityCommand { get; private set; }

        private void AssignLocationsToLocationsDataGrid()
        {
            if (AdministratorWindow.Instance.LocationsDataGrid.ItemsSource != null)
            {
                //Despite using ObservableCollection and INotifyPropertyChanged(), 'Locations' aren't refreshed
                //by UI, so I need to assign Locations to LocationsDatGrid.ItemsSource manually
                //and it works, it refreshes list of Locations in UI
                AdministratorWindow.Instance.LocationsDataGrid.ItemsSource = Locations;
            }
        }

        private static Location GetSelectedLocation()
        {
            if (AdministratorWindow.Instance.LocationsDataGrid.SelectedItem != null)
            {
                return AdministratorWindow.Instance.LocationsDataGrid.SelectedItem as Location;
            }
            else
            {
                return null;
            }
        }

        public static string ManageDeskForLocation
        {
            get { return $"Manage desks for selected location: {GetSelectedLocation()?.Name}"; }
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
                        Locations = new ObservableCollection<Location>(DataGetter<Location>.GetAllRows());
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
                Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());
                if (AdministratorWindow.Instance != null && GetSelectedLocation() != null)
                {
                    if (Desks.Any(x => x.LocationId == GetSelectedLocation().Id) == false)
                    {
                        if (DataDeletion.Delete(GetSelectedLocation()))
                        {
                            Locations = new ObservableCollection<Location>(DataGetter<Location>.GetAllRows());
                            AssignLocationsToLocationsDataGrid();
                            MessageBox.Show("Location deleted");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can't delete location. \nThere is a desk bound with this location");
                    }
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
                if (AdministratorWindow.Instance != null && GetSelectedLocation() != null)
                {
                    if (SelectedDesk != null)
                    {
                        if (SelectedDesk.IsReserved == false)
                        {
                            if (DataDeletion.Delete(SelectedDesk))
                            {
                                Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == GetSelectedLocation().Id));
                                MessageBox.Show("Desk deleted");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Can't delete desk. \nThis desk is reserved");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No desk was selected. \nSelect a desk");
                    }
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
                if (AdministratorWindow.Instance != null && GetSelectedLocation() != null)
                {
                    DeskValidator Validator = new();
                    Desk.LocationId = GetSelectedLocation().Id;
                    Desk.LocationName = GetSelectedLocation().Name;
                    if (Validator.Validate(Desk))
                    {
                        if (DataInsertion.AddData(Desk))
                        {
                            Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == GetSelectedLocation().Id));
                            MessageBox.Show("New desk added");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void ShowManageDesksWindow()
        {
            if (GetSelectedLocation() != null)
            {
                ManageDesksWindow ManageDesksWindow = new();
                if (ManageDesksWindow.Instance != null)
                {
                    Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == GetSelectedLocation().Id));
                    ManageDesksWindow.Instance.DesksDataGrid.ItemsSource = Desks;
                }
                ManageDesksWindow.Show();
            }
            else
            {
                MessageBox.Show("No location was selected\nSelect a location");
            }
        }

        private void ChangeDeskAvailability()
        {
            if (AdministratorWindow.Instance != null && GetSelectedLocation() != null)
            {
                if (SelectedDesk != null)
                {
                    if (SelectedDesk.IsReserved == false)
                    {
                        if (SelectedDesk.IsAvailable == true)
                        {
                            SelectedDesk.IsAvailable = false;
                        }
                        else
                        {
                            SelectedDesk.IsAvailable = true;
                        }
                        if (DataUpdate.Update(SelectedDesk))
                        {
                            Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == GetSelectedLocation().Id));
                            MessageBox.Show("Availability changed");
                        }
                    }
                }
            }
        }
    }
}