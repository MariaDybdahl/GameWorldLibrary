using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWorldLibrary.Interface;

namespace GameWorldLibrary.DesignPattern.Decorator
{
    public class WeakenAttackDecorator : IAttackItem
    {
        private readonly IAttackItem _attackItem;

        public WeakenAttackDecorator(IAttackItem attackItem)
        {
            _attackItem = attackItem;
        }

        public int Attack(int currentHitpoints)
        {
            int baseDamage = _attackItem.Attack(currentHitpoints);

            if (currentHitpoints < 25)
            {
                return (int)(baseDamage * 0.5); 
            }

            return baseDamage;
        }
    }
}
