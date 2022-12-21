using CommunityToolkit.Mvvm.Input;
using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Models;
using HotDeskBookingSystem.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Data;
using System.Windows.Input;

namespace HotDeskBookingSystem.ViewModels
{
    public class EmployeeWindowVM : MainWindowVM
    {
        public static EmployeeWindowVM? Instance { get; private set; }

        public EmployeeWindowVM()
        {
            Instance = this;
            Location = new();
            Desk = new();
            NameFilter = string.Empty;
            Locations = new ObservableCollection<Location>(DataGetter<Location>.GetAllRows());
            Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());
            DesksToFilter = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());
            ShowReserveDeskWindowCommand = new RelayCommand(() => 
            {
                Desk = GetSelectedDesk();
                ReserveDeskWindow ReserveDeskWindow = new();
                ReserveDeskWindow.ShowDialog();
            });
        }

        public ICommand ShowReserveDeskWindowCommand { get; private set; } 

        private ObservableCollection<Desk> desksToFilter;

        public ObservableCollection<Desk> DesksToFilter
        {
            get { return desksToFilter; }
            set { desksToFilter = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Desk> GetFilteredDesks(string locationName)
        {
            return new ObservableCollection<Desk>(Desks.ToList().Where(x => x.LocationName.Contains(locationName, StringComparison.OrdinalIgnoreCase)));
        }

        private string nameFilter;

        public string NameFilter
        {
            get { return nameFilter; }
            set 
            {
                nameFilter = value; 
                OnPropertyChanged(); 
                DesksToFilter = GetFilteredDesks(nameFilter);
            }
        }

        private static Desk GetSelectedDesk()
        {
            if (EmployeeWindow.Instance.DesksDataGrid.SelectedItem != null)
            {
                return EmployeeWindow.Instance.DesksDataGrid.SelectedItem as Desk;
            }
            else
            {
                return null;
            }
        }
    }
}