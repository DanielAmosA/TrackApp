using System.Windows.Input;

namespace DanielAmosApp.ViewModels
{
    public interface ILogInViewModel 
    {
        string Email { get; set; }
        string ErrorMessage { get; set; }
        ICommand LoginCommand { get; }
        string Password { get; set; }
    }
}