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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DanielAmosApp.Utills.Custom
{
    /// <summary>
    /// The class responsible for Custom a button.
    /// </summary>
    public class ActionButton : Button
    {
        // Dependency Property for Button Size
        public static readonly DependencyProperty ButtonSizeProperty =
            DependencyProperty.Register("ButtonSize", typeof(double), typeof(ActionButton), new PropertyMetadata(100.0));

        public double ButtonSize
        {
            get => (double)GetValue(ButtonSizeProperty);
            set => SetValue(ButtonSizeProperty, value);
        }

        // Dependency Property for Button Color
        public static readonly DependencyProperty ButtonColorProperty =
            DependencyProperty.Register("ButtonColor", typeof(Brush), typeof(ActionButton), new PropertyMetadata(Brushes.Blue));

        public Brush ButtonColor
        {
            get => (Brush)GetValue(ButtonColorProperty);
            set => SetValue(ButtonColorProperty, value);
        }

        // Dependency Property for Command
        public static readonly DependencyProperty ButtonCommandProperty =
            DependencyProperty.Register("ButtonCommand", typeof(ICommand), typeof(ActionButton), new PropertyMetadata(null));

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }

        static ActionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ActionButton), new FrameworkPropertyMetadata(typeof(ActionButton)));
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
