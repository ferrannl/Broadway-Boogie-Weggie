using System.Collections.Generic;

namespace Broadway_Boogie_Weggie.Models
{
    public class Tile : Square
    {
        public int Weight { get; set; }

        public List<Tile> Neighbours { get; }

        public override double Width => 800 / 53;

        public override double Height => 800 / 53;

        public Tile(int galleryX, int galleryY, int weight, string color) : base(galleryX, galleryY, color)
        {
            this.Weight = weight;
            Neighbours = new List<Tile>();
        }
    }
}
