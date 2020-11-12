using Broadway_Boogie_Weggie.Commands;
using Broadway_Boogie_Weggie.Factories;
using Broadway_Boogie_Weggie.Importers;
using Broadway_Boogie_Weggie.Models;
using Broadway_Boogie_Weggie.Parsers;
using Broadway_Boogie_Weggie.Readers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Broadway_Boogie_Weggie.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand SetupGalleryDiscCommand { get; set; }
        public ObservableCollection<Tile> ObservableTiles { get; set; }
        public ObservableCollection<Artist> ObservableArtists { get; set; }

        //public GalaxyThread galaxyThread;

        public MainViewModel()
        {
            ObservableTiles = new ObservableCollection<Tile>();
            ObservableArtists = new ObservableCollection<Artist>();
            SetupGalleryDiscCommand = new SetupGalleryDiscCommand();
        }

        public void SetupGallery(string importType)
        {
            try
            {
                this.ObservableTiles.Clear();
                this.ObservableArtists.Clear();
                string filePath = Import(importType);
                List<string> content = Read(filePath);
                List<List<KeyValuePair<string, string>>> starList;
                List<KeyValuePair<string, string>> neighbourList;
                Parse(content, out starList, out neighbourList);
                Gallery galaxy = Build(starList, neighbourList);
                //StartGalaxyThread(galaxy);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private List<string> Read(string filePath)
        {
            IReader reader = ReaderFactory.Instance.CreateReader(filePath);
            return reader.Read(filePath);
        }

        private string Import(string importType)
        {
            Importer importer = ImporterFactory.Instance.CreateImporter(importType);
            return importer.Import();
        }

        private void Parse(List<string> content, out List<List<KeyValuePair<string, string>>> tileList, out List<KeyValuePair<string, string>> neighbourList)
        {
            Parser parser = ParserFactory.Instance.CreateParser(content[0]);
            parser.Parse(content, out tileList, out neighbourList);
        }
        private Gallery Build(List<List<KeyValuePair<string, string>>> tileList, List<KeyValuePair<string, string>> neighbourList)
        {
            GalleryBuilder builder = new GalleryBuilder();

            foreach (List<KeyValuePair<string, string>> t in tileList)
            {
                builder.AddTile(t);
            }
            foreach (KeyValuePair<string, string> n in neighbourList)
            {
                builder.LinkTiles(n.Key, n.Value);
            }

            return builder.Build();
        }
    }
}

