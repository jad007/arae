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
using System.Windows.Shapes;

namespace Arae
{
    /// <summary>
    /// Interaction logic for AddTagWindow.xaml
    /// </summary>
    public partial class AddTagWindow : Window
    {
        private string SelectedPathItem;
        //private List<TagView> AllTags;
        private FileSystemView fileSystemView;
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AddTagView(SelectedPathItem, fileSystemView);
        }

        public AddTagWindow(string InputSelectedPath, FileSystemView inFileSystemView)
        {
            SelectedPathItem = InputSelectedPath;
            fileSystemView = inFileSystemView;
            //AllTags = fileSystemView.AllTags;
            InitializeComponent();
        }

        private void textBlockNewTag_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "Enter New Tag")
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void buttonExistingTag_Click(object sender, RoutedEventArgs e)
        {
            ((AddTagView)DataContext).AddExistingTag(((Specializer)listBoxExistingTags.SelectedItem));
        }

        private void buttonNewTag_Click(object sender, RoutedEventArgs e)
        {
            ((AddTagView)DataContext).AddNewTag(textBlockNewTag.Text);
            listBoxExistingTags.Items.Refresh();
        }

        private void buttonDone_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
