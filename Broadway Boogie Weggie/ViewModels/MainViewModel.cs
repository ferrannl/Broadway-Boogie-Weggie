using Broadway_Boogie_Weggie.Commands;
using Broadway_Boogie_Weggie.Factories;
using Broadway_Boogie_Weggie.Importers;
using Broadway_Boogie_Weggie.Models;
using Broadway_Boogie_Weggie.Parsers;
using Broadway_Boogie_Weggie.Readers;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace Broadway_Boogie_Weggie.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public bool Running { get; set; }
        public ICommand SetupGalleryDiscCommand { get; set; }
        public ICommand PausePlayGalleryCommand { get; set; }
        public ObservableCollection<SquareViewModel> Squares { get; set; }

        public MainViewModel()
        {
            Squares = new ObservableCollection<SquareViewModel>();
            SetupGalleryDiscCommand = new SetupGalleryDiscCommand();
            PausePlayGalleryCommand = new PausePlayGalleryCommand();
            CompositionTarget.Rendering += (s, e) => UpdateGallery();
        }

        public void SetupGallery(string importType)
        {
            string filePath = Import(importType);
            List<string> content = Read(filePath);
            if (content[0].Equals("csv"))
            {
                foreach (var artist in CsvParse(content))
                {
                    Squares.Add(new SquareViewModel(artist));
                }
            }
            else if (content[0].Equals("xml"))
            {
                foreach (var tile in XmlParse(content))
                {
                    Squares.Add(new SquareViewModel(tile));
                }
            }
        }

        private void UpdateGallery()
        {
            if (Running)
            {
                foreach (var square in Squares.Select(sq => sq.Square))
                {
                    if (square is Artist artist)
                    {
                        artist.Step();
                    }
                }
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

        private List<Tile> XmlParse(List<string> content)
        {
            XmlParser parser = ParserFactory.Instance.CreateXmlParser(content[0]);
            return parser.Parse(content);
        }
        private List<Artist> CsvParse(List<string> content)
        {
            CsvParser parser = ParserFactory.Instance.CreateCsvParser(content[0]);
            return parser.Parse(content);
        }

    }
}

