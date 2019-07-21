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
using System.Windows.Shapes;
using System.Data.Entity;

namespace TestTaskInfocom
{
    /// <summary>
    /// Interaction logic for WinEquipments.xaml
    /// </summary>
    public partial class WinEquipments : Window
    {
        TestTaskInfocomEntities context = new TestTaskInfocomEntities();
        private CollectionViewSource equipmentViewSource;
        List<EquipmentType> listEquipmentType;

        public WinEquipments()
        {         
            InitializeComponent();
            equipmentViewSource = ((CollectionViewSource)(FindResource("equipmentViewSource")));
           // cbxequipmentType.ItemsSource = context.EquipmentType.ToList();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            context.Equipment.Load();

            equipmentViewSource.Source = context.Equipment.Local;
            //equipmentViewSource.Source = context.Equipment.Local;


            //System.Windows.Data.CollectionViewSource equipmentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("equipmentViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            ///equipmentViewSource.Source = equipmentViewSource;
            System.Windows.Data.CollectionViewSource fileViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("fileViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // fileViewSource.Source = [generic data source]
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var equipment = (Equipment)grEquipment.SelectedItem;
            var fmEquipment = new WinEquipmentEditor(equipment, context);
            fmEquipment.ShowDialog();
            if (fmEquipment.DialogResult == true)
            {
                var entity = context.Equipment.Find(equipment.Id);
                if (entity == null) return;
                //entity.Floor = room.Floor;
                entity.Name = equipment.Name;
                entity.Description = equipment.Description;
                context.SaveChanges();
                context.Room.Load();
                equipmentViewSource.Source = context.Equipment.ToList();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var equipment = (Equipment)grEquipment.SelectedItem;
            context.Equipment.Remove(equipment);
            context.SaveChanges();
            equipmentViewSource.Source = context.Equipment.Local;
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var fmEquipment = new WinEquipmentEditor(context);
            fmEquipment.ShowDialog();
            if (fmEquipment.DialogResult == true)
            {
                context.Equipment.Load();
                equipmentViewSource.Source = context.Equipment.ToList();
            }
        }

    }
}
