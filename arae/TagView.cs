using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Arae
{
    [Serializable]
    class TagView : Specializer
    {
        public HashSet<string> Files { get; private set; }

        public TagView()
        {
            Files = new HashSet<string>();
        }

        public bool Matches(string file)
        {
            return Files.Contains(file);
        }
    }
}
