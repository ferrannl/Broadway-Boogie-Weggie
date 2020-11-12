using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Broadway_Boogie_Weggie.Parsers
{
    public class XmlParser : Parser
    {
        protected override string FileType()
        {
            return "xml";
        }

        public override void Parse(List<string> content, out List<List<KeyValuePair<string, string>>> tileList, out List<KeyValuePair<string, string>> neighbourList)
        {
            try
            {
                tileList = new List<List<KeyValuePair<string, string>>>();
                neighbourList = new List<KeyValuePair<string, string>>();

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(string.Join("", content.Skip(2)));

                XmlNodeList xnList = xml.SelectNodes("/galaxy/planet");
                foreach (XmlNode xn in xnList)
                {
                    tileList.Add(getTile(xn));
                    neighbourList.AddRange(getNeighbours(xn));
                }

                xnList = xml.SelectNodes("/canvas/nodes");
                foreach (XmlNode xn in xnList)
                {
                    tileList.Add(getTile(xn));
                }
            }
            catch (Exception e)
            {
                throw new Exception("Bestands inhoud is niet valide.");
            }
        }

        private List<KeyValuePair<string, string>> getTile(XmlNode xn)
        {
            var tile = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("type", xn.LocalName),
                new KeyValuePair<string, string>("name", xn["name"] != null ? xn["name"].InnerText : ""),
                new KeyValuePair<string, string>("x", xn["position"]["x"].InnerText),
                new KeyValuePair<string, string>("y", xn["position"]["y"].InnerText),
                new KeyValuePair<string, string>("vx", xn["speed"]["x"].InnerText),
                new KeyValuePair<string, string>("vy", xn["speed"]["y"].InnerText),
                new KeyValuePair<string, string>("radius", xn["position"]["radius"].InnerText),
                new KeyValuePair<string, string>("color", xn["color"].InnerText),
                new KeyValuePair<string, string>("oncollision", xn["oncollision"].InnerText)
            };
            return tile;
        }

        private List<KeyValuePair<string, string>> getNeighbours(XmlNode xn)
        {
            var neighbours = new List<KeyValuePair<string, string>>();
            var xnList = xn.SelectNodes("edges/edge");
            foreach (XmlNode xnNeighbour in xnList)
            {
                neighbours.Add(new KeyValuePair<string, string>(xn["name"].InnerText, xnNeighbour.InnerText));
            }
            return neighbours;
        }
    }
}