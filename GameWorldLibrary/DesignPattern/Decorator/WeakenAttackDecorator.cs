using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWorldLibrary.Interface;
using GameWorldLibrary.Logger;

namespace GameWorldLibrary.DesignPattern.Decorator
{
    public class WeakenAttackDecorator : IAttackItem
    {
        private readonly IAttackItem _attackItem;
        private Creature _creature;
        private readonly MyLogger logger = MyLogger.GetInstance();


        public WeakenAttackDecorator(IAttackItem attackItem)
        {
            _attackItem = attackItem;
        }
        /// <summary>
        /// Tildeler en creature til objektet, som bruges ved beregning af angreb.
        /// Denne metode gør det muligt at tilknytte en creature efter oprettelse.
        /// </summary>
        /// <param name="creature">Den creature, der skal tilknyttes.</param>
        public void SetCreature(Creature creature)
        {
            _creature = creature;
        }

        /// <summary>
        /// Udfører et angreb baseret på et angrebsitem og creature's nuværende helbred.
        /// Hvis creature er null, logges en fejl og skaden sættes til 0.
        /// Hvis creature har under 25 HP, halveres skaden og der logges en advarsel.
        /// Ellers logges et normalt angreb og skaden returneres.
        /// </summary>
        /// <returns>Et heltal, der repræsenterer den beregnede skade.</returns>
        public int Attack()
        {
            int baseDamage = _attackItem.Attack();
            if (_creature == null)
            {
                logger.LogError("Creature reference is null in Attack(). Damage calculation may be incorrect.");
                return 0; 
            }


            if (_creature.HitPoint < 25)
            {
                logger.LogWarning($"Low HP: {_creature.HitPoint} on {_creature.GetType().Name}. Damage halved.");
                return (int)(baseDamage * 0.5); 
            }
            logger.LogInfo($"Normal attack by {_creature.GetType().Name}. Damage: {baseDamage}");
            return baseDamage;
        }
    }
}
