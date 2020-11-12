using Broadway_Boogie_Weggie.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Factories
{
    public class ReaderFactory
    {
        private static ReaderFactory instance;

        public static ReaderFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReaderFactory();
                }
                return instance;
            }
        }

        public IReader CreateReader(string filePath)
        {
            Type type = typeof(IReader);
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (Type t in types)
            {
                if (!t.IsAbstract)
                {
                    IReader reader = (IReader)Activator.CreateInstance(t);
                    if (reader.CanRead(filePath))
                    {
                        return reader;
                    }
                }
            }
            throw new Exception("Bestand type wordt niet ondersteund.");
        }
    }
}