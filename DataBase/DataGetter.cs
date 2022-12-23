using SQLite;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public static class DataGetter<T> where T : class, new()
    {
        /// <summary>
        /// Gets data from all rows of table
        /// </summary>
        /// <returns>List <typeparamref name="T"/></returns>
        public static List<T> GetAllRows()
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
                {
                    List<T> Data = sqliteConnection.Table<T>().ToList();
                    //ObservableCollection<T> Data = new(DataList);
                    return Data;
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