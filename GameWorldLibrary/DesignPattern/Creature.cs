using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GameWorldLibrary.DesignPattern.Strategy;
using GameWorldLibrary.Interface;
using GameWorldLibrary.Models;

namespace GameWorldLibrary.DesignPattern
{
    public abstract class Creature : INotifyPropertyChanged
    {
        private string _name;
        private int _hitPoint;

        #region Properties
        public List<IAttackItem> AttackList { get; set; } = new List<IAttackItem>();
        public List<DefenceItem> DefenceList { get; set; } = new List<DefenceItem> { };
        public event PropertyChangedEventHandler? PropertyChanged;
        public IAttackStrategy AttackStrategy { get; set; } = new BasicAttackStrategy();

        public int HitPoint
        {
            get => _hitPoint;
            set
            {
                if (value == _hitPoint) return;
                _hitPoint = value;
                Notify("HitPoint");
            }
        }
        private readonly MyLogger logger = null;
        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                Notify("Name");
            }
        }
        #endregion

        #region Constructor
        public Creature(string name, int hitPoint)
        {
            Name = name;
            HitPoint = hitPoint;
            logger = MyLogger.GetInstance();

            logger.AddListener(new TextWriterTraceListener("log.txt"));
        }

        public Creature()
        {

            logger = MyLogger.GetInstance();

            logger.AddListener(new TextWriterTraceListener("log.txt"));
        }

        #endregion

        #region Methods
        protected void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //Template Method
        public void Fight(Creature creature)
        {
            int hit = Hit(creature.HitPoint);
            creature.ReceiveHit(hit);
        }

        public virtual int Hit(int currentHitpoints)
        {
            return AttackStrategy.CalculateHit(this, currentHitpoints);
        }
        public virtual void ReceiveHit(int hit)
        {
            int totalDefense = CalculateTotalDefense();

            int damage = Math.Max(hit - totalDefense, 0);

            HitPoint -= damage;

            logger.LogInfo($"{Name} receives {damage} damage after defense. Remaining HP: {HitPoint}");

            if (IsDead())
            {
                logger.LogInfo($"{Name} has died.");

            }
        }
        private int CalculateTotalDefense()
        {
            if (DefenceList == null || !DefenceList.Any())
                return 0;

            return DefenceList.Sum(defense => defense.ReduceHitPoint);

        }

        /// <summary>
        /// den tjekker om man kan loot worldObject hvis man kan tjekker den om det er Attack og Defense 
        /// hvis der er en eller flere end en ting så vil alle ting blive tilføjet til deres list
        /// </summary>
        /// <param name="worldObject">Det objekt vi gerne vil loote</param>
        public virtual void Loot(WorldObject worldObject)
        {

            if (!worldObject.Lootable)
            {
                logger.LogInfo("You can not loot the object");
                return;
            }

            if (worldObject.AttackList?.Any() == true)
            {
                LootAttack(worldObject);  
            }
            if (worldObject.DefenseList?.Any() == true)
            {
                LootDefense(worldObject);
            }
        }
        private void LootAttack(WorldObject worldObject)
        {
            foreach (var item in worldObject.AttackList)
            {
                AttackList.Add(item);
                logger.LogInfo($"You looted {item.Name}");
            }
            logger.LogInfo($"Your attack list has {AttackList.Count} items");
        }
        private void LootDefense(WorldObject worldObject)
        {
            foreach (var item in worldObject.DefenseList)
            {
                DefenceList.Add(item);
                logger.LogInfo($"You looted {item.Name}");
            }
            logger.LogInfo($"Your Defense list has {DefenceList.Count} items");
        }
        /// <summary>
        /// Tjekker om hitpoint er 0 eller under 0
        /// </summary>
        /// <returns>sandt hvis den er og falsk hvis den ikke er</returns>
        public virtual bool IsDead()
        {
            return HitPoint <= 0;
        }

        #endregion



        #region Delete

        //public virtual int Hit(int currentHitpoints)
        //{
        //    int totalHit = 0;

        //    if (AttackList != null && AttackList.Count > 0)
        //    {
        //        foreach (var attackItem in AttackList)
        //        {
        //            totalHit += attackItem.Attack(currentHitpoints);
        //        }
        //    }

        //    return totalHit;
        //}
        #endregion
    }
}
