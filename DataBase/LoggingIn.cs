using HotDeskBookingSystem.Model;
using HotDeskBookingSystem.Models.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public class LoggingIn
    {
        public static Person? Login(string email, string password)
        {
            try
            {
                Person Person;
                using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
                {
                    Person = sqliteConnection.Table<Person>().FirstOrDefault(x => x.Email == email);
                    if (Person != null)
                    {
                        if (Hasher.VerifyPassword(password, Person.Password))
                        {
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
