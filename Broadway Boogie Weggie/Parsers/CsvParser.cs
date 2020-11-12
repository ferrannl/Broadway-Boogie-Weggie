using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Parsers
{
    public class CsvParser : Parser
    {
        protected override string FileType()
        {
            return "csv";
        }

        public override void Parse(List<string> content, out List<List<KeyValuePair<string, string>>> starList, out List<KeyValuePair<string, string>> neighbourList)
        {
            try
            {
                List<string> def = content[1].Split(';').ToList();
                starList = new List<List<KeyValuePair<string, string>>>();
                neighbourList = new List<KeyValuePair<string, string>>();

                foreach (string l in content.Skip(2))
                {
                    string[] props = l.Split(';');
                    // create new star
                    starList.Add(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("type", props[def.FindIndex(x => x.Equals("type"))]),
                    new KeyValuePair<string, string>("name", props[def.FindIndex(x => x.Equals("name"))]),
                    new KeyValuePair<string, string>("x", props[def.FindIndex(x => x.Equals("x"))]),
                    new KeyValuePair<string, string>("y", props[def.FindIndex(x => x.Equals("y"))]),
                    new KeyValuePair<string, string>("vx", props[def.FindIndex(x => x.Equals("vx"))]),
                    new KeyValuePair<string, string>("vy", props[def.FindIndex(x => x.Equals("vy"))]),
                    new KeyValuePair<string, string>("radius", props[def.FindIndex(x => x.Equals("radius"))]),
                    new KeyValuePair<string, string>("color", props[def.FindIndex(x => x.Equals("color"))]),
                    new KeyValuePair<string, string>("oncollision", props[def.FindIndex(x => x.Equals("oncollision"))]),
                });

                    if (props[def.FindIndex(x => x.Equals("type"))].ToLower().Equals("planet"))
                    {
                        string neighbours = props[def.FindIndex(x => x.Equals("neighbours"))];
                        if (!string.IsNullOrWhiteSpace(neighbours))
                        {
                            string[] neigboursArr = neighbours.Split(',');
                            foreach (string n in neigboursArr)
                            {
                                // create and add new neighbours
                                neighbourList.Add(new KeyValuePair<string, string>(props[def.FindIndex(x => x.Equals("name"))], n));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Bestand inhoud in not valide.");
            }
        }
    }
}