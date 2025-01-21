using DanielAmosApp.Data.Implementation;
using DanielAmosApp.Data.Interfaces;
using DanielAmosApp.Services.Implementation;
using DanielAmosApp.Services.Interfaces;
using DanielAmosApp.Utills.Common;
using DanielAmosApp.ViewModels;
using DanielAmosApp.ViewModels.Interfaces;
using DanielAmosApp.Views;
using danielAmosServer_Core.Helpers.DB;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace DanielAmosApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public IServiceProvider serviceProvider { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            string logURL = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                @"..\..\..\Logs\log-.txt"));

            // Serilog configuration.
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logURL, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Application started!");

            // Creating a Splash Screen
            var splashScreen = new Views.SplashScreenView();

            // Creating a ViewModel for the Splash Screen
            var splashViewModel = new ViewModels.SplashScreenViewModel();
            splashScreen.DataContext = splashViewModel;

            // Displaying the Splash Screen
            splashScreen.Show();

            // Starting the Loading with the ViewModel
            _ = splashViewModel.StartLoadingAsync();

            // The MainWindow will be launched from the ViewModel once the loading is complete.

            Task.Run(() =>
            {
                splashViewModel.StartLoadingAsync().ContinueWith(t =>
                {
                    // After we finished the waiting, we use DI to extract the MainWindow.
                    var mainWindow = serviceProvider.GetRequiredService<MainWindow>();

                    //Displaying the MainWindow and removing the Splash Screen.
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        mainWindow.Show();
                        Application.Current.MainWindow = mainWindow;
                        splashScreen.Close();
                    });
                });
            });

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        }

        

        public App()
        {
            DbConfigReader dbConfigReader = new DbConfigReader();
            string connectionString = dbConfigReader.GetConnectionString();

            DataHelper dataHelper = new DataHelper();
            AppActionModel appActionModel = new AppActionModel();

            var services = new ServiceCollection();
            services.AddSingleton<IUsersRepository>(sp =>
            new UsersRepository(connectionString, dataHelper, appActionModel));

            services.AddSingleton<IMachinesRepository>(sp =>
            new MachinesRepository(connectionString, dataHelper, appActionModel));

            services.AddSingleton<IStatusesRepository>(sp =>
            new StatusesRepository(connectionString, dataHelper, appActionModel));

            services.AddSingleton<IUsersService, UsersService>();
            services.AddSingleton<IMachinesService, MachinesService>();
            services.AddSingleton<IStatusesService, StatusesService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<ViewModelLocator>();

            services.AddTransient<ILogInViewModel, LogInViewModel>();
            services.AddTransient<IRegisterViewModel, RegisterViewModel>();
            services.AddTransient<IAddViewModel, AddViewModel>();
            services.AddTransient<IMachineViewModel, MachineViewModel>();
            services.AddTransient<IMachinesContainerViewModel, MachinesContainerViewModel>();

            services.AddScoped<AddViewModel>();
            services.AddScoped<BaseViewModel>();
            services.AddScoped<DashBoardViewModel>();
            services.AddScoped<HomeViewModel>();
            services.AddScoped<InformationViewModel>();
            //services.AddScoped<LogInViewModel>();
            services.AddScoped<LogOutConffirmViewModel>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<MachinesContainerView>();
            services.AddScoped<NavigationItemViewModel>();
            services.AddScoped<RegisterViewModel>();
            services.AddScoped<SearchViewModel>();
            services.AddScoped<SplashScreenViewModel>();

            services.AddScoped<AddView>();
            services.AddScoped<DashBoardView>();
            services.AddScoped<HomeView>();
            services.AddScoped<InformationView>();
            services.AddScoped<LogInView>();
            services.AddScoped<LogOutConffirmView>();
            services.AddScoped<AddView>();
            services.AddScoped<RegisterView>();
            services.AddScoped<SearchView>();
            services.AddScoped<SplashScreenView>();
            services.AddScoped<SplashScreenView>();

            services.AddSingleton<MainWindow>();

            serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Information("Application exited.");
            Log.CloseAndFlush();
            base.OnExit(e);
        }
    }

}
