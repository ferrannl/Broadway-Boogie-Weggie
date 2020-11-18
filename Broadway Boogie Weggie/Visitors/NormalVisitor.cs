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
            //main.ObservableTiles = new ObservableCollection<Tile>();
            //main.ObservableArtists = new ObservableCollection<Artist>();
            main.ObservableArtists.Clear();
            main.ObservableTiles.Clear();
            main.RaisePropertyChanged(() => main.ObservableTiles);
            main.RaisePropertyChanged(() => main.ObservableArtists);
        }

        public void Visit(Tile tile)
        {
            main.ObservableTiles.Add(new Tile(tile.X * tile.Width, tile.Y * tile.Height, tile.Weight, tile.Color));
        }

        public void Visit(Artist artist)
        {
            main.ObservableArtists.Add(new Artist(artist.X * artist.Width * 2, artist.Y * artist.Height * 2));
        }
    }
}
