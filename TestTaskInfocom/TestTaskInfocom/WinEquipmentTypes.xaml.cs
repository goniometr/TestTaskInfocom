using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace TestTaskInfocom
{
    /// <summary>
    /// Interaction logic for EquipmentTypes.xaml
    /// </summary>
    public partial class WinEquipmentTypes : Window
    {
        TestTaskInfocomEntities context = new TestTaskInfocomEntities();
        CollectionViewSource equipmentTypeViewSource;

        public WinEquipmentTypes()
        {
            InitializeComponent();
            equipmentTypeViewSource = ((CollectionViewSource)(FindResource("equipmentTypeViewSource")));
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.EquipmentType.Load();
            equipmentTypeViewSource.Source = context.EquipmentType.Local;
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var equipmentType = (EquipmentType)grEquipmentType.SelectedItem;
            var fmEditor = new WinEquipmentTypeEditor(equipmentType);
            fmEditor.ShowDialog();
            if (fmEditor.DialogResult == true)
            {
                var entity = context.Room.Find(equipmentType.Id);
                if (entity == null) return;
                entity.Name = equipmentType.Name;
                entity.Description = equipmentType.Description;
                context.SaveChanges();
                context.Room.Load();
                equipmentTypeViewSource.Source = context.EquipmentType.ToList();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var value = (EquipmentType)grEquipmentType.SelectedItem;
            context.EquipmentType.Remove(value);
            context.SaveChanges();
            equipmentTypeViewSource.Source = context.Room.Local;
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var fmEquipmentTypeEditor = new WinEquipmentTypeEditor(context);
            fmEquipmentTypeEditor.ShowDialog();
            if (fmEquipmentTypeEditor.DialogResult == true)
            {
                context.EquipmentType.Load() ;
                equipmentTypeViewSource.Source = context.EquipmentType.ToList();
            }
        }
    }
}
