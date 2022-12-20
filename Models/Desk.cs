using SQLite;
using System;

namespace HotDeskBookingSystem.Models
{
    public class Desk
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsReserved { get; set; } = false;

        public DateTime? ReservationStartDate { get; set; }

        public DateTime? ReservationEndDate { get; set; }

        public string? UserFullName = null;

        public int LocationId { get; set; }

        public int PersonId { get; set; }
    }
}