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

namespace Arae
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand CustomRoutedCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new FileSystemView();
        }



        private void ExecutedCustomCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (((MainWindow)sender).listBoxDirectories.SelectedItem is FileView)
            {
                var newWindow = new AddTagWindow(((FileView)((MainWindow)sender).listBoxDirectories.SelectedItem).Name, ((FileSystemView)DataContext));
                newWindow.ShowDialog();
                listBoxTags.Items.Refresh();
            }
            else if (((MainWindow)sender).listBoxDirectories.SelectedItem is DirectoryView)
            {
                var newWindow = new AddTagWindow(((DirectoryView)((MainWindow)sender).listBoxDirectories.SelectedItem).Name, ((FileSystemView)DataContext));
                newWindow.ShowDialog();
                listBoxTags.Items.Refresh();
            }
        }

        private void CanExecuteCustomCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            Control target = e.Source as Control;

            if (target != null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }



        private void Item_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!(((ListBoxItem)sender).DataContext is FileView))
            {
                ((FileSystemView)DataContext).AddSpecializer((Specializer)((ListBoxItem)sender).DataContext);
                listBoxDirectories.Items.Refresh();
                listBoxActiveTags.Items.Refresh();
            }
        }

        private void ActiveTag_Click(object sender, RoutedEventArgs e)
        {
            ((FileSystemView)DataContext).RemoveSpecializer((Specializer)((Button)sender).DataContext);
            listBoxDirectories.Items.Refresh();
            listBoxActiveTags.Items.Refresh();
            listBoxTags.Items.Refresh();
        }

        private void ActiveTags_MouseEnter(object sender, RoutedEventArgs e)
        {
            if ((Specializer)((Button)sender).DataContext is DirectoryView)
            {
                List<DirectoryView> directoriesToColor = ((FileSystemView)DataContext).GetSpecializersToRemove((Specializer)((Button)sender).DataContext);
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
    }
}
