using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDeskBookingSystem.DataBase
{
    public class DataBaseInformation
    {
        public static string DataBaseName { get; } = "HotDeskBookingSystemDataBase.db";
        
        public static string BaseApplicationDirectory { get => Path.Combine(AppDomain.CurrentDomain.BaseDirectory); }

        public static string DataBaseFullPath { get => Path.Combine(BaseApplicationDirectory, DataBaseName); }

    }
}
