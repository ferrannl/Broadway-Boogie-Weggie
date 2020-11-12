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
        public string Color { get; set; }
        public Artist(double x, double y, double vx, double vy, string color)
        {
            this.X = x;
            this.Y = y;
            this.Vx = vx;
            this.Vy = vy;
            this.Color = color;
        }
        public void Step()
        {
            this.X += Vx;
            this.Y += Vy;
        }
        public void CheckCollisionWall()
        {
            if (X <= 3.5 || X + 3.5 >= Gallery.WIDTH)
                Vx = -Vx;
            if (Y <= 3.5 || Y + 3.5 >= Gallery.HEIGHT)
                Vy = -Vy;
        }
    }
}
