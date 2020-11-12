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
        public string Color { get; set; }
        public List<Tile> neighbours;

        public Tile(double x, double y, string color)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
            neighbours = new List<Tile>();

        }
    }
}
