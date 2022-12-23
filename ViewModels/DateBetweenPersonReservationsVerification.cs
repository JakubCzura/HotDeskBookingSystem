using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDeskBookingSystem.ViewModels
{
    public class DateBetweenPersonReservationsVerification
    {
        public static bool HaveDeskReservedInThisTimeSpan(Desk selectedDesk, List<Desk> personDesks)
        {
            if(personDesks.Count > 0)
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
