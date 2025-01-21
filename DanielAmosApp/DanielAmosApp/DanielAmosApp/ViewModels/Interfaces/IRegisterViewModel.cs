using System.Windows.Input;

namespace DanielAmosApp.ViewModels
{
    public interface IRegisterViewModel
    {
        string Email { get; set; }
        string EmailValidation { get; set; }
        string Name { get; set; }
        string NameValidation { get; set; }
        string Password { get; set; }
        string PasswordValidation { get; set; }
        ICommand RegisterCommand { get; }
    }
}