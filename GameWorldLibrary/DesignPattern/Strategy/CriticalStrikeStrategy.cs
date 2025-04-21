using GameWorldLibrary.Interface;
using GameWorldLibrary.Logger;
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
        private readonly MyLogger logger = MyLogger.GetInstance();

        public int CalculateHit(Creature creature)
        {
            int baseHit = new BasicAttackStrategy().CalculateHit(creature);
            bool isCrit = rand.Next(0, 100) < 25;
            logger.LogInfo("");

            return isCrit ? baseHit * 2 : baseHit;
        }
    }
}
