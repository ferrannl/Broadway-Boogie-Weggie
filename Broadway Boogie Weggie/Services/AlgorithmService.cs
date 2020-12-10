using Broadway_Boogie_Weggie.Models;
using Broadway_Boogie_Weggie.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Services
{
    public class AlgorithmService : IAlgorithmService
    {
        private QuadTree _quadTree;
        public void ResetAlgorithm(IEnumerable<Square> squares)
        {
            foreach (var square in squares)
            {
                square.IsPath = false;
                square.IsVisited = false;
            }
        }

        public IEnumerable<Square> GetShortestPath(Tile start, Tile end)
        {
            if (start == null || end == null)
            {
                throw new ArgumentNullException();
            }
            var visited = new Dictionary<Tile, Tile>();
            var queue = new Queue<Tile>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var tile = queue.Dequeue();
                tile.IsVisited = true;
                foreach (var neighbor in tile.Neighbours)
                {
                    if (neighbor.IsVisited)
                    {
                        continue;
                    }
                    queue.Enqueue(neighbor);
                    neighbor.IsVisited = true;

                    visited.Add(neighbor, tile);
                    if (neighbor == end)
                    {
                        queue.Clear();
                        break;
                    }

                }
            }
            return ReconstructPath(start, end, visited);
        }
        private IEnumerable<Tile> ReconstructPath(Tile start, Tile end, IDictionary<Tile, Tile> visitedEdges)
        {
            List<Tile> path = new List<Tile>();
            var current = end;
            path.Add(current);
            while (current != start)
            {
                path.Add(visitedEdges[current]);
                current = visitedEdges[current];
            }
            path.Reverse();
            return path;
        }

        public IEnumerable<Square> GetCheapestPath(Tile start, Tile end)
        {
            var nearestToStart = Dijkstra(start, end);
            return ReconstructPath(start, end, nearestToStart);
        }

        private Dictionary<Tile, Tile> Dijkstra(Tile start, Tile end)
        {
            var nearestToStart = new Dictionary<Tile, Tile>();
            var distanceTo = new Dictionary<Tile, int>();
            distanceTo.Add(start, 0);
            var priorityQueue = new Queue<Tile>();
            priorityQueue.Enqueue(start);
            while (priorityQueue.Count > 0)
            {
                priorityQueue = new Queue<Tile>(priorityQueue.OrderBy(tile => distanceTo[tile]));
                var previous = priorityQueue.Dequeue();
                if (previous == end)
                {
                    return nearestToStart;
                }
                foreach (var neighbour in previous.Neighbours.OrderBy(tile => tile.Weight))
                {
                    var distanceToNeigbour = distanceTo[previous] + neighbour.Weight;
                    if (!neighbour.IsVisited || distanceToNeigbour < distanceTo[neighbour])
                    {

                        distanceTo[neighbour] = distanceToNeigbour;
                        nearestToStart[neighbour] = previous;
                        priorityQueue.Enqueue(neighbour);
                    }

                    neighbour.IsVisited = true;
                }

            }
            return nearestToStart;
        }

        public QuadTree BuildQuadTree(IEnumerable<Artist> artists)
        {
            _quadTree = new QuadTree(0, 0, 0, 800, 800);
            foreach (var artist in artists)
            {
                _quadTree.Insert(artist);
            }
            return _quadTree;
        }

        public IEnumerable<Boundry> GetBoundries()
        {
            return _quadTree != null ? _quadTree.GetBoundries() : new List<Boundry>();
        }
    }
}
