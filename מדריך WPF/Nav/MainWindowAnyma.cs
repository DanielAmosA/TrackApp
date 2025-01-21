public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void NavigateToPage1(object sender, RoutedEventArgs e)
    {
        // הפעלת האנימציה של Fade-out לפני המעבר לדף החדש
        Storyboard fadeOutStoryboard = (Storyboard)Resources["FadeOutStoryboard"];
        fadeOutStoryboard.Completed += (s, args) =>
        {
            // ניווט לדף 1
            MainFrame.Navigate(new Page1());
            // הפעלת האנימציה של Fade-in לאחר ניווט לדף
            Storyboard fadeInStoryboard = (Storyboard)Resources["FadeInStoryboard"];
            fadeInStoryboard.Begin();
        };
        fadeOutStoryboard.Begin();
    }
}
