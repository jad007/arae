﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new FileSystemView();
        }

        private void Item_RightClick(object sender, MouseButtonEventArgs e)
        {
            //TODO: open a dialog box or something here

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
        }
    }
}