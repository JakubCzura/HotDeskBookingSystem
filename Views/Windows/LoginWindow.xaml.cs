using HotDeskBookingSystem.DataBase;
using System.Windows;

namespace HotDeskBookingSystem.Views.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static LoginWindow? Instance { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
            Instance = this;
            DataBaseCreator.CreateEmptyDataBase();
        }
    }
}