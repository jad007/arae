using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace arae
{
    class Directory : Specializer
    {
        public string Name { get; set; }

        public List<FileView> Files { get; private set; }

        public Directory(String Name)
        {
            this.Name = Name;

            Files = new List<FileView>();
            Files.Add(new FileView(new FileInfo("test1")));
            Files.Add(new FileView(new FileInfo("test2")));
            Files.Add(new FileView(new FileInfo("test3")));
        }
    }
}
