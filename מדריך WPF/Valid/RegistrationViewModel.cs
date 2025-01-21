using System.ComponentModel;
using System.Windows.Input;

public class RegistrationViewModel : INotifyPropertyChanged
{
    private string _name;
    private string _email;
    private string _password;
    private string _confirmationPassword;
    private string _message;
    private bool _isSuccessMessageVisible;
    private bool _isErrorMessageVisible;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public string ConfirmationPassword
    {
        get => _confirmationPassword;
        set
        {
            _confirmationPassword = value;
            OnPropertyChanged(nameof(ConfirmationPassword));
        }
    }

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    public bool IsSuccessMessageVisible
    {
        get => _isSuccessMessageVisible;
        set
        {
            _isSuccessMessageVisible = value;
            OnPropertyChanged(nameof(IsSuccessMessageVisible));
        }
    }

    public bool IsErrorMessageVisible
    {
        get => _isErrorMessageVisible;
        set
        {
            _isErrorMessageVisible = value;
            OnPropertyChanged(nameof(IsErrorMessageVisible));
        }
    }

    public ICommand RegisterCommand { get; }

    public RegistrationViewModel()
    {
        RegisterCommand = new RelayCommand(Register, CanRegister);
    }

    private void Register()
    {
        // כאן מתבצע הלוגיקת רישום
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) 
			|| string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmationPassword))
        {
            ShowErrorMessage("נא למלא את כל השדות.");
            return;
        }

        if (Password != ConfirmationPassword)
        {
            ShowErrorMessage("הסיסמאות אינן תואמות.");
            return;
        }

        if (!IsValidEmail(Email))
        {
            ShowErrorMessage("כתובת האימייל אינה תקינה.");
            return;
        }

        ShowSuccessMessage("הרישום הצליח!");
    }

    private bool CanRegister()
    {
        return true;
    }

    private void ShowSuccessMessage(string message)
    {
        Message = message;
        IsSuccessMessageVisible = true;
        IsErrorMessageVisible = false;
    }

    private void ShowErrorMessage(string message)
    {
        Message = message;
        IsSuccessMessageVisible = false;
        IsErrorMessageVisible = true;
    }

    private bool IsValidEmail(string email)
    {
        // בדיקת תקינות כתובת האימייל (פשוטה)
        return email.Contains("@");
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

