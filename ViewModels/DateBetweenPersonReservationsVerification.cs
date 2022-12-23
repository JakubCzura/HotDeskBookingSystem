using System;
using System.Collections.Generic;

namespace HotDeskBookingSystem.ViewModels
{
    public class DateBetweenPersonReservationsVerification
    {
        /// <summary>
        /// Check if selected time span is already reserved by person
        /// </summary>
        /// <param name="selectedDesk">Desk to verify it's resevation start date and reservation end date</param>
        /// <param name="personDesks">All desks of person</param>
        /// <returns>True if time span between selectedDesk's reservation start date and reservation end date is not already reserved by user</returns>
        public static bool HaveDeskReservedInThisTimeSpan(Desk selectedDesk, List<Desk> personDesks)
        {
            if (personDesks.Count > 0)
            {
                foreach (Desk desk in personDesks)
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
    }
}