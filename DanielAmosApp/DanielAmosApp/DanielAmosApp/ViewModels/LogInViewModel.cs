using DanielAmosApp.Services.Implementation;
using DanielAmosApp.Services.Interfaces;
using DanielAmosApp.Utills.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DanielAmosApp.ViewModels
{
    public class LogInViewModel : BaseViewModel, ILogInViewModel
    {
        private string? email;
        private string? password;
        private string? errorMessage;
        private ICommand? loginCommand;
        private readonly LoggerModel loggerModel;

        private readonly INavigationService navigationService;
        private readonly IAuthService authService;

        public LogInViewModel(INavigationService navigationService, IAuthService authService)
        {
            this.navigationService = navigationService;
            this.authService = authService;
            loggerModel = new LoggerModel();
        }

        public ICommand LoginCommand => loginCommand ??= new RelayCommand(async () => await ExecuteLogin());

        public string Password
        {
            get => password!;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private async Task ExecuteLogin()
        {
            try
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    ErrorMessage = "A email or password is required.";
                    return;
                }

                bool success = await authService.LoginAsync(Email, Password);
                if (success)
                {
                    ErrorMessage = string.Empty;
                    loggerModel.CreateLogInfo("LogInViewModel", $"User with email {Email} logged in");
                    // Navigate to the dashboard instead of the homepage.
                    navigationService.NavigateTo(new DashBoardViewModel());
                }
                else
                {
                    ErrorMessage = "Incorrect email or password.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Login error : {ex.Message}";
                loggerModel.CreateLogInfo("LogInViewModel", $"Login error - {ex.Message}");
            }
        }

        public string Email
        {
            get => email!;
            set
            {
                SetProperty(ref email, value);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string ErrorMessage
        {
            get => errorMessage!;
            set => SetProperty(ref errorMessage, value);
        }

        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(email);
        }

    }
}
