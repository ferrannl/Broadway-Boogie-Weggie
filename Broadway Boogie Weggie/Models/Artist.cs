namespace Broadway_Boogie_Weggie.Models
{
    public class Artist : Square
    {
        public double Vx { get; set; }
        public double Vy { get; set; }

        public override double Width => 0.5;

        public override double Height => 0.5;

        public Artist(double x, double y, double vx, double vy) : base(x, y, "black")
        {
            this.Vx = vx;
            this.Vy = vy;
        }

        public void Step()
        {
            this.GalleryX += Vx;
            this.GalleryY += Vy;
            CheckCollisionWall();
        }
        private void CheckCollisionWall()
        {
            if (GalleryX < 0 || GalleryX > 53)
                Vx = -Vx;
            if (GalleryY < 0 || GalleryY > 53)
                Vy = -Vy;
        }
    }
}
