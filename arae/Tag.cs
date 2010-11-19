using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Arae
{
    [Serializable]
    class Tag : Specializer
    {
        public string Name { get; set; }
        public HashSet<FileInfo> Files { get; private set; }

        public Tag()
        {
            Files = new HashSet<FileInfo>();
        }
    }
}
