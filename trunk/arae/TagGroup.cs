using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace arae
{
    class TagGroup
    {
        public string Name { get; set; }

        public List<Specializer> Tags { get; private set; }

        public TagGroup()
        {
            Tags = new List<Specializer>();
            Tags.Add(new Tag { Name = "test1" });
            Tags.Add(new Tag { Name = "test2" });
            Tags.Add(new Tag { Name = "test3" });
            Tags.Add(new Tag { Name = "test4" });
            Tags.Add(new Tag { Name = "test5" });
        }
    }
}
