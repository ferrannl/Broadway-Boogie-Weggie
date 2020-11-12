using Broadway_Boogie_Weggie.ViewModels;
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
        public readonly static int WIDTH, HEIGHT = 800;
        public List<Tile> tiles;

        public Gallery()
        {
            tiles = new List<Tile>();

        }

        public void SetupGallery()
        {

        }

        public void Tick()
        {
            foreach (Star s in stars)
            {
                s.Step();
                s.CheckCollisionWall();
            }
            foreach (KeyValuePair<Star, bool> c in collisionDetection.CheckCollision(stars))
            {
                if (c.Value)
                {
                    c.Key.OnCollision(this);
                }
                else
                {
                    c.Key.OnNoCollision();
                }
            }
        }

        public void Draw()
        {
            try
            {
                var mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
                // DrawStarVisitor starVisitor = new DrawStarVisitor(mainViewModel);
                //   DrawConnectionVisitor connectionVisitor = new DrawConnectionVisitor(mainViewModel);
                foreach (Tile tile in tiles)
                {
                    // star.Accept(starVisitor);
                    // star.Accept(connectionVisitor);
                }
            }
            catch (Exception e) { }
        }

    }
}
