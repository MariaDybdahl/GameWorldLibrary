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
        /// <summary>
        /// Beregner skade baseret på en grundlæggende angrebsstrategi og afgør, om angrebet er et kritisk træf.
        /// Et kritisk træf fordobler skaden.
        /// Logger resultatet som enten et normalt eller kritisk angreb.
        /// </summary>
        /// <param name="creature">Den creature, der udfører angrebet.</param>
        /// <returns>Et heltal, som repræsenterer den samlede skade.</returns>

        public int CalculateHit(Creature creature)
        {
            int baseHit = new BasicAttackStrategy().CalculateHit(creature);
            bool isCrit = rand.Next(0, 100) < 25;
            if (isCrit)
            {
                logger.LogInfo($"Critical hit! Base hit: {baseHit}, Final hit: {baseHit * 2}");
            }
            else
            {
                logger.LogInfo($"Normal hit. Base hit: {baseHit}");
            }

            return isCrit ? baseHit * 2 : baseHit;
        }
    }
}
