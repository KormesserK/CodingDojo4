   M S B A M L   `   `   `  ����A  =Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Server.ViewModelServer.ViewModel  5   	       Server.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Server.ViewModel     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � DataContext 5	      $   Server.ViewModel.MainViewModel  5
   
   .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Right=�6.   .+�5      �� $��Start��6   $��80��6%   $��Center=�60   $��10q�6K   ��6W   �� *StartBtnClickCmd+�� 5      $
��Stop��6   $��80��6$   $��Center=�6/   $��10q�6J   ��6V   �� *StopBtnClickCmd+5      �� 5   
   $��10q�6   $��1��6!   .@�5      �� $
��Chat��6   $��22��6$   $��Bottom=�60   .��5      �  � ColumnDefinitions 5      �� 5      $��2* �6+   �� 5      $��3* �6+    5      �� 5      $��40 �6(   �� 5      $��* �6(   �� 5      $��40 �6(   .+�5!      �� $��0��6   $��0��6)   $��Connected Users��69   $
��10,5q�6S   $��Center=�6a   �� 5"      $ 
Horizontal=�6"   $��0��6;   $��1��6H   .+�5#      �� $��Received Messages:��6    $
��10,5q�6=   $��Center=�6K   �� 5$      $��5q�6I   $��Center=�6T   ��6    �� *NoOfReceivedMessages+6T   5%      �� 5&      $��1��6   $��10q�6+   ��67   �� *Users+ �� SelectedItem 6U   �� *SelectedUser+6}   �� 5'      $
��Drop��6   $��3��6,   $��Center=�69   $��80��6T   $��Right=�6_   $��10q�6{   ��6�   �� *DropClientBtnClickCmd+�� 5)      $��1��6   $��1��6+   $��10q�6;   ��6G   �� *
Messages+6j   �� 5*      $��Save to Log��6   $��3��63   $��1��6A   $��Center=�6Q   $��80��6l   $��Right=�6w   $��10q�6�   ��6�   �� *SaveToLogBtnClickCmd+5+      5,      �� 5-      $
��Logs��6   .��5.      �  5/      �� 50      $��2* �6+   �� 51      $��3* �6+    53      �� 54      $��40 �6(   �� 55      $��* �6(   �� 56      $��40 �6(   .+�58      �� $��0��6   $��0��6)   $��	Log files��69   $
��10,5q�6M   $��Center=�6[   �� 5:      $��0��6   $��1��6)   $��Log Messages:��6:   $
��10,5q�6R   $��Center=�6`   �� 5;      $��1��6   $��10q�6+   ��67   �� *
LogFiles+ 6X   �� *SelectedLogFile+6�   �� 5<      $ 
Horizontal=�6!   $��3��6:   $��Right=�6G   .+�5=      �� $
��Show��6!   $��Center=�61   $��60��6L   $��Right=�6W   $
��5,10q�6s   ��6�   �� *OpenLogFileBtnClickCmd+�� 5>      $
��Drop��6!   $��Center=�60   $��60��6K   $��Right=�6V   $��
5,10,10,10q�6r   ��6�   �� *DropLogFileBtnClickCmd+5?      �� 5B      $��1��6   $��1��6+   $��2��6;   $��10q�6L   ��6X   �� *LogMessages+6~   5D      5E      5F      5G      5H      