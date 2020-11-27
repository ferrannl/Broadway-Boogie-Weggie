using Broadway_Boogie_Weggie.Models;
using Broadway_Boogie_Weggie.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Services
{
    public class CollisionService : ICollisionService
    {
        public void CheckCollision(ICollection<Square> squares)
        {
            foreach (var square in squares)
            {
                square.IsColliding = false;
                square.IsVisited = true;
            }
            foreach (var square1 in squares)
            {
                if (square1 is Artist)
                {
                    foreach (var square2 in squares)
                    {
                        if (square1 != square2 && square2 is Artist)
                        {
                            if (square1.GalleryX < square2.GalleryX + square2.Width &&
                               square1.GalleryX + square1.Width > square2.GalleryX &&
                               square1.GalleryY < square2.GalleryY + square2.Height &&
                               square1.GalleryY + square1.Height > square2.GalleryY)
                            {
                                square1.IsColliding = true;
                                square2.IsColliding = true;
                            }
                        }
                    }
                }
            }
        }

        //TODO
        //CheckCollisionQuadTree

    }
}
