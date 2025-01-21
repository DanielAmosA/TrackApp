using DanielAmosApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Services.Interfaces
{
    public interface INavigationService : INotifyPropertyChanged
    {
        BaseViewModel CurrentView { get; }
        bool CanGoBack { get; }
        bool CanGoForward { get; }

        bool IsAuthenticated { get; set; }

        void NavigateTo(BaseViewModel viewModel);
        void GoBack();
        void GoForward();
        void OnUserLoggedIn();
        void OnUserLoggedOut();
    }
}
