using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Importers
{
    public abstract class Importer
    {
        public string Import()
        {
            string path = OpenPrompt();
            return PrependType(path);
        }

        public abstract string OpenPrompt();

        public abstract string PrependType(string path);
    }
}