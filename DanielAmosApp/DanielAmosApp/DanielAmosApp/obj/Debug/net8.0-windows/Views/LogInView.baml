   M S B A M L   `   `   `  ����H  DDanielAmosApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:DanielAmosApp.ViewsDanielAmosApp.Views  5      O,clr-namespace:DanielAmosApp.Utills.ConvertorDanielAmosApp.Utills.Convertor  5	      O,clr-namespace:DanielAmosApp.Utills.BehaviorsDanielAmosApp.Utills.Behaviors  5
      #    DanielAmosApp.Views.LogInView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:DanielAmosApp.Views     5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      <	convertor,clr-namespace:DanielAmosApp.Utills.Convertor  5	      <	behaviors,clr-namespace:DanielAmosApp.Utills.Behaviors  5
        � 	Resources  5      <   6DanielAmosApp.Utills.Convertor.BooleanToColorConverter   BooleanToColorConverter��%   &	          5   
   6,   5      .��5      �� $	��300��6   $��20q�6   .+�5   
   �� $ ��Logging into the system : ��6    $  MaterialDesignHeadline5TextBlock#��[ 5      $��Center=�5      $��0,0,0,20q�5      �� 5   
   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Email : ��5       ! MaterialDesignOutlinedTextBox#��[ 5      $
��0,10q�5      ��5      �� *Email+ �� UpdateSourceTrigger$ PropertyChanged=�5      G� 5   
   -   5         G�Name$ PasswordBox��5      $ Password : ��5       % !MaterialDesignOutlinedPasswordBox#��[ 5       $
��0,10q�5!      6   0DanielAmosApp.Utills.Behaviors.PasswordBoxHelper   BindPassword$
 True��5#         BoundPassword 5"      �� *
Password+ �� Mode$ TwoWay=�5#      �� 5%   
   $��Log In��6     MaterialDesignPrimaryButton#���  5'      $
��0,20q�5(      ��5&      �� *LoginCommand+5(      �� 5*   
   
���  ��$
��Wrap=�5,      $��Center=�5-      $
��0,10q�5.      ��5*      �� *ErrorMessage+5.      5/      51      