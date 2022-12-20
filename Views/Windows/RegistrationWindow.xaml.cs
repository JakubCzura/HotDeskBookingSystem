using System.Windows;

namespace HotDeskBookingSystem.Views.Windows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public static RegistrationWindow? Instance { get; private set; }

        public RegistrationWindow()
        {
            InitializeComponent();
            Instance = this;
        }
    }
}