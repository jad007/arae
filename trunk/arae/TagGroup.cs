using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arae
{
    class TagGroup
    {
        public string Name { get; set; }

        public List<Tag> Tags { get; private set; }

        public TagGroup()
        {
            Tags = new List<Tag>();
            Tags.Add(new Tag { Name = "test1" });
            Tags.Add(new Tag { Name = "test2" });
            Tags.Add(new Tag { Name = "test3" });
            Tags.Add(new Tag { Name = "test4" });
            Tags.Add(new Tag { Name = "test5" });
        }
    }
}
