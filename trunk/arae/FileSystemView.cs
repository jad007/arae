using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Arae
{
    public class FileSystemView
    {
        public List<Specializer> SuggestedTags { get; private set; }

        public List<TagView> AllTags;

        public List<TagView> getAllTags()
        {
            return AllTags;
        }

        public void AddExistingTag(Specializer InTag, string SelectedPath)
        {
            foreach (Specializer spec in AllTags)
            {
                if (InTag == spec)
                {
                    ((TagView)spec).AddFile(SelectedPath);
                }
            }
        }

        public void AddNewTag(string InNewTagName, string SelectedPath)
        {
            TagView newTag = new TagView { Name = InNewTagName };
            newTag.AddFile(SelectedPath);
            AllTags.Add(newTag);
            SuggestedTags.Add(newTag);
        }

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
                    if (t.ProbableDirectories.Contains(dir))
                        Files.Add(t);
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

        public List<Specializer> Files { get; private set; }

        private void AddActiveTag(Specializer newActiveTag)
        {
            SuggestedTags.Remove(newActiveTag);
            ActiveTags.Add(newActiveTag);
        }

        private void RemoveActiveTag(Specializer ThisActiveTag)
        {
            ActiveTags.Remove(ThisActiveTag);
            if (ThisActiveTag is TagView)
            {
                SuggestedTags.Add(ThisActiveTag);
            }
        }

        public FileSystemView()
        {
            SuggestedTags = new List<Specializer>();
            //SuggestedTags.Add(new TagGroup { Name = "Time" });
            //SuggestedTags.Add(new TagGroup { Name = "Favorites" });
            //SuggestedTags.Add(new TagGroup { Name = "Locations" });

            AllTags = new List<TagView>();
            /*TagView t = new TagView { Name = "MyTag" };
            t.AddFile(@"C:\DOCS\userguide.pdf");
            t.AddFile(@"C:\DOCS\UserGuide.ico");
            AllTags.Add(t);
            SuggestedTags.Add(t);

            TagView root = new TagView { Name = "Root Directory" };
            root.AddDirectory(@"C:\");
            AllTags.Add(root);
            SuggestedTags.Add(root);*/

            ActiveTags = new List<Specializer>();

            ActiveTags.Add(new DirectoryView(@"C:\"));
            //ActiveTags.Add(t);
            //AddActiveTag(root);

            Files = new List<Specializer>();


            try
            {
                ComputeFiles();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
