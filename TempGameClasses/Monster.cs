using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TempGameClasses
{
    [Serializable]
    public class Monster : Actor , IRepeatable<Monster>, ICombat
    {
    //fields
        private double _ATKValue;
    //properties
        public double ATKValue
        {
            get { return _ATKValue; }
            set { _ATKValue = value; }
        }
    //constructors and methods
        public Monster(string name, string title, double HP, double atkSpeed, int sX, int sY, double ATKVal) 
        : base(name, title, HP, atkSpeed, sX, sY)
        {

            _ATKValue = ATKVal;

        }
        /// <summary>
        /// IRepeatable deepclone
        /// </summary>
        /// <returns>cloned monster</returns>
        public Monster CreateCopy()
        {
            return new Monster(Name, Title, CurrentHP, _AtkSpeed, Row, Col, _ATKValue);
        }

        /// <summary>
        /// ICombat attack
        /// </summary>
        /// <param name="act">actor to attack</param>
        /// <returns>true if attacked actor is alive, false if not</returns>
        public bool Attack(Actor act)
        {
            act.TakeDamage(ATKValue);
            return act.IsAlive;
        }
    }
}
