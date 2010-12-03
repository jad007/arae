using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Arae
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand CustomRoutedCommand = new RoutedCommand();

        private FileSystemView fsv;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("tags.xml"))
                DataContext = fsv = FileSystemView.Load("tags.xml");
            else
                DataContext = fsv = new FileSystemView();

            fsv.ComputeFiles();
        }

        private void ExecutedCustomCommand(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void CanExecuteCustomCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            /*Control target = e.Source as Control;

            if (target != null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }*/
            e.CanExecute = false;
        }

        private void Item_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((ListBoxItem)sender).DataContext is FileView)
            {
                //System.Console.WriteLine(((FileView)((ListBoxItem)sender).DataContext).Name);
                FileSystem.RunFile(((FileView)((ListBoxItem)sender).DataContext).Path);
            }
            else
            {
                fsv.AddSpecializer((Specializer)((ListBoxItem)sender).DataContext);
                listBoxDirectories.Items.Refresh();
                listBoxActiveTags.Items.Refresh();
                listBoxTags.Items.Refresh();
            }

            ResetTagView();
        }

        private void ActiveTag_Click(object sender, RoutedEventArgs e)
        {
            fsv.RemoveSpecializer((Specializer)((Button)sender).DataContext);
            listBoxDirectories.Items.Refresh();
            listBoxActiveTags.Items.Refresh();
            listBoxTags.Items.Refresh();
        }

        private void ActiveTags_MouseEnter(object sender, RoutedEventArgs e)
        {
            if ((Specializer)((Button)sender).DataContext is DirectoryView)
            {
                List<DirectoryView> directoriesToColor = fsv.GetSpecializersToRemove((Specializer)((Button)sender).DataContext);
                foreach (DirectoryView directory in directoriesToColor)
                {
                    directory.Color = System.Windows.Media.Brushes.Red;
                }
            }
            else
            {

            }
            listBoxActiveTags.Items.Refresh();
        }

        private void ResetTagView()
        {
            textBoxNewTag.Text = "Enter New Tag";
        }

        private void listBoxDirectories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxDirectories.SelectedItem is FileView)
                gridTags.DataContext = new FileTagView(fsv, ((Specializer)listBoxDirectories.SelectedItem).Name);
            listBoxSelectedItemTags.Items.Refresh();
            textBoxNewTag.Text = "Enter New Tag";
        }

        private void textBoxNewTag_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxNewTag.Text == "Enter New Tag")
                textBoxNewTag.Text = "";
        }

        private void textBlockNewTag_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxNewTag.Text == "")
                textBoxNewTag.Text = "Enter New Tag";
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = null;
            if (listBoxDirectories.SelectedItem is FileView)
            {
                name = ((FileView)listBoxDirectories.SelectedItem).Name;
                fsv.AddTagToFile(textBoxNewTag.Text, name);
            }
            else if(listBoxDirectories.SelectedItem is DirectoryView)
            {
                name =  ((DirectoryView)listBoxDirectories.SelectedItem).Name;
                fsv.AddTagToDirectory(textBoxNewTag.Text, name);
            }
            if (name != null)
            {
                ((FileTagView)listBoxSelectedItemTags.DataContext).AddTag(fsv.AllTags[textBoxNewTag.Text]);
                listBoxSelectedItemTags.Items.Refresh();
            }
            fsv.ComputeFiles();
            listBoxDirectories.Items.Refresh();
            listBoxTags.Items.Refresh();
        }

        private void textBlockNewTag_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buttonAdd == null)
                return;

            if (textBoxNewTag.Text != "" && textBoxNewTag.Text != "Enter New Tag")
                buttonAdd.IsEnabled = true;
            else
                buttonAdd.IsEnabled = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            fsv.Save("tags.xml");
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            fsv.RemoveTagFromFile(((TagView)((Control)sender).DataContext).Name, ((FileView)listBoxDirectories.SelectedItem).Name);
            ((FileTagView)listBoxSelectedItemTags.DataContext).RemoveTag((TagView)((Control)sender).DataContext);
            listBoxSelectedItemTags.Items.Refresh();
        }
    }
}
