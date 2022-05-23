using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Orden.ViewModels
{
    public class NotifyPropertyChangedImpl : INotifyPropertyChanged
    {
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // interface implemetation
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
