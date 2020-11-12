using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Readers
{
    public abstract class IReader
    {
        protected abstract string ImportType();

        public bool CanRead(string filePath)
        {
            return filePath.StartsWith(ImportType());
        }

        public List<string> Read(string filePath)
        {
            string path = RemoveImportType(filePath);
            List<string> result = new List<string>();
            result.Add(GetType(path));
            result.AddRange(DoRead(path));
            return result;
        }

        public string RemoveImportType(string filePath)
        {
            return filePath.Substring(ImportType().Length);
        }

        public string GetType(string filePath)
        {
            string[] words = filePath.Split('.');
            return words[words.Length - 1];
        }

        public abstract List<string> DoRead(string filePath);

        public List<string> GetLines(StreamReader file)
        {
            string ln;
            List<string> lines = new List<string>();

            while ((ln = file.ReadLine()) != null)
            {
                lines.Add(ln);
            }
            return lines;
        }
    }
}