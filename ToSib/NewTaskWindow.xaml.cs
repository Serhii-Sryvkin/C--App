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

namespace ToSib
{
    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        public Task Result { get; set; }

        public NewTaskWindow()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveTaskButton_Click(object sender, RoutedEventArgs e)
        {
            Task t = new Task();
            t.Name = NewTaskNameTextBox.Text;
            t.isDone = Sibbed.IsChecked.Value;
            t.Desc = NewDescTextBox.Text;
            if (t.Name == "") { NewTaskNameTextBox.Background = Brushes.OrangeRed; }
            else {Result = t; DialogResult = true;}
        }
    }
}
