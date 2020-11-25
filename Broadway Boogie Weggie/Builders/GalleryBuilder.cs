//using Broadway_Boogie_Weggie.Factories;
//using Broadway_Boogie_Weggie.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Shapes;

//namespace Broadway_Boogie_Weggie.Builders
//{
//    public class GalleryBuilder
//    {
//        private Gallery gallery;

//        public GalleryBuilder()
//        {
//            gallery = new Gallery();
//        }

//        public void AddTile(Tile tile)
//        {
//            try
//            {
//                gallery.Tiles.Add(tile);
//            }
//            catch (Exception e)
//            {
//                throw new Exception("File content is not valid.");
//            }
//        }
//        public void AddArtist(Artist artist)
//        {
//            try
//            {
//                gallery.Artists.Add(artist);
//            }
//            catch (Exception e)
//            {
//                throw new Exception("File content is not valid.");
//            }
//        }

//        private static double ToDouble(string s)
//        {
//            s = s.Replace('.', ',');
//            if (double.TryParse(s, out var result))
//                return result;
//            throw new ArgumentException("Failed to parse a string to a double!");
//        }

//        public void LinkTiles(string tile1, string tile2)
//        {
//            //List<Planet> planets = Gallery.stars.Where(s => s.GetType() == typeof(Planet)).Select(s => (Planet)s).ToList();

//            //Planet p1 = planets.Where(p => p.name.Equals(starName1)).FirstOrDefault();
//            //Planet p2 = planets.Where(p => p.name.Equals(starName2)).FirstOrDefault();
//            //if (p1 != null && p2 != null)
//            //{
//            //    p1.neighbours.Add(p2);
//            //}
//            //else
//            //{
//            //    throw new Exception("Ongeldige linkpoging, het Gallery-bestand is niet geldig.");
//            //}
//        }

//        public Gallery Build()
//        {
//            return gallery;
//        }
//    }
//}