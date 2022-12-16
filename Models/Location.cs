namespace HotDeskBookingSystem.Models.Interfaces
{
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Floor { get; set; }

        public int PersonId { get; set; }
    }
}