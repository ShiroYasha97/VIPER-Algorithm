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

namespace VIPER_Algorithm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            //Logic to load the Bank Line Up Window with the class Simulation
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
                    MessageBox.Show("Error, must select a text file");
                }
                else
                {
                    Viper v = new Viper(this, path, minSup);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Export_Button_Click(object sender, RoutedEventArgs e)
        {
            //Open a file dialog and get the user to save it
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog
                {
                    Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
                };
                saveFile.ShowDialog();

                string sim = (String)Result_Box.Content;

                string[] resbox = sim.Split('\n');

                if (saveFile.FileName != "")
                {
                    StreamWriter writer = new StreamWriter(saveFile.OpenFile());
                    for (int i = 0; i < resbox.Length; i++)
                    {
                        writer.WriteLine(resbox[i]);
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
