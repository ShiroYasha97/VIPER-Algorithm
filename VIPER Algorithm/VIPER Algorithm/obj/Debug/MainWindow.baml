   M S B A M L   `   `   `  ����J  FVIPER Algorithm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:VIPER_AlgorithmVIPER_Algorithm  5   	        VIPER_Algorithm.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:VIPER_Algorithm     5   	   -   5          �Name$  VIPER_Window��5   	    � Title$ VIPER��5   	   $	��500��6   $	��700��6$    � 
ResizeMode$ NoResize=�60    � WindowStartupLocation$ CenterScreen=�6F    � Icon$ 	VIPER.png��6k   .��5	      � ��5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    � MappingMode$ RelativeToBoundingBox=�63    �� 
StartPoint$ 0.5,0&�6W   .��5      �  � Color$ Black��6   	 � Offset$
	 0.15Q�6-   � 5      $ 	#FFFF1D1D��6   $	 1Q�61   5      .+�5   
   �� -   5        
 ��Name$
 File_Path_Label��5      $��Path of File��6)   $
��Left=�6@   $��26��6[   $��
80,411,0,0q�6g   $	��Top=�6{   $	��501��6�   
�������$��10(�6�   �� 5   
   -   5         ��Name$ 
Result_Box��5      $
��Left=�6+   $	��272��6F   $��
20,110,0,0q�6S   $	��Top=�6g   $	��499��6   
�������$%��Mining results will appear here��6�   $��14(�6�   �� 5   
   -   5         ��Name$ Minimum_Support_Label��5      $
��Left=�61   $��27��6L   $��539,286,0,0q�6X    �� TextWrapping$
 Wrap=�6m   $��MinimumSupport��6�   $	��Top=�6�   $	��124��6�   $��14(�6�   �� 5   
   -   5         ��Name$ Run_Viper_Button��5      $��	Run VIPER��6+   $
��Left=�6?   $��570,345,0,0q�6Z   $	��Top=�6o   $��93��6�   $��26��6�   �� 5   
   -   5        $ Export_Button��5      $��Export Results��6(   $
��Left=�6A   $��595,411,0,0q�6\   $	��Top=�6q   $��68��6�   $��26��6�   $��10(�6�   �� 5   
   -   5        $ Load_File_Button��5      $��	Load File��6+   $
��Left=�6?   $��
20,411,0,0q�6Z   $	��Top=�6n   $��55��6�   $��26��6�    J� RenderTransformOrigin$ 0.521,1.077&�6�   $��10(�6�   �� 5   
   -   5        $
 Title_Label��5      $��VIPER Algorithm��6%   $
��Left=�6?   $��61��6Z   $��
190,15,0,0q�6f   $	��Top=�6z   $	��274��6�   $��36(�6�   ��6�   Z� ��5      �� 5      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5      � $ Black��6#   $	 0Q�61   � 5      $ 	#FF00FFE8��6#   $		 0.7Q�65   5      5      �� 5   
   -	   5        $
 Frequent_Patterns_Label��5      $��FPs = 0��61   $
��Left=�6C   $��30��6^   $��539,209,0,0q�6j   $	��Top=�6   $	��124��6�   
�������$ 
0.544,-0.1&�6�   {�.  $��14(�6�   �� 5   
   -
   5        $
 Time_Taken_Label��5      $��Time Taken:��6*   $
��Left=�6@   $��50��6[   $��539,119,0,0q�6g   $	��Top=�6|   $	��124��6�   
�������$��14(�6�   {�.  6�   5       5!      