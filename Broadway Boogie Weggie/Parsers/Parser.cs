using Broadway_Boogie_Weggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Parsers
{
    public abstract class Parser<T>
    {
        protected abstract string FileType();

        public bool CanParse(string fileType)
        {
            return fileType.ToLower().Equals(FileType());
        }

        public abstract List<T> Parse(List<string> content);
    }
}