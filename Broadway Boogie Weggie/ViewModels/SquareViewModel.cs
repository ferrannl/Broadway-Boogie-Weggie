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
            Color = obj.Color;
        }
    }
}
