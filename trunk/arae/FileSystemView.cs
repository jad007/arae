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

        public List<TagView> AllTags { get; private set; }

        public List<Specializer> ActiveTags { get; private set; }        

        private string dir;

        public bool Matches(string file)
        {
            if(!file.StartsWith(dir))
                return false;
            foreach (Specializer tag in ActiveTags)
            {
                var t = tag as TagView;
                if (t != null && !t.Matches(file))
                    return false;
            }
            return true;
        }

        public bool MatchesDirectory(string d)
        {
            if (!d.StartsWith(dir))
                return false;
            foreach (Specializer tag in ActiveTags)
            {
                var t = tag as TagView;
                if (t != null && !t.ProbableDirectories.Contains(d))
                    return false;
            }
            return true;
        }

        private void ComputeFiles()
        {
            Files.Clear();
            dir = "";
            int count = 0;
            foreach (Specializer tag in ActiveTags)
            {
                if (tag is DirectoryView)
                    dir = Path.Combine(dir, tag.Name);
                else if(tag is TagView)
                    count++;
            }
            if (count > 0)
            {
            }
            else
            {
                foreach (var t in AllTags)
                {
                    if (t.ProbableDirectories.Contains(dir))
                        Files.Add(t);
                }
            }
            foreach (var d in new DirectoryInfo(dir).GetDirectories())
            {
                if(MatchesDirectory(d.FullName))
                    Files.Add(new DirectoryView(d));
            }
            foreach (var file in new DirectoryInfo(dir).GetFiles())
            {
                if(Matches(file.FullName))
                    Files.Add(new FileView(file));
            }
        }

        public void AddSpecializer(Specializer spec)
        {
            ActiveTags.Add(spec);
            ComputeFiles();
        }

        public void RemoveSpecializer(Specializer spec)
        {
            if (spec is DirectoryView)
            {
                int i = ActiveTags.IndexOf(spec);
                while (i < ActiveTags.Count)
                {
                    if (ActiveTags[i] is DirectoryView)
                        ActiveTags.RemoveAt(i);
                    else
                        i++;
                }
            }
            else
                ActiveTags.Remove(spec);
            ComputeFiles();
        }

        public List<Specializer> Files { get; private set; }

        public FileSystemView()
        {
            SuggestedTags = new List<TagGroup>();
            SuggestedTags.Add(new TagGroup { Name = "Time" });
            SuggestedTags.Add(new TagGroup { Name = "Favorites" });
            SuggestedTags.Add(new TagGroup { Name = "Locations" });

            AllTags = new List<TagView>();
            TagView t = new TagView { Name = "MyTag" };
            t.AddFile(@"C:\eula.1028.txt");
            t.AddFile(@"C:\eula.1031.txt");
            AllTags.Add(t);

            ActiveTags = new List<Specializer>();

            ActiveTags.Add(new DirectoryView(@"C:\"));
            //ActiveTags.Add(t);

            Files = new List<Specializer>();


            ComputeFiles();
        }
    }
}
