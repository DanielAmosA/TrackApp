   M S B A M L   `   `   `  ����  DanielAmosApp*    $DanielAmosApp.Views.SplashScreenView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Splash Screen��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5	   	   
������ $	��500��5   	   $	��800��6    � Icon$ /Assets/Images/Icon.png��5   	   .��5      � ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,1&�63   .��5      �  � Color$ 	#FF001F3F��6    � Offset$	 0.0Q�61   � 5      $ 	#FF00AEEF��6   $	 1.0Q�61   5      .+�5   
   �� $	��200��6   $	��200��6   $��Center=�6)   $��Center=�6F   $%��/Assets/Images/SplashScreen.png��6a   z�5      �� 5      -   5        
LogoRotate��	 �� Angle$	 0Q�5   6   
 �� CenterX$	
 100Q�6@    �� CenterY$	 100Q�6N   5      �� 5   
   $��	TrackTrix��6   $��36(�6%   $
��Bold"�63   
�������$��Center=�5      $	��Top=�61   $��0,59,0,0q�6I   z�5       �� 5!      -   5        	NameScale�� �� ScaleX$ 1Q�5!   4    �� ScaleY$ 1Q�6?   5#      � 5&   
   -   5        $��
LoadingBar��5&      $	��600��6(   $��10��64    � Maximum$	 100Q�6b   $��Center=�5'      $��Bottom=�63   $��0,0,0,50q�6N    � Value 5&   @   �� *LoadingProgress+5'   N   �� 5*   
   $��16(�64   
�������$��Italic$�6U   $��Center=�5+      $��Bottom=�61   $��0,0,0,20q�6L   ��5*      �� *LoadingMessage+5+   L   �� 5,   
   $
��Left=�6   $	��100��6+   $��149,286,0,0q�68   $	��Top=�6M   $	��100��6e   5-      5.      