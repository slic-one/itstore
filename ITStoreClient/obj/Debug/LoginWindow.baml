   M S B A M L   `   `   `  ����H  DITStoreClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ITStoreClientITStoreClient  5   	       ITStoreClient.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ITStoreClient     5   	     � WindowStyle$
  None=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � Title$ LoginWindow��5
   	   $��188.06��6   $��360.768��6-   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      $
��0.7* �6   �� 5      .+�5   
   �� -   5         ��Name$ textBoxLogin��5      $��10q�6(   $��16(�64   �� 5   
    J� IsHitTestVisible .  $��
Логін��6-   $��Center=�6:   $
��Left=�6U   $��10,0,0,0q�6p   
���������5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � y� �� Value$ 	Collapsed=�63   	 �� Triggers	 5      �� 5      
 �� Binding
 6&   �� *Text+ �� ElementName$ textBoxLogin�� �� Value  6Y   .a�5      ��  � y�$ Visible=�6;   5      5      5      G� 5   
   -   5         G�Name$ passwordBox��5   #   $��1��6   $��10q�68   $��20(�6D   �� 5    
   -   5         ��Name$ labelStatus��5       $��2��6   $;��5Введіть свій логін та пароль.��62   � 5"   
   $��3��6    � ColumnDefinitions 5#      �� 5$      �� 5%      .+�5'      �� -   5         ��Name$ buttonLogin��5'      $��Center=�6*   $��10q�6G   $
��20,0q�6S   $��Login��6b   6r   �� 5(      -   5        $ 
buttonQuit��5(   %   $��1��6   $��Center=�69   $��10q�6V   $
��20,0q�6b   $
��Quit��6q   6�   5)      5*      5+      