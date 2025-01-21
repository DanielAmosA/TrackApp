using DanielAmosApp.Models;
using DanielAmosApp.Services.Interfaces;
using DanielAmosApp.Utills.Common;
using DanielAmosApp.ViewModels.Interfaces;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DanielAmosApp.ViewModels
{
    public class MachineViewModel : BaseViewModel, IMachineViewModel
    {
        private string machineName;
        private string machineType;
        private string machineIcon;
        private string machineID;
        private string selectedStatus;

        private ICommand? deleteCommand;
        private ICommand? saveCommand;

        private readonly IMachinesService machinesService;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly LoggerModel logger;

        public MachineViewModel(IMachinesService machinesService)
        {
            StatusOptions = new ObservableCollection<string>
            {
                "Running",
                "Offline",
                "Idle"
            };
             this.machinesService =  machinesService;
             this.logger = new LoggerModel();
        }


        public ICommand DeleteCommand =>
         deleteCommand = new RelayCommand(async () => await ExecutDelete());

        public ICommand SaveCommand =>
            saveCommand ??= new RelayCommand(async () => await ExecutSeaveChanges());

        public string MachineName
        {
            get => machineName;
            set
            {
                machineName = value;
                OnPropertyChanged(nameof(MachineName));
            }
        }

        public string MachineType
        {
            get => machineType;
            set
            {
                machineType = value;
                OnPropertyChanged(nameof(MachineType));
                UpdateMachineIcon(); // עדכון האייקון בהתאם לסוג המכונה
            }
        }

        public string MachineIcon
        {
            get => machineIcon;
            private set
            {
                machineIcon = value;
                OnPropertyChanged(nameof(MachineIcon));
            }
        }

        public ObservableCollection<string> StatusOptions { get; }

        public string SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                OnPropertyChanged(nameof(SelectedStatus));
            }
        }

        private void UpdateMachineIcon()
        {
            // כאן תוכל להוסיף לוגיקה להתאמת האייקון לפי סוג המכונה
            MachineIcon = MachineType?.ToLower() switch
            {
                "Running" => "WashingMachine",
                "Offline" => "Tumble",
                "Idle" => "Iron",
                _ => "Machine"
            };
        }

        private async Task ExecutSeaveChanges()
        {
            try
            {

                List<Models.Machine> machines = await machinesService.MachinesUpdate(1,machineName,"a",Utills.Common.MachineType.Robot,1,"a");
                if (machines != null)
                {
                    MessageBox.Show($"{machines.ElementAt(0).Name} You have successfully Update!", "💎 Successful 💎", MessageBoxButton.OK, MessageBoxImage.Information);

                    machineName = string.Empty;

                    logger.CreateLogInfo("MachineViewModel", $"Update machine : {machineName} ");
                }

                else
                {
                    MessageBox.Show("Update failed.", "🧯 Error 🧯");
                    logger.CreateErrorInfo("MachineViewModel", $"Update failed : {machineName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update error : {ex.Message}", "🧯 Error 🧯");
                logger.CreateErrorInfo("MachineViewModel", $"Update failed : {ex.Message}");
            }

        }

        private async Task ExecutDelete()
        {
            try
            {

                string? res = await machinesService.MachinesDelete(1);
                if (res != null)
                {
                    MessageBox.Show($" You have successfully Delete !", "💎 Successful 💎", MessageBoxButton.OK, MessageBoxImage.Information);

                    machineName = string.Empty;

                    logger.CreateLogInfo("MachineViewModel", $"Delete machine : {machineName} ");
                }

                else
                {
                    MessageBox.Show("Update failed.", "🧯 Error 🧯");
                    logger.CreateErrorInfo("MachineViewModel", $"Delete failed : {machineName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update error : {ex.Message}", "🧯 Error 🧯");
                logger.CreateErrorInfo("MachineViewModel", $"Delete failed : {ex.Message}");
            }

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
