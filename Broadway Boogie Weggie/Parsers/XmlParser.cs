using Broadway_Boogie_Weggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Broadway_Boogie_Weggie.Parsers
{
    public class XmlParser : Parser<Tile>
    {
        private List<Tile> tileList;
        protected override string FileType()
        {
            return "xml";
        }

        public override List<Tile> Parse(List<string> content)
        {
            try
            {
                tileList = new List<Tile>();

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(string.Join("", content.Skip(2)));

                XmlNodeList xnList = xml.SelectNodes("/canvas/nodes")[0].ChildNodes;
                //xnList.Cast<XmlNode>().Concat(xml.SelectNodes("/canvas/nodes/B").Cast<XmlNode>());
                //xnList.Cast<XmlNode>().Concat(xml.SelectNodes("/canvas/nodes/R").Cast<XmlNode>());
                //xnList.Cast<XmlNode>().Concat(xml.SelectNodes("/canvas/nodes/G").Cast<XmlNode>());

                XmlNodeList xnNodeTypes = xml.SelectNodes("/canvas/nodeTypes/nodeType");

                foreach (XmlNode xn in xnList)
                {
                    XmlNode xnWeight = null;
                    string color = "";
                    switch (xn.Name)
                    {
                        case "Y":
                            xnWeight = xnNodeTypes.Cast<XmlNode>().ToList().Find(xnNodeType => xnNodeType.Attributes["tag"].Value.Equals("Y"));
                            color = "Yellow";
                            break;
                        case "B":
                            xnWeight = xnNodeTypes.Cast<XmlNode>().ToList().Find(xnNodeType => xnNodeType.Attributes["tag"].Value.Equals("B"));
                            color = "Blue";
                            break;
                        case "R":
                            xnWeight = xnNodeTypes.Cast<XmlNode>().ToList().Find(xnNodeType => xnNodeType.Attributes["tag"].Value.Equals("R"));
                            color = "Red";
                            break;
                        case "G":
                            xnWeight = xnNodeTypes.Cast<XmlNode>().ToList().Find(xnNodeType => xnNodeType.Attributes["tag"].Value.Equals("G"));
                            color = "LightGray";
                            break;
                    }
                    tileList.Add(ConvertXmlToTile(xn, xnWeight, color));
                }

                for (int i = 0; i < tileList.Count(); i++)
                {
                    XmlNode xmlTile = xnList[i];
                    Tile tile = tileList[i];
                    tile.Neighbours.AddRange(GetNeighbours(xmlTile));
                }
                return tileList;
            }
            catch (Exception e)
            {
                throw new Exception("Bestands inhoud is niet valide.");
            }
        }

        private Tile ConvertXmlToTile(XmlNode xn, XmlNode xnWeight, string color)
        {
            var tile = new Tile(
                int.Parse(xn.Attributes["x"].Value),
                int.Parse(xn.Attributes["y"].Value),
                int.Parse(xnWeight.Attributes["weight"].Value),
                color);
            return tile;
        }

        private List<Tile> GetNeighbours(XmlNode xn)
        {
            List<Tile> neighbours = new List<Tile>();
            XmlNodeList xnList = xn.SelectNodes("edges/edge");
            foreach (XmlNode xnNeighbour in xnList)
            {
                foreach (Tile tile in tileList)
                {
                    double parsedX = double.Parse(xnNeighbour.Attributes["x"].Value);
                    double parsedY = double.Parse(xnNeighbour.Attributes["y"].Value);
                    if (tile.GalleryX == parsedX && tile.GalleryY == parsedY)
                    {
                        neighbours.Add(tileList.Find(currentTile => currentTile.GalleryX == parsedX && currentTile.GalleryY == parsedY));
                    }
                }
            }
            return neighbours;
        }
    }
}