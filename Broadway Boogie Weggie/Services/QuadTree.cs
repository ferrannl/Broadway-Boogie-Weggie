using Broadway_Boogie_Weggie.Models;
using System.Collections.Generic;
using System.Linq;

namespace Broadway_Boogie_Weggie.Services
{
    public class QuadTree
    {
        private const int MAX_SPLIT_AMOUNT = 5;
        private const int MAX_CAPACITY = 4;
        private const int QUADTREE_CHILDS = 4;
        private Boundry _boundry;
        private readonly List<Artist> _artists;
        private readonly List<QuadTree> _quadTreeChilds;
        private bool _needsSplitting => ((_artists.Count > MAX_CAPACITY) && (_currentDepth < MAX_SPLIT_AMOUNT));
        private int _currentDepth;
        public bool IsSplitted => _quadTreeChilds.Count() > 0;
        public IEnumerable<Artist> Content => _artists;

        public QuadTree(int depth, double x, double y, double height, double width)
        {
            _artists = new List<Artist>();
            _quadTreeChilds = new List<QuadTree>(QUADTREE_CHILDS);
            _currentDepth = depth;
            _boundry = new Boundry() { X = x, Y = y, Height = height, Width = width };
        }

        public void Insert(Artist artist)
        {

            if (IsSplitted)
            {
                foreach (var position in this.GetQuadTreeIndex(artist))
                {
                    _quadTreeChilds[position].Insert(artist);
                }
                return;
            }

            _artists.Add(artist);
            if (_needsSplitting)
            {
                Split();
                foreach (var item in _artists)
                {
                    foreach (var position in this.GetQuadTreeIndex(item))
                    {
                        _quadTreeChilds[position].Insert(item);
                    }
                }
                _artists.Clear();
            }
        }

        private List<int> GetQuadTreeIndex(Artist artist)
        {
            var _result = new List<int>();
            bool isTop = (ToRenderPosition(artist.GalleryY) <= _boundry.Y + (_boundry.Height / 2));
            bool isBottom = (ToRenderPosition(artist.GalleryY + (artist.Height / 2)) >= _boundry.Y + (_boundry.Height / 2));
            bool isLeft = (ToRenderPosition(artist.GalleryX) <= _boundry.X + (_boundry.Width / 2));
            bool isRight = (ToRenderPosition(artist.GalleryX + (artist.Width / 2)) >= _boundry.X + (_boundry.Width / 2));
            if (isTop && isLeft)
            {
                _result.Add(0);
            }
            if (isTop && isRight)
            {
                _result.Add(1);
            }
            if (isBottom && isLeft)
            {
                _result.Add(2);
            }
            if (isBottom && isRight)
            {
                _result.Add(3);
            }
            return _result;
        }

        private double ToRenderPosition(double pos)
        {
            return (pos * 800 / 52);
        }

        public IEnumerable<QuadTree> GetOuterChilds()
        {
            var childs = new List<QuadTree>();
            if (IsSplitted)
            {
                foreach (var child in _quadTreeChilds)
                {
                    childs.AddRange(child.GetOuterChilds());
                }
            }
            else
            {
                childs.Add(this);
            }
            return childs;
        }

        private void Split()
        {
            //Links boven idx 0
            _quadTreeChilds.Add(new QuadTree(_currentDepth + 1, _boundry.X, _boundry.Y, (_boundry.Height / 2), (_boundry.Width / 2)));

            //Rechtsboven idx 1
            _quadTreeChilds.Add(new QuadTree(_currentDepth + 1, _boundry.X + _boundry.Width / 2, _boundry.Y, (_boundry.Height / 2), (_boundry.Width / 2)));

            //Linksonder idx 2
            _quadTreeChilds.Add(new QuadTree(_currentDepth + 1, _boundry.X, _boundry.Y + _boundry.Height / 2, (_boundry.Height / 2), (_boundry.Width / 2)));

            //Rechtsonder idx 3
            _quadTreeChilds.Add(new QuadTree(_currentDepth + 1, _boundry.X + _boundry.Width / 2, _boundry.Y + _boundry.Height / 2, (_boundry.Height / 2), (_boundry.Width / 2)));
        }

        public IEnumerable<Boundry> GetBoundries()
        {
            var boundries = new List<Boundry>();

            boundries.Add(this._boundry);
            foreach (var child in _quadTreeChilds)
            {
                boundries.AddRange(child.GetBoundries());
            }
            return boundries;
        }
    }
}