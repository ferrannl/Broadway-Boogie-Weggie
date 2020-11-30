using Broadway_Boogie_Weggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Services.Interfaces
{
    public interface ICollisionService
    {
        void CheckCollision(ICollection<Square> squares, bool CollisionWithPath);
    }
}
