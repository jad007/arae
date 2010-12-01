﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Arae
{
    class DirectoryView : Specializer
    {
        private static ImageSource image = new BitmapImage(new Uri("pack://application:,,,/Folder.png"));

        public DirectoryView(string name)
        {
            Name = name;
            Color = Brushes.Black;
            Icon = image;
        }

        public DirectoryView(DirectoryInfo d)
        {
            Name = d.Name;
            Color = Brushes.Black;
            Icon = image;
        }
    }
}
