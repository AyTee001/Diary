using System.ComponentModel;

namespace Diary.ViewModels.Abstract
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string property) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
