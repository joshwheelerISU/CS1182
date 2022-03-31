using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TempGameClasses
{
    [Serializable]
    public class DoorKey : Item
    {
    //fields
        private string _Code;
    //properties
        public string Code
        {
            get { return _Code; }
        }
    //methods and constructors
        public DoorKey(string name, double value, string code) : base(name, value)
        {
            _Code = code;
        }


    }
}
