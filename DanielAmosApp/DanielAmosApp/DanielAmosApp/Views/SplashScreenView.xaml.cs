using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DanielAmosApp.Views
{
    /// <summary>
    /// Interaction logic for SplashScreenView.xaml
    /// </summary>
    public partial class SplashScreenView : Window
    {

        public SplashScreenView()
        {
            InitializeComponent();
            Loaded += SplashScreenView_Loaded;
        }

        private async void SplashScreenView_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ViewModels.SplashScreenViewModel;

            // Logo animation
            var logoAnimation = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(5)))
            {
                RepeatBehavior = RepeatBehavior.Forever
            };
            LogoRotate.BeginAnimation(System.Windows.Media.RotateTransform.AngleProperty, logoAnimation);

            // App name animation
            var nameAnimation = new DoubleAnimation(1, 1.5, new Duration(TimeSpan.FromSeconds(1)))
            {
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            NameScale.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleXProperty, nameAnimation);
            NameScale.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleYProperty, nameAnimation);

            // Start loading process
            if (viewModel != null)
            {
                await viewModel.StartLoadingAsync();
            }
        }
    }
}
