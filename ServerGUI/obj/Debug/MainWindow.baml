   M S B A M L   `   `   `  ����D  @ServerGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ServerGUI	ServerGUI  5   	   C&clr-namespace:ServerGUI.MVVM.ViewModelServerGUI.MVVM.ViewModel  5   	       ServerGUI.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ServerGUI     5   	   <	viewModel&clr-namespace:ServerGUI.MVVM.ViewModel     5   	   $	��620��5
   	   $	��980��6     � WindowStyle$
  None=�5   	    � 
ResizeMode$ NoResize=�5   	   
������  � AllowsTransparency .  � DataContext 5      ,  &ServerGUI.MVVM.ViewModel.MainViewModel  5   
   .��5      �� ���#272537 �� CornerRadius$ 20��5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5       � RowDefinitions 5      �� 5      $��75 �6    �� 5       .+�5$      �� $��Stellar��6   $��Center=�5%      $
��Left=�5&      
�������$��22(�5(      $��30,0,0,0q�5)      �� 5,      $��1��6   .+�5.      	� $
��Home��6   $��50��5/      
�������$��14(�51         MenuButtonTheme#��[  52      	� 54      $��Message Board��6   $��50��55      
�������$��14(�57      #��[  58      	� 5:      $��Settings��6   $��50��5;      
�������$��14(�5=      #��[  5>      5A      �� 5C      $	��250��6   $��40��5D      $��Center=�5E      $
��Left=�5F      $��5q�5G      $��1��5H        ModernTextbox#��[ 5I      �� 5K      $��1��6   $��1��5L      $��10q�5M      ��5N      �� *CurrentView+�� 5R      -   5         ��Name$ serverStatusLabel��5R      $��Server not started��6/   $
��Left=�5S      $	��Top=�6/   
�������$��1��5T   '   $��
589,20,0,0q�67   �� 5V      -   5        $ numUsersLabel��5V      $��0��6+   $��20(�67   $
��Left=�5W      $	��Top=�61   
�������$��1��5X   )   $��
42,198,0,0q�69   $��1��6M   �� 5Z      -   5        $ numReceivedMessages��5Z      $��0��61   $��20(�6=   $
��Left=�5[      $	��Top=�61   
�������$��1��5\   )   $��428,198,0,0q�69   $��1��6N    J� RenderTransformOrigin$ -2.018,0.475&�6[   �� 5^      -   5        $ latestMessage��5^      $��-��6+   $��20(�67   $
��Left=�5_      $	��Top=�61   
�������$��1��5`   (   $��
42,360,0,0q�68   $��1��6L   �� 5b      -   5        $ latestMessageReceivedTime��5b      $��-��67   $��20(�6C   $
��Left=�5c      $	��Top=�61   
�������$��1��5d   )   $��428,360,0,0q�69   $��1��6N   5h      5l      5n      