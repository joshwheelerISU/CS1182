using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TempGameClasses
{
    [Serializable]
    public class Door : Item
    {
    //fields
        private string _Code;
    //properties
        public string Code
        {
            get { return _Code; }
        }

    //methods and constructors

        public Door(string name, double value, string code) : base(name, value)
        {
            _Code = code;
        }

        public bool IsKey(DoorKey dk)
        {
            if(dk.Code == _Code)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
