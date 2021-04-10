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

namespace milestone1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //[DllImport("SimpleAnomalyDetectorDll.dll")]
        //public static extern IntPtr CreateSimpleAnomalyDetector();

        //[DllImport("SimpleAnomalyDetectorDll.dll")]
        //public static extern void simpleLearnNormal(IntPtr a, Dictionary<string,ArrayList> dict);
        //[DllImport("SimpleAnomalyDetectorDll.dll")]

        //public static extern  void simpleDetect(IntPtr a, string CSVfileName);
        //[DllImport("SimpleAnomalyDetectorDll.dll")]
        //public static extern int simplevectorAnomalyreportSize(IntPtr a);
        //[DllImport("SimpleAnomalyDetectorDll.dll")]
        //public static extern int simplevectorCorrelatedFeaturesSize(IntPtr a);
        //[DllImport("SimpleAnomalyDetectorDll.dll")]
        //public static extern IntPtr simplegetVecAnomalyReportByIndex(IntPtr a, int index);
        //[DllImport("SimpleAnomalyDetectorDll.dll")]
        //public static extern IntPtr simplegetVecCorrelatedFeaturesByIndex(IntPtr a, int index);


        //[DllImport("CircleAnomalyDetectorDll.dll")]
        //public static extern IntPtr CreateCircleAnomalyDetector();
        //[DllImport("CircleAnomalyDetectorDll.dll")]
        //public static extern void CircleLearnNormal(IntPtr a, string CSVfileName);
        //[DllImport("CircleAnomalyDetectorDll.dll")]
        //public static extern void CircleDetect(IntPtr a, string CSVfileName);

        //[DllImport("CircleAnomalyDetectorDll.dll")]
        //public static extern int circlevectorAnomalyreportSize(IntPtr a);
        //[DllImport("CircleAnomalyDetectorDll.dll")]
        //public static extern int circlevectorCorrelatedFeaturesSize(IntPtr a);
        //[DllImport("CircleAnomalyDetectorDll.dll")]
        //public static extern IntPtr circlegetVecAnomalyReportByIndex(IntPtr a,int index);
        //[DllImport("CircleAnomalyDetectorDll.dll")]

        //public static extern IntPtr circlegetVecCorrelatedFeaturesByIndex(IntPtr a,int index);


        private FlightGearViewModel vm;
        string learnFilePath;
        string testFilePath;
        volatile Boolean playFlag;
        volatile Boolean stopFlag;
        //private ArrayList SimplecorrelatedFeaturesArr;
        //private ArrayList SimpleanomalyReportArr;
        //private ArrayList CirclecorrelatedFeaturesArr;
        //private ArrayList CircleanomalyReportArr;
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

                //TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }
        }
        private void executeSimpleAnomalyDetector(string learnFile, string testFile)
        {

            //learn normal
            //IntPtr vec = CreateSimpleAnomalyDetector();
            //simpleLearnNormal(vec, dict);
            //int vecSize = simplevectorCorrelatedFeaturesSize(vec);
            //SimplecorrelatedFeaturesArr = new ArrayList(vecSize);
            //for (int i = 0; i < vecSize; i++)
            //{
            //    SimplecorrelatedFeaturesArr.Add(simplegetVecCorrelatedFeaturesByIndex(vec, i));
            //}

            //// detect
            //IntPtr vec1 = CreateSimpleAnomalyDetector();
            //simpleDetect(vec1, testFile);
            //int vecSize1 = simplevectorAnomalyreportSize(vec1);
            //SimpleanomalyReportArr= new ArrayList(vecSize1);
            //for (int i = 0; i < vecSize1; i++)
            //{
            //    SimpleanomalyReportArr.Add(simplegetVecAnomalyReportByIndex(vec1, i));
            //}


        }

        private void executeCircleAnomalyDetector(string learnFile, string testFile)
        {
            ////learn normal

            //IntPtr vec = CreateCircleAnomalyDetector();
            //CircleLearnNormal(vec, learnFile);
            //int vecSize = circlevectorCorrelatedFeaturesSize(vec);
            //CirclecorrelatedFeaturesArr = new ArrayList(vecSize);
            //for (int i = 0; i < vecSize; i++)
            //{
            //    CirclecorrelatedFeaturesArr.Add(circlegetVecCorrelatedFeaturesByIndex(vec, i));
            //}
            ////detect

            //IntPtr vec1 = CreateCircleAnomalyDetector();
            //CircleDetect(vec1, testFile);
            //int vecSize1 = circlevectorAnomalyreportSize(vec1);
            //CircleanomalyReportArr = new ArrayList(vecSize1);
            //for (int i = 0; i < vecSize1; i++)
            //{
            //    CircleanomalyReportArr.Add(circlegetVecAnomalyReportByIndex(vec1, i));
            //}
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
                if (dllFileName.EndsWith("SimpleAnomalyDetectorDll.dll"))
                {
                    vm.VM_SimpleAnomalyDetector(learnFilePath, testFilePath);
                }
                else // ends with CircleAnomalyDetectorDll.dll
                {
                    vm.VM_CircleAnomalyDetector(learnFilePath, testFilePath);
                }

                //TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
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

                //TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }
        }



    }
    
}
