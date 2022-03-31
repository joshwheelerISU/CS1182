using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TempGameClasses
{
    [Serializable]
    public class Hero : Actor , ICombat
    {
    //fields
        private Weapon _HeldWeapon;
        private bool _IsRunningAway;
        private DoorKey _HeldKey;
        private List<Item> _Inventory = new List<Item>(8);

        //properties
        public List<Item> Inventory
        {
            get { return _Inventory; }
        }
        public Weapon HeldWeapon
        {
            get { return _HeldWeapon; }
        }
        public bool IsRunningAway
        {
            get { return _IsRunningAway; }
            set { _IsRunningAway = value; }
        }
        public DoorKey HeldKey
        {
            get { return _HeldKey; }
            set { _HeldKey = value; }
        }

        public double AttackDamage
        {

            get
            {

                if (_HeldWeapon == null)
                {
                    return 1;
                }
                else
                {
                    return _HeldWeapon.AffectValue;
                }

            }

        }



    //constructors and methods


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="HP"></param>
        /// <param name="atkSpeed"></param>
        /// <param name="sX"></param>
        /// <param name="sY"></param>
        public Hero(string name, string title, double HP, double atkSpeed, int sX, int sY  ):base(name, title, HP, atkSpeed, sX, sY)
        {

            

        }
        /// <summary>
        /// overrides the atkSpeed from the base class, returns half if weapon equipped
        /// </summary>
        public override double AtkSpeed
        {
           get {
                if( _HeldWeapon == null)
                {
                    return _AtkSpeed;
                }
                else
                {
                    return _AtkSpeed - _HeldWeapon.AtkModifier;
                }
            }
        }

        /// <summary>
        /// Adds Item to Inventory
        /// </summary>
        /// <param name="I">Item to Add</param>
        public void AddToInventory(Item I)
        {
            _Inventory.Add(I);
        }
        /// <summary>
        /// Overrides the base class move method, and turns right around and calls it again.
        /// </summary>
        /// <param name="direc">direction to move</param>
        public override void Move(Direction direc)
        {
            base.Move(direc);
        }
        /// <summary>
        /// ICombat interface. Attacks an actor.
        /// </summary>
        /// <param name="act">actor to attack</param>
        /// <returns>whether or not the attacked actor is alive or dead</returns>
        public bool Attack(Actor act)
        {
            act.TakeDamage(AttackDamage);
            return act.IsAlive;
        }
        /// <summary>
        /// applies an item to the hero
        /// </summary>
        /// <param name="item">item to apply</param>
        /// <returns>null if used, item if not able to be used</returns>
        public Item ApplyItem(Item item)
        {
            if (item.GetType() == typeof(Potion))
            {
                GetHealed(item.AffectValue);
                return null;
            }
            else if (item.GetType() == typeof(Weapon))
            {


                if(_HeldWeapon == null)
                {
                    _HeldWeapon = (Weapon)item;
                    return null;
                }
                else
                {
                    Item temp = _HeldWeapon;
                    _HeldWeapon = (Weapon)item;
                    return temp;
                }
                
            } else if(item.GetType() == typeof(DoorKey))
            {
                if (_HeldKey == null)
                {
                    _HeldKey = (DoorKey)item;
                    return null;
                }
                else
                {
                    Item temp = _HeldKey;
                    _HeldKey = (DoorKey)item;
                    return temp;
                }
            }
            else
            {
                return item;
            }
        }
        /// <summary>
        /// Addition of Hero and Monster
        /// </summary>
        /// <param name="H">Hero</param>
        /// <param name="M">Monster</param>
        /// <returns>whether hero is alive or dead</returns>
        public static bool operator +(Hero H, Monster M)
        {

            if (H.IsRunningAway == false)
            {

                if (H.AtkSpeed > M.AtkSpeed)
                {

                    if (H.Attack(M))
                    {
                        M.Attack(H);
                    }
                }
                else if (M.AtkSpeed > H.AtkSpeed)
                {
                    if (M.Attack(H))
                    {
                        H.Attack(M);
                    }
                }
                else if (M.AtkSpeed == H.AtkSpeed)
                {
                    M.Attack(H);
                    H.Attack(M);
                }
            }else if(H.IsRunningAway == true)
            {
                if (H.AtkSpeed > M.AtkSpeed)
                {
                    //nothing
                }else
                {
                    M.Attack(H);
                }
            }

            return H.IsAlive;
        }

    }
}
