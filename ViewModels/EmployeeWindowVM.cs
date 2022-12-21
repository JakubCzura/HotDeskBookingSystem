using CommunityToolkit.Mvvm.Input;
using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Model;
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
    public class EmployeeWindowVM : MainWindowVM
    {
        public static EmployeeWindowVM? Instance { get; private set; }

        public EmployeeWindowVM()
        {
            Instance = this;
            Location = new();
            Desk = new();
            NameFilter = string.Empty;
            SelectedDesk = new();
            UserDesks = new();
            Locations = new ObservableCollection<Location>(DataGetter<Location>.GetAllRows());
            Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());
            DesksToFilter = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());
            UserDesks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().Where(x => x.PersonId == LoggedPersonData.Id));
            ShowReserveDeskWindowCommand = new RelayCommand(ShowReserveDeskWindow);
            ReserveDeskCommand = new RelayCommand(ReserveDesk);
            //DeskName = GetSelectedDesk().Name;
            //LocationName = GetSelectedDesk().LocationName;
        }

        public string DeskName
        {
            get { return GetSelectedDesk().Name; }
            set { GetSelectedDesk().Name = value; OnPropertyChanged(); }
        }

        public string LocationName
        {
            get { return GetSelectedDesk().LocationName; }
            set { GetSelectedDesk().LocationName = value; OnPropertyChanged(); }
        }

        //public string DeskName
        //{
        //    get { return SelectedDesk.Name; }
        //    set { SelectedDesk.Name = value; OnPropertyChanged(); }
        //}

        //public string LocationName
        //{
        //    get { return SelectedDesk.LocationName; }
        //    set { SelectedDesk.LocationName = value; OnPropertyChanged(); }
        //}

        public ICommand ShowReserveDeskWindowCommand { get; private set; }
        public ICommand ReserveDeskCommand { get; private set; }

        private ObservableCollection<Desk> userDesks;

        public ObservableCollection<Desk> UserDesks
        {
            get { return userDesks; }
            set { userDesks = value; OnPropertyChanged(); }
        }

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

        private void ShowReserveDeskWindow()
        {
            SelectedDesk = GetSelectedDesk();
            ReserveDeskWindow ReserveDeskWindow = new();
            ReserveDeskWindow.ShowDialog();
        }

        private void ReserveDesk()
        {
            try
            {
                DeskValidator DeskValidator = new();
                Person Person = LoggedPersonData.GetLoggedPersonData();
                SelectedDesk = GetSelectedDesk();
                SelectedDesk.UserFullName = $"{Person.Name} {Person.LastName}";
                SelectedDesk.Name = GetSelectedDesk().Name;
                SelectedDesk.LocationName = GetSelectedDesk().Name;
                SelectedDesk.LocationId = GetSelectedDesk().LocationId;
                SelectedDesk.PersonId = LoggedPersonData.Id;
                SelectedDesk.IsAvailable = false;
                SelectedDesk.IsReserved = true;
                if (DeskValidator.Validate(SelectedDesk))
                {
                    if (SelectedDesk.ReservationStartDate != null && SelectedDesk.ReservationEndDate != null)
                    {
                        // 0-6 allows to reserve a desk from 1 up to 7 days
                        if ((SelectedDesk.ReservationStartDate.Value - SelectedDesk.ReservationStartDate.Value).Days >= 0 &&
                            (SelectedDesk.ReservationStartDate.Value - SelectedDesk.ReservationStartDate.Value).Days <= 6)
                        {
                            if (DataUpdate.Update(SelectedDesk))
                            {
                                MessageBox.Show("The desk reserved");
                                MessageBox.Show(SelectedDesk.PersonId.ToString());
                                MessageBox.Show(SelectedDesk.LocationId.ToString());
                                UserDesks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().Where(x => x.PersonId == LoggedPersonData.Id));
                                EmployeeWindow.Instance.PersonDesksDataGrid.ItemsSource = UserDesks;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}\nTry to reserve desk again", "Desk reservation error");
            }
        }
    }
}