   M S B A M L   `   `   `  ����C  ?MVVMDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MVVMDemo.ViewsMVVMDemo.Views  5      7 clr-namespace:MVVMDemo.ViewModelMVVMDemo.ViewModel  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L5clr-namespace:System.Diagnostics;assembly=WindowsBaseSystem.Diagnostics 5      /clr-namespace:MVVMDemo.ModelMVVMDemo.Model  5	      +clr-namespace:MVVMDemo.VMLMVVMDemo.VML  5
           MVVMDemo.Views.StudentView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:MVVMDemo.Views     5      6	viewModel clr-namespace:MVVMDemo.ViewModel     5      Fdiag5clr-namespace:System.Diagnostics;assembly=WindowsBase     5      -dataclr-namespace:MVVMDemo.Model     5	      *vmlclr-namespace:MVVMDemo.VML     5
      #   MVVMDemo.VML.ViewModelLocator    AutoHookedUpViewModel  .   � 	Resources 5       �� DataType   MVVMDemo.Model.IP �� Orientation �� Path �� Mode   MVVMDemo.Model.PORT��%�  (��       5      *, +)(�� �     5      *, +)�� 5   
   "  6   .R�5      �� $ 
Horizontal=�6   .+�5      �� .��6   IP-Address:6%   �� 5      $	��145��6   ��6'   �� $ 	Ipaddress�$ TwoWay=�5      5      �� 5   
   "  6   .R�5      �� $ 
Horizontal=�6   .+�5      �� .��6   Port:6   �� 5      $	��180��6   ��6&   �� $ 
Portnumber�$ TwoWay=�5      5      5       .��5!      �  � ColumnDefinitions 5"   
   �� 5#      $	��110 �6   �� 5$      $	��110 �6   �� 5%       � RowDefinitions 5(   
   �� 5)      �� 5*      $
��Auto �6   �� 5+      $
��Auto �6   �� 5,      $
��Auto �6   �� 5-      $
��Auto �6   .+�52   
   �� -   5        $��title��52      $��2,0,10,0q�6   $	��Top=�60   $
��Left=�6H   $��3��6c   .��6w   1/Welcome, chose your name and start a connection6�   �� 54   
   $��0��6   $��4��6!   $	��asd��55      ��54   .   �� *InviteCommand+1  +System.Diagnostics.PresentationTraceSources   
TraceLevel$
 High=�.��55   (   Invite60   �� 56   
   $��1��6   $��4��6!   $��	customers��57      ��56   .   �� *ListenCommand+$
 High=�.��57   .   Listen66   �� 58   
   $��0��6   $��1��6"   $��3��6/   ��6C   �� *Ips+�� 59   
   $��0��6   $��2��6"   $��3��6/   ��6C   �� *Ports+5>      5A      