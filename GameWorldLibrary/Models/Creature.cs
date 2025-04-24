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
using GameWorldLibrary.Logger;
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
        public int Id { get; private set; }
        private static int nextId = 1;
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
                if (string.IsNullOrWhiteSpace(value))
                {
                    logger.LogError("Name was null, empty, or whitespace – defaulting to 'Unknown'");
                    value = "Unknown";

                }
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
            Id = nextId++;
         
        }

        public Creature()
        {

            logger = MyLogger.GetInstance();

        }

        #endregion

        #region Methods

        #region Public Method
        //Template Method
        /// <summary>
        /// Udfører et angreb mod en anden creature ved at beregne skade og sende den videre.
        /// Logger information om angrebet, herunder navn på angriber, mål og skade.
        /// </summary>
        /// <param name="target">Den creature, der bliver angrebet.</param>
        public void Fight(Creature creature)
        {
            int hit = this.Hit();
            creature.ReceiveHit(hit);
            logger.LogInfo($"{Name} attacked {creature.Name} for {hit} damage.");
        }

        /// <summary>
        /// Beregner og returnerer skaden ved at bruge den tilknyttede angrebsstrategi.
        /// Kan overskrives i nedarvede klasser for at ændre angrebsadfærd.
        /// </summary>
        /// <returns>Et heltal, der repræsenterer den beregnede skade.</returns>
        public virtual int Hit()
        {
            return AttackStrategy.CalculateHit(this);
        }

        /// <summary>
        /// Modtager skade og reducerer HitPoint baseret på creature's samlede forsvar.
        /// Logger mængden af skade og det resterende helbred.
        /// Hvis creature dør, logges dette også.
        /// </summary>
        /// <param name="hit">Den skade der modtages før forsvar anvendes.</param>
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
       
        /// <summary>
        /// Undersøger om et world object kan lootes, og tilføjer dets angrebs- og forsvarsgenstande til creature's lister.
        /// Logger hvert lootet item og opdaterer status på listerne.
        /// </summary>
        /// <param name="worldObject">Det objekt, der forsøges lootet.</param>
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

        /// <summary>
        /// Tjekker om creature er død ved at se om HitPoint er 0 eller mindre.
        /// </summary>
        /// <returns>True hvis HitPoint er 0 eller lavere, ellers false.</returns>
        public virtual bool IsDead()
        {
            return HitPoint <= 0;
        }
        #endregion

        #region Protected Method
        protected void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Private Method
        private void LootAttack(WorldObject worldObject)
        {
            foreach (var item in worldObject.AttackList)
            {
                AttackList.Add(item);
                logger.LogInfo($"You looted {item.GetType().Name}");
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

        private int CalculateTotalDefense()
        {
            if (DefenceList == null || !DefenceList.Any())
            {
                logger.LogInfo("DefenseList is empty. No damage reduction applied.");
                return 0;
            }
            int totalDefense = DefenceList.Sum(defense => defense.ReduceHitPoint);
            logger.LogInfo($"Your defense is {totalDefense}");
            return totalDefense;

        }
        #endregion

        #endregion

    }
}
