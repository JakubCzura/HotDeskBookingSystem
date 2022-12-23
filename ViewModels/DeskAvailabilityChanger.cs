using HotDeskBookingSystem.DataBase;

namespace HotDeskBookingSystem.ViewModels
{
    public class DeskAvailabilityChanger
    {
        /// <summary>
        /// Change desk availability
        /// </summary>
        /// <param name="selectedDesk">Desk to change it's availability</param>
        /// <returns>True if desk's availability is changed and saved in database, otherwise false</returns>
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