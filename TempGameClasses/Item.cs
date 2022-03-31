using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TempGameClasses
{
    [Serializable]
    public class Item
    {

        private string _Name;
        private double _AffectValue;


    //getters and setter
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public double AffectValue
        {
            get { return _AffectValue; }
            set { _AffectValue = value; }
        }


    //constructors
        /// <summary>
        /// creates an item
        /// </summary>
        /// <param name="name">item name</param>
        /// <param name="value">Affect value</param>
        public Item(string name, double value)
        {
            Name = name;
            AffectValue = value;
        }


    }
}
