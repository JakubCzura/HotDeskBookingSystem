using CommunityToolkit.Diagnostics;
using HotDeskBookingSystem.Model;
using HotDeskBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotDeskBookingSystem.Validators
{
    public class LocationValidator : IValidator<Location>
    {
        public bool Validate(Location location)
        {
            try
            {
                Guard.IsNotNullOrWhiteSpace(location.Name);
                Guard.IsInRange<int>(location.Floor, int.MinValue, int.MaxValue);
                Guard.IsNotNullOrWhiteSpace(location.Description);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "An exception in Location's data");
                return false;
            }
        }
    }
}
