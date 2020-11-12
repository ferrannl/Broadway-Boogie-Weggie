using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Parsers
{
    public abstract class Parser
    {
        protected abstract string FileType();

        public bool CanParse(string fileType)
        {
            return fileType.ToLower().Equals(FileType());
        }

        public abstract void Parse(List<string> content, out List<List<KeyValuePair<string, string>>> tileList, out List<KeyValuePair<string, string>> neighbourList);
    }
}