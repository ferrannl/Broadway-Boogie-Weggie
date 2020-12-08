using Broadway_Boogie_Weggie.Commands;
using Broadway_Boogie_Weggie.Factories;
using Broadway_Boogie_Weggie.Importers;
using Broadway_Boogie_Weggie.Models;
using Broadway_Boogie_Weggie.Parsers;
using Broadway_Boogie_Weggie.Readers;
using Broadway_Boogie_Weggie.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace Broadway_Boogie_Weggie.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ICollisionService _collisionService;
        private readonly IAlgorithmService _algorithmService;
        private bool _selectedPathInitiated;
        public bool UseBfsAlgorithm { get; set; }
        public bool Running { get; set; }
        public bool SelectedPathInitiated
        {
            get => _selectedPathInitiated;
            set
            {
                _selectedPathInitiated = value;
                RaisePropertyChanged(() => SelectedPathInitiated);
            }
        }
        public SquareViewModel SelectedBeginning { get; set; }
        public SquareViewModel SelectedEnd { get; set; }
        public ICommand SetupGalleryDiscCommand { get; set; }
        public ICommand PausePlayGalleryCommand { get; set; }
        public ICommand SelectSquareAsStartingPointCommand { get; set; }
        public ICommand SelectSquareAsEndPointCommand { get; set; }
        public ICommand ToggleAlgorithmCommand { get; set; }

        public ObservableCollection<SquareViewModel> Squares { get; set; }

        public MainViewModel(ICollisionService collisionService, IAlgorithmService algorithmService)
        {
            UseBfsAlgorithm = false;
            this._collisionService = collisionService;
            this._algorithmService = algorithmService;
            Squares = new ObservableCollection<SquareViewModel>();
            SetupGalleryDiscCommand = new SetupGalleryDiscCommand();
            SelectSquareAsStartingPointCommand = new RelayCommand<SquareViewModel>(SelectSquareAsStartingPoint);
            SelectSquareAsEndPointCommand = new RelayCommand<SquareViewModel>(SelectSquareAsEndPoint);
            ToggleAlgorithmCommand = new RelayCommand(() => UseBfsAlgorithm ^= true);
            PausePlayGalleryCommand = new RelayCommand(() => Running ^= true);
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

        private void SelectSquareAsStartingPoint(SquareViewModel squareViewModel)
        {
            _algorithmService.ResetAlgorithm(Squares.Select(s => s.Square));
            if (SelectedBeginning != null)
            {
                SelectedBeginning.IsSelected = false;
            }
            if (SelectedEnd != null)
            {
                SelectedEnd.IsSelected = false;
                SelectedEnd = null;
            }
            SelectedPathInitiated = true;
            SelectedBeginning = squareViewModel;
            squareViewModel.IsSelected = true;
        }
        private void SelectSquareAsEndPoint(SquareViewModel squareViewModel)
        {
            if (SelectedEnd != null)
            {
                SelectedEnd.IsSelected = false;
                _algorithmService.ResetAlgorithm(Squares.Select(s => s.Square));
            }
            SelectedEnd = squareViewModel;
            squareViewModel.IsSelected = true;

            if (UseBfsAlgorithm)
            {
                var shortestPath = _algorithmService.GetShortestPath(SelectedBeginning.Square as Tile, SelectedEnd.Square as Tile);
                foreach (var tile in shortestPath)
                {
                    tile.IsPath = true;
                }

            }
            else
            {
                _algorithmService.GetCheapestPath(SelectedBeginning.Square as Tile, SelectedEnd.Square as Tile);
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
                _collisionService.CheckCollision(Squares.Select(sq => sq.Square).ToList(), true);
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

