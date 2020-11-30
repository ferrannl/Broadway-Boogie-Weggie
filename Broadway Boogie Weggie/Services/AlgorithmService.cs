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
        public IEnumerable<Square> GetCheapestPath()
        {
            throw new NotImplementedException();
        }

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
    }
}
