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
        TestTaskInfocomEntities context = new TestTaskInfocomEntities();
         CollectionViewSource roomViewSource;
        //CollectionViewSource ordViewSource;
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var fmRoom = new WinRoomEditor((Room)grRooms.SelectedItem);
            fmRoom.ShowDialog();
            roomViewSource.Source = context.Room.Local;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var room = (Room)grRooms.SelectedItem;
            context.Room.Remove(room);
            context.SaveChanges();
            roomViewSource.Source = context.Room.Local;
        }
    }
}
