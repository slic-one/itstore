   M S B A M L   `   `   `  ����H  DITStoreClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ITStoreClientITStoreClient  5   	   !    ITStoreClient.PaymentWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ITStoreClient     5   	     � WindowStyle$
  None=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � Title$ PaymentWindow��5
   	   $��212.903��6   $��369.678��60   .��5      � $	��203��6   $	��Top=�6    � ColumnDefinitions 5   
   �� 5      �� 5      $
��2.5* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      .+�5   
   �� -   5         ��Name$ 
labelToPay��5      $��До сплати:��6$   $��Center=�69   $��10q�6V   $��Center=�6b   �� 5   
   -   5        $ labelToPayValue��5       $��1��6   $
��0.00��69   $��Center=�6H   $��10q�6e   $��Center=�6q   �� 5   
   -   5        $ 
labelPayed��5      $��1��6   $��Внесено:��61   $��Center=�6D   $��10q�6a   $��Center=�6m   �� 5   
   -   5         ��Name$ textBox_submitted��5      $��1��6-   $��Center=�6=   $��24��6Z   $��10q�6f   $��1��6r   $��Center=�6   $	��120��6�    �� 	IsEnabled . 6�   �� 5   
   -   5        $ labelChange��5      $��2��6   $��Решта:��62   $��Center=�6C   $��10q�6`   $��Center=�6l   �� 5   
   -   5        $ labelChangeValue��5   -   $��2��6   $��1��6   $
��0.00��6G   $��Center=�6V   $��10q�6s   $��Center=�6   �� 5    
   -   5         ��Name$ 
buttonDone��5       $��Готово��6%   $��2��66   $��Center=�6J   $��10q�6g   $��3��6s   $��Center=�6�   $��75��6�   6�   5!      5"      