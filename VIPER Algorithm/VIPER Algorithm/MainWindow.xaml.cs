using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

/*
 * @Author Raymond Strohschein
 * UNBC Winter 2018 Semester
 * CPSC473 Final Project
 */

namespace VIPER_Algorithm
{
    public partial class MainWindow : Window
    {
        private List<String> frequentItemSets;
        public MainWindow()
        {
            InitializeComponent();
            frequentItemSets = null;
        }

        private void Load_File_Button_Click(object sender, RoutedEventArgs e)
        {
            //Logic to open a window to search for a file
            String path = "";
            //Logic to open a window to search for a file
            OpenFileDialog file = new OpenFileDialog();
            //Filters to find .txt files
            file.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (file.ShowDialog() == true)
            {
                path = file.FileName;
            }
            File_Path_Label.Content = (path);
        }

        private void Run_Viper_Button_Click(object sender, RoutedEventArgs e)
        {
            //Get the path from File Path Label
            String path = null;
            int minSup = 0;
            this.Result_Box.Content = ("");
            //Logic to check if it's a .txt file for sure, and if its the correct formatting
            try
            {
                path = (String)File_Path_Label.Content;
                minSup = Convert.ToInt32(Minimum_Support_Label.Text);
                if (minSup > 99 || minSup < 1)
                {
                    MessageBox.Show("Error, minimum support must be between 0% and 100%");
                }
                else if(path == null || !path.EndsWith(".txt"))
                {
                    MessageBox.Show("Error, must select a database text file");
                }
                else
                {
                    Viper v = new Viper(this, path, minSup);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error, check Minimum Support or input file format");
            }
        }

        public void SetFrequentItemSets(List<String> l)
        {
            frequentItemSets = l;
        }

        private void Export_Button_Click(object sender, RoutedEventArgs e)
        {
            //Open a file dialog and get the user to save it
            if (frequentItemSets == null)
            {
                MessageBox.Show("Error, no frequent itemsets found!");
            }
            else
            {
                try
                {
                    SaveFileDialog saveFile = new SaveFileDialog
                    {
                        Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
                    };
                    saveFile.ShowDialog();
                    if (saveFile.FileName != "")
                    {
                        StreamWriter writer = new StreamWriter(saveFile.OpenFile());
                        foreach (String s in frequentItemSets)
                        {
                            writer.WriteLine(s);
                        }
                        writer.Dispose();
                        writer.Close();
                    }
                }
                catch (Exception error)
                {
                    //Nothing in the simulation
                }
            }
        }
    }
}
