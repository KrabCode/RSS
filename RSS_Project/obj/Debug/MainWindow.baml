   M S B A M L   `   `   `  ����>  :RSS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:RSSRSS  5   	       RSS.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:RSS     5   	   -   5          �Name$  MainWindow1��5   	    � Title$	 RSS��5   	   $	��400��6   $
��1000��6"    � Icon$ __TemplateIcon.ico��6F    � 
ResizeMode$ CanResizeWithGrip=�6`    � WindowStartupLocation$ CenterScreen=�6   .��5
      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��1* �6   �� 5      $��20 �6    � ColumnDefinitions 5   
   �� 5      .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��LeftToRight=�6;   $��Stretch=�6Y   $��Stretch=�6w   .+�5      �� -   5         ��Name$ btAdd��5      $��+��6$   $��
5, 5, 0, 5q�60   $��10, 5, 10, 5q�6D   6[   �� 5      -   5        $ 	btRecheck��5      $	��↻��6(   $��
5, 5, 0, 5q�64   $��10, 5, 10, 5q�6H   6_   �� 5      $��
5, 6, 5, 5q�6   	 �� Padding$	 
5, 5, 5, 5q�6,   $��	↻ every��6A   6R   �� 5      -   5        
 ��Name$
 tbRecheckFrequency��5      $��Center=�63   6}   �� 5      $��
5, 6, 5, 5q�6   $	 
5, 5, 5, 5q�6,   $��hours��6A   6P   5      �� 5   
   -   5         ��Name$ tbNextRefreshAt��5      $��Right=�6-   $	��0,5q�6I   $	 
5, 5, 5, 5q�6V   $��	next ↻:��6k   �� 5   
   -   5         ��Name$ watchlistGui��5       $��1��6   $��Stretch=�66   $��Stretch=�6T   ���	#FFF1F1F1��5      �� 5      .R�5      �� $ 
Horizontal=�6!   .+�5      �� $ bt_refreshFeed��64   -   5        $��5,0,5,5q�5   !   $	��↻��6L   
 � Tag 6u   �� *url+�� 5       $��
5, 1, 5, 0q�6$   $��last:��68   �� 5!      ��6#   �� *backlogSize+�� 5"      $��
5, 0, 5, 0q�6$   ��68   �� *status+�� 5#      $ btOpenFolderInExplorer��6#   -   5        $
��📂��5#   C    6u   �� *url+�� 5$      $��4,0,2,0q�6#   ��64   �� *title+�� 5%      $��2,0,2,0q�6#   ��64   �� *folder+�� 5&      $��2,0,4,0q�6#   ��64   �� *url+�� 5'      $ bt_removeFeed��64   -	   5        $��5,0,5,5q�5'   !   $
��🗑��6K    6t   �� *url+5(      5*      5,      �� 5-   
   -
   5        $
 	tbConsole��5-      $��2��6   65   5.      5/      