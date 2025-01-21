using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

public class MainViewModel : INotifyPropertyChanged
{
    private object _currentView;
    private readonly Stack<object> _navigationHistory = new Stack<object>();

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
    public ICommand GoBackCommand { get; }

    public MainViewModel()
    {
        NavigateCommand = new RelayCommand(Navigate);
        GoBackCommand = new RelayCommand(GoBack, CanGoBack);

        // תצוגת ברירת מחדל
        CurrentView = new HomeViewModel();
    }

    private void Navigate(object parameter)
    {
        if (CurrentView != null)
        {
            // הוספת התצוגה הנוכחית להיסטוריה
            _navigationHistory.Push(CurrentView);
        }

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

    private void GoBack(object parameter)
    {
        if (_navigationHistory.Count > 0)
        {
            // חזרה לתצוגה הקודמת
            CurrentView = _navigationHistory.Pop();
        }
    }

    private bool CanGoBack(object parameter)
    {
        return _navigationHistory.Count > 0;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
