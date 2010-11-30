using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arae
{
    class AddTagView
    {
        public string SelectedPath {get; private set;}
        public AddTagView(string NewPath)
        {
            SelectedPath = NewPath;
        }
    }
}
