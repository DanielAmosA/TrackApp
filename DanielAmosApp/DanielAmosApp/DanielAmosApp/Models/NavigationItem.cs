using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstApp.ViewModels
{

    /// <summary>
    /// We will declare the properties of the navigation item, such as the name, icon, and more.
    /// </summary>
    public class NavigationItem : INotifyPropertyChanged
    {
        private bool _isSelected;

        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string? ViewName { get; set; }

        private bool _isVisible = true;
        public bool RequiresAuth { get; set; }
        public bool HideWhenAuthenticated { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
