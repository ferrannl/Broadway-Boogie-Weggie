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

        public XmlParser CreateXmlParser(string fileType)
        {
            return new XmlParser();
        }

        public CsvParser CreateCsvParser(string fileType)
        {
            return new CsvParser();
        }
    }
}