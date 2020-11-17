using Broadway_Boogie_Weggie.Models;
using Broadway_Boogie_Weggie.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Visitors
{
    public class NormalVisitor : IVisitor
    {
        private MainViewModel main;
        public NormalVisitor(MainViewModel main)
        {
            this.main = main;
            main.ObservableTiles = new ObservableCollection<DrawTile>();
        }

        public void Visit(Tile tile)
        {
            main.ObservableTiles.Add(new DrawTile(tile.X, tile.Y, tile.Weight, tile.Color));
        }

        public void Visit(Artist artist)
        {
            main.ObservableArtists.Add(artist);
        }

    }
}
