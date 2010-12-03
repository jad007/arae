using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Arae
{
    public class DirectoryView : Specializer
    {
        private static ImageSource image = new BitmapImage(new Uri("pack://application:,,,/Folder.png"));

        public DirectoryView(string name)
        {
            Name = name;
            Icon = image;
        }

        public DirectoryView(DirectoryInfo d)
        {
            Name = d.Name;
            Icon = image;
        }

        public DirectoryView()
        {
            Icon = image;
        }
    }
}
