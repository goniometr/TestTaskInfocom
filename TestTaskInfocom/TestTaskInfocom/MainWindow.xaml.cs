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
        TestTaskEntities context = new TestTaskEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemEquipment_Click(object sender, RoutedEventArgs e)
        {
            var winEquipments = new WinEquipments();
            winEquipments.ShowDialog();
            if (winEquipments.DialogResult == true) LoadTree(); 
        }

        private void MenuItemTypesEquipment_Click(object sender, RoutedEventArgs e)
        {
            var winEquipmentType = new WinEquipmentTypes();
            winEquipmentType.ShowDialog();
            if (winEquipmentType.DialogResult == true) LoadTree();
        }

        private void MenuItemRooms_Click(object sender, RoutedEventArgs e)
        {
            var winRooms = new winRooms();
            winRooms.ShowDialog();
            if (winRooms.DialogResult == true) LoadTree();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource equipmentTypeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("equipmentTypeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // equipmentTypeViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource equipmentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("equipmentViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // equipmentViewSource.Source = [generic data source]
            LoadTree();
        }

        private void LoadTree()
        {
            var node = new TreeViewItem();
            node.Header = "Отчет наличия техники в кабинетах";
            node.IsExpanded = true;
            treeReport.Items.Add(node);
            LoadRooms(node);

        }

        private void LoadRooms(TreeViewItem node)
        {
            var listRooms = context.Room;
            foreach (var item in listRooms)
            {
                var nodeRoom = new TreeViewItem();
                nodeRoom.Header = item.Name;               
                nodeRoom.Tag = item;
                nodeRoom.IsExpanded = true;
                node.Items.Add(nodeRoom);                
                LoadTypesEquipment(nodeRoom);
                
            }
        }

        private void LoadTypesEquipment(TreeViewItem node)
        {
            var roomId = (node.Tag as Room).Id;
            var listTypesEquipment = context.Equipment.Where(x=>x.RoomId == roomId).Select(x=>x.EquipmentType).Distinct().ToList();
            foreach (var item in listTypesEquipment)
            {
                var nodeTypeEquipment = new TreeViewItem();
                nodeTypeEquipment.Header = item.Name;
                nodeTypeEquipment.IsExpanded = true;
                nodeTypeEquipment.Tag = item;
                node.Items.Add(nodeTypeEquipment);
                LoadEquipments(nodeTypeEquipment, item.Id, roomId);
            }
        }

        private void LoadEquipments(TreeViewItem node, long typeId, long roomId)
        {
            var listEquipment = context.Equipment.Where(x => x.EquipmentTypeId == typeId && x.RoomId == roomId).Distinct().ToList();
            foreach (var item in listEquipment)
            {
                var nodeEquipment = new TreeViewItem();
                nodeEquipment.Header = item.Name;                              
                node.Items.Add(nodeEquipment);
            }
        }


    }
}
