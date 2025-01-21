public class MainViewModel : INotifyPropertyChanged
    {
        private bool _isPopupOpen;

        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                _isPopupOpen = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenPopupCommand => new RelayCommand(OpenPopup);
        public ICommand ClosePopupCommand => new RelayCommand(ClosePopup);

        private void OpenPopup()
        {
            IsPopupOpen = true;
        }

        private void ClosePopup()
        {
            IsPopupOpen = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }