using Broadway_Boogie_Weggie.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Factories
{
    public class ParserFactory
    {
        private static ParserFactory instance;

        public static ParserFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ParserFactory();
                }
                return instance;
            }
        }

        public Parser CreateParser(string fileType)
        {
            Type type = typeof(Parser);
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (Type t in types)
            {
                if (!t.IsAbstract)
                {
                    Parser parser = (Parser)Activator.CreateInstance(t);
                    if (parser.CanParse(fileType))
                    {
                        return parser;
                    }
                }
            }
            throw new Exception("Bestand type wordt niet ondersteund.");

        }
    }
}