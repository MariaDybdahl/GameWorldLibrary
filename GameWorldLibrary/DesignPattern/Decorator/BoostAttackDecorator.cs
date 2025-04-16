using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWorldLibrary.Interface;

namespace GameWorldLibrary.DesignPattern.Decorator
{
    public class BoostAttackDecorator : IAttackItem
    {


        private readonly IAttackItem _attackItem;

        public BoostAttackDecorator(IAttackItem attackItem)
        {
            _attackItem = attackItem;
        }

        public int Attack(int currentHitpoints)
        {
            return _attackItem.Attack(currentHitpoints) + 5;
        }
    }
}
