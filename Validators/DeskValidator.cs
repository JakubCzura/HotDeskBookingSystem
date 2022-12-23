using CommunityToolkit.Diagnostics;
using System;
using System.Windows;

namespace HotDeskBookingSystem.Validators
{
    public class DeskValidator : IValidator<Desk>
    {
        /// <summary>
        /// Validate Desk's data
        /// </summary>
        /// <param name="desk">Desk to validate</param>
        /// <returns>True if data is correct, otherwise false</returns>
        public bool Validate(Desk desk)
        {
            try
            {
                Guard.IsNotNullOrWhiteSpace(desk.Name);
                Guard.IsNotNullOrWhiteSpace(desk.LocationName);
                Guard.IsInRange<int>(desk.LocationId, 1, int.MaxValue);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "An exception in Desk's data");
                return false;
            }
        }
    }
}