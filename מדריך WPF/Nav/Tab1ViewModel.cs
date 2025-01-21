public class Tab1ViewModel : INotifyPropertyChanged
{
    private string _data;
    public string Data
    {
        get { return _data; }
        set
        {
            _data = value;
            OnPropertyChanged(nameof(Data));
        }
    }

    public async Task LoadDataAsync()
    {
        // נבצע טעינה א-סינכרונית
        Data = await GetDataFromServerAsync();
    }

    private async Task<string> GetDataFromServerAsync()
    {
        // נ simולציה של חיבור לשרת
        await Task.Delay(2000); // חיקוי של זמן טעינה
        return "Data loaded from server!";
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}