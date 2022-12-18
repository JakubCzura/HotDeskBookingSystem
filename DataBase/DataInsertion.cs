using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotDeskBookingSystem.DataBase
{
    public class DataInsertion
    {
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
