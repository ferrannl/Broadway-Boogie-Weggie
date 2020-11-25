using System;
using System.Collections.Generic;
using System.IO;

namespace Broadway_Boogie_Weggie.Readers
{
    public class DiscReader : IReader
    {
        protected override string ImportType()
        {
            return "disc";
        }

        public override List<string> DoRead(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (var file = new StreamReader(filePath))
                {
                    var lines = GetLines(file);
                    file.Close();
                    return lines;
                }
            }
            throw new Exception("Bestand bestaat niet.");
        }
    }
}