using Broadway_Boogie_Weggie.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Models
{
    public class Artist
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Vx { get; set; }
        public double Vy { get; set; }
        public double Width { get => Gallery.ARTIST_WIDTH; }
        public double Height { get => Gallery.ARTIST_HEIGHT; }
        public string Color { get => "black"; }
        public Artist(double x, double y, double vx, double vy)
        {
            this.X = x;
            this.Y = y;
            this.Vx = vx;
            this.Vy = vy;
        }
        public Artist(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        public void Step()
        {
            this.X += Vx;
            this.Y += Vy;
        }
        public void CheckCollisionWall()
        {
            if (X <= Gallery.ARTIST_WIDTH || X + Gallery.ARTIST_WIDTH >= Gallery.WIDTH)
                Vx = -Vx;
            if (Y <= Gallery.ARTIST_HEIGHT || Y + Gallery.ARTIST_HEIGHT >= Gallery.HEIGHT)
                Vy = -Vy;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
