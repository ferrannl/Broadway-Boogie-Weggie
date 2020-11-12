using Broadway_Boogie_Weggie.Importers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Factories
{
    public class ImporterFactory
    {
        private static ImporterFactory instance;

        public static ImporterFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ImporterFactory();
                }
                return instance;
            }
        }

        public Importer CreateImporter(string importType)
        {
            if (importType.Equals("disc"))
            {
                return new DiscImporter();
            }
            throw new Exception("Importeer type wordt niet ondersteund.");
        }
    }
}