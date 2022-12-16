using HotDeskBookingSystem.Model;
using HotDeskBookingSystem.Models;
using SQLite;
using System;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public class DataBaseCreator : DataBaseInformation
    {
        public static void CreateEmptyDataBase()
        {
            try
            {
                using (SQLiteConnection sqltieConnection = new (DataBaseFullPath))
                {
                    sqltieConnection.CreateTable<Administrator>();
                    sqltieConnection.CreateTable<Desk>();
                    sqltieConnection.CreateTable<Employee>();
                    sqltieConnection.CreateTable<Location>();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\nRestart the application", "Database creation error");
            }
        }
    }
}
