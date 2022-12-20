using System;
using System.IO;

namespace HotDeskBookingSystem.DataBase
{
    public class DataBaseInformation
    {
        public static string DataBaseName { get; } = "HotDeskBookingSystemDataBase.db";

        public static string BaseApplicationDirectory { get => Path.Combine(AppDomain.CurrentDomain.BaseDirectory); }

        public static string DataBaseFullPath { get => Path.Combine(BaseApplicationDirectory, DataBaseName); }
    }
}