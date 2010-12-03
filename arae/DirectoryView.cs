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

        public string Path { get; set; }

        public DirectoryView(string name, string path)
        {
            Name = name;
            Path = path;
            Icon = image;
        }

        public DirectoryView(DirectoryInfo d)
        {
            Name = d.Name;
            Path = d.FullName;
            Icon = image;
        }

        public DirectoryView()
        {
            Icon = image;
        }
    }
}
