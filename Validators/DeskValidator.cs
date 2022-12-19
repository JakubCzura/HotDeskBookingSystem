﻿using CommunityToolkit.Diagnostics;
using HotDeskBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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