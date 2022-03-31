using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TempGameClasses
{
    [Serializable]
    public class Weapon : Item, IRepeatable<Weapon>
    {
        //fields
        private double _AtkModifier;
        //properties
        public double AtkModifier
        {
            get { return _AtkModifier; }
        }
        /// <summary>
        /// constructor for weapon
        /// </summary>
        /// <param name="ATK">ATK modifier</param>
        /// <param name="name">Name</param>
        /// <param name="value">Effect Value</param>
        public Weapon(double ATK, string name, double value):base(name, value)
        {
            _AtkModifier = ATK;
        }

        /// <summary>
        /// IRepeatable makes a deep copy of the current weapon
        /// </summary>
        /// <returns>weapon object</returns>
        public Weapon CreateCopy()
        {
            Weapon myTemp = new Weapon(this.AtkModifier, this.Name, this.AffectValue);

            return myTemp;
        }

    }
}
