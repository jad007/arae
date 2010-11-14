using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace arae
{
    class FileSystemView
    {
        public List<FileView> Files { get; private set; }

        public List<TagGroup> Tags { get; private set; }

        public List<Tag> ActiveTags { get; private set; }

        public List<Specializer> Specializers { get; private set; }

        public FileSystemView()
        {
            Files = new List<FileView>();
            Files.Add(new FileView { Name = "test" });
            Files.Add(new FileView { Name = "test2" });
            Files.Add(new FileView { Name = "test3" });

            Tags = new List<TagGroup>();
            Tags.Add(new TagGroup { Name = "Time" });
            Tags.Add(new TagGroup { Name = "Favorites" });
            Tags.Add(new TagGroup { Name = "Locations" });

            ActiveTags = new List<Tag>();
            ActiveTags.Add(new Tag { Name = "tag1" });
            ActiveTags.Add(new Tag { Name = "tag2" });
            ActiveTags.Add(new Tag { Name = "tag3" });
        }
    }
}
