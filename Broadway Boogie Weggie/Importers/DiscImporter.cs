using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Broadway_Boogie_Weggie.Importers
{
    public class DiscImporter : Importer
    {
        public override string OpenPrompt()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }
            throw new Exception("Selecteer een bestand.");
        }

        public override string PrependType(string path)
        {
            return $"disc{path}";
        }
    }
}