using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Arae
{
    static class FileSystem
    {
        private static extern IntPtr FindExecutable(string lpFile, string lpDirectory, [Out] StringBuilder lpResult);

        public static void RunFile(string file)
        {
            StringBuilder res = new StringBuilder();
            FindExecutable(file, null, res);
            Process.Start(res.ToString(), file);
        }

        public static IEnumerable<string> AllFilesUnder(string dir)
        {
            Queue<DirectoryInfo> dirs = new Queue<DirectoryInfo>();
            dirs.Enqueue(new DirectoryInfo(dir));
            while (dirs.Count > 0)
            {
                var d = dirs.Dequeue();
                FileInfo[] ef = null;
                DirectoryInfo[] ed = null;
                try
                {
                    ef = d.GetFiles();
                    ed = d.GetDirectories();
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                foreach (var file in ef)
                    yield return file.FullName;
                foreach (var dd in ed)
                    dirs.Enqueue(dd);
            }
            yield break;
        }

        public static IEnumerable<string> AllDirectoriesUnder(string dir)
        {
            Queue<DirectoryInfo> dirs = new Queue<DirectoryInfo>();
            dirs.Enqueue(new DirectoryInfo(dir));
            while (dirs.Count > 0)
            {
                var d = dirs.Dequeue();
                DirectoryInfo[] ed = null;
                try
                {
                    ed = d.GetDirectories();
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                foreach (var dd in ed)
                {
                    dirs.Enqueue(dd);
                    yield return dd.FullName;
                }
            }
            yield break;
        }
    }
}
