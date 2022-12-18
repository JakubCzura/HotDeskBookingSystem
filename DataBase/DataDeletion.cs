using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public class DataDeletion
    {
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
