using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWorldLibrary.Interface;
using GameWorldLibrary.Logger;

namespace GameWorldLibrary.DesignPattern.Decorator
{
    public class BoostAttackDecorator : IAttackItem
    {


        private readonly IAttackItem _attackItem;
        private readonly MyLogger _logger = MyLogger.GetInstance();
        public BoostAttackDecorator(IAttackItem attackItem)
        {
            _attackItem = attackItem;
        }
        /// <summary>
        /// Udfører et angreb og lægger 5 bonus-skade til resultatet.
        /// Logger den samlede skade inklusive bonus.
        /// </summary>
        /// <returns>Et heltal, der repræsenterer den samlede skade med bonus.</returns>
        public int Attack()
        {
            int attackDamaged = _attackItem.Attack() + 5;
            _logger.LogInfo($"Attack performed. Bonus damage applied (+5). Total damage: {attackDamaged}.");
            return attackDamaged;

        }
    }
}
