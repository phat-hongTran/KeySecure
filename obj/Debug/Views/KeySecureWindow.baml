   M S B A M L   `   `   `  ����D  @KeySecure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:KeySecure	KeySecure  5   	   ;"clr-namespace:KeySecure.ComponentsKeySecure.Components  5   	   9!clr-namespace:KeySecure.ConverterKeySecure.Converter  5   	       KeySecure.SecureKeyWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:KeySecure     5   	   9
Components"clr-namespace:KeySecure.Components     5   	   7	Converter!clr-namespace:KeySecure.Converter     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ 
Key Secure��5   	   $	��330��5   	   $	��400��5   	    � 	Resources 5        BoolToVisibilityConverter0   *KeySecure.Converter.ConcatenationConverter  ConcatenationConverter��%:   &	       &	      �� 5   
   6'     5   
   6+   5      .��5      �� .+�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5       $
��Auto �6     � ColumnDefinitions 5"      �� 5#      �� 5$      $
��auto �6#   �� 5%      .+�5(      �� $��0��6   $��0��5)      $��3��5*      $��Center=�5,      $��Center=�5-      $��25(�5.      ��5+      �� *Title+ �� UpdateSourceTrigger$ PropertyChanged=�5.      d� 5/       * &MaterialDesignSwitchAccentToggleButton#��[ 6    � ToolTip$" Change to Encrypt or Decrypt��50      $��50��52      $��1��53      $��1��54      	 d� 	IsChecked	 51      �� *	IsDecrypt+$ PropertyChanged=�54      *   $KeySecure.Components.PasswordControl  55      -   5        
  Name$
 txtPasswordBox��55   )   $��2��57   )   $��0��58   )   $��3��59   )   $��Center=�5:   )   $��18(�5;   )   $��50 20 50 20q�5<   )   )  #MaterialDesignThemes.Wpf.HintAssist   Hint 56   )   �� *PassWordBoxHint+$ PropertyChanged=�   Password 5=   )   �� *
Password+$ PropertyChanged=� �� Mode$ TwoWay=��� 5>       �� Orientation$ Vertical=�6   $��3��5?      $��1��5@      $��Center=�5A      .+�5B      �  5C      �� 5D      $
��Auto �6(   �� 5E      $
��Auto �6(   �� 5F      $
��Auto �6(   �� 5G      $
��Auto �6(   .+�5I      �� -   5         ��Name$ txtAddSecureKey1��5I      $��0��5J      $ Add secure key��5K      $��17(�5L      $��Center=�5M      $��Center=�5N      $��0 5 0 5q�5O      ��5P      �� *InputText1Encr+�� 5R      -   5        $ txtAddSecureKey2��5R      $��1��5S      $ Add secure key��5T      $��17(�5U      $��Center=�5V      $��Center=�5W      $��0 5 0 5q�5X      y�5Y      �� *InputText2Visibility+��5Z      �� *InputText2Encr+�� 5[      -   5        $ txtAddSecureKey3��5[      $��2��5\      $ Add secure key��5]      $��17(�5^      $��Center=�5_      $��Center=�5`      $��0 5 0 15q�5a      y�5b      �� *InputText3Visibility+��5c      �� *InputText3Encr+� 5d      $��3��6    5e      �� 5f      �� 5g      .+�5i      �� $��Reset��6!   $��0 0 15 0q�61   ��5j   !   �� *ResetToDefault+�� 5k      -   5         ��Name$ 
btnEncrypt��5k   !   $��Encrypt��5l      $��1��5m      
��� d ���5o      �� *EncryptCommand+y�5p      �� *IsEncryptButtonVisible+ �� 	Converter# [ �� 5q      -   5        $ 
btnDecrypt��5q   !   $��Decrypt��5r      $��1��5s      
��� d ���5u      �� *DecryptCommand+y�5v      �� *IsDecryptButtonVisible+# [ 5w      5x      5y      �� 5z      -   5        $ btnAddSecureKey��5�      $��3��5z      $��2��5{      $��45��5|      $��25��5}      $
��Left=�5~      $	��Top=�5      
������ $��	10 10 0 0q�5�      ��5�      �� *
ColorAdd+��5�      �� *ToggleVisibilityCommand+.��5�      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�6*   $��3��5�   *   $��2��5�   *   $��Center=�5�   *   $��Center=�5�   *   $��25��5�   *   $��25��5�   *   5�      5�      5�      5�      