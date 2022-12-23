using HotDeskBookingSystem.DataBase;
using SQLite;
using System;
using System.Windows;

namespace HotDeskBookingSystem.ViewModels
{
    public class ReservationCancellation
    {
        /// <summary>
        /// Cancel desk's reservation
        /// </summary>
        /// <param name="selectedPersonDesk">Desk to cancel it's reservation</param>
        /// <returns>True if reservation is cancelled and data is saved in database, otherwise false</returns>
        public static bool CancelReservation(Desk selectedPersonDesk)
        {
            if (selectedPersonDesk != null)
            {
                DateTime? Today = DateTime.Today;
                if ((selectedPersonDesk.ReservationStartDate.Value - Today.Value).TotalHours >= 24)
                {
                    using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
                    {
                        selectedPersonDesk.IsAvailable = true;
                        selectedPersonDesk.IsReserved = false;
                        selectedPersonDesk.ReservationStartDate = null;
                        selectedPersonDesk.ReservationEndDate = null;
                        selectedPersonDesk.UserFullName = null;
                        selectedPersonDesk.PersonId = null;
                        DataUpdate.Update(selectedPersonDesk);
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("You can't change desk later than 24 hours before reservation");
                    return false;
                }
            }
            return false;
        }
    }
}