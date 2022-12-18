using SQLite;

namespace HotDeskBookingSystem.Models
{
    public class Location
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Floor { get; set; }

        public string Description { get; set; } = null!;
    }
}