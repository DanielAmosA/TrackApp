using DanielAmosApp.Services.Interfaces;
using DanielAmosApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.ConstrainedExecution;
using DanielAmosApp.Utills.Common;

namespace DanielAmosApp.Services.Implementation
{
    public class NavigationService : INavigationService, INotifyPropertyChanged
    {
        private readonly Stack<BaseViewModel> backStack;
        private readonly Stack<BaseViewModel> forwardStack;
        private BaseViewModel? currentView;
        private bool isAuthenticated;
        private readonly HashSet<Type> securedViews;
        public event PropertyChangedEventHandler? PropertyChanged;

        // Creating navigation services along with secure pages.
        public NavigationService()
        {
            backStack = new Stack<BaseViewModel>();
            forwardStack = new Stack<BaseViewModel>();
            securedViews = new HashSet<Type>
        {
            typeof(AddViewModel),
            typeof(SearchViewModel),
            typeof(DashBoardViewModel)
            };
        }

        //After updating the user's status,
        //we will check what is now accessible to the user.

        public bool IsAuthenticated
        {
            get => isAuthenticated;
            set
            {
                isAuthenticated = value;
                OnPropertyChanged();
                ValidateCurrentView();
            }
        }

        public bool CanGoBack => backStack.Count > 0;
        public bool CanGoForward => forwardStack.Count > 0;

        // If the user is not logged in and is on a secure page,
        // we will redirect them to the homepage.

        private void ValidateCurrentView()
        {
            if (!IsAuthenticated && CurrentView != null && IsSecuredView(CurrentView.GetType()))
            {              
                NavigateTo(new HomeViewModel());
                backStack.Clear();
                forwardStack.Clear();
            }
        }

        //Checking which page is secured.

        private bool IsSecuredView(Type viewModelType)
        {
            return securedViews.Contains(viewModelType);
        }

        //When navigating between pages,
        //we will also update the navigation caches.

        public BaseViewModel CurrentView
        {
            get => currentView!;
            private set
            {
                currentView = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanGoBack));
                OnPropertyChanged(nameof(CanGoForward));
            }
        }
        private readonly ViewModelLocator locator;


        public void NavigateTo(BaseViewModel viewModel)
        {
            //Check if the page is secure and the user is not logged in.
            //In this case, redirect to the login page.

            if (IsSecuredView(viewModel.GetType()) && !IsAuthenticated)
            {
                //NavigateTo(new LogInViewModel());
                return;
            }

            if (CurrentView != null)
            {
                backStack.Push(CurrentView);
                forwardStack.Clear();
            }
            CurrentView = viewModel;
        }

       
        public void GoBack()
        {
            if (!CanGoBack) return;


            var previousView = backStack.Peek();
            if (IsSecuredView(previousView.GetType()) && !IsAuthenticated)
            {
                // Remove the secure page from the history  
                // Try again with the next page in the history.

                backStack.Pop(); 
                GoBack();
            }

            forwardStack.Push(CurrentView);
            CurrentView = backStack.Pop();
        }

        public void GoForward()
        {
            if (!CanGoForward) return;

            var nextView = forwardStack.Peek();

            // Remove the secure page from the history  
            // Try again with the next page in the history.
            if (IsSecuredView(nextView.GetType()) && !IsAuthenticated)
            {
                forwardStack.Pop();
                GoForward(); 
                return;
            }

            backStack.Push(CurrentView);
            CurrentView = forwardStack.Pop();
        }

        // Upon login, we will update the user to logged in and redirect
        // to the dashboard page.

        public void OnUserLoggedIn()
        {
            IsAuthenticated = true;
            NavigateTo(new DashBoardViewModel());
        }

        //Upon logout, we will return to the homepage
        //and clear secure pages from the history.

        public void OnUserLoggedOut()
        {
            IsAuthenticated = false;
            NavigateTo(new HomeViewModel());
            backStack.Clear();
            forwardStack.Clear();

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
