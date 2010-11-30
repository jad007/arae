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
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AddTagView(SelectedPathItem);
        }

        public AddTagWindow(string InputSelectedPath)
        {
            SelectedPathItem = InputSelectedPath;
            InitializeComponent();
        }

        private void textBlock1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "Enter New Tag")
            {
                ((TextBox)sender).Text = "";
            }
        }
    }
}
