using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Arae
{
    class FileSystemView
    {
        public List<TagGroup> SuggestedTags { get; private set; }

        public List<Specializer> ActiveTags { get; private set; }

        private string dir;

        public bool Matches(string file)
        {
            if(!file.StartsWith(dir))
                return false;
            foreach (Specializer tag in ActiveTags)
            {
                var t = tag as Tag;
                if (t != null && !t.Matches(file))
                    return false;
            }
            return true;
        }

        private void ComputeFiles()
        {
            Files.Clear();
            dir = "";
            foreach (Specializer tag in ActiveTags)
            {
                if (tag is Directory)
                    dir = Path.Combine(dir, tag.Name);
            }
            foreach (var file in new DirectoryInfo(dir).GetFiles())
            {
                if(Matches(file.FullName))
                    Files.Add(new FileView(file));
            }
        }

        public List<FileView> Files { get; private set; }

        public FileSystemView()
        {
            SuggestedTags = new List<TagGroup>();
            SuggestedTags.Add(new TagGroup { Name = "Time" });
            SuggestedTags.Add(new TagGroup { Name = "Favorites" });
            SuggestedTags.Add(new TagGroup { Name = "Locations" });

            Tag t = new Tag { Name = "MyTag" };
            t.Files.Add(@"C:\eula.1028.txt");
            t.Files.Add(@"C:\eula.1031.txt");

            ActiveTags = new List<Specializer>();
            ActiveTags.Add(new Directory { Name = @"C:\" });
            //ActiveTags.Add(t);

            Files = new List<FileView>();

            ComputeFiles();
        }
    }
}
