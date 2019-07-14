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

        public WinRoomEditor()
        {
            InitializeComponent();
            //var d = Properties.Resources.add;
        }

        public WinRoomEditor(Room _room)
        {
            room = _room;
            InitializeComponent();

            
            //var d = Properties.Resources.add;
        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            descriptionTextBox.Text = room.Description;
            floorTextBox.Text = room.Name;
            nameTextBox.Text = room.Name;




            //System.Windows.Data.CollectionViewSource roomViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("roomViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // roomViewSource.Source = [generic data source]
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {


            //var fmRoom = new WinRoomEditor((Room)grRooms.SelectedItem);
            //fmRoom.ShowDialog();
            //roomViewSource.Source = context.Room.Local;
        }

    }
}
