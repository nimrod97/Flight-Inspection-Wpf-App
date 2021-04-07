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
  /*          string [] arr = (string[]) dataListBox.ItemsSource;
            int len = arr.Length;
            for (int i = 0; i < len; i++)
            {
                dataListBox.Items.Add(arr[i]);
            }*/

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
/*            System.Object[] ItemObject = new System.Object[10];
            for (int i = 0; i <= 9; i++)
            {
                ItemObject[i] = "Item" + i;
            }*/
            /*            dataListBox.Items.Add("maor");
                        dataListBox.EndUpdate();*/



        }
    }
}
