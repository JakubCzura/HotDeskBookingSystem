using SQLite;
using System;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public class DataDeletion
    {
        /// <summary>
        /// Delete data from database
        /// </summary>
        /// <typeparam name="T">Type of data to delete</typeparam>
        /// <param name="Data">Data to delete</param>
        /// <returns>True if successful, otherwise false</returns>
        public static bool Delete<T>(T Data) where T : class
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
                {
                    sqliteConnection.Delete(Data);
                }
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}\nTry to delete data again", "Error while deleting data");
                return false;
            }
        }
    }
}