using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWorldLibrary.Interface;
using GameWorldLibrary.Logger;
namespace GameWorldLibrary.DesignPattern.Composite
{
    public class AttackItemComposite : IAttackItem
    {
        private List<IAttackItem> items;
        private readonly MyLogger _logger = MyLogger.GetInstance();

        public AttackItemComposite()
        {
            items = new List<IAttackItem>();
        }
        /// <summary>
        /// Tilføjer et angrebsitem til listen over items.
        /// Logger navnet på det item, der er blevet tilføjet.
        /// </summary>
        /// <param name="item">Det angrebsitem, der ønskes tilføjet.</param>
        public void Add(IAttackItem item)
        {
            items.Add(item);
            _logger.LogInfo($"Attack item added: {item.GetType().Name}");
        }
        /// <summary>
        /// Fjerner et angrebsitem fra listen over items.
        /// Logger om fjernelsen var succesfuld eller om item ikke blev fundet.
        /// </summary>
        /// <param name="item">Det angrebsitem, der ønskes fjernet.</param>
        public void Remove(IAttackItem item)
        {
            if (items.Remove(item))
            {
                _logger.LogInfo($"Attack item {item.GetType().Name} removed successfully.");
            }
            else
            {
                _logger.LogWarning($"Attempted to remove attack item {item.GetType().Name}, but it was not found.");
            }
        }

        /// <summary>
        /// Beregner den samlede skade ved at summere skaden fra alle angrebsitems i listen.
        /// Returnerer 0 hvis listen er tom eller null.
        /// Logger om angrebslisten er tom, og hvis ikke, hvor mange items der blev brugt og den samlede skade.
        /// </summary>
        /// <returns>Et heltal der repræsenterer den samlede skade fra angrebslisten.</returns>

        public int Attack()
        {
            if (items == null || items.Count == 0)
            {
                _logger.LogWarning($"No attack items found for {GetType().Name}. Returning 0 damage.");
                return 0;
            }

            int totalDamage = items.Sum(i => i.Attack());
            _logger.LogInfo($"Total damage from {items.Count} items: {totalDamage}");

            return totalDamage;
        }
        public void PrintItems()
        {
            Console.WriteLine("AttackItemComposite contains:");
            foreach (var item in items)
            {
                string name = item is IAttackItem ai ? ai.ToString() : item.GetType().Name;
                int damage = item.Attack();
                Console.WriteLine($"- {name}: {damage} damage");
            }
        }

    }
}
