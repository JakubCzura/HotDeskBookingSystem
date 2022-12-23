using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Models;
using HotDeskBookingSystem.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotDeskBookingSystem.ViewModels
{
    public class DeskAvailabilityChanger
    {
        public static bool ChangeDeskAvailability(Desk selectedDesk)
        {

            if (selectedDesk != null)
            {
                if (selectedDesk.IsReserved == false)
                {
                    if (selectedDesk.IsAvailable == true)
                    {
                        selectedDesk.IsAvailable = false;
                    }
                    else
                    {
                        selectedDesk.IsAvailable = true;
                    }
                    if (DataUpdate.Update(selectedDesk))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
