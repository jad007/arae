using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Arae
{
    class FileView
    {
        public string Name { get; set; }
        public ImageSource Icon { get; set; }

        public FileView(FileInfo file)
        {
            Name = file.Name;
            Icon = FindIcon.IconFromExtension(".doc", FindIcon.SystemIconSize.Large);
        }
    }
}
