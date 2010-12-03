using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace Arae
{
    class FileView : Specializer
    {
        private static Dictionary<string, ImageSource> cache = new Dictionary<string, ImageSource>();

        public FileView(string file)
        {
            Name = file;
            Color = Brushes.Black;
            Icon = GetIcon(Path.GetExtension(file));
        }

        private static ImageSource GetIcon(string ext)
        {
            if (String.IsNullOrEmpty(ext))
                ext = ".txt";
            if (cache.ContainsKey(ext))
                return cache[ext];
            try
            {
                var i = FindIcon.IconFromExtension(ext, FindIcon.SystemIconSize.Large);
                cache[ext] = i;
                return i;
            }
            catch (Exception e)
            {
                if (cache.ContainsKey(".txt"))
                {
                    cache[ext] = cache[".txt"];
                    return cache[".txt"];
                }
                try
                {
                    var i = FindIcon.IconFromExtension(".txt", FindIcon.SystemIconSize.Large);
                    cache[ext] = cache[".txt"] = i;
                    return i;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Couldn't find .txt icon: " + ee.Message);
                    return null;
                }
            }
        }

        public override bool Equals(object obj)
        {
            var fv = obj as FileView;
            if (fv == null)
                return false;
            return Name == fv.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
