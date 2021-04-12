using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Reflection;

namespace milestone1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FlightGearViewModel vm;
        string learnFilePath;
        string testFilePath;
        volatile Boolean playFlag;
        volatile Boolean stopFlag;

        public MainWindow()
        {
            InitializeComponent();
            vm = new FlightGearViewModel(new MyFlightGearModel(new MyTelnetClient()));
            DataContext = vm;
            playFlag = false;
            stopFlag = false;
            PropertiesList.DataContext = vm;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                FileNameTextBox.Text = openFileDlg.FileName;
                learnFilePath = openFileDlg.FileName;
                vm.VM_initializingComponentsByPath(learnFilePath);
            }
        }
        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (stopFlag)
            {
                vm = null;
                // creating new instance of vm
                vm = new FlightGearViewModel(new MyFlightGearModel(new MyTelnetClient()));
                DataContext = vm;
                playFlag = false;
                stopFlag = false;
                vm.VM_connect("localhost", 5400);
                vm.VM_initializingComponentsByPath(learnFilePath);
                vm.VM_start();
                playFlag = true;
            }

            else if (!playFlag)
            {
                vm.VM_connect("localhost", 5400);
                vm.VM_start();
                playFlag = true;
            }
            else // pauseFlag is pressed
            {
                vm.VM_resume();
            }

        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);

        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_pause();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            stopFlag = true;
            vm.VM_stop();
        }

        private void goRight_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_goRight();

        }

        private void goLeft_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_goLeft();
        }

        private void goToEnd_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_goToEnd();
        }

        private void goToStart_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_goToStart();
        }

        private void FileNameTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SimulatorSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double newSpeed = Convert.ToDouble(SimulatorSpeed.Text);
                vm.VM_SimulatorSpeed = newSpeed;
                speedSlider.Value = newSpeed;
            }
            catch { }
        }

        private void SimulatorSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SimulatorSpeed.Text = String.Format("{0:0.00}", e.NewValue);
        }

        private void LabelplaySpeed_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BrowseDllFile_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock

            if (result == true)
            {
                DllFileTextBox.Text = openFileDlg.FileName;
                string dllFileName = openFileDlg.FileName;
                var assembly = Assembly.LoadFile(dllFileName);
                vm.VM_sendAssembly(assembly, learnFilePath, testFilePath);
                //if (assembly.ManifestModule.FullyQualifiedName.EndsWith("SimpleAnomalyDetectorDll.dll")) 
                //{
                //    vm.VM_SimpleAnomalyDetector(learnFilePath, testFilePath);
                //}
                //else // ends with CircleAnomalyDetectorDll.dll
                //{
                //    vm.VM_CircleAnomalyDetector(learnFilePath, testFilePath);
                //}

            }
        }

        private void BrowseTestFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                testFilePathTextBox.Text = openFileDlg.FileName;
                testFilePath = openFileDlg.FileName;

            }
        }


    }

}



