using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TempGameClasses
{
    [Serializable]
    public class MapCell
    {

        private bool _HasBeenSeen;
        private Monster _ContainedMonster;
        private Item _ContainedItem;
        private bool _HasDoor;
        private bool _HasKey;
        
        //start getters and setters

        public bool HasBeenSeen
        {
            get { return _HasBeenSeen; }
            set { _HasBeenSeen = value; }
        }
        public bool HasKey
        {
            get { return _HasKey; }
            set { _HasKey = value; }
        }
        public bool HasDoor
        {
            get { return _HasDoor; }
            set { _HasDoor = value; }
        }
        public Monster ContainedMonster
        {
            get { return _ContainedMonster; }
            set { _ContainedMonster = value; }
        }

        public Item ContainedItem
        {
            get { return _ContainedItem; }
            set { _ContainedItem = value; }
        }

        //end getters n setters

        /// <summary>
        /// creates a mapcell
        /// </summary>
        public MapCell()
        {         
            _HasBeenSeen = false;
        }
        
    }
}
