using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arae
{
    class AddTagView
    {
        public string SelectedPath {get; private set;}
        public List<TagView> AllTags { get; private set; }
        public FileSystemView fileSystemView { get; private set; }
        public AddTagView(string NewPath, FileSystemView inFileSystemView)
        {
            SelectedPath = NewPath;
            fileSystemView = inFileSystemView;
            AllTags = fileSystemView.AllTags;
        }

        public void AddExistingTag(Specializer InTag)
        {
            fileSystemView.AddExistingTag(InTag, SelectedPath);
            //foreach (Specializer spec in AllTags)
            //{
            //    if (InTag == spec)
            //    {
            //        ((TagView)spec).AddFile(SelectedPath);
            //    }
            //}
            //fileSystemView.AllTags = AllTags;
        }

        public void AddNewTag(string InNewTagName)
        {
            fileSystemView.AddNewTag(InNewTagName, SelectedPath);
            //TagView newTag = new TagView { Name = InNewTagName };
            //newTag.AddFile(SelectedPath);
            //AllTags.Add(newTag);
            //fileSystemView.AllTags = AllTags;
        }
    }
}
