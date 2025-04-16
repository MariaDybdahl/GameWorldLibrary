using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorldLibrary.Models
{
    public class DefenceItem
    {

        #region Properties

        public string Name { get; set; }
        public int ReduceHitPoint { get; set; }


        #endregion


        #region Constructor

        public DefenceItem(string name, int reduceHitPoint)
        {
            Name = name;
            ReduceHitPoint = reduceHitPoint;
        }

        public DefenceItem()
        {
        }

        #endregion

    }
}
