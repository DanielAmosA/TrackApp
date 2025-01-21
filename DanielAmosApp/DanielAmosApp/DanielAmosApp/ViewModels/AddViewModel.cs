using DanielAmosApp.Models;
using DanielAmosApp.Services.Implementation;
using DanielAmosApp.Services.Interfaces;
using DanielAmosApp.Utills.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DanielAmosApp.ViewModels
{
    public class AddViewModel : BaseViewModel, IAddViewModel
    {
        private readonly IStatusesService statusesService;
        private readonly IMachinesService machinesService;
        private readonly LoggerModel loggerModel;
        private readonly AppActionModel appActionModel;
        private string name;
        private string nameValidation;
        private string description;
        private string descriptionValidation;
        private string notes;
        private string notesValidation;
        private ICommand? addCommand;


        private ObservableCollection<Status> statuses;
        private Status status;
        public ObservableCollection<Status> Statuses
        {
            get { return statuses; }
            set { statuses = value; OnPropertyChanged(); }
        }

        public Status Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }

        public Status SelectedStatus { get; set; }

        private MachineType machineType;
                   
        public AddViewModel(IStatusesService statusesService, IMachinesService machinesService)
        {
            this.statusesService = statusesService;
            this.machinesService = machinesService;
            this.loggerModel = new LoggerModel();
            this.appActionModel = new AppActionModel();
            InitializeData();
        }

        public string Name
        {
            get => name;
            set
            {
                if (SetField(ref name, value))
                {
                    ValidateName();
                    ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string NameValidation
        {
            get => nameValidation;
            set => SetField(ref nameValidation, value);
        }

        private bool ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameValidation = "Name is a required field.";
                return false;
            }

            if (Name.Length < 2)
            {
                NameValidation = "The name must contain at least 2 characters.";
                return false;
            }

            NameValidation = string.Empty;
            return true;
        }

        public string Description
        {
            get => description;
            set
            {
                if (SetField(ref description, value))
                {
                    ValidateDescription();
                    ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string DescriptionValidation
        {
            get => descriptionValidation;
            set => SetField(ref descriptionValidation, value);
        }

        private bool ValidateDescription()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                DescriptionValidation = "Description is a required field.";
                return false;
            }

            if (Description.Length < 2)
            {
                DescriptionValidation = "The description must contain at least 2 characters.";
                return false;
            }

            DescriptionValidation = string.Empty;
            return true;
        }

        public string Notes
        {
            get => notes;
            set
            {
                if (SetField(ref notes, value))
                {
                    ValidateNotes();
                    ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string NotesValidation
        {
            get => notesValidation;
            set => SetField(ref notesValidation, value);
        }

        private bool ValidateNotes()
        {
            if (string.IsNullOrWhiteSpace(Notes))
            {
                NotesValidation = "Notes is a required field.";
                return false;
            }

            if (Notes.Length < 2)
            {
                NotesValidation = "The notes must contain at least 2 characters.";
                return false;
            }

            NotesValidation = string.Empty;
            return true;
        }


        private ObservableCollection<string> machineTypes;
        private string selectedMachineType;

        public ObservableCollection<string> MachineTypes
        {
            get { return machineTypes; }
            set { machineTypes = value; OnPropertyChanged(); }
        }

        public string SelectedMachineType
        {
            get { return selectedMachineType; }
            set { selectedMachineType = value; OnPropertyChanged(); }
        }

        private async Task InitializeData()
        {
            Statuses = await appActionModel.GetItems<Status>(statusesService.StatusesGetAllStatus);
            MachineTypes = new ObservableCollection<string>
            {
                "Human Operated",
                "Robot",
            };
        }

        public ICommand AddCommand => addCommand ??= new RelayCommand(async () => await ExecuteAdd(),CanAdd);

        private bool isMachineTypeValid;
        public bool IsMachineTypeValid
        {
            get { return isMachineTypeValid; }
            set { isMachineTypeValid = value; OnPropertyChanged(); }
        }

        private bool isStatusValid;
        public bool IsStatusValid
        {
            get { return isStatusValid; }
            set { isStatusValid = value; OnPropertyChanged(); }
        }

        private bool CanAdd()
        {
            IsMachineTypeValid = !string.IsNullOrEmpty(SelectedMachineType);
            IsStatusValid = SelectedStatus != null;

            return ValidateName() && ValidateDescription() && ValidateNotes() & IsMachineTypeValid & IsStatusValid;
        }

        private async Task ExecuteAdd()
        {
            try
            {           
                MachineType machineType = MachineType.HumanOperated;
                switch (selectedMachineType)
                {
                    case "Human Operated":
                        machineType = MachineType.HumanOperated;
                        break;
                    case "Robot":
                        machineType= MachineType.Robot;
                        break;
                }
                List<Machine> machines = await machinesService.MachinesInsert(Name, Description, machineType, SelectedStatus.StatusID, Notes);
                if (machines != null)
                {
                    MessageBox.Show($"{machines.ElementAt(0).Name} You have successfully Add!", "💎 Successful 💎", MessageBoxButton.OK, MessageBoxImage.Information);

                    Name = string.Empty;
                    Description = string.Empty;
                    Notes = string.Empty;
                    loggerModel.CreateLogInfo("AddViewModel", $"Add new machine : {Name} {Description}");
                }

                else
                {
                    MessageBox.Show("Add failed.", "🧯 Error 🧯");
                    loggerModel.CreateErrorInfo("AddViewModel", $"addition failed : {Name} {Description}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Addition error : {ex.Message}", "🧯 Error 🧯");
                loggerModel.CreateErrorInfo("AddViewModel", $"Addition failed : {ex.Message}");
            }
        }






    }
}
