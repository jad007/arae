using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Arae
{
    [XmlInclude(typeof(DirectoryView))]
    [XmlInclude(typeof(TagView))]
    public class Specializer
    {
        public string Name { get; set; }

        [XmlIgnore]
        public Brush Color { get; set; }

        [XmlIgnore]
        public ImageSource Icon { get; set; }

        public Specializer()
        {
            Color = Brushes.Black;
        }
    }
}
