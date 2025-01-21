using DanielAmosApp.Models;
using DanielAmosApp.Services.Interfaces;
using DanielAmosApp.Utills.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DanielAmosApp.ViewModels
{
    public class MachinesContainerViewModel : BaseViewModel, IMachinesContainerViewModel
    {

        private ObservableCollection<MachineViewModel> filteredMachines;
        private string _currentFilter;
        private readonly IMachinesService machinesService;
        private readonly LoggerModel loggerModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand FilterCommand { get; }

        public MachinesContainerViewModel(IMachinesService machinesService)
        {
            this.machinesService = machinesService;
            this.loggerModel = new LoggerModel();
            FilterCommand = new RelayCommand<string>(ExecuteFilter);
        }
      
        public ObservableCollection<MachineViewModel> FilteredMachines
        {
            get => filteredMachines;
            set
            {
                filteredMachines = value;
                OnPropertyChanged(nameof(FilteredMachines));
            }
        }

        //private void LoadMachines()
        //{
        //    // Here you would normally load from your data source
        //    //var machines = new List<Machine>
        //    //{
        //    //    new Machine
        //    //    {
        //    //        MachineID = 1,
        //    //        Name = "מכונה 1",
        //    //        MachineType = "מכונת כביסה",
        //    //        CurrentStatusId = 2,
        //    //    },
        //    //    new Machine
        //    //    {
        //    //       MachineID = 2,
        //    //        Name = "מכונה 3",
        //    //        MachineType = "מכונת כביסה",
        //    //        CurrentStatusId = 2
        //    //    },
        //    //    new Machine
        //    //    {
        //    //       MachineID = 1,
        //    //        Name = "מכונה 2",
        //    //        MachineType = "מכונת כביסה",
        //    //        CurrentStatusId = 3
        //    //    }
        //    //};

        //    //_allMachines = new ObservableCollection<MachineViewModel>(
        //    //    machines.Select(m => new MachineViewModel
        //    //    {
        //    //        MachineName = m.Name,
        //    //        MachineType = m.Description,
        //    //        SelectedStatus = m.Notes
        //    //    }));
        //}

        private void ExecuteFilter(string status)
        {
            
        }

        private async Task GetMachinesByStatus(string status)
        {
            try
            {
            
                List<Machine> machines = await machinesService.MachinesGetMachinesByStatus(status);
                if (machines != null)
                {
                    
                }

                else
                {
                    MessageBox.Show("There are no machines in this status.", "🧯 Error 🧯");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filter error : {ex.Message}", "🧯 Error 🧯");
                loggerModel.CreateErrorInfo("RegisterViewModel", $"Registration failed : {ex.Message}");
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
