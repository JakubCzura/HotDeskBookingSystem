using System;
using System.Collections.Generic;
using System.Linq;

namespace HotDeskBookingSystem.Models
{
    public class WorkersRoles
    {
        public enum Roles
        {
            Administrator,
            Employee
        }

        public static List<string> RolesList
        {
            get => Enum.GetValues(typeof(Roles))
                       .Cast<Roles>()
                       .Select(v => v.ToString())
                       .ToList();
        }
    }
}