   public partial class App : Application
   {
       private void Application_Startup(object sender, StartupEventArgs e)
       {
           // יצירת Splash Screen
           var splashScreen = new Views.SplashScreenView();

           // יצירת ViewModel ל-Splash Screen
           var splashViewModel = new ViewModels.SplashScreenViewModel();
           splashScreen.DataContext = splashViewModel;

           // הצגת ה-Splash Screen
           splashScreen.Show();

           // התחלת טעינה עם ה-ViewModel
           _ = splashViewModel.StartLoadingAsync(); // פונקציה אסינכרונית

           // MainWindow יופעל מתוך ה-ViewModel ברגע שהטעינה תסתיים
       }
   }