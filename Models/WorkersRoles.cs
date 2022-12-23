using System;
using System.Collections.Generic;
using System.Linq;

namespace HotDeskBookingSystem.Models
{
    public class WorkersRoles
    {
        /// <summary>
        /// Enum of people's roles in company
        /// </summary>
        public enum Roles
        {
            Administrator,
            Employee
        }

        /// <summary>
        /// Returns list of people's roles in company
        /// </summary>
        public static List<string> RolesList
        {
            get => Enum.GetValues(typeof(Roles))
                       .Cast<Roles>()
                       .Select(v => v.ToString())
                       .ToList();
        }
    }
}