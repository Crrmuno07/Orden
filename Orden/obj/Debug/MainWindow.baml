   M S B A M L   `   `   `  ����	  Orden    Orden.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Icon$  /Img/favicon.ico��5   	    � Title$ SplashScreen��5	   	    � WindowStyle$
 None=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � ShowInTaskbar .  $	��600��5   	   $	��350��6    � 
ResizeMode$ NoResize=�6"   $��Arial Rounded MT Bold)�5   	    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $k epack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.ProgressBar.xaml?�6%   %    5      .��5      � -   5        $��
gridSplash��5   ,   ���#f6f6f6$��20q�6    .+�5   
   �� $��img\LogoCedex_V0.2.PNG��6   	 �� BitmapScalingMode$
	 Fant=�61   $
��Left=�6X   $	��Top=�6s   $	��100��6�   $��
10,274,0,0q�6�   �� 5   
   
 �� Orientation$
 Vertical=�6   $��Center=�6,   $��Center=�6I   $��5,5,5,5q�6d   .+�5      �� $��Orden de Desembolso��6   ���#4C4C4C$��Center=�6H   $��Center=�6e   $��24(�6�   $��0,0,0,0q�6�   �� 5      -   5         ��Name$ NombreAsistente��5      $��
Procesando��6/   ���#4C4C4C$��Center=�6Z   $��Center=�6w   $��48(�6�   $��0,0,0,0q�6�   � 5       � IsIndeterminate . ���#4C4C4C$��Center=�6G   $	��350��6d   $��10��6p   ���#f6f6f6���#f6f6f66�   5      � 5   
   -   5        $��
gridSelect��5   .   
�������$��50q�6"    � ColumnDefinitions 5      �� 5      $��* �6#    � RowDefinitions 5!      �� 5"      $��* �6    �� 5#      $��* �6    .+�5%      �� $��0��6   $��Center=�6!   $��0 80 0 0q�6>   $��Center=�6P   .��6k   #!SELECCIONE LA ORDEN DE DESEMBOLSO6�   �� 5&      -   5         ��Name$ cbOrders��5&      $	��250��6R   $��1��6^   $��	0 -80 0 0q�6l   $��Center=�6   $��Center=�6�      MaterialDesignComboBox#��[  6�   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Ordenes de Desembolso��6�   5'      5(      5)      