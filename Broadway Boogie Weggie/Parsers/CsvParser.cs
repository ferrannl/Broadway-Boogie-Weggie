using Broadway_Boogie_Weggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Parsers
{
    public class CsvParser : Parser<Artist>
    {
        List<Artist> artistList;
        protected override string FileType()
        {
            return "csv";
        }

        public override List<Artist> Parse(List<string> content)
        {
            try
            {
                artistList = new List<Artist>();

                foreach (string l in content.Skip(2))
                {
                    string[] props = l.Split(',');
                    // create new Artist
                    artistList.Add(new Artist(props[0].ToDouble(), props[1].ToDouble(), props[2].ToDouble(), props[3].ToDouble()));

                }
                return artistList;
            }
            catch (Exception e)
            {
                throw new Exception("File content is not valid.");
            }
        }

    }

    static class ExtentionMethods
    {
        public static double ToDouble(this string s)
        {
            s = s.Replace('.', ',');
            if (double.TryParse(s, out var result))
                return result;
            throw new ArgumentException("Failed to parse a string to a double!");
        }
    }
}