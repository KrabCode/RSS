   M S B A M L   `   `   `  ���� >  :RSS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:RSSRSS      RSS.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     #localclr-namespace:RSS     -     �Name$  MainWindow1�� � Title$	 RSS��$	��400��$
��1000�� � Icon$ __TemplateIcon.ico�� � 
ResizeMode$ CanResizeWithGrip=� � WindowStartupLocation$ CenterScreen=�.���  � RowDefinitions �� $��40 ��� $��1* ��� $��20 � � ColumnDefinitions �� .+���  �� Orientation$ 
Horizontal=�$��0��$��LeftToRight=�$��Stretch=�$��Stretch=�.+��� -    ��Name$ btAdd��$��+��$��
5, 5, 0, 5q�$��10, 5, 10, 5q��� -   $ 	btRecheck��$	��↻��$��
5, 5, 0, 5q�$��10, 5, 10, 5q��� $��
5, 6, 5, 5q�	 �� Padding$	 
5, 5, 5, 5q�$��	↻ every���� -   
 ��Name$
 tbRecheckFrequency��$��Center=��� $��
5, 6, 5, 5q�$	 
5, 5, 5, 5q�$��hours���� -    ��Name$ tbNextRefreshAt��$��Right=�$	��0,5q�$	 
5, 5, 5, 5q�$��	next ↻:���� -    ��Name$ watchlistGui��$��1��$��Stretch=�$��Stretch=����	#FFF1F1F1���� .R��� $ 
Horizontal=�.+��� $ bt_refreshFeed��-   $��5,0,5,5q�$	��↻��
 � Tag �� *url+�� $��
5, 1, 5, 0q�$��last:���� ���� *backlogSize+�� $��
5, 0, 5, 0q����� *status+�� $ btOpenFolderInExplorer��-   $
��📂�� �� *url+�� $��4,0,2,0q����� *title+�� $��2,0,2,0q����� *folder+�� $��2,0,4,0q����� *url+�� $ bt_removeFeed��-	   $��5,0,5,5q�$
��🗑�� �� *url+�� -
   $
 	tbConsole��$��2��