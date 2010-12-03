using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arae
{
    class FileTagView
    {
        public FileSystemView FileSystemView { get; private set; }
        public string File { get; private set; }
        public List<TagView> Tags { get; private set; }

        public FileTagView(FileSystemView fs, string file)
        {
            FileSystemView = fs;
            File = file;
            Tags = fs.TagsOnFile(file);
        }

        public void Refresh()
        {
            Tags = FileSystemView.TagsOnFile(File);
        }

        public void AddTag(TagView tagView)
        {
            Tags.Add(tagView);
        }

        public void RemoveTag(TagView tagView)
        {
            Tags.Remove(tagView);
            System.Console.WriteLine("HERE");
        }
    }
}
