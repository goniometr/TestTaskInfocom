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

namespace TestTaskInfocom
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

        private void MenuItemEquipment_Click(object sender, RoutedEventArgs e)
        {
            var winEquipments = new WinEquipments();
            winEquipments.ShowDialog();
        }

        private void MenuItemTypesEquipment_Click(object sender, RoutedEventArgs e)
        {
            var winEquipmentType = new WinEquipmentTypes();
            winEquipmentType.ShowDialog();
        }

        private void MenuItemRooms_Click(object sender, RoutedEventArgs e)
        {
            var winRooms = new winRooms();
            winRooms.ShowDialog();
        }

        private void MenuItemReport_Click(object sender, RoutedEventArgs e)
        {
            var f = new Window1();
            f.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource equipmentTypeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("equipmentTypeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // equipmentTypeViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource equipmentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("equipmentViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // equipmentViewSource.Source = [generic data source]
        }
    }
}
