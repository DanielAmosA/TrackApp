   M S B A M L   `   `   `  ����H  DDanielAmosApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DanielAmosAppDanielAmosApp  5   	   9!clr-namespace:DanielAmosApp.ViewsDanielAmosApp.Views  5   	   C&clr-namespace:DanielAmosApp.ViewModelsDanielAmosApp.ViewModels  5   	       DanielAmosApp.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:DanielAmosApp     5   	   _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -views!clr-namespace:DanielAmosApp.Views  5   	   7
viewmodels&clr-namespace:DanielAmosApp.ViewModels  5   	     � Title$  My Application��5	      $	��750��6   $
��1200��6,      MaterialDesignBody#��[  5
   	     MaterialDesignPaper#��[ 5   	   $��Medium"�5   	   $��14(�5   	    � Icon$ /Assets/Images/Icon.png��5   	   $	��700��5   	   ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension   � 	Resources 5        SharedViewAnimation �� 
TargetType �� Triggers :� RoutedEvent �� TargetProperty W� From	 W� To	 g� Duration
 W� EasingFunction.  (System.Windows.Media.Animation.CubicEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode t� From	 t� To t� EasingFunction �� DataType+   %DanielAmosApp.ViewModels.AddViewModel!   DanielAmosApp.Views.AddView  ViewTransitionScaleBounce1   +DanielAmosApp.ViewModels.DashBoardViewModel'   !DanielAmosApp.Views.DashBoardView,   &DanielAmosApp.ViewModels.HomeViewModel"	   DanielAmosApp.Views.HomeView3
  -DanielAmosApp.ViewModels.InformationViewModel)   #DanielAmosApp.Views.InformationView-   'DanielAmosApp.ViewModels.LogInViewModel#   DanielAmosApp.Views.LogInView6   0DanielAmosApp.ViewModels.LogOutConffirmViewModel,   &DanielAmosApp.Views.LogOutConffirmView0   *DanielAmosApp.ViewModels.RegisterViewModel&    DanielAmosApp.Views.RegisterView.   (DanielAmosApp.ViewModels.SearchViewModel$   DanielAmosApp.Views.SearchView/   )DanielAmosApp.ViewModels.MachineViewModel%   DanielAmosApp.Views.MachineView9   3DanielAmosApp.ViewModels.MachinesContainerViewModel/   )DanielAmosApp.Views.MachinesContainerView��%�  &	       (�� r    58      *, +)7  (�� �    5;      *, +)7  (�� �    5>      *, +)7  (�� &    5A      *,
 +)7  (�� b    5D      *, +)7  (�� �    5G      *, +)7  (�� �    5J      *, +)7  (��     5M      *, +)7  (�� R    5P      *, +)7  (�� �    5S      *, +)7  �� 5   
   " >�6,    5      :� 5      $ UserControl.Loaded��6   .[�5      �� .��5      �� .��5      W� $ Opacity�5       05       16#   $	 0:0:0.3D�6*   
 5   "     5   &   $ EaseOut=�60   5!      t� 5"      $ Margin�5#       50,0,0,05$       06*   $	 0:0:0.3D�61    5%   "     5&   &   $ EaseOut=�60   5(      5)      5*      5+      :� 5,      $ UserControl.Unloaded��6   .[�5-      �� .��5.      �� .��5/      W� $ Opacity�50       151       06#   $	 0:0:0.2D�6*   52      53      54      56      �� 58   
   "  6   .R�59        8��  6   5:      �� 5;   
   "  6   .R�5<        8��  6"   5=      �� 5>   
   "  6   .R�5?      	  8��  6   5@      �� 5A   
   " 
 6   .R�5B        8��  6$   5C      �� 5D   
   "  6   .R�5E        8��  6   5F      �� 5G   
   "  6   .R�5H        8��  6'   5I      �� 5J   
   "  6   .R�5K        8��  6!   5L      �� 5M   
   "  6   .R�5N        8��  6   5O      �� 5P   
   "  6   .R�5Q        8��  6    5R      �� 5S   
   "  6   .R�5T        8��  6*   5U      5V      .��5Y      ]� $
��True��6   .+�5[   
   (  "MaterialDesignThemes.Wpf.ColorZone  $	��Top=�6#      Mode$ PrimaryDark=�5\   !   .  (MaterialDesignThemes.Wpf.ElevationAssist   	Elevation$	 Dp4=�5]   !   .��5^      ]� $
��True��6   $��70��6-   .+�5`      �� $
��Left=�6   $��/Assets/Images/Logo.png��5a      $��50��5b      $��16,10q�5c      $��Uniform=�5d      �� 5g      $��Right=�6    �� Orientation$ 
Horizontal=�5h      $
��16,0q�5i      .+�5j      ��   MaterialDesignIconButton#��[ 6    � ToolTip$ Go Back��5l      ��5k      �� *GoBackCommand+{�5m      �� *	CanGoBack+.��5n      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 	ArrowLeft=�62   5o      �� 5p      #��[ 6   $ 
Go Forward��5r      $	��8,0q�5s      ��5q      �� *GoForwardCommand+{�5t      �� *CanGoForward+.��5u        $ 
ArrowRight=�62   5v      5w      �� 5z      $��Right=�6   $b��\Success is like a cup of hot chocolate ☕️ – it's sweeter when everyone shares it! 🍫��5{      $��Center=�5|      $��16q�5}      $
��Wrap=�5~      $	��300��5      
�������$��Italic$�5�        5�      $ Standard=�6+   $��Center=�5�      $	��8 4q�5�         CornerRadius$ 2��5�       K� ZIndex$ 1��5�      $
��16 8q�5�      $	 Dp2=�5�      .��5�      ]� .+�5�      �� $ 
Horizontal=�6%   $��Center=�5�      $	��Top=�5�      .+�5�      �� ��6+   �� *NavigationItems+��5�   "   �� 5�   &   .R�5�   *   �� $ 
Horizontal=�65   5�   '   ��5�   "   �� 5�   &   .R�5�   *   	� $��4q�5�   $    d� 	IsChecked 5�   $   �� *
IsSelected+��5�   $   �� *DataContext.NavigateCommand+ �� RelativeSource ��  �� AncestorType
 Window��5�   $   �� *
ViewName+��5�   .   �� 5�   2   " 	�68      MaterialDesignTabRadioButton �� BasedOn# [ 6R    5�   6   �� 5�   :    �� Binding 6F   �� *	IsVisible+ �� Value True6d   .a�5�   >   ��  �� Property � y� �� Value$ Visible=�6[   5�   ;   �� 5�   :    6F   �� *	IsVisible+	 False6d   .a�5�   >   ��  � y�$ 	Collapsed=�6[   5�   ;   5�   3   .��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $��0 0 8 0q�5�   8    5�   J   �� *Icon+5�   8   �� 5�   2   ��6<   �� *Name+5�   /   5�   +   5�   '   5�      5�      5�      5�      5�      5�        5�   
   $��Bottom=�6#   $ PrimaryDark=�5�   "   $��16q�5�   "   .��5�      �� $"��© 2025 All rights reserved.��6   $��Center=�5�      5�      7  1MaterialDesignThemes.Wpf.Transitions.Transitioner  5�   
    �� SelectedIndex$ 0��6&   .@�5�      <  6MaterialDesignThemes.Wpf.Transitions.TransitionerSlide      ForwardWipe  5�      3  -MaterialDesignThemes.Wpf.Transitions.FadeWipe  5�      !   Duration$! 0:0:0.5h�6.   "   BackwardWipe" 5�      5  /MaterialDesignThemes.Wpf.Transitions.CircleWipe  5�      .��5�      � $��16q�6   .+�5�      #  MaterialDesignThemes.Wpf.Card  $��32q�6*   $	 Dp4=�5�      .��5�      �� ��6)   �� *SelectedViewModel+5�      5�      5�      5�      5�      5�      