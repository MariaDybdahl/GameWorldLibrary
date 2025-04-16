using GameWorldLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorldLibrary.Models
{
    public class AttackItem : IAttackItem
    {

        #region Properties

        public string Name { get; set; }
        public int Hit { get; set; }
        public int Range { get; set; }


        #endregion

        #region Constructor

        public AttackItem()
        {

        }

        public AttackItem(string name, int hit, int range)
        {
            Name = name;
            Hit = hit;
            Range = range;
        }

        #endregion
        #region Method
        public int Attack(int currentHitpoints)
        {
            return Hit; 
        }
        #endregion
    }
}
