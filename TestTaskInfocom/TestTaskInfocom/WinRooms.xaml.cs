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
    /// Interaction logic for winRooms.xaml
    /// </summary>
    public partial class winRooms : Window
    {

        
        TestTaskEntities context = new TestTaskEntities();
        CollectionViewSource roomViewSource;
        public winRooms()
        {
            InitializeComponent();
            roomViewSource = ((CollectionViewSource)(FindResource("roomViewSource")));
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.Room.Load();
            roomViewSource.Source = context.Room.Local;
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var room = (Room)grRooms.SelectedItem;
            var fmRoom = new WinRoomEditor(room);
            fmRoom.ShowDialog();
            if (fmRoom.DialogResult == true)
            {
                var entity = context.Room.Find(room.Id);
                if (entity == null) return;
                entity.Floor = room.Floor;
                entity.Name = room.Name;
                entity.Description = room.Description;
                context.SaveChanges();
                context.Room.Load();
                roomViewSource.Source = context.Room.ToList();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var room = (Room)grRooms.SelectedItem;
            context.Room.Remove(room);
            context.SaveChanges();
            roomViewSource.Source = context.Room.Local;
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var fmRoom = new WinRoomEditor(context);
            fmRoom.ShowDialog();
            if (fmRoom.DialogResult == true)
            {               
                context.Room.Load();
                roomViewSource.Source = context.Room.ToList();
            }
        }

    }
}
