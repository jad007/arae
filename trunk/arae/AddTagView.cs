using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arae
{
    class AddTagView
    {
        public string SelectedPath {get; private set;}
        public bool IsFile { get; private set; }
        public List<TagView> Tags { get; private set; }
        public FileSystemView FileSystemView { get; private set; }

        public AddTagView(string path, bool isFile, FileSystemView fileSystemView)
        {
            SelectedPath = path;
            IsFile = isFile;
            this.FileSystemView = fileSystemView;
        }
    }
}
