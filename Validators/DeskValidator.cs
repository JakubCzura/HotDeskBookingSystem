using CommunityToolkit.Diagnostics;
using HotDeskBookingSystem.Models;
using System;
using System.Windows;

namespace HotDeskBookingSystem.Validators
{
    public class DeskValidator : IValidator<Desk>
    {
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