using GameWorldLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorldLibrary.DesignPattern.Strategy
{
    public class CriticalStrikeStrategy : IAttackStrategy
    {
        private Random rand = new Random();

        public int CalculateHit(Creature creature)
        {
            int baseHit = new BasicAttackStrategy().CalculateHit(creature);
            bool isCrit = rand.Next(0, 100) < 25;

            return isCrit ? baseHit * 2 : baseHit;
        }
    }
}
