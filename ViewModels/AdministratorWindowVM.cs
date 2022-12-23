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
        public AdministratorWindowVM()
        {
            NewLocation = new();
            Desk = new();
            Locations = new ObservableCollection<Location>(DataGetter<Location>.GetAllRows());
            //AdministratorWindow.Instance.LocationsDataGrid.ItemsSource = Locations;
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

        private static int selectedLocationIndex;

        /// <summary>
        /// There are some issues with refresh UI when using SelectedItem of DataGrid, so there is need to use SelectedIndex and the DataGrid can refresh it's SelectedItem
        /// </summary>
        public int SelectedLocationIndex
        {
            get { return selectedLocationIndex; }
            set { selectedLocationIndex = value; OnPropertyChanged(); }
        }

        public new Location SelectedLocation
        {
            get
            {
                if (SelectedLocationIndex >= 0)
                {
                    return Locations[selectedLocationIndex];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (SelectedLocationIndex >= 0)
                {
                    Locations[selectedLocationIndex] = value; OnPropertyChanged();
                }
            }
        }

        public string ManageDeskForLocation
        {
            get { return $"Manage desks for selected location: {SelectedLocation.Name}"; }
        }

        private void AddNewLocation()
        {
            try
            {
                if (AdministratorWindow.Instance != null)
                {
                    LocationValidator Validator = new();
                    if (Validator.Validate(NewLocation))
                    {
                        if (DataInsertion.AddData(NewLocation))
                        {
                            Locations = new ObservableCollection<Location>(DataGetter<Location>.GetAllRows());
                            AdministratorWindow.Instance.LocationsDataGrid.ItemsSource = Locations;
                            MessageBox.Show("New location added");
                        }
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
                if (AdministratorWindow.Instance != null && SelectedLocation != null)
                {
                    if (Desks.Any(x => x.LocationId == SelectedLocation.Id) == false)
                    {
                        if (DataDeletion.Delete(SelectedLocation))
                        {
                            Locations = new ObservableCollection<Location>(DataGetter<Location>.GetAllRows());
                            Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());
                            AdministratorWindow.Instance.LocationsDataGrid.ItemsSource = Locations;
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
                if (AdministratorWindow.Instance != null && SelectedLocation != null)
                {
                    if (SelectedDesk != null)
                    {
                        if (SelectedDesk.IsReserved == false)
                        {
                            if (DataDeletion.Delete(SelectedDesk))
                            {
                                Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == SelectedLocation.Id));
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
                if (AdministratorWindow.Instance != null && SelectedLocation != null)
                {
                    DeskValidator Validator = new();
                    Desk.LocationId = SelectedLocation.Id;
                    Desk.LocationName = SelectedLocation.Name;
                    if (Validator.Validate(Desk))
                    {
                        if (DataInsertion.AddData(Desk))
                        {
                            Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == SelectedLocation.Id));
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
            if (SelectedLocation != null)
            {
                ManageDesksWindow ManageDesksWindow = new();
                if (ManageDesksWindow.Instance != null)
                {
                    Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == SelectedLocation.Id));
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
            if (AdministratorWindow.Instance != null && SelectedLocation != null)
            {
                if (DeskAvailabilityChanger.ChangeDeskAvailability(SelectedDesk))
                {
                    Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().ToList().Where(x => x.LocationId == SelectedLocation.Id));
                    MessageBox.Show("Availability changed");
                }
                else
                {
                    MessageBox.Show("Availability can't be changed if the desk is reserved");
                }
            }
        }
    }
}