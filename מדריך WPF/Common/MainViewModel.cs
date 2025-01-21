public class MainViewModel
{
    public RelayCommand SaveCommand { get; }

    private bool _canSave;

    public bool CanSave
    {
        get => _canSave;
        set
        {
            _canSave = value;
            // עדכון מצב הפקודה
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public MainViewModel()
    {
        SaveCommand = new RelayCommand(Save, () => CanSave);
    }

    private void Save()
    {
        MessageBox.Show("Saved successfully!");
    }
}