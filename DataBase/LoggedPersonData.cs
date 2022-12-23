using HotDeskBookingSystem.Model;
using SQLite;
using System;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public class LoggedPersonData
    {
        /// <summary>
        /// It of user who has logged in
        /// </summary>
        public static int Id { get; set; } = 0;

        /// <summary>
        /// Get instance of logged person
        /// </summary>
        /// <returns>Instance of logged person</returns>
        public static Person? GetLoggedPersonData()
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
                {
                    if (Id > 0)
                    {
                        Person Person = sqliteConnection.Table<Person>().FirstOrDefault(x => x.Id == Id);
                        if (Person != null)
                        {
                            return Person;
                        }
                        throw new Exception("Person is not logged");
                    }
                    throw new Exception("Person is not logged");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return null;
            }
        }
    }
}