using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Arae
{
    [Serializable]
    public class TagView : Specializer
    {
        private static ImageSource image = new BitmapImage(new Uri("pack://application:,,,/Tag.png"));

        public HashSet<string> Files { get; set; }

        public HashSet<string> Directories { get; set; }

        public HashSet<string> ProbableDirectories { get; set; }

        public HashSet<TagView> Subtags { get; set; }

        public TagView()
        {
            Icon = image;
            Files = new HashSet<string>();
            Directories = new HashSet<string>();
            ProbableDirectories = new HashSet<string>();
            Subtags = new HashSet<TagView>();
            Color = Brushes.Black;
        }

        public void AddFile(string file)
        {
            Files.Add(file);
            AddPatentProbableDirs(file);
        }

        private void AddPatentProbableDirs(string file)
        {
            file = Path.GetDirectoryName(file);
            while (!String.IsNullOrEmpty(file))
            {
                string dir = file;
                if (!dir.EndsWith("\\"))
                    dir += '\\';
                ProbableDirectories.Add(Path.GetFullPath(dir));
                file = Path.GetDirectoryName(file);
            }
        }

        public void AddDirectory(string dir)
        {
            Directories.Add(dir);
            AddPatentProbableDirs(dir);
        }

        public bool Matches(string file)
        {
            return Files.Contains(file) || Directories.Contains(Path.GetDirectoryName(file));
        }

        public IEnumerable<string> GetPossibleFiles()
        {
            foreach (var f in Files)
                yield return f;
            foreach (var d in Directories)
            {
                foreach (var dd in FileSystem.AllFilesUnder(d))
                    yield return dd;
            }
        }

        public void RemoveFile(string file)
        {
            Files.Remove(file);
        }
    }
}
