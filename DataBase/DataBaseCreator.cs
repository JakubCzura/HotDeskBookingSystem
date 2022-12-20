using HotDeskBookingSystem.Model;
using HotDeskBookingSystem.Models;
using SQLite;
using System;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public class DataBaseCreator : DataBaseInformation
    {
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