using Broadway_Boogie_Weggie.Commands;
using Broadway_Boogie_Weggie.Factories;
using Broadway_Boogie_Weggie.Importers;
using Broadway_Boogie_Weggie.Models;
using Broadway_Boogie_Weggie.Parsers;
using Broadway_Boogie_Weggie.Readers;
using Broadway_Boogie_Weggie.Services;
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
        private bool _useBfsAlgorithm;
        private bool _useQuadTree;
        private bool _showPath;
        private bool _showVisited;
        public ObservableCollection<Boundry> RenderQuadTreeBoundries { get; set; }
        public bool UsePathCollision { get; set; }
        public bool ShowArtists { get; set; }
        public bool ShowPath
        {
            get => _showPath;
            set
            {
                _showPath = value;
                RaisePropertyChanged(() => ShowPath);
            }
        }

        private bool _running { get; set; }
        public bool ShowVisited
        {
            get => _showVisited;
            set
            {
                _showVisited = value;
                RaisePropertyChanged(() => ShowVisited);
            }
        }

        public bool ShowQuadTree { get; private set; }
        public bool UseBfsAlgorithm
        {
            get => _useBfsAlgorithm;
            set
            {
                _useBfsAlgorithm = value;
                RaisePropertyChanged(() => UseBfsAlgorithm);
            }
        }
        public bool UseQuadTree
        {
            get => _useQuadTree;
            set
            {
                _useQuadTree = value;
                RaisePropertyChanged(() => UseQuadTree);
            }
        }
        public bool SelectedPathInitiated
        {
            get => _selectedPathInitiated;
            set
            {
                _selectedPathInitiated = value;
                RaisePropertyChanged(() => SelectedPathInitiated);
            }
        }
        #region
        public SquareViewModel SelectedBeginning { get; set; }
        public SquareViewModel SelectedEnd { get; set; }
        public ICommand SetupGalleryDiscCommand { get; set; }
        public ICommand PausePlayGalleryCommand { get; set; }
        public ICommand SelectSquareAsStartingPointCommand { get; set; }
        public ICommand SelectSquareAsEndPointCommand { get; set; }
        public ICommand ToggleAlgorithmCommand { get; set; }
        public ICommand ToggleCollisionMethodCommand { get; set; }
        public ICommand ToggleQuadTreeCommand { get; set; }
        public ICommand ToggleArtistsCommand { get; set; }
        public ICommand TogglePathCollisionCommand { get; set; }
        public ICommand TogglePathCommand { get; set; }
        public ICommand ToggleVisitedCommand { get; set; }
        #endregion

        public ObservableCollection<SquareViewModel> Squares { get; set; }

        public MainViewModel(ICollisionService collisionService, IAlgorithmService algorithmService)
        {
            UseBfsAlgorithm = false;
            ShowVisited = true;
            this._collisionService = collisionService;
            this._algorithmService = algorithmService;
            Squares = new ObservableCollection<SquareViewModel>();
            RenderQuadTreeBoundries = new ObservableCollection<Boundry>();
            SetupGalleryDiscCommand = new SetupGalleryDiscCommand();
            SelectSquareAsStartingPointCommand = new RelayCommand<SquareViewModel>(SelectSquareAsStartingPoint);
            SelectSquareAsEndPointCommand = new RelayCommand<SquareViewModel>(SelectSquareAsEndPoint);
            ToggleAlgorithmCommand = new RelayCommand(() => UseBfsAlgorithm ^= true);
            ToggleCollisionMethodCommand = new RelayCommand(() => UseQuadTree ^= true);
            PausePlayGalleryCommand = new RelayCommand(() => _running ^= true);
            ToggleQuadTreeCommand = new RelayCommand(() => { if (UseQuadTree) { ShowQuadTree ^= true; } });
            ToggleArtistsCommand = new RelayCommand(() => ShowArtists ^= true);
            TogglePathCollisionCommand = new RelayCommand(() => UsePathCollision ^= true);
            TogglePathCommand = new RelayCommand(() => ShowPath ^= true);
            ToggleVisitedCommand = new RelayCommand(() => ShowVisited ^= true);
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
            IEnumerable<Square> path;
            if (UseBfsAlgorithm)
            {
                path = _algorithmService.GetShortestPath(SelectedBeginning.Square as Tile, SelectedEnd.Square as Tile);
            }
            else
            {
                path = _algorithmService.GetCheapestPath(SelectedBeginning.Square as Tile, SelectedEnd.Square as Tile);
            }
            HighlightPath(path);
        }

        private void HighlightPath(IEnumerable<Square> squares)
        {
            foreach (var square in squares)
            {
                square.IsPath = true;
            }
        }

        private void UpdateGallery()
        {
            if (_running)
            {
                foreach (var square in Squares.Select(sq => sq.Square))
                {
                    if (square is Artist artist)
                    {
                        artist.Step();
                    }
                }
                _collisionService.CheckCollision(Squares.Select(sq => sq.Square).ToList(), UsePathCollision, UseQuadTree);
                if (ShowQuadTree)
                {
                    RenderQuadTreeBoundries.Clear();
                    foreach (var item in _algorithmService.GetBoundries())
                    {
                        RenderQuadTreeBoundries.Add(item);
                    }
                }
                else
                {
                    RenderQuadTreeBoundries.Clear();
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

