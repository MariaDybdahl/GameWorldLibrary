using GameWorldLibrary.DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorldLibrary.Interface
{
    public interface IAttackStrategy
    {
        int CalculateHit(Creature attacker);
    }
}
