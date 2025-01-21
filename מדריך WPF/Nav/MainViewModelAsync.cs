public class MainViewModel : INotifyPropertyChanged
{
    private object _currentContent;
    public object CurrentContent
    {
        get { return _currentContent; }
        set
        {
            _currentContent = value;
            OnPropertyChanged(nameof(CurrentContent));
        }
    }

    public ICommand ChangeTabCommand { get; private set; }

    public MainViewModel()
    {
        ChangeTabCommand = new RelayCommand(ChangeTab);
    }

    private async void ChangeTab(object parameter)
    {
        if (parameter is string tabName)
        {
            if (tabName == "Tab 1")
            {
                var tab1ViewModel = new Tab1ViewModel();
                await tab1ViewModel.LoadDataAsync(); // טעינה א-סינכרונית של נתונים
                CurrentContent = tab1ViewModel;
            }
            else if (tabName == "Tab 2")
            {
                // כאן אפשר להוסיף טעינה א-סינכרונית לדף אחר אם יש צורך
                CurrentContent = new Tab2ViewModel();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}