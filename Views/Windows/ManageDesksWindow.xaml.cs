using System.Windows;

namespace HotDeskBookingSystem.Views.Windows
{
    /// <summary>
    /// Interaction logic for ManageDesksWindow.xaml
    /// </summary>
    public partial class ManageDesksWindow : Window
    {
        public static ManageDesksWindow? Instance { get; private set; }

        public ManageDesksWindow()
        {
            InitializeComponent();
            Instance = this;
        }
    }
}