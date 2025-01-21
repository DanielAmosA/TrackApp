using DanielAmosApp.Models;
using System.Collections.ObjectModel;

namespace DanielAmosApp.ViewModels
{
    public interface IAddViewModel
    {
        ObservableCollection<string> MachineTypes { get; set; }
        string SelectedMachineType { get; set; }
        Status SelectedStatus { get; set; }
        Status Status { get; set; }
        ObservableCollection<Status> Statuses { get; set; }
    }
}