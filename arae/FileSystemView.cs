using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace arae
{
    class FileSystemView
    {
        public List<FileView> Files { get; private set; }

        public List<TagGroup> SuggestedTags { get; private set; }

        public List<Tag> ActiveTags { get; private set; }

        public FileSystemView()
        {
            Files = new List<FileView>();
            Files.Add(new FileView(new FileInfo("test1")));
            Files.Add(new FileView(new FileInfo("test2")));
            Files.Add(new FileView(new FileInfo("test3")));

            SuggestedTags = new List<TagGroup>();
            SuggestedTags.Add(new TagGroup { Name = "Time" });
            SuggestedTags.Add(new TagGroup { Name = "Favorites" });
            SuggestedTags.Add(new TagGroup { Name = "Locations" });

            ActiveTags = new List<Tag>();
            ActiveTags.Add(new Tag { Name = "tag1" });
            ActiveTags.Add(new Tag { Name = "tag2qweqw" });
            ActiveTags.Add(new Tag { Name = "tag3" });
            ActiveTags.Add(new Tag { Name = "tag3asdf" });
            ActiveTags.Add(new Tag { Name = "tag3asdfgd" });
            ActiveTags.Add(new Tag { Name = "tag3" });
            ActiveTags.Add(new Tag { Name = "tag3hgdft5r" });
        }
    }
}
