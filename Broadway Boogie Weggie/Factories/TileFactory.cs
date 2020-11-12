using Broadway_Boogie_Weggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Factories
{
    class TileFactory
    {
        private static TileFactory instance;

        public static TileFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TileFactory();
                }
                return instance;
            }
        }

        public Tile CreateTile(double x, double y, string color)
        {
            return new Tile(x, y, color);
        }
    }
}
