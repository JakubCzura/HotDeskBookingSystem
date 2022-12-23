using SQLite;
using System;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public class DataUpdate
    {
        /// <summary>
        /// Update data in table
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="Data">Data to update</param>
        /// <returns>True if successful, otherwise false</returns>
        public static bool Update<T>(T Data) where T : class
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
                {
                    sqliteConnection.Update(Data);
                }
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}\nTry to edit data again", "Error while editing data");
                return false;
            }
        }
    }
}