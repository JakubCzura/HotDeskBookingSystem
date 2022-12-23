using CommunityToolkit.Diagnostics;
using System;
using System.Windows;

namespace HotDeskBookingSystem.Validators
{
    public class LocationValidator : IValidator<Location>
    {
        /// <summary>
        /// Validate Location's data
        /// </summary>
        /// <param name="location">Location to validate</param>
        /// <returns>True if data is correct, otherwise false</returns>
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
                MessageBox.Show(exception.Message, "An exception in location's data");
                return false;
            }
        }
    }
}