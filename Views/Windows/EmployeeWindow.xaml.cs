using System.Windows;

namespace HotDeskBookingSystem.Views.Windows
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public static EmployeeWindow? Instance { get; private set; }

        public EmployeeWindow()
        {
            InitializeComponent();
            Instance = this;
        }
    }
}