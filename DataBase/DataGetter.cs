using HotDeskBookingSystem.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public static class DataGetter<T> where T : class, new()
    {
        public static ObservableCollection<T> GetAllRows()
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
                {
                    List<T> DataList = sqliteConnection.Table<T>().ToList();
                    ObservableCollection<T> Data = new(DataList);
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
