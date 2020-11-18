using Broadway_Boogie_Weggie.ViewModels;
using Broadway_Boogie_Weggie.Visitors;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Models
{
    public class Gallery
    {
        public readonly static int WIDTH = 800;
        public readonly static int HEIGHT = 800;
        public readonly static int TILE_WIDTH = WIDTH / 53;
        public readonly static int TILE_HEIGHT = HEIGHT / 53;
        public readonly static int ARTIST_WIDTH = TILE_WIDTH / 2;
        public readonly static int ARTIST_HEIGHT = TILE_WIDTH / 2;
        public List<Tile> Tiles;
        public List<Artist> Artists;

        public Gallery()
        {
            Tiles = new List<Tile>();
            Artists = new List<Artist>();
        }

        public void Tick()
        {
            foreach (Artist a in Artists)
            {
                a.Step();
                a.CheckCollisionWall();
            }
        }

        public void Draw()
        {
            try
            {
                var mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
                NormalVisitor normalVisitor = new NormalVisitor(mainViewModel);
                if (Tiles.Count > 0)
                {
                    foreach (Tile tile in Tiles)
                    {
                        tile.Accept(normalVisitor);
                    }
                }
                foreach (Artist artist in Artists)
                {
                    artist.Accept(normalVisitor);
                }
            }
            catch (Exception e) { }
        }

    }
}
