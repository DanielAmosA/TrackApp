using DanielAmosApp.Models;
using DanielAmosApp.Services.Interfaces;
using DanielAmosApp.Utills.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DanielAmosApp.Services.Implementation;
using System.Windows.Navigation;

namespace DanielAmosApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel, IRegisterViewModel
    {
        private string name;
        private string email;
        private string password;
        private string nameValidation;
        private string emailValidation;
        private string passwordValidation;
        readonly IUsersService userService;
        private readonly Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        private readonly LoggerModel loggerModel;

        private ICommand? registerCommand;

        public RegisterViewModel(IUsersService userService)
        {
            this.userService = userService;
            loggerModel = new LoggerModel();
        }

        public string Name
        {
            get => name;
            set
            {
                if (SetField(ref name, value))
                {
                    ValidateName();
                    ((RelayCommand)RegisterCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (SetField(ref email, value))
                {
                    ValidateEmail();
                    ((RelayCommand)RegisterCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (SetField(ref password, value))
                {
                    ValidatePassword();
                    ((RelayCommand)RegisterCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string NameValidation
        {
            get => nameValidation;
            set => SetField(ref nameValidation, value);
        }

        public string EmailValidation
        {
            get => emailValidation;
            set => SetField(ref emailValidation, value);
        }

        public string PasswordValidation
        {
            get => passwordValidation;
            set => SetField(ref passwordValidation, value);
        }

        public ICommand RegisterCommand =>
        registerCommand ??= new RelayCommand(async () => await ExecuteRegister(), CanRegister);

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

        private bool ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailValidation = "Email address is a required field.";
                return false;
            }

            if (!emailRegex.IsMatch(Email))
            {
                EmailValidation = "The email address is invalid.";
                return false;
            }

            EmailValidation = string.Empty;
            return true;
        }

        private bool ValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordValidation = "Password is a required field";
                return false;
            }

            if (Password.Length < 8)
            {
                PasswordValidation = "The password must contain at least 8 characters.";
                return false;
            }

            if (!Password.Any(char.IsUpper))
            {
                PasswordValidation = "The password must contain at least one uppercase letter.";
                return false;
            }

            if (!Password.Any(char.IsLower))
            {
                PasswordValidation = "The password must contain at least one lowercase letter.";
                return false;
            }

            if (!Password.Any(char.IsDigit))
            {
                PasswordValidation = "The password must contain at least one digit.";
                return false;
            }

            PasswordValidation = string.Empty;
            return true;
        }

        private bool CanRegister()
        {
            return ValidateName() && ValidateEmail() && ValidatePassword();
        }

        private async Task ExecuteRegister()
        {
            try
            {

                User user = new User
                {
                    Name = Name,
                    Email = Email,
                    Password = Password
                };

                List<User> users = await userService.UsersInsert(user);
                if (users != null)
                {
                    MessageBox.Show($"{users.ElementAt(0).Name} You have successfully registered!", "💎 Successful 💎", MessageBoxButton.OK, MessageBoxImage.Information);

                    Name = string.Empty;
                    Email = string.Empty;
                    Password = string.Empty;

                    loggerModel.CreateLogInfo("RegisterViewModel", $"Add new user : {Name} {Email} {Password}");
                }

                else
                {
                    MessageBox.Show("Registration failed.", "🧯 Error 🧯");
                    loggerModel.CreateErrorInfo("RegisterViewModel", $"Registration failed : {Name} {Email} {Password}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration error : {ex.Message}", "🧯 Error 🧯");
                loggerModel.CreateErrorInfo("RegisterViewModel", $"Registration failed : {ex.Message}");
            }
        }
    }
}
