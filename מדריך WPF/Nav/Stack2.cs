private readonly Stack<object> _backStack = new Stack<object>();
private readonly Stack<object> _forwardStack = new Stack<object>();

public ICommand GoForwardCommand { get; }

public MainViewModel()
{
    NavigateCommand = new RelayCommand(Navigate);
    GoBackCommand = new RelayCommand(GoBack, CanGoBack);
    GoForwardCommand = new RelayCommand(GoForward, CanGoForward);

    CurrentView = new HomeViewModel();
}

private void Navigate(object parameter)
{
    if (CurrentView != null)
    {
        _backStack.Push(CurrentView);
        _forwardStack.Clear(); // אם עוברים לניווט חדש, מחיקת ההיסטוריה קדימה
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
    if (_backStack.Count > 0)
    {
        _forwardStack.Push(CurrentView);
        CurrentView = _backStack.Pop();
    }
}

private bool CanGoBack(object parameter) => _backStack.Count > 0;

private void GoForward(object parameter)
{
    if (_forwardStack.Count > 0)
    {
        _backStack.Push(CurrentView);
        CurrentView = _forwardStack.Pop();
    }
}

private bool CanGoForward(object parameter) => _forwardStack.Count > 0;