using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotDeskBookingSystem.ViewModels
{
    //Verify all desks' reservations and make desks available if ReservationsEndDate elapsed
    public class ReservationsVerification
    {
        public ReservationsVerification()
        {
            Today = DateTime.Now;
            Desks = GetElapsedDesks();
            UpdateElapsedDesksInDataBase();
        }

        private DateTime? Today { get; set; }
        private List<Desk> Desks { get; set; }

        private List<Desk> GetElapsedDesks()
        {
            List<Desk> ElapsedDesks = DataGetter<Desk>.GetAllRows();

            using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
            {
                ElapsedDesks = ElapsedDesks.Where(x => x.ReservationEndDate.HasValue == true).ToList();
                return ElapsedDesks.Where(x => (x.ReservationEndDate.Value - Today.Value).Days < 0).ToList();
            }
        }

        public void UpdateElapsedDesksInDataBase()
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
