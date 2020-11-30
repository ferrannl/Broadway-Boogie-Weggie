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

        public IEnumerable<Square> GetShortestPath(Tile start, Tile end, IEnumerable<Tile> tiles)
        {
            if (start == null || end == null)
            {
                throw new ArgumentNullException();
            }
            var visited = new List<Tile>();
            var queue = new Queue<Tile>();
            queue.Enqueue(start);
            start.IsVisited = true;
            while (queue.Count > 0)
            {
                var tile = queue.Dequeue();

                visited.Add(tile);

                foreach (var neighbor in tile.Neighbours)
                {
                    neighbor.IsVisited = true;
                    if (neighbor == end)
                    {
                        queue.Clear();
                        break;
                    }
                    //Check dat een neighbour niet al is bezocht.
                    if (!visited.Contains(neighbor))
                        queue.Enqueue(neighbor);
                }
            }
            return visited;
        }
    }
}
