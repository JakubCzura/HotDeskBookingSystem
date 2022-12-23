using HotDeskBookingSystem.Model;
using SQLite;
using System;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public class DataBaseCreator : DataBaseInformation
    {
        /// <summary>
        /// Creates empty database if not exists
        /// </summary>
        /// <returns>True is succesful, otherwise false</returns>
        public static bool CreateEmptyDataBase()
        {
            try
            {
                using (SQLiteConnection sqltieConnection = new(DataBaseFullPath))
                {
                    sqltieConnection.CreateTable<Desk>();
                    sqltieConnection.CreateTable<Person>();
                    sqltieConnection.CreateTable<Location>();
                    return true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\nRestart the application", "Database creation error");
                return false;
            }
        }
    }
}