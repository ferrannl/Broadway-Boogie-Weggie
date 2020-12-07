﻿using Broadway_Boogie_Weggie.Models;
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
            throw new NotImplementedException();
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
                var previous = priorityQueue.ElementAt(0);
                foreach (var neighbour in priorityQueue.ElementAt(0).Neighbours)
                {
                    int weight = neighbour.Weight + distanceTo[previous];
                    distanceTo.Add(neighbour, weight);
                }

            }


            return nearestToStart;
        }
    }
}
