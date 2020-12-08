﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Services.Interfaces
{
    public interface IQuadNode
    {
        double X { get; set; }
        double Y { get; set; }
        double Width { get; }
        double Height { get; }
    }
}
