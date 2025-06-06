﻿using GameWorldLibrary.Interface;
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
        /// <summary>
        /// Returnerer den aktuelle skadeværdi (Hit) for objektet.
        /// </summary>
        /// <returns>Et heltal, som repræsenterer skaden.</returns>
        public int Attack()
        {
            return Hit; 
        }
        #endregion
    }
}
