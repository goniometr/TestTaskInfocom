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
        TestTaskEntities context = new TestTaskEntities();
        private CollectionViewSource equipmentViewSource;


        public WinEquipments()
        {
            InitializeComponent();
            equipmentViewSource = ((CollectionViewSource)(FindResource("equipmentViewSource")));
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.Equipment.Load();
            equipmentViewSource.Source = context.Equipment.Local;
            System.Windows.Data.CollectionViewSource fileViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("fileViewSource")));
            LoadDataForFilter();
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
                entity.Name = equipment.Name;
                entity.Description = equipment.Description;
                context.SaveChanges();
                context.Room.Load();
                equipmentViewSource.Source = context.Equipment.ToList();
                LoadPictures();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var equipment = (Equipment)grEquipment.SelectedItem;
            context.Equipment.Remove(equipment);
            context.SaveChanges();
            equipmentViewSource.Source = context.Equipment.Local;
            LoadPictures();
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


        private void GrEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPictures();
        }

        private void LoadPictures()
        {
            var row = grEquipment.SelectedItem;
            if (row == null)
            {
                listFiles.ItemsSource = null;
                return;
            }
            if (!(grEquipment.SelectedItem is Equipment))
            {                
                listFiles.ItemsSource = null;
                return;
            }
            var equipment = (Equipment)grEquipment.SelectedItem;
            if (equipment == null) return;
            foreach (var item in equipment.File)
            {
                if (!System.IO.File.Exists(item.Name))
                    System.IO.File.WriteAllBytes(item.Name, item.Base64);
            }
            var list = equipment.File.Select(x => new ValueImage() { photo = System.IO.Directory.GetCurrentDirectory() + "\\" + x.Name, name = x.Name }).ToList();

            listFiles.ItemsSource = list;
        }

        private void LoadDataForFilter()
        {
            cbxRoom.ItemsSource = context.Room.ToList();
            cbx_TypeEquipment.ItemsSource = context.EquipmentType.ToList();
        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            var equipmentTypeId = ((EquipmentType)cbx_TypeEquipment.SelectedItem)?.Id ?? 0;
            var roomId = ((Room)cbxRoom.SelectedItem)?.Id ?? 0;

            if (equipmentTypeId == 0 && roomId == 0) return;

            equipmentViewSource.Source = context.Equipment.Where(x =>            
            (x.RoomId == roomId || roomId == 0) &&
            (x.EquipmentTypeId == equipmentTypeId || equipmentTypeId == 0)).ToList();

        }

        private void BtnAll_Click(object sender, RoutedEventArgs e)
        {
            equipmentViewSource.Source = context.Equipment.ToList();
        }
    }
}
