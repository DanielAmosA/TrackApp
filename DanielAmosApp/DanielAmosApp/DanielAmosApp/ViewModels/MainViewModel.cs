using DanielAmosApp.Services.Implementation;
using DanielAmosApp.Services.Interfaces;
using DanielAmosApp.Utills.Common;
using DanielAmosApp.Views;
using MyFirstApp.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace DanielAmosApp.ViewModels
{
    public class MainViewModel : BaseViewModel, IDisposable
    {
       
        private readonly IStatusesService statusesService;
        private readonly IUsersService usersService;
        private readonly IMachinesService machinesService;
        private readonly INavigationService navigationService;
        private readonly IAuthService authService;

        private string? statusMessage;
        private BaseViewModel selectedViewModel;
        private ICommand? navigateCommand;
        private ICommand? goBackCommand;
        private ICommand? goForwardCommand;
        private ICommand? logoutCommand;
        public ICommand? customButtonCommand;
        private bool isLoading;


        private readonly ViewModelLocator locator;

        public MainViewModel(
            ViewModelLocator locator,
            IUsersService userService, IStatusesService statusService, IMachinesService machinesService,
                             INavigationService navigationService, IAuthService authService)
        {
            this.navigationService = navigationService;
            this.authService = authService;
            this.statusesService = statusService;
            this.usersService = userService;
            this.machinesService = machinesService;
            this.locator = locator;

            InitializeNavigationItems();

            if (selectedViewModel == null)
            {
                selectedViewModel = (BaseViewModel?)locator.AddViewModel;
            }

            

            navigationService.PropertyChanged += NavigationServicePropertyChanged!;
            authService.AuthenticationChanged += AuthServiceAuthenticationChanged!;
        }

        #region Initialize Members
        public INavigationService NavigationService => navigationService;
        public IAuthService AuthService => authService;

        public ObservableCollection<NavigationItem>? NavigationItems { get; private set; }

        public BaseViewModel SelectedViewModel
        {
            get => selectedViewModel;
            private set
            {
                if (selectedViewModel != value)
                {
                    selectedViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StatusMessage
        {
            get => statusMessage!;
            set
            {
                if (statusMessage != value)
                {
                    statusMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            private set
            {
                // When IsLoading changes, update the availability of the commands.
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged();                 
                    InvalidateCommands();
                }
            }
        }

        public ICommand NavigateCommand =>
                navigateCommand ??= new RelayCommand<string>(
                async param => await ExecuteNavigateAsync(param),
                CanNavigate);

        public ICommand GoBackCommand =>
                goBackCommand ??= new RelayCommand(
                    () => navigationService.GoBack(),
                    () => navigationService.CanGoBack && !IsLoading);

        public ICommand GoForwardCommand =>
                goForwardCommand ??= new RelayCommand(
                    () => navigationService.GoForward(),
                    () => navigationService.CanGoForward && !IsLoading);

        public ICommand LogoutCommand =>
           logoutCommand ??= new RelayCommand(ExecuteLogout);

        #endregion

        #region NavAction
        private void InitializeNavigationItems()
        {
            NavigationItems = new ObservableCollection<NavigationItem>
            {
                new NavigationItem
                {
                    Name = "Home",
                    Icon = "Home",
                    ViewName = "HomeView",
                    IsSelected = true,
                    RequiresAuth = false,
                },
                new NavigationItem
                {
                    Name = "Log In",
                    Icon = "Login",
                    ViewName = "LogInView",
                    RequiresAuth = false,
                },
                new NavigationItem
                {
                    Name = "Register",
                    Icon = "AccountPlus",
                    ViewName = "RegisterView",
                    RequiresAuth = false
                },
                new NavigationItem
                {
                    Name = "DashBoard",
                    Icon = "ViewDashboard",
                    ViewName = "DashBoardView",
                    RequiresAuth = true,
                },
                new NavigationItem
                {
                    Name = "Log Out",
                    Icon = "ExitRun",
                    ViewName = "LogOutConffirmView",
                    RequiresAuth = true
                },
                new NavigationItem
                {
                    Name = "Edit View",
                    Icon = "AccountEdit",
                    ViewName = "EditView",
                    RequiresAuth = true
                },
                new NavigationItem
                {
                    Name = "Add",
                    Icon = "Plus",
                    ViewName = "AddView",
                    RequiresAuth = true
                },
                new NavigationItem
                {
                    Name = "Search",
                    Icon = "Crosshairs",
                    ViewName = "SearchView",
                    RequiresAuth = true
                },
                new NavigationItem
                {
                    Name = "Information",
                    Icon = "InformationVariantBoxOutline",
                    ViewName = "InformationView",
                    RequiresAuth = false
                }
            };

            UpdateNavigationItemsVisibility();
        }

        // Hide items that require login when the user is not logged in
        // and hide items that are defined to be hidden when the user is logged in

        private void UpdateNavigationItemsVisibility()
        {
            foreach (var item in NavigationItems!)
            {
                item.IsVisible = (item.RequiresAuth && authService.IsAuthenticated) ||
                               (!item.RequiresAuth && !authService.IsAuthenticated);
            }
        }

        #endregion

        //public MainViewModel(INavigationService? navigationService = null, IAuthService? authService = null)
        //{
        //    ////////navigationService = navigationService ?? new NavigationService();
        //    ////////authService = authService ?? new AuthService();




        //    // הרשמה לשינויים


        //}

        // You have successfully logged out.
        private void ExecuteLogout()
        {
            authService.Logout();
            StatusMessage = "התנתקת בהצלחה";
        }

        // Updating the user interface upon login status change.
        private void AuthServiceAuthenticationChanged(object sender, bool isAuthenticated)
        {
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                OnPropertyChanged(nameof(NavigationService.IsAuthenticated));
                UpdateNavigationItemsVisibility();
            });
        }

        // Update the SelectedViewModel when the CurrentView changes.
        private void NavigationServicePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(INavigationService.CurrentView))
            {
                SelectedViewModel = navigationService.CurrentView;
            }
        }

        public void Dispose()
        {
            navigationService.PropertyChanged -= NavigationServicePropertyChanged!;
            authService.AuthenticationChanged -= AuthServiceAuthenticationChanged!;
        }
        

        private async Task ExecuteNavigateAsync(string viewName)
        {
            if (string.IsNullOrEmpty(viewName)) return;

            var navigationItem = NavigationItems!.FirstOrDefault(x => x.ViewName == viewName);
            if (navigationItem?.RequiresAuth == true && !authService.IsAuthenticated)
            {
                StatusMessage = "Login is required to access this page...";
                await ExecuteNavigateAsync("LogInView");
                return;
            }

            IsLoading = true;
            StatusMessage = "Loading ...";

            try
            {
                await Task.Run(() =>
                {
                    var newViewModel = viewName switch
                    {
                        "HomeView" => new HomeViewModel(),
                        //"LoginView" =>  locator.LogInViewModel,
                        //"RegisterView" => new RegisterViewModel(),
                        "InformationView" => new InformationViewModel(),
                        "DashBoardView" => new DashBoardViewModel(),
                        //"AddView" => new AddViewModel(),
                        "SearchView" => new SearchViewModel(),
                        "LogOutConffirmView" => new LogOutConffirmViewModel(),
                        
                        _ => SelectedViewModel
                    };

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        foreach (var item in NavigationItems!)
                        {
                            item.IsSelected = item.ViewName == viewName;
                        }

                        navigationService.NavigateTo(newViewModel);
                        SelectedViewModel = newViewModel;
                    });
                });

                StatusMessage = $"Loaded successfully : {navigationItem?.Name ?? viewName}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading : {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        //Checking the possibility of navigating to the next page.
        private bool CanNavigate(string viewName)
        {
            if (string.IsNullOrEmpty(viewName) || IsLoading) return false;

            var navigationItem = NavigationItems!.FirstOrDefault(x => x.ViewName == viewName);
            return navigationItem != null && (!navigationItem.RequiresAuth || authService.IsAuthenticated);
        }

        private void InvalidateCommands()
        {
            (NavigateCommand as RelayCommand<string>)?.RaiseCanExecuteChanged();
            (GoBackCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (GoForwardCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }





        private async Task LoadUsers()
        {
            try
            {
                //var users = await statusesService.StatusesGetAllStatus();
                var users2 = await machinesService.MachinesInsert("a", "b", MachineType.HumanOperated, 2, "c");
                var users3 = await machinesService.MachinesUpdate(28, "a", "b", MachineType.HumanOperated, 2, "d");
                //var users4 = await machinesService.MachinesGetMachinesByStatusId(3);
                //var users5 = await machinesService.MachinesDelete(21);
                //var users6 = await usersService.UsersGetUserByEmailAndPassword("c", "c");
                //var users7 = await usersService.UsersInsert("a","c","c");

            }
            catch (Exception ex)
            {
                // Handle error appropriately
                Debug.WriteLine($"Error loading users: {ex.Message}");
            }
        }











    }
}
