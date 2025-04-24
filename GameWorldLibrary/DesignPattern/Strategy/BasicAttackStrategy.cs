using GameWorldLibrary.Interface;
using GameWorldLibrary.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorldLibrary.DesignPattern.Strategy
{
    public class BasicAttackStrategy : IAttackStrategy
    {
        private readonly MyLogger _logger = MyLogger.GetInstance();
        /// <summary>
        /// Beregner den samlede skade, som en creature laver ved at bruge sine angrebsgenstande i listen.
        /// Metoden logger hver enkelt skade samt den samlede skade.
        /// Hvis creaturen ikke har nogen angreb, logges en advarsel, og metoden returnerer 0.
        /// </summary>
        /// <param name="creature">Creaturen, der udfører angrebet.</param>
        /// <returns>Et heltal, som repræsenterer den samlede skade fra alle angreb.</returns>
        public int CalculateHit(Creature creature)
        {
            int totalHit = 0;

            if (creature.AttackList != null && creature.AttackList.Any())
            {
                _logger.LogInfo($"{creature.GetType().Name} performing {creature.AttackList.Count} attacks...");

                foreach (var item in creature.AttackList)
                {
                    int damage = item.Attack();
                    _logger.LogInfo($"Attack from {ReflectionHelper.GetDisplayName(item)} dealt {damage} damage.");
                    totalHit += damage;
                }


                _logger.LogInfo($"Total damage dealt by {creature.GetType().Name}: {totalHit}");
            }
            else
            {
                _logger.LogWarning($"No attacks found for {creature.GetType().Name}. AttackList is null or empty.");
            }

            return totalHit;

        }
    }
}
