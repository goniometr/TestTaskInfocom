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
    /// Interaction logic for EquipmentTypeEditor.xaml
    /// </summary>
    public partial class WinEquipmentTypeEditor : Window
    {
        EquipmentType equipmentType;
        bool edit;
        TestTaskEntities context;
        public WinEquipmentTypeEditor()
        {
            InitializeComponent();
        }

        public WinEquipmentTypeEditor(EquipmentType _equipmentType)
        {
            edit = true;
            equipmentType = _equipmentType;
            InitializeComponent();
        }

        //Вызывается при добавлении кабинета
        public WinEquipmentTypeEditor(TestTaskEntities _context)
        {
            edit = false;
            context = _context;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                descriptionTextBox.Text = equipmentType.Description;        
                nameTextBox.Text = equipmentType.Name;
            }
        }

        //Кнопка сохранить
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (edit) EditData();
            else CreateData();
        }


        private void EditData()
        {
            equipmentType.Description = descriptionTextBox.Text;
            equipmentType.Name = nameTextBox.Text;
            this.DialogResult = true;
            this.Close();
        }

        private void CreateData()
        {
            equipmentType = context.EquipmentType.Create();
            equipmentType.Description = descriptionTextBox.Text;
            equipmentType.Name = nameTextBox.Text;
            context.EquipmentType.Add(equipmentType);
            context.SaveChanges();
            this.DialogResult = true;
            this.Close();
        }
    }
}
