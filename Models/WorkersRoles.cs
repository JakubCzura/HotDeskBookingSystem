using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
