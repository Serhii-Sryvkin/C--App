using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace ToSib
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ObservableCollection<Task> tasklist = new ObservableCollection<Task>();
        Collection<Task> tasklist = new Collection<Task>();
        ObservableCollection<Task> showlist = new ObservableCollection<Task>();

        public MainWindow()
        {
            InitializeComponent();
            //Task t1 = new Task();
            //t1.Name = "Sib this app";
            //t1.Desc = "You have to sib this app ASAP";
            //t1.isDone = false;

            //Task t2 = new Task();
            //t2.Name = "App by Sib";
            //t2.Desc = "Siba have to app it now";
            //t2.isDone = false;

            //Task t3 = new Task();
            //t3.Name = "Try to sib rest";
            //t3.Desc = "You have to sib the rest of apps";
            //t3.isDone = false;

            //tasklist.Add(t1);
            //tasklist.Add(t2);
            //tasklist.Add(t3);
            showlist.Clear();
            foreach (Task t in tasklist) showlist.Add(t);
            ToSibListBox.ItemsSource = showlist;
            ToSibListBox.DisplayMemberPath = "Name";
        }
               
        private void ToSibListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Task selected = ToSibListBox.SelectedItem as Task;
            if (selected != null) { MessageBox.Show(selected.Desc); }
            
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            NewTaskWindow window = new NewTaskWindow();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (window.ShowDialog() == true) {
                Task newTask = window.Result;
                newTask.id = tasklist.Count;
                newTask.idCur = ToSibListBox.Items.Count;
                tasklist.Add(newTask); };
            if (AllRB.IsChecked == true) { AllRB_Show(); }
            else if (ToSibRB.IsChecked == true) { ToSibRB_Show(); }
            else if (DoneRB.IsChecked == true) { DoneRB_Show(); }
            else
            {
                showlist.Clear();
                foreach (Task t in tasklist) showlist.Add(t);
            }

        }

        private void Del_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = ToSibListBox.SelectedIndex;
            //MessageBox.Show("Del : "+ Convert.ToString(index));
            if (index != -1)
            {
                for (int i = 0; i < tasklist.Count; i++)
                {
                    if (tasklist[i].idCur == index)
                    {
                        //MessageBox.Show("Task List Name:"+tasklist[i].Name);
                        tasklist.RemoveAt(i);
                    }
                }
                if (AllRB.IsChecked == true) { AllRB_Show(); }
                else if (ToSibRB.IsChecked == true) { ToSibRB_Show(); }
                else if (DoneRB.IsChecked == true) { DoneRB_Show(); }
                else
                {
                    showlist.Clear();
                    foreach (Task t in tasklist) showlist.Add(t);
                }

            }
        }

        private void Sib_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = ToSibListBox.SelectedIndex;
//            Task current = ToSibListBox.SelectedItem as Task;
            if (index != -1) {
                for (int i = 0; i < tasklist.Count; i++)
                {
                    if (tasklist[i].idCur == index) {
                        if (DoneRB.IsChecked == false)
                        {
                            tasklist[i].isDone = true;
                        } else tasklist[i].isDone = false;
                        //MessageBox.Show("i = " + Convert.ToString(i)+ " index = " + Convert.ToString(index));
                    }
                }
                if (AllRB.IsChecked == true) { AllRB_Show(); }
                else if (ToSibRB.IsChecked == true) { ToSibRB_Show(); }
                else if (DoneRB.IsChecked == true) { DoneRB_Show(); }
                else
                {
                    showlist.Clear();
                    foreach (Task t in tasklist) showlist.Add(t);
                }
            }
        }
        private void AllRB_Checked(object sender, RoutedEventArgs e)
        { AllRB_Show(); }
        private void AllRB_Show()
        {
            Sib_Button.Content = "Sib";
            int idCur = 0;
            showlist.Clear();
            foreach (Task t in tasklist) showlist.Add(t);
            ToSibListBox.ItemsSource = showlist;
            //MessageBox.Show("Task list 0 :" + tasklist[0].Name);
            if (tasklist.Count > 0)
            {
                for (int i = 0; i < tasklist.Count; i++)
                {
                    tasklist[i].idCur = idCur; idCur++;
                }
            }
        }
        private void ToSibRB_Checked(object sender, RoutedEventArgs e)
        { ToSibRB_Show(); }
            private void ToSibRB_Show()
        {
            int idCur = 0;
            Sib_Button.Content = "Sib";
            showlist.Clear();
            //ObservableCollection<Task> filtList = new ObservableCollection<Task>();
            if (tasklist.Count > 0)
           {
               for (int i = 0; i < tasklist.Count; i++)
               {
                                   Task current = tasklist[i];
                                   if (current.isDone == false)
                                    {
                                        current.idCur = idCur;
                                        tasklist[i].idCur = idCur;
                                        showlist.Add(current); idCur++;
                                    }  else tasklist[i].idCur = -1;
                }
            }
                ToSibListBox.ItemsSource = showlist;
            //MessageBox.Show("Task list 0 :" + tasklist[0].Name);
        }

        private void DoneRB_Checked(object sender, RoutedEventArgs e)
        { DoneRB_Show(); }
        private void DoneRB_Show()
        {
            Sib_Button.Content = "UnSib"; 
            int idCur = 0;
            showlist.Clear();
            //ObservableCollection<Task> doneList = new ObservableCollection<Task>();
            if (tasklist.Count > 0)
            {
                for (int i = 0; i < tasklist.Count; i++)
                {
                    Task current = tasklist[i];
                    if (current.isDone == true)
                    {
                        current.idCur = idCur;
                        tasklist[i].idCur = idCur;
                        showlist.Add(current); idCur++;
                    } else tasklist[i].idCur = -1;
                }
            }
            ToSibListBox.ItemsSource = showlist;
            //MessageBox.Show("Task list 0 :" + tasklist[0].Name);
        }

        string fileName = "data.bin";

        private void Window_Closed(object sender, EventArgs e)
        {
            File.Delete(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Task>));
            // BinaryFormatter formatter = new BinaryFormatter();
            Stream file = File.OpenWrite(fileName);
            //formatter.Serialize(file, tasklist); 
            serializer.Serialize(file, tasklist);
            file.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(fileName))
            {
                // read
                AllRB.IsChecked = true;
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Task>));
                Stream file = File.OpenRead(fileName);
                tasklist.Clear();
                tasklist = serializer.Deserialize(file) as ObservableCollection<Task>;
                file.Close();
                showlist.Clear();
                foreach (Task t in tasklist) showlist.Add(t);
                ToSibListBox.ItemsSource = showlist;
            }
        }

    }
}
