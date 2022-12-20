using HotDeskBookingSystem.Model;
using SQLite;
using System;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public class LoggingIn
    {
        public static Person Login(string email, string password)
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
                {
                    Person Person = sqliteConnection.Table<Person>().FirstOrDefault(x => x.Email == email);
                    if (Person != null)
                    {
                        if (Hasher.VerifyPassword(password, Person.Password))
                        {
                            LoggedPersonData.Id = Person.Id;
                            return Person;
                        }
                        throw new Exception("Try to login again");
                    }
                    throw new Exception("Try to login again");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}", "Incorrect credentials");
                return null;
            }
        }
    }
}