using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWorldLibrary.Interface;
namespace GameWorldLibrary.DesignPattern.Composite
{
    public class AttackItemComposite : IAttackItem
    {
        private List<IAttackItem> items;

        public AttackItemComposite()
        {
            items = new List<IAttackItem>();
        }

        public void Add(IAttackItem item)
        {
            items.Add(item);
        }

        public void Remove(IAttackItem item)
        {
            items.Remove(item);
        }
        /// <summary>
        /// Ser om den er noget i listen, hvis ikke er så sætter den attack til 0, ellers tager den sum af attack fra list, og lægger sammen
        /// </summary>
        /// <param name="currentHitpoints"> </param>
        /// <returns>et number af attack</returns>
        public int Attack(int currentHitpoints)
        {
            return items.Count == 0 ? 0 : items.Sum(i => i.Attack(currentHitpoints));
        }
    }
}
