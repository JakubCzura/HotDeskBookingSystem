using HotDeskBookingSystem.Models.Interfaces;
using SQLite;

namespace HotDeskBookingSystem.Model
{
    public class Person : IPerson
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}