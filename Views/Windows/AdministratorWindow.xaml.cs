using System.Windows;

namespace HotDeskBookingSystem.Views.Windows
{
    /// <summary>
    /// Interaction logic for AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        public static AdministratorWindow? Instance { get; private set; }

        public AdministratorWindow()
        {
            InitializeComponent();
            Instance = this;
        }
    }
}