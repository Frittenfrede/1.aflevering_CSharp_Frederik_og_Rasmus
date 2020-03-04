using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aflevering
{
    //Opens a window that allows the user to choose a txtfile
    public class OpenFileDialogForm : Form
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        // Constructor for OpenDialogForm
        public OpenFileDialogForm()
        {
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog()
            {
                FileName = "Select a text file",
                Filter = "Text files (*.txt)|*.txt",
                Title = "Open text file"
            };
        }

        public void OpenFileDialog(object sender, RoutedEventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<Person> persons;
                    //Read file
                    using (Stream str = openFileDialog1.OpenFile())
                    {
                       persons = Person.ReadCSVFile(openFileDialog1.FileName);
                    }
                    //Add persons to the list
                    if(persons != null)
                    {
                        MainWindow.persons.Clear();
                      foreach(Person p in persons)
                        {
                            MainWindow.persons.Add(p);
                        }
                    } 
                }
                catch (SecurityException ex)
                {
                    System.Windows.MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
    }

    public partial class MainWindow : Window
    {
        public static ObservableCollection<Person> persons = new ObservableCollection<Person>();
 
        //Main window initialize
        public MainWindow()
        {
            InitializeComponent();
            lbpersons.ItemsSource = persons;
        }
        //Opens dialogform for loading txtfile and changes the content of the statuslabel
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialogForm form = new OpenFileDialogForm();
            form.OpenFileDialog(sender, e);
            DateTime loadTime = DateTime.Now;
            listStatusLabel.Content = "File loaded: " + loadTime + " - Number of people in the list: " + lbpersons.Items.Count;
        }

        //Updates the datacontent for detailed person information
        private void lbpersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbpersons.Items.Refresh();
            innerGrid.DataContext = lbpersons.SelectedItem;
        }
    }
    }

