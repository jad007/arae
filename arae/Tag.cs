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
        public HashSet<string> Files { get; private set; }

        public Tag()
        {
            Files = new HashSet<string>();
        }

        public bool Matches(string file)
        {
            return Files.Contains(file);
        }
    }
}
