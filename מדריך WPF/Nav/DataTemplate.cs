public class MainViewModel : INotifyPropertyChanged
{
    private object _currentView;

    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
        }
    }

    public ICommand NavigateCommand { get; }

    public MainViewModel()
    {
        NavigateCommand = new RelayCommand(Navigate);
        CurrentView = new HomeViewModel(); // תצוגת ברירת מחדל
    }

    private void Navigate(object parameter)
    {
        switch (parameter?.ToString())
        {
            case "Home":
                CurrentView = new HomeViewModel();
                break;
            case "Settings":
                CurrentView = new SettingsViewModel();
                break;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}