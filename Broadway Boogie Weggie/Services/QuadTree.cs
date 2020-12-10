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
        private Boundry boundry;
        private readonly List<Artist> _artists;
        private readonly List<QuadTree> _quadTreeChilds;
        private bool _needsSplitting => _artists.Count > MAX_CAPACITY && _currentDepth < MAX_SPLIT_AMOUNT;
        private int _currentDepth;
        public bool IsSplitted => _quadTreeChilds.Count() > 0;
        public IEnumerable<Artist> Content => _artists;

        public QuadTree(int depth, double x, double y, double height, double width)
        {
            _artists = new List<Artist>(MAX_CAPACITY);
            _quadTreeChilds = new List<QuadTree>(QUADTREE_CHILDS);
            _currentDepth = depth;
            boundry = new Boundry() { X = x, Y = y, Height = height, Width = width };
        }

        public void Insert(Artist artist)
        {
            if (IsSplitted)
            {
                _quadTreeChilds[GetQuadTreeIndex(artist)].Insert(artist);
                return;
            }

            _artists.Add(artist);
            if (_needsSplitting)
            {
                Split();
                foreach (var item in this._artists)
                {
                    _quadTreeChilds[GetQuadTreeIndex(artist)].Insert(artist);
                }
                _artists.Clear();
            }
        }

        private int GetQuadTreeIndex(Artist artist)
        {
            bool isTop = (ToRenderPosition(artist.GalleryY) < boundry.Height / 2);
            bool isBottom = (ToRenderPosition(artist.GalleryY) > boundry.Height / 2);
            bool isLeft = (ToRenderPosition(artist.GalleryX) < boundry.Width / 2);
            bool isRight = (ToRenderPosition(artist.GalleryX) > boundry.Width / 2);
            if (isTop && isLeft)
            {
                return 0;
            }
            if (isTop && isRight)
            {
                return 1;
            }
            if (isBottom && isLeft)
            {
                return 2;
            }
            if (isBottom && isRight)
            {
                return 3;
            }
            else
            {
                return -1;
            }
        }

        private double ToRenderPosition(double pos)
        {
            return (pos * 800 / 52);
        }

        public IEnumerable<QuadTree> GetOuterChilds()
        {
            var childs = new List<QuadTree>();
            if (this.IsSplitted)
            {
                foreach (var child in this._quadTreeChilds)
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
            if (!_needsSplitting || IsSplitted) { return; }

            //Links boven idx 0
            _quadTreeChilds.Add(new QuadTree(_currentDepth + 1, boundry.X, boundry.Y, (boundry.Height / 2), (boundry.Width / 2)));

            //Rechtsboven idx 1
            _quadTreeChilds.Add(new QuadTree(_currentDepth + 1, boundry.X + boundry.Width / 2, boundry.Y, (boundry.Height / 2), (boundry.Width / 2)));

            //Linksonder idx 2
            _quadTreeChilds.Add(new QuadTree(_currentDepth + 1, boundry.X, boundry.Y + boundry.Height / 2, (boundry.Height / 2), (boundry.Width / 2)));

            //Rechtsonder idx 3
            _quadTreeChilds.Add(new QuadTree(_currentDepth + 1, boundry.X + boundry.Width / 2, boundry.Y + boundry.Height / 2, (boundry.Height / 2), (boundry.Width / 2)));
        }

        public IEnumerable<Boundry> GetBoundries()
        {
            var boundries = new List<Boundry>();

            boundries.Add(this.boundry);
            foreach (var child in _quadTreeChilds)
            {
                boundries.AddRange(child.GetBoundries());
            }
            return boundries;
        }
    }
}