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
    /// Interaction logic for flightDataWindow.xaml
    /// </summary>
    public partial class FlightDataWindow : UserControl
    {
        //private FlightDataViewModel vm;
        public FlightDataWindow()
        {
            InitializeComponent();
            //vm = new FlightDataViewModel(new MyFlightGearModel());
            //DataContext = vm;
        }

        private void altitude_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void airSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void headingDeg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pitchDeg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void rollDeg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void yawDeg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
