   M S B A M L   `   `   `  ����L  HATP2016ProjectWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:ATP2016ProjectWPF.ViewATP2016ProjectWPF.View  5   	   ,    &ATP2016ProjectWPF.View.SolutionDetails   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:ATP2016ProjectWPF.View     5   	     � Title$  SolutionDetails��5   	   $	��300��6!   $	��300��6.   .��5	      � #��� ��6    J� OpacityMask 5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ Black��6    � Offset$ 0Q�6-   � 5      $ 	#FF4D2323��6   $ 1Q�61   5       � RowDefinitions 5   
   �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $
��186* �6   �� 5      $
��331* �6   .+�5   
   �� $��0��6   $��0��5      $��Center=�5      $��  Maze Name:��5      $��25��6(   $��	0,19,0,10q�64   �� 5   
   -   5        $��txtbx_mazeName��5      $��0��5      $��1��5      $��3q�6"   #��� ��66   # � ��6�   �� 5!   
   $��1��6   $��0��5"      $��Center=�5#      $��  Algorithm:��5$      $��25��6(   $��	0,19,0,10q�64   �� 5%   
   -   5        $��comboBox��5%      $��1��6#   $��1��60   $��	0,37,0,28q�6@   .@�5&      �� -   5        $	��bfs��5&      $	��BFS��6&   64   �� 5'      -   5        $	��dfs��5'      $	��DFS��6&   64   5(      �� 5)   
   -   5        $��
btn_cancel��5)      $��2��5*      $��1��5+      $��3q�5,      $��Cancel��5.      �� 5/   
   -   5        $��	btn_solve��5/      $��2��50      $��0��51      $��3q�52      $��Solve��54      56      57      