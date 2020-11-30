using Broadway_Boogie_Weggie.Models;
using GalaSoft.MvvmLight;
using System;

namespace Broadway_Boogie_Weggie.ViewModels
{
    public class SquareViewModel : ViewModelBase, Models.IObserver<Square>
    {
        private double _x;
        private double _y;
        private string _color;
        private bool _isVisited;
        private bool _isPath;
        private bool _isSelected;

        public bool IsVisited
        {
            get => _isVisited; set
            {
                _isVisited = value;
                RaisePropertyChanged(() => IsVisited);
            }
        }

        public bool IsPath
        {
            get => _isPath; set
            {
                _isPath = value;
                RaisePropertyChanged(() => IsPath);
            }
        }

        public bool IsSelected
        {
            get => _isSelected; set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        public double GalleryX
        {
            get => _x; set
            {
                if (_x == value)
                    return;
                _x = value;
                RaisePropertyChanged(() => GalleryX);
            }
        }
        public double GalleryY
        {
            get => _y; set
            {
                if (_y == value)
                    return;
                _y = value;
                RaisePropertyChanged(() => GalleryY);
            }
        }

        public double Width => Square.Width;
        public double Height => Square.Height;
        public string Color
        {
            get => _color; set
            {
                if (_color == value)
                    return;
                _color = value;
                RaisePropertyChanged(() => Color);
            }
        }
        public Square Square { get; }

        public SquareViewModel(Square square)
        {
            this.Square = square;
            square.AddObserver(this);
            this.Square.Notify();
        }

        public void Update(Square obj)
        {
            GalleryX = obj.GalleryX;
            GalleryY = obj.GalleryY;
            IsVisited = obj.IsVisited;
            IsPath = obj.IsPath;
            if (obj.IsColliding)
            {
                Color = "red";
            }
            else
            {
                Color = obj.Color;
            }
        }
    }
}
