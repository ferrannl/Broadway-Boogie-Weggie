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
        public double X { get; set; }
        public double Y { get; set; }
        public int Weight { get; set; }
        public double Width { get => Gallery.TILE_WIDTH; }
        public double Height { get => Gallery.TILE_HEIGHT; }
        public string Color { get; set; }

        public List<Tile> neighbours;

        public Tile(double x, double y, int weight, string color)
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
