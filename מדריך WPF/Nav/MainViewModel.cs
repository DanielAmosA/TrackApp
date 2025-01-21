

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

    private void ChangeTab(object parameter)
    {
        if (parameter is string tabName)
        {
            // שליחה מותאמת אישית של פרמטרים או פקודות
            if (tabName == "Tab 1")
                CurrentContent = new Tab1ViewModel();
            else if (tabName == "Tab 2")
                CurrentContent = new Tab2ViewModel();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

