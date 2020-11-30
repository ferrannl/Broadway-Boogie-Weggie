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
        public void CheckCollision(ICollection<Square> squares, bool CollisionWithPath)
        {
            foreach (var square in squares)
            {
                square.IsColliding = false;
            }
            foreach (var artist in squares.OfType<Artist>())
            {
                foreach (var square in squares)
                {
                    if (artist != square && (square is Artist || square.IsPath))
                    {
                        if (artist.GalleryX < square.GalleryX + square.Width &&
                           artist.GalleryX + artist.Width > square.GalleryX &&
                           artist.GalleryY < square.GalleryY + square.Height &&
                           artist.GalleryY + artist.Height > square.GalleryY)
                        {

                            artist.IsColliding = true;
                            if (!square.IsPath && CollisionWithPath)
                            {
                                square.IsColliding = true;
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
