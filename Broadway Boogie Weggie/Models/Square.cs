
using System.Collections.Generic;

namespace Broadway_Boogie_Weggie.Models
{
    public abstract class Square : IObservable<Square>
    {
        private readonly List<IObserver<Square>> _observers;
        private double _x;
        private double _y;
        private string _color;
        private bool _isColliding;
        private bool _isVisited;
        private bool _isPath;

        public bool IsColliding
        {
            get => _isColliding;
            set
            {
                _isColliding = value;
                Notify();
            }
        }

        public bool IsVisited
        {
            get => _isVisited;
            set
            {
                _isVisited = value;
                Notify();
            }
        }

        public bool IsPath
        {
            get => _isPath;
            set
            {
                _isPath = value;
                Notify();
            }
        }

        public double GalleryX
        {
            get => _x;
            set
            {
                if (_x == value)
                    return;
                _x = value;
                Notify();
            }
        }
        public double GalleryY
        {
            get => _y;
            set
            {
                if (_y == value)
                    return;
                _y = value;
                Notify();
            }
        }
        public abstract double Width { get; }
        public abstract double Height { get; }
        public string Color
        {
            get => _color;
            set
            {
                if (_color == value)
                    return;
                _color = value;
                Notify();
            }
        }

        public Square(double x, double y, string color)
        {
            _observers = new List<IObserver<Square>>();
            this.GalleryX = x;
            this.GalleryY = y;
            this.Color = color;
        }

        public void AddObserver(IObserver<Square> observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver<Square> observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
    }
}
