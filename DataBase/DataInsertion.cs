using SQLite;
using System;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public class DataInsertion
    {
        /// <summary>
        /// Add row to table
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="Data">Data to add to table</param>
        /// <returns>True if successful, otherwise false</returns>
        public static bool AddData<T>(T Data) where T : class
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
                {
                    sqliteConnection.Insert(Data);
                }
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}\nTry to add information again", "Error while adding information");
                return false;
            }
        }
    }
}