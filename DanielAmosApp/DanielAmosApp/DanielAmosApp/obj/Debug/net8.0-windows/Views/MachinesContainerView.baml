   M S B A M L   `   `   `  ����H  DDanielAmosApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:DanielAmosApp.ViewsDanielAmosApp.Views  5      C&clr-namespace:DanielAmosApp.ViewModelsDanielAmosApp.ViewModels  5	      /    )DanielAmosApp.Views.MachinesContainerView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:DanielAmosApp.Views     5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      /vm&clr-namespace:DanielAmosApp.ViewModels  5	        � DataContext  5      9   3DanielAmosApp.ViewModels.MachinesContainerViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�5      $��Center=�5      $��	0,10,0,20q�5      .+�5      ��    MaterialDesignRaisedButton#��[  6   $��running��5      $
��10,0q�5      ��5      �� *FilterCommand+.��5      �� $ 
Horizontal=�6   .+�5       '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ CheckCircle=�6.   $��0,0,8,0q�5!   ,   �� 5"      $��Running��6    5#      5$      �� 5&      #��[  6   $��offline��5(      $
��10,0q�5)      ��5'      �� *FilterCommand+.��5*      �� $ 
Horizontal=�6   .+�5+        $ CloseCircle=�6.   $��0,0,8,0q�5,   ,   �� 5-      $��Offline��6    5.      5/      �� 51      #��[  6   $
��idle��53      $
��10,0q�54      ��52      �� *FilterCommand+.��55      �� $ 
Horizontal=�6   .+�56        $ Wrench=�6.   $��0,0,8,0q�57   ,   �� 58      $
��Idle��6    59      5:      5;      �� 5>   
   $��1��6   $
��Auto=�5?      .��5@      �� $��Center=�5A      ��5@      �� *FilteredMachines+��5B      �� 5C      .R�5D      � 5E      ��5G      �� 5H      .R�5I      %   DanielAmosApp.Views.MachineView  $��10q�6,     5J   *   �� 5K      5M      5N      5O      5P      