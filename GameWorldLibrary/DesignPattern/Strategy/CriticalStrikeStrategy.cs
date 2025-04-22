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
        private readonly IAttackStrategy _baseStrategy;
        private readonly Random rand = new Random();
        private readonly MyLogger logger = MyLogger.GetInstance();


        public CriticalStrikeStrategy(IAttackStrategy baseStrategy)
        {
            if (baseStrategy == null)
            {
                logger.LogCritical("CriticalStrikeStrategy constructor failed: baseStrategy is null.");
                throw new ArgumentNullException(nameof(baseStrategy));
            }

            _baseStrategy = baseStrategy;
        }


        /// <summary>
        /// Beregner skade ved først at bruge en basisstrategi og derefter afgøre om slaget er kritisk.
        /// Et kritisk slag fordobler skaden og logger resultatet.
        /// </summary>
        /// <param name="creature">Creaturen der udfører angrebet.</param>
        /// <returns>Den samlede beregnede skade, fordoblet hvis kritisk slag opstår.</returns>
        public int CalculateHit(Creature creature)
        {
            int baseHit = _baseStrategy.CalculateHit(creature);
            bool isCrit = rand.Next(0, 100) < 25;

            if (isCrit)
            {
                logger.LogInfo($"[CRITICAL] Base hit: {baseHit}, Final hit: {baseHit * 2}");
                return baseHit * 2;
            }

            logger.LogInfo($"[NORMAL] Base hit: {baseHit}");
            return baseHit;
        }
    }
}
