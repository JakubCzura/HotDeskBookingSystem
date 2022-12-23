using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HotDeskBookingSystem.ViewModels
{
    /// <summary>
    /// Base class for view-model
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}