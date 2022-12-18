using HotDeskBookingSystem.Model;
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
    public class LocationsGetter
    {
        public static ObservableCollection<Location>? GetAllLocations()
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new(DataBaseInformation.DataBaseFullPath))
                {
                    List<Location> LocationsList = sqliteConnection.Table<Location>().ToList();
                    ObservableCollection<Location> Locations= new ObservableCollection<Location>(LocationsList);
                    return Locations;
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
