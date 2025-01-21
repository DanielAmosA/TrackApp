 public class RelayCommand<T> : ICommand
 {
     private readonly Action<T> _execute;
     private readonly Func<T, bool>? _canExecute;

     public RelayCommand(Action<T> execute, Func<T, bool>? canExecute = null)
     {
         _execute = execute ?? throw new ArgumentNullException(nameof(execute));
         _canExecute = canExecute;
     }

     public bool CanExecute(object? parameter)
     {
         if (_canExecute == null) return true;
         return parameter is T t ? _canExecute(t) : true;
     }

     public void Execute(object? parameter)
     {
         if (parameter is T t || parameter == null)
             _execute(parameter is T value ? value : default!);
     }

     public event EventHandler? CanExecuteChanged
     {
         add => CommandManager.RequerySuggested += value;
         remove => CommandManager.RequerySuggested -= value;
     }

     public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
 }