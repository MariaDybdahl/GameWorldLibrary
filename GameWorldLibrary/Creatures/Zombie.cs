using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWorldLibrary.DesignPattern;

namespace GameWorldLibrary.Creatures
{
    public class Zombie : Creature
    {
        public Zombie(string name, int hitPoint) : base(name, hitPoint)
        {
        }
    }
}
