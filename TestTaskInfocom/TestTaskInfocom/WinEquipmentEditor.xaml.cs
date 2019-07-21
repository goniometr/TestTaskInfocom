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

namespace TestTaskInfocom
{
    

    /// <summary>
    /// Interaction logic for WinEquipmetEditor.xaml
    /// </summary>
    public partial class WinEquipmentEditor : Window
    {
        Equipment  equipment;
        bool edit;
        TestTaskInfocomEntities context;
        List<EquipmentType> equipmentType = new List<EquipmentType>();

        public WinEquipmentEditor()
        {
            InitializeComponent();
        }

        //Конструктор вызывается при редактировании Кабинета
        public WinEquipmentEditor(Equipment _equipment, TestTaskInfocomEntities _context)
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
            cbxEquipmentType.ItemsSource =   context.EquipmentType.ToList();
            cbxEquipmentType.SelectedItem = equipment?.EquipmentType;

            //locad rooms
            cbxRoom.ItemsSource = context.Room.ToList();
            cbxRoom.SelectedItem = equipment?.Room;
        }



        //Вызывается при добавлении кабинета
        public WinEquipmentEditor(TestTaskInfocomEntities _context)
        {
            edit = false;
            context = _context;
            InitializeComponent();
            LoadData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {          
            System.Windows.Data.CollectionViewSource equipmentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("equipmentViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            equipmentViewSource.Source = new List<Equipment>() { equipment };
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

        private void CreateData()
        {
            try
            {
                equipment = context.Equipment.Create();
                equipment.Description = descriptionTextBox.Text;
                equipment.InventoryNumber = inventoryNumberTextBox.Text;
                equipment.Name = nameTextBox.Text;
                equipment.Room = (Room)cbxRoom.SelectedItem;
                equipment.EquipmentType = (EquipmentType)cbxEquipmentType.SelectedItem;
                context.Equipment.Add(equipment);
                context.SaveChanges();
                this.DialogResult = true;
                this.Close();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                MessageBox.Show("Ошибка валидации данных");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
