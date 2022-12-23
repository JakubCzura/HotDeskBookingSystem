using HotDeskBookingSystem.DataBase;
using HotDeskBookingSystem.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace HotDeskBookingSystem.ViewModels
{
    public class LocationDeletion
    {
        public static bool DeleteLocation(Location selectedLocation, ObservableCollection<Desk> allDesks)
        {
            if (allDesks.Any(x => x.LocationId == selectedLocation.Id) == false)
            {
                if (DataDeletion.Delete(selectedLocation))
                {
                    return true;
                }
            }
            return false;
        }
    }
}