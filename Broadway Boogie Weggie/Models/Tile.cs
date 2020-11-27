using System.Collections.Generic;

namespace Broadway_Boogie_Weggie.Models
{
    public class Tile : Square
    {
        public int Weight { get; set; }

        public List<Tile> Neighbours { get; }

        public override double Width => 1;

        public override double Height => 1;

        public Tile(int galleryX, int galleryY, int weight, string color) : base(galleryX, galleryY, color)
        {
            this.Weight = weight;
            Neighbours = new List<Tile>();
        }
    }
}
