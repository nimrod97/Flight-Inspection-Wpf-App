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

using System.Drawing;
using System.Collections;
using System.Reflection;

namespace milestone1
{
    /// <summary>
    /// Interaction logic for DataList.xaml
    /// </summary>
    public partial class DataList : UserControl
    {
        public DataList()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (this.DataContext as FlightGearViewModel).VM_CurrerntChoice = dataListBox.SelectedItem.ToString();
            if (MainWindow.dllFilePath != null)
            {
                (this.DataContext as FlightGearViewModel).VM_sendAssembly(MainWindow.assembly, MainWindow.detectFilePath, MainWindow.properFilePath);
            }
        }
    }
}
