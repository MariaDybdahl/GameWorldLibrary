using GameWorldLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameWorldLibrary.Models
{
    public class WorldObject
    {

        #region Properties

        public string Name { get; set; }
        public bool Lootable { get; set; }
        public bool Removeable { get; set; }
        public List<AttackItem> AttackList { get; set; } = new List<AttackItem>();
        public List<DefenceItem> DefenseList { get; set; } = new List<DefenceItem>();
        #endregion

        #region Constructor

        public WorldObject(string name, bool lootable, bool removeable)
        {
            Name = name;
            Lootable = lootable;
            Removeable = removeable;
        }

        public WorldObject()
        {

        }

        #endregion

    }
}
