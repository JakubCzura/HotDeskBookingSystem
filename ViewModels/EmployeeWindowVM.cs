using CommunityToolkit.Mvvm.Input;
using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Model;
using HotDeskBookingSystem.Models;
using HotDeskBookingSystem.Validators;
using HotDeskBookingSystem.Views.Windows;
using SQLite;
using System;
using System.Collections.Generic;
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
            Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());
            UserDesks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().Where(x => x.PersonId == LoggedPersonData.Id));
            DesksToFilter = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());
            Locations = new ObservableCollection<Location>(DataGetter<Location>.GetAllRows());
            Desk = new();
            NameFilter = string.Empty;
            Location = new();
            ShowReserveDeskWindowCommand = new RelayCommand(ShowReserveDeskWindow);
            ReserveDeskCommand = new RelayCommand(ReserveDesk);
            CancelReservationCommand = new RelayCommand(CancelReservation);
        }

        private static int selectedDeskIndex;
        /// <summary>
        /// There are some issues with refresh UI when using SelectedItem of DataGrid, so there is need to use SelectedIndex and the DataGrid can refresh it's SelectedItem
        /// </summary>
        public int SelectedDeskIndex
        {
            get { return selectedDeskIndex; }
            set { selectedDeskIndex = value; OnPropertyChanged(); }
        }


        public new Desk SelectedDesk
        {
            get
            {
                if (SelectedDeskIndex >= 0)
                {
                    return Desks[SelectedDeskIndex];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (SelectedDeskIndex >= 0)
                {
                    Desks[SelectedDeskIndex] = value; OnPropertyChanged();
                }
            }
        }

        public ICommand ShowReserveDeskWindowCommand { get; private set; }
        public ICommand ReserveDeskCommand { get; private set; }
        public ICommand CancelReservationCommand { get; private set; }

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

        private Desk selectedUserDesk;

        public Desk SelectedUserDesk
        {
            get { return selectedUserDesk; }
            set { selectedUserDesk = value; OnPropertyChanged(); }
        }

        private void ShowReserveDeskWindow()
        {
            // SelectedDesk = GetSelectedDesk();
            if(SelectedDesk != null)
            {
                if (SelectedDesk.IsAvailable == true)
                {
                    ReserveDeskWindow ReserveDeskWindow = new();
                    ReserveDeskWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("This desk has been already reserved");
                }
            }          
        }

        private void ReserveDesk()
        {
            try
            {
                DeskValidator DeskValidator = new();
                SelectedDesk.PersonId = LoggedPersonData.Id;
                SelectedDesk.IsAvailable = false;
                SelectedDesk.IsReserved = true;
                if (DeskValidator.Validate(SelectedDesk))
                {
                    if (SelectedDesk.ReservationStartDate != null && SelectedDesk.ReservationEndDate != null)
                    {
                        //Check if ReservationStartDate is earlier or the same as ReservationEndDate
                        if (DateTime.Compare(SelectedDesk.ReservationStartDate.Value, SelectedDesk.ReservationEndDate.Value) <= 0)
                        {
                            // 0-6 allows to reserve a desk from 1 up to 7 days
                            if ((SelectedDesk.ReservationEndDate.Value - SelectedDesk.ReservationStartDate.Value).Days >= 0 &&
                                                (SelectedDesk.ReservationEndDate.Value - SelectedDesk.ReservationStartDate.Value).Days <= 6)
                            {
                                if (HaveDeskReservedInThisTimeSpan(SelectedDesk) == false)
                                {
                                    if (DataUpdate.Update(SelectedDesk))
                                    {
                                        UserDesks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().Where(x => x.PersonId == LoggedPersonData.Id));
                                        EmployeeWindow.Instance.PersonDesksDataGrid.ItemsSource = UserDesks;
                                        Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());
                                        EmployeeWindow.Instance.DesksDataGrid.ItemsSource = Desks;
                                        MessageBox.Show("The desk reserved");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error while reserving the desk");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("You have already reserved a desk in this time span");
                                }
                            }
                            else
                            {
                                MessageBox.Show("You can't reserve the desk for more than 7 days");
                            }
                        }
                        else
                        {
                            MessageBox.Show("You have chosen Reservation end date before Reservation start date");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}\nTry to reserve desk again", "Desk reservation error");
            }
        }

        private bool HaveDeskReservedInThisTimeSpan(Desk selectedDesk)
        {
            List<Desk> PersonDesks = DataGetter<Desk>.GetAllRows();
            if (PersonDesks != null)
            {
                PersonDesks = PersonDesks.Where(x => x.PersonId == LoggedPersonData.Id).ToList();
                foreach (Desk desk in PersonDesks)
                {
                    if (DateTime.Compare(selectedDesk.ReservationStartDate.Value, desk.ReservationStartDate.Value) >= 0
                        && DateTime.Compare(selectedDesk.ReservationStartDate.Value, desk.ReservationEndDate.Value) <= 0)
                    {
                        return true;
                    }
                    if (DateTime.Compare(selectedDesk.ReservationEndDate.Value, desk.ReservationStartDate.Value) >= 0
                        && DateTime.Compare(selectedDesk.ReservationEndDate.Value, desk.ReservationEndDate.Value) <= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void CancelReservation()
        {
            if(ReservationCancellation.CancelReservation(SelectedUserDesk))
            {
                UserDesks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows().Where(x => x.PersonId == LoggedPersonData.Id));
                Desks = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());
                DesksToFilter = new ObservableCollection<Desk>(DataGetter<Desk>.GetAllRows());

            }
        }
    }
}