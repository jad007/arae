using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Arae
{
    [XmlRootAttribute]
    public class FileSystemView
    {
        public List<Specializer> SuggestedTags { get; set; }

        public SerializableDictionary<string, TagView> AllTags { get; set; }

        [XmlIgnore]
        public List<Specializer> ActiveTags { get; set; }

        private string dir;

        public void AddTagToFile(string tag, string file)
        {
            if (!AllTags.ContainsKey(tag))
            {
                TagView newTag = new TagView { Name = tag };
                AllTags.Add(newTag.Name, newTag);
                SuggestedTags.Add(newTag);
            }
            AllTags[tag].AddFile(file);
        }

        public void AddTagToDirectory(string tag, string file)
        {
            if (!AllTags.ContainsKey(tag))
            {
                TagView newTag = new TagView { Name = tag };
                AllTags.Add(newTag.Name, newTag);
            }
        }

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

        public void ComputeFiles()
        {
            Files.Clear();
            dir = "";
            Specializer nonDir = null;
            int count = 0;
            const int maxCount = 100;
            foreach (Specializer tag in ActiveTags)
            {
                if (tag is DirectoryView)
                    dir = Path.Combine(dir, tag.Name);
                else if(tag is TagView && nonDir == null)
                    nonDir = tag;
            }
            IEnumerable<string> en;
            IEnumerable<string> dn;
            if (nonDir != null)
            {
                en = ((TagView)nonDir).GetPossibleFiles();
                dn = ((TagView)nonDir).ProbableDirectories;
            }
            else
            {
                foreach (var t in AllTags)
                {
                    if (t.Value.ProbableDirectories.Contains(dir))
                        Files.Add(t.Value);
                }
                en = FileSystem.AllFilesUnder(dir);
                dn = FileSystem.AllDirectoriesUnder(dir);
            }
            foreach (var d in dn)
            {
                if(MatchesDirectory(d))
                    Files.Add(new DirectoryView(d));
                if (++count >= maxCount)
                {
                    break;
                }
            }
            count = 0;
            foreach (var file in en)
            {
                if(Matches(file))
                    Files.Add(new FileView(file));
                if (++count >= maxCount)
                {
                    break;
                }

            }
        }

        public void AddSpecializer(Specializer spec)
        {
            AddActiveTag(spec);
            ComputeFiles();
        }

        public List<DirectoryView> GetSpecializersToRemove(Specializer spec)
        {
            List<DirectoryView> directories = new List<DirectoryView>();
            if (spec is DirectoryView)
            {
                int i = ActiveTags.IndexOf(spec);
                while (i < ActiveTags.Count)
                {
                    if (ActiveTags[i] is DirectoryView)
                    {
                        directories.Add((DirectoryView)ActiveTags[i]);
                        i++;
                    }
                    else
                        i++;
                }
            }
            return directories;
        }

        public void RemoveSpecializer(Specializer spec)
        {
            if (spec is DirectoryView)
            {
                int i = ActiveTags.IndexOf(spec);
                while (i < ActiveTags.Count)
                {
                    if (ActiveTags[i] is DirectoryView)
                       RemoveActiveTag(ActiveTags[i]);
                    else
                        i++;
                }
            }
            else
                RemoveActiveTag(spec);
            ComputeFiles();
        }

        [XmlIgnore]
        public List<Specializer> Files { get; private set; }

        private void AddActiveTag(Specializer newActiveTag)
        {
            foreach (Specializer tag in SuggestedTags)
            {
                if (tag.Name == newActiveTag.Name)
                {
                    SuggestedTags.Remove(tag);
                    break;
                }
            }
            ActiveTags.Add(newActiveTag);
        }

        private void RemoveActiveTag(Specializer tag)
        {
            ActiveTags.Remove(tag);
            if (tag is TagView)
            {
                SuggestedTags.Add(tag);
            }
        }

        public FileSystemView()
        {
            SuggestedTags = new List<Specializer>();

            AllTags = new SerializableDictionary<string, TagView>();

            SuggestedTags.AddRange(AllTags.Values);

            ActiveTags = new List<Specializer>();

            ActiveTags.Add(new DirectoryView("C:\\"));

            Files = new List<Specializer>();
        }

        public List<TagView> TagsOnFile(string file)
        {
            List<TagView> tags = new List<TagView>();
            foreach (var t in AllTags.Values)
            {
                if (t.Matches(file))
                    tags.Add(t);
            }
            return tags;
        }

        public void Save(string file)
        {
            using(Stream str = File.Open(file, FileMode.Create))
            {
                var xml = new XmlTextWriter(str, Encoding.UTF8);
                //xml.Settings.Indent = true;
                var ser = new XmlSerializer(typeof(FileSystemView));
                ser.Serialize(xml, this);
            }
        }

        public static FileSystemView Load(string file)
        {
            using (Stream str = File.Open(file, FileMode.Open))
            {
                return (FileSystemView)new XmlSerializer(typeof(FileSystemView)).Deserialize(str);
            }
        }

        public void RemoveTagFromFile(string tag, string file)
        {
            TagView t = AllTags[tag];
            t.RemoveFile(file);
        }
    }
}
