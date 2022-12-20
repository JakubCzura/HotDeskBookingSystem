using SQLite;
using System;

namespace HotDeskBookingSystem.Models
{
    public class Desk
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsAvailable { get; set; } = true;

        public bool IsReserved { get; set; } = false;

        public DateTime? ReservationStartDate { get; set; }

        public DateTime? ReservationEndDate { get; set; }

        //Person who reserves the desk
        public string? UserFullName { get; set; } = null;

        public int LocationId { get; set; }

        public int PersonId { get; set; }
    }
}