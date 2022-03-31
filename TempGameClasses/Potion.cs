using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;

namespace TempGameClasses
{
    [Serializable]
    public class Potion : Item , IRepeatable<Potion>
    {
    //fields
        private Colors _Color;
    //properties
        public Colors Color
        {
            get { return _Color; }
            set { _Color = value; }
        }
        public enum Colors
        {
            Blue, Red, Green, Orange
        }
        //constructors and methods
        /// <summary>
        /// Creates a potion
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="value">effect value</param>
        /// <param name="color">color</param>
        public Potion(string name, double value, Colors color ) : base(name, value)
        {
            Color = color;
        }
        /// <summary>
        /// IRepeatablemakes a deep copy of the current potion
        /// </summary>
        /// <returns>potion object</returns>
        public Potion CreateCopy()
        {
            Potion myTemp = new Potion(this.Name, this.AffectValue, this.Color);

            return myTemp;
        }
    }
}
