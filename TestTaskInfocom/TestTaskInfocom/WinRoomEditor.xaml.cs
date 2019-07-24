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
    /// Interaction logic for WinRoomEditor.xaml
    /// </summary>
    public partial class WinRoomEditor : Window
    {
        Room room;
        bool edit;
        TestTaskEntities context;

        public WinRoomEditor()
        {
            InitializeComponent();
        }

        //Конструктор вызывается при редактировании Кабинета
        public WinRoomEditor(Room _room)
        {
            edit = true;
            room = _room;
            InitializeComponent();
        }

        //Вызывается при добавлении кабинета
        public WinRoomEditor(TestTaskEntities _context)
        {
            edit = false;
            context = _context;
            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                descriptionTextBox.Text = room.Description;
                floorTextBox.Text = room.Floor.ToString();
                nameTextBox.Text = room.Name;
            }
        }


        private void EditData()
        {
            room.Description = descriptionTextBox.Text;
            room.Name = nameTextBox.Text;
            var floor = 0;
            var res = int.TryParse(floorTextBox.Text, out floor);
            if (res)
            {
                room.Floor = floor;
                this.DialogResult = true;
                this.Close();
            }
            else MessageBox.Show("Этаж указан не корректно!");
        }

        private void CreateData()
        {
            room = context.Room.Create();
            room.Description = descriptionTextBox.Text;
            room.Name = nameTextBox.Text;
            var floor = 0;
            var res = int.TryParse(floorTextBox.Text, out floor);
            if (res)
            {
                room.Floor = floor;
                context.Room.Add(room);
                context.SaveChanges();
                this.DialogResult = true;
                this.Close();
            }
            else MessageBox.Show("Этаж указан не корректно!");
        }


        //Кнопка сохранить
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (edit) EditData();
            else CreateData();
        }
    }
}
