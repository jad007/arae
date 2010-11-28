﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Arae
{
    [Serializable]
    class TagView : Specializer
    {
        private static ImageSource image = new BitmapImage(new Uri("pack://application:,,,/Tag.png"));

        public HashSet<string> Files { get; private set; }

        public HashSet<string> Directories { get; private set; }

        public HashSet<string> ProbableDirectories { get; private set; }

        public HashSet<TagView> Subtags { get; private set; }

        public TagView()
        {
            Icon = image;
            Files = new HashSet<string>();
            Directories = new HashSet<string>();
            ProbableDirectories = new HashSet<string>();
            Subtags = new HashSet<TagView>();
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
                ProbableDirectories.Add(file);
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
    }
}