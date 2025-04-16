using GameWorldLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorldLibrary.DesignPattern.Strategy
{
    public class BasicAttackStrategy : IAttackStrategy
    {
        public int CalculateHit(Creature attacker, int targetCurrentHitPoints)
        {
            int totalHit = 0;
            if (attacker.AttackList != null)
            {
                foreach (var item in attacker.AttackList)
                {
                    totalHit += item.Attack(targetCurrentHitPoints);
                }
            }
            return totalHit;
        }
    }
}
