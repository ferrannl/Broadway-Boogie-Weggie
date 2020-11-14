using Broadway_Boogie_Weggie.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Models
{
    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Weight { get; set; }
        public string Color { get; set; }

        public List<Tile> neighbours;

        public Tile(int x, int y, int weight, string color)
        {
            this.X = x;
            this.Y = y;
            this.Weight = weight;
            this.Color = color;
            neighbours = new List<Tile>();
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
