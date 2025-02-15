public class MainViewModel
    {
        public ICommand CustomButtonCommand { get; }

        public MainViewModel()
        {
            CustomButtonCommand = new RelayCommand(OnButtonClicked);
        }

        private void OnButtonClicked(object parameter)
        {
            // Logic for button click
            Console.WriteLine("Custom button clicked!");
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter) => _execute(parameter);
    }