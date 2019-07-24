using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for WinEquipmetEditor.xaml
    /// </summary>
    public partial class WinEquipmentEditor : Window
    {
        Equipment equipment;
        bool edit;
        TestTaskEntities context;
        List<EquipmentType> equipmentType = new List<EquipmentType>();
        List<ValueImage> listImage = new List<ValueImage>();

        public WinEquipmentEditor()
        {
            InitializeComponent();
        }

        //Конструктор вызывается при редактировании Кабинета
        public WinEquipmentEditor(Equipment _equipment, TestTaskEntities _context)
        {
            edit = true;
            equipment = _equipment;
            context = _context;
            InitializeComponent();
            LoadData();

        }

        public void LoadData()
        {
            //load equipmentType
            cbxEquipmentType.ItemsSource = context.EquipmentType.ToList();
            cbxEquipmentType.SelectedItem = equipment?.EquipmentType;

            //locad rooms
            cbxRoom.ItemsSource = context.Room.ToList();
            cbxRoom.SelectedItem = equipment?.Room;
        }



        //Вызывается при добавлении кабинета
        public WinEquipmentEditor(TestTaskEntities _context)
        {
            edit = false;
            context = _context;
            InitializeComponent();
            LoadData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource equipmentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("equipmentViewSource")));
            equipmentViewSource.Source = new List<Equipment>() { equipment };
            System.Windows.Data.CollectionViewSource fileViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("fileViewSource")));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (edit) EditData();
            else CreateData();
        }

        private void EditData()
        {
            equipment.InventoryNumber = inventoryNumberTextBox.Text;
            equipment.Description = descriptionTextBox.Text;
            equipment.Name = nameTextBox.Text;
            equipment.Room = (Room)cbxRoom.SelectedItem;
            equipment.EquipmentType = (EquipmentType)cbxEquipmentType.SelectedItem;
            this.DialogResult = true;
            this.Close();
        }


        private void CreateEquipment()
        {                        
                if (equipment != null) return;
                equipment = context.Equipment.Create();
                equipment.Description = descriptionTextBox.Text;
                equipment.InventoryNumber = inventoryNumberTextBox.Text;
                equipment.Name = nameTextBox.Text;
                equipment.Room = (Room)cbxRoom.SelectedItem;
                equipment.EquipmentType = (EquipmentType)cbxEquipmentType.SelectedItem;                                    
        }


        private void CreateData()
        {
            try
            {
                if (equipment == null)
                {
                    CreateEquipment();
                    context.Equipment.Add(equipment);
                    context.SaveChanges();
                }
                this.DialogResult = true;
                this.Close();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException)
            {
                MessageBox.Show("Ошибка валидации данных");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_AddFile(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif" };
            var result = ofd.ShowDialog();
            if (result == false) return;
            if (equipment == null) CreateEquipment();
            Byte[] bytes = System.IO.File.ReadAllBytes(ofd.FileName);
            var file = context.File.Create();

            file.Base64 = bytes;
            file.Equipment = equipment;
            file.Name = System.IO.Path.GetFileName(ofd.FileName);

            context.File.Add(file);
            context.SaveChanges();
            MessageBox.Show("Файл успешно добавлен");
        }

    }

    public class ValueImage
    {
        public string photo { get; set; }
        public string name { get; set; }
    }
}
