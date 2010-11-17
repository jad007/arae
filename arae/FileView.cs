using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;

namespace arae
{
    class FileView
    {
        public string Name { get; set; }
        public Image Icon { get; set; }

        public FileView(FileInfo file)
        {
            Name = file.Name;
            Icon = null;//fixme
        }
    }
}
