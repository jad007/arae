using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arae
{
    class TagGroup
    {
        public string Name { get; set; }

        public List<TagView> Tags { get; private set; }

        public TagGroup()
        {
            Tags = new List<TagView>();
            Tags.Add(new TagView { Name = "test1" });
            Tags.Add(new TagView { Name = "test2" });
            Tags.Add(new TagView { Name = "test3" });
            Tags.Add(new TagView { Name = "test4" });
            Tags.Add(new TagView { Name = "test5" });
        }
    }
}
