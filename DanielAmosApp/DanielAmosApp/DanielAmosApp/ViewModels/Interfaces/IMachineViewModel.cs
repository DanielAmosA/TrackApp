using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace DanielAmosApp.ViewModels.Interfaces
{
    public interface IMachineViewModel
    {
        string MachineIcon { get; }
        string MachineName { get; set; }
        string MachineType { get; set; }
        ICommand SaveCommand { get; }
        string SelectedStatus { get; set; }
        ObservableCollection<string> StatusOptions { get; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}