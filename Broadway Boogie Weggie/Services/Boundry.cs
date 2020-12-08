using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Services
{
    public class Boundry
    {
        public double MinX { get; set; }
        public double MinY { get; set; }
        public double MaxX { get; set; }
        public double MaxY { get; set; }

        public Boundry(double entityMinX, double entityMinY, double entityMaxX, double entityMaxY)
        {
            this.MaxX = entityMaxX;
            this.MinX = entityMinX;

            this.MinY = entityMinY;
            this.MaxY = entityMaxY;
        }

        public bool Intersects(Boundry other)
        {
            if (other.MinX >= MinX && other.MinX <= MaxX)
            {
                if (other.MinY >= MinY && other.MinY <= MaxY)
                {
                    return true;
                }
                if (other.MaxY <= MaxY && other.MaxY >= MinY)
                {
                    return true;
                }
            }
            if (other.MaxX <= MaxX && other.MaxX >= MinX)
            {

                if (other.MinY >= MinY && other.MinY <= MaxY)
                {
                    return true;
                }
                if (other.MaxY <= MaxY && other.MaxY >= MinY)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsPoint(IQuadNode node)
        {
            if (node == null) return false;
            var entityMinX = node.X - node.HalfDimension;
            var entityMaxX = node.X + node.HalfDimension;

            var entityMinY = node.Y - node.HalfDimension;
            var entityMaxY = node.Y + node.HalfDimension;

            if (entityMinX >= MinX && entityMinX <= MaxX)
            {
                if (entityMinY >= MinY && entityMinY <= MaxY)
                {
                    return true;
                }
                if (entityMaxY <= MaxY && entityMaxY >= MinY)
                {
                    return true;
                }
            }
            if (entityMaxX <= MaxX && entityMaxX >= MinX)
            {

                if (entityMinY >= MinY && entityMinY <= MaxY)
                {
                    return true;
                }
                if (entityMaxY <= MaxY && entityMaxY >= MinY)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
