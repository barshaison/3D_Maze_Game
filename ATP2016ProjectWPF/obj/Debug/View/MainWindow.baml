   M S B A M L   `   `   `  ����L  HATP2016ProjectWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:ATP2016ProjectWPF.View.ControlsATP2016ProjectWPF.View.Controls  5   	   "    ATP2016ProjectWPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   BControls-clr-namespace:ATP2016ProjectWPF.View.Controls     5   	   -   5          � Title$  
MainWindow��5	   	   $	��660��6   $	��660��6)   .��5      �  � ShowGridLines .   � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��2��6/   $��3q�6C    �� CornerRadius$ 3��6W   #��� ��6h   ._�5      o� -   5        $��	menu_name��5      |�.  .@�5      m� |�.  $
��File��6-   .@�5      m� -   5        $��	Save Maze��5      62   m� 5      -   5        $��	Load Maze��5      63   m� 5      -   5        $
��Exit��5      6-   5      m� 5      |�.  $��Options��6-   .@�5      m� -   5        $��
Properties��5      63   5      m� 5      |�.  $
��Help��6-   .@�5      m� -   5        $��Controls��5      61   m� 5       -   5        $��Markings��5       61   m� 5!      -	   5        $��
Game Story��5!      63   5"      m� 5#      -
   5        |�.  $��About��5#   -   5$      5%      5&      �� 5'   
   $��1��6   $��0��6   $��3q�6.   $ 3��6B   #��� ��6S   ._�5(      �� -   5        $��buttons_name��5(   +   |�.  .+�5)      �� -   5        $��btn_GenerateMaze��5)   +   |�.  $��Generate Maze��5+      �� 5,      -   5        $��displayMaze_name��5,      |�.  $��Display Maze��6C   6Z   �� 5-      -   5        $��solveMaze_name��5-      |�.  $��
Solve Maze��6W   �� 5.      -   5        $��displaySol_name��5.      |�.  $��Display Solution��6B   6]   �� 5/      -   5        $��	curr_maze��5/      $�� Current Maze:��6.   �� 50      -   5        $��CurrMaze��50      �� 51      -   5        $��
curr_floor��51      $�� Current Floor:��6/   �� 52      -   5        $��	CurrFloor��52      �� 53      -   5        $��up_floor��53      $��	   Go Up:��6-   �� 54      -   5        $��upFloor��54      $��   available��6-   �� 55      -   5        $��
down_floor��55      $��   Go Down:��6/   �� 56      -   5        $��	downFloor��56      $��   available��6/   59      5:      �� 5;   
   $��1��6   $��1��6   $��3q�6.   $ 3��6B   #��� ��6S   ._�5=      � -   5        $��	cnvs_main��5=      5?      5@      5A      5D      