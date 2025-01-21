namespace MyFirstApp.Utils.Custom
{
    public class CustomButton : Button
    {
        // Dependency Property for Button Size
        public static readonly DependencyProperty ButtonSizeProperty =
            DependencyProperty.Register("ButtonSize", typeof(double), typeof(CustomButton), new PropertyMetadata(100.0));

        public double ButtonSize
        {
            get => (double)GetValue(ButtonSizeProperty);
            set => SetValue(ButtonSizeProperty, value);
        }

        // Dependency Property for Button Color
        public static readonly DependencyProperty ButtonColorProperty =
            DependencyProperty.Register("ButtonColor", typeof(Brush), typeof(CustomButton), new PropertyMetadata(Brushes.Blue));

        public Brush ButtonColor
        {
            get => (Brush)GetValue(ButtonColorProperty);
            set => SetValue(ButtonColorProperty, value);
        }

        // Dependency Property for Command
        public static readonly DependencyProperty ButtonCommandProperty =
            DependencyProperty.Register("ButtonCommand", typeof(ICommand), typeof(CustomButton), new PropertyMetadata(null));

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }

        static CustomButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomButton), new FrameworkPropertyMetadata(typeof(CustomButton)));
        }

        protected override void OnClick()
        {
            base.OnClick();

            if (ButtonCommand != null && ButtonCommand.CanExecute(null))
            {
                ButtonCommand.Execute(null);
            }
        }
    }
}
