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

        public WinEquipmentEditor()
        {
            InitializeComponent();
        }

        //Конструктор вызывается при редактировании Кабинета
        public WinEquipmentEditor(Equipment _equipment)
        {
            edit = true;
            equipment = _equipment;
            InitializeComponent();
        }

        //Вызывается при добавлении кабинета
        public WinEquipmentEditor(TestTaskInfocomEntities _context)
        {
            edit = false;
            context = _context;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            if (edit)
            {
                descriptionTextBox.Text = equipment.Description;                
                nameTextBox.Text = equipment.Name;
                equipmentTypeComboBox.

            }
            */
            System.Windows.Data.CollectionViewSource equipmentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("equipmentViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            equipmentViewSource.Source = new List<Equipment>() { equipment };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (edit) EditData();
            //else CreateData();
        }
    }
}
