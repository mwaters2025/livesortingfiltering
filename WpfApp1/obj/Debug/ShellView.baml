   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       WpfApp1.ShellView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	     � Title$  	ShellView��5	   	   $	��450��6   $	��800��6(    � 	Resources 5
         SortTypeDataSource S� 
ObjectType  System.Enum S� 
MethodName S� MethodParameters M� TypeName  FilterTypeDataSource  ListViewItemStyle �� 
TargetType �� Property �� Value��%X  &	        &	 s     &	 �     S� 5   
   "  5      $ 	GetValues��5       5      M� 5      $ local:SortType��6   5      S� 5   
   "  5      $ 	GetValues��5       5      M� 5      $ local:FilterType��6   5      �� 5   
   " ��6*   .�5      ��  � �� 6+   �� *Color+5      5      .��5      ]� .+�5   
   �� 	 �� Orientation$	 Vertical=�6    5       ��%S   '��       �� 5!      " ��6   .�5"      ��  � ��$	 2.5q�6/   5#      5$      .+�5%      �� $��Start��6   ��6%   �� *StartThreadCommand+�� 5&      $
��Sort��6   .��5'      �� 
 �� SelectionMode$
 Single=�6    �� SelectedValue 62   �� *
SortType+��6U   ��  �� Source# [  5(      �� 5)      $��Filter��6   .��5*      �� $
 Single=�6    62   �� *
FilterType+��6W   �� # [ 5+      5,      � 5-   
    � ColumnDefinitions 5.      �� 5/      �� 50      .+�52      �� $��Original view��6   .��53      �� #��[ 6   $
 Single=�6S    �� DisplayMemberPath$	 Age��6j    %� IsVirtualizing .  %� VirtualizationMode$ 	Recycling=�6�    %� 
ScrollUnit$ Pixel=�6�   ��6   �� *OriginalView+54      �� 55      $��Sorted View��6   $��1��6,   .��56      �� #��[ 6   $
 Single=�6S   $	 Age��6j    . $ 	Recycling=�6�   $ Pixel=�6�   ��6   �� *
SortedView+57      58      59      5:      