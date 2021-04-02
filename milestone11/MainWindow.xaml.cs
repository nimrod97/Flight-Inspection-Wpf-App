using System;
using System.Collections.Generic;
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

namespace milestone1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FlightGearViewModel vm;
        string path;
        Boolean playFlag;
        Boolean stopFlag;
        //private MediaPlayer media = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
            vm = new FlightGearViewModel(new MyFlightGearModel(new MyTelnetClient()));
            DataContext = vm;
            playFlag = false;
            stopFlag = false;
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
                path = openFileDlg.FileName;

                //TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (!playFlag||stopFlag)
            {
                vm.VM_connect("localhost", 5400);
                vm.VM_start(path);
                playFlag = true;
            }
            else 
            {
                vm.VM_resume();
            }

        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);

            /*string msg = String.Format("Current value: {0}", val);
            this.textBlock1.Text = msg;*/
        }

        private void FileNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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

        }

        private void goLeft_Click(object sender, RoutedEventArgs e)
        {

        }

        private void goToEnd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void goToStart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
