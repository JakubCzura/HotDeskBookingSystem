using HotDeskBookingSystem.Model;
using SQLite;
using System;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public class Registration
    {
        /// <summary>
        /// Register new user to application, add user to database
        /// </summary>
        /// <param name="person">Person instance to register</param>
        /// <returns>True if user is register and added to database, otherwise false</returns>
        public static bool Register(Person person)
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
                {
                    Person Person = person;
                    Person TemporaryPerson = sqliteConnection.Table<Person>().FirstOrDefault(x => x.Email == Person.Email);
                    if (TemporaryPerson != null)
                    {
                        MessageBox.Show($"User already exists", "This e-mail is already used");
                        return false;
                    }
                    sqliteConnection.Insert(Person);
                }
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}\nTry to sing up again", "Registration error");
                return false;
            }
        }
    }
}