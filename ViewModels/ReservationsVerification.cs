using HotDeskBookingSystem.DataBase;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotDeskBookingSystem.ViewModels
{
    //Verify all desks' reservations and make desks available if ReservationsEndDate elapsed
    public class ReservationsVerification
    {
        public ReservationsVerification()
        {
            Today = DateTime.Now;
            Desks = GetDesksWithElapsedReservation();
            UpdateDesksWithElapsedReservationInDataBase();
        }

        private DateTime? Today { get; set; }
        private List<Desk> Desks { get; set; }

        /// <summary>
        /// Get List of desks whose reservation ended
        /// </summary>
        /// <returns>List of desks whose reservation ended</returns>
        private List<Desk> GetDesksWithElapsedReservation()
        {
            List<Desk> ElapsedDesks = DataGetter<Desk>.GetAllRows();

            using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
            {
                ElapsedDesks = ElapsedDesks.Where(x => x.ReservationEndDate.HasValue == true).ToList();
                return ElapsedDesks.Where(x => (x.ReservationEndDate.Value - Today.Value).Days < 0).ToList();
            }
        }

        /// <summary>
        /// Update desks with elapsed reservations in data base, make them available to be reserved again. 
        /// This method should be invoke before logging to provide current data
        /// </summary>
        public void UpdateDesksWithElapsedReservationInDataBase()
        {
            using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
            {
                foreach (Desk desk in Desks)
                {
                    desk.IsAvailable = true;
                    desk.IsReserved = false;
                    desk.ReservationStartDate = null;
                    desk.ReservationEndDate = null;
                    desk.UserFullName = null;
                    desk.PersonId = null;
                    DataUpdate.Update(desk);
                }
            }
        }
    }
}