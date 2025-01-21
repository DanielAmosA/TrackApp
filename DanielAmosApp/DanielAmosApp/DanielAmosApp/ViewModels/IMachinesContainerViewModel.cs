using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace DanielAmosApp.ViewModels
{
    public interface IMachinesContainerViewModel
    {
        ICommand FilterCommand { get; }
        ObservableCollection<MachineViewModel> FilteredMachines { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}