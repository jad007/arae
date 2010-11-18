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
        public List<TagGroup> SuggestedTags { get; private set; }

        public List<Tag> ActiveTags { get; private set; }

        public List<Directory> childDirectories { get; private set; }

        public FileSystemView()
        {
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

            childDirectories = new List<Directory>();
            childDirectories.Add(new Directory("directory1"));
            childDirectories.Add(new Directory("directory2"));
            childDirectories.Add(new Directory("directory3"));
        }
    }
}
