using Broadway_Boogie_Weggie.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Services
{
    public interface IQuadTree<T> where T : IQuadNode
    {
        int Depth();
        IEnumerable<IQuadNode> QueryRange(Boundry range);
        IEnumerable<T> DetectCollision(IOnCollisionDetection<T> strategy);
        void Insert(T entity);
        IEnumerable<Boundry> GetBoundries();
    }

    public class QuadTree<T> : IQuadTree<T> where T : IQuadNode
    {
        private static int MAX_DEPTH = 8;

        private int _node_capacity = 4;
        private int numerOfItems = 0;
        public int NumberOfItems => numerOfItems;
        private T[] entities;

        public Boundry Boundry { get; private set; }
        public QuadTree<T> NorthWest { get; private set; }
        public QuadTree<T> NorthEast { get; private set; }
        public QuadTree<T> SouthWest { get; private set; }
        public QuadTree<T> SouthEast { get; private set; }

        public QuadTree(Boundry b)
        {
            this.Boundry = b;
            entities = new T[_node_capacity];
        }

        private int depth = 1;
        public QuadTree(int depth, Boundry b)
        {
            this.depth = depth;
            this.Boundry = b;
            entities = new T[_node_capacity];
        }

        private void subDivide()
        {
            var halfY = (Boundry.MaxY + Boundry.MinY) / 2;
            var halfX = (Boundry.MaxX + Boundry.MinX) / 2;

            this.NorthEast = new QuadTree<T>(depth + 1, new Boundry(halfX, halfY, Boundry.MaxX, Boundry.MaxY));
            this.NorthWest = new QuadTree<T>(depth + 1, new Boundry(Boundry.MinX, halfY, halfX, Boundry.MaxY));
            this.SouthWest = new QuadTree<T>(depth + 1, new Boundry(Boundry.MinX, Boundry.MinY, halfX, halfY));
            this.SouthEast = new QuadTree<T>(depth + 1, new Boundry(halfX, Boundry.MinY, Boundry.MaxX, halfY));
        }

        public int Depth()
        {
            if (this.NorthEast != null)
            {
                var nwD = NorthWest.Depth();
                var neD = NorthEast.Depth();
                var swD = SouthWest.Depth();
                var seD = SouthEast.Depth();

                if (nwD >= neD && nwD >= swD && nwD >= seD) return nwD + 1;
                if (neD >= nwD && neD >= swD && neD >= seD) return neD + 1;
                if (swD >= neD && swD >= nwD && swD >= seD) return swD + 1;
                if (seD >= neD && seD >= swD && seD >= nwD) return seD + 1;
            }
            return 1;
        }

        private int getDepth() => depth;

        public IEnumerable<IQuadNode> QueryRange(Boundry range)
        {
            // RsultArray
            List<IQuadNode> retList = new List<IQuadNode>();

            // Does not instersect with this quad
            if (!this.Boundry.Intersects(range))
                return retList;

            // Add entities and return, this is lowest level element
            if (this.NorthEast == null)
            {
                for (int p = 0; p < this.entities.Length; p++)
                {
                    // Check entity in range
                    if (range.ContainsPoint(entities[p]))
                        retList.Add(entities[p]);
                }
                return retList;
            }

            // Ask children for points
            retList.AddRange(this.NorthWest.QueryRange(range));
            retList.AddRange(this.NorthEast.QueryRange(range));
            retList.AddRange(this.SouthWest.QueryRange(range));
            retList.AddRange(this.SouthEast.QueryRange(range));

            return retList;
        }

        public IEnumerable<T> DetectCollision(IOnCollisionDetection<T> strategy)
        {
            var retList = new List<T>();
            // No childs
            if (NorthEast == null)
            {
                retList.AddRange(strategy.DetectCollision(entities));
            }
            // Append child returns
            else
            {
                retList.AddRange(NorthWest.DetectCollision(strategy));
                retList.AddRange(NorthEast.DetectCollision(strategy));
                retList.AddRange(SouthWest.DetectCollision(strategy));
                retList.AddRange(SouthEast.DetectCollision(strategy));
            }
            return retList;
        }

        public IEnumerable<Boundry> GetBoundries()
        {
            var retList = new List<Boundry>();
            retList.Add(this.Boundry);

            if (NorthWest != null)
                retList.AddRange(NorthWest.GetBoundries());
            if (NorthEast != null)
                retList.AddRange(NorthEast.GetBoundries());
            if (SouthWest != null)
                retList.AddRange(SouthWest.GetBoundries());
            if (SouthEast != null)
                retList.AddRange(SouthEast.GetBoundries());

            return retList;
        }

        public void Insert(T entity)
        {
            // Check if point is within range
            if (!Boundry.ContainsPoint(entity))
            {
                return;
            }
            // Check wheter there can be an insert
            else if (numerOfItems < _node_capacity)
            {
                entities[numerOfItems] = entity;
                numerOfItems++;
                return;
            }

            if (NorthEast == null)
            {
                if (this.getDepth() < MAX_DEPTH)
                {
                    subDivide();

                    // Insert existing in new quadrant
                    for (int i = 0; i < numerOfItems; i++)
                    {
                        NorthWest.Insert(entities[i]);
                        NorthEast.Insert(entities[i]);
                        SouthWest.Insert(entities[i]);
                        SouthEast.Insert(entities[i]);
                    }
                }
                else
                {
                    if (entities.Length == numerOfItems)
                    {
                        var newLenght = entities.Length * 2;
                        var newArr = new T[newLenght];
                        entities.CopyTo(newArr, 0);
                        entities = newArr;
                    }
                    entities[numerOfItems] = entity;
                    numerOfItems++;
                    return;
                }
            }
            // Insert the new entity
            NorthWest.Insert(entity);
            NorthEast.Insert(entity);
            SouthWest.Insert(entity);
            SouthEast.Insert(entity);
        }
    }
}
