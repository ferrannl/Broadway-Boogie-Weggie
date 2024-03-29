﻿using Broadway_Boogie_Weggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Services.Interfaces
{
    public interface IAlgorithmService
    {
        /// <summary>
        /// Lijst van Squares
        /// </summary>
        /// <returns>Entire path in the form of an ordened list, starting with begin point, ending with end point</returns>

        void ResetAlgorithm(IEnumerable<Square> squares);
        IEnumerable<Square> GetShortestPath(Tile start, Tile end);
        IEnumerable<Square> GetCheapestPath(Tile start, Tile end);
        QuadTree BuildQuadTree(IEnumerable<Artist> artists);
        IEnumerable<Boundry> GetBoundries();

    }
}
