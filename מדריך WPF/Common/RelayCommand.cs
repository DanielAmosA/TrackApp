public class RelayCommand : ICommand
{
    private readonly Action _execute; // פעולה לביצוע
    private readonly Func<bool> _canExecute; // בדיקת האם ניתן לבצע

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    // קובע אם הפקודה זמינה
    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute();
    }

    // הפעולה שתתבצע
    public void Execute(object parameter)
    {
        _execute();
    }

    // אירוע שמודיע על שינוי במצב הפקודה
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}