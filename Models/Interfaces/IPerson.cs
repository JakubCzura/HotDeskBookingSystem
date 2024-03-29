﻿namespace HotDeskBookingSystem.Models.Interfaces
{
    /// <summary>
    /// Interface of person who works in company
    /// </summary>
    public interface IPerson
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}