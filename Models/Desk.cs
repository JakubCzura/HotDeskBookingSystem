using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDeskBookingSystem.Models
{
    public class Desk
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsReserved { get; set; } = false;

        public int LocationId { get; set; }
    }
}
