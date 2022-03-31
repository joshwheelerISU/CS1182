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
    public abstract class Actor
    {
        //variables
        private string _Name;
        private string _Title;
        private double _MaxHP;
        protected double _CurrentHP;
        private int _PosX;
        private int _PosY;
        protected double _AtkSpeed;
        protected bool _IsAlive;

        public enum Direction
        {
            left,right,up,down
        }


    //getters and setters

        public string Name
        {
            get { return _Name; }
            
        }
        public string Title
        {
            get { return _Title; }
          
        }
        public double MaxHP
        {
            get { return _MaxHP; }
            set { _MaxHP = value; }
        }
        public double CurrentHP
        {
            get { return _CurrentHP; }
            set { _CurrentHP = value; }
        }
        public int Row
        {
            get { return _PosX; }
            set { _PosX = value; }
        }
        public int Col
        {
            get { return _PosY; }
            set { _PosY = value; }
        }
        virtual public double AtkSpeed
        {
         get { return _AtkSpeed; }  
        }
        public bool IsAlive
        {
            get {
                if(CurrentHP <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


    //constructor

        /// <summary>
        /// creates an actor
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="title">title</param>
        /// <param name="HP">starting hp</param>
        /// <param name="atkSpeed">attack speed</param>
        /// <param name="startX">starting position</param>
        /// <param name="startY">starting position</param>
        public Actor(string name, string title, double HP, double atkSpeed, int startX, int startY)
        {
            _Name = TitleCase(name);
            _Title = TitleCase(title);
            _CurrentHP = HP;
            _AtkSpeed = atkSpeed;
            _PosX = startX;
            _PosY = startY;

            _MaxHP = HP;
            _IsAlive = true;

           

        }

    //methods
        /// <summary>
        /// Capitalizes the first letter of the string passed in. Pulled from Encapsulation Mechanics, written by me.
        /// </summary>
        /// <param name="toCap">String to capitalize</param>
        private string CapFirstLetter(string toCap)
        {
            string temp = "";
            for (int i = 0; i < toCap.Length; i++)
            {


                if (i == 0)
                {
                    temp += (char.ToUpper(toCap[0]).ToString());
                }
                else
                {
                    temp += toCap[i];
                }

            }

            return temp;
        }

        /// <summary>
        /// Reads in a list of words that should not be capitalized in a title, and capitalizes a title correctly.
        /// words that should not be capitalized came from
        // https://www.smart-words.org/linking-words/list-of-prepositions.pdf
        // http://www.marshall.k12.il.us/data/webcontent/245/file/realname/files/List-of-Conjunctions.pdf
        // http://www.marshall.k12.il.us/data/webcontent/245/file/realname/files/List-of-Conjunctions.pdf
        // text file composed by me.
        /// </summary>
        /// <param name="titleToCap"></param>
        /// <returns></returns>
        private string TitleCase(string titleToCap)
        {
            string[] excludeThese = File.ReadAllLines("../../../Deliv7/data/WordsToExclude.txt");
            string[] tempArray = titleToCap.Split(' ');
            bool spaceCheck = true;
            string returnThis = "";

            for (int i = 0; i < tempArray.GetLength(0); i++)
            {
                bool shouldCap = true;

  
                for (int j = 0; j < excludeThese.Length;j++)
                {
                    if (tempArray[i] == excludeThese[j])
                    {
                        shouldCap = false; //word should not be capitalized
                    }

                    
                }

                if (shouldCap == true)
                {
                    if (spaceCheck == true) //word does not need a space in front of it
                    {
                        returnThis = CapFirstLetter(tempArray[i]);
                        spaceCheck = false;
                    }
                    else //word does need a space
                    {
                        returnThis = returnThis + " " + CapFirstLetter(tempArray[i]);
                    }
                }
                else
                {
                    if (spaceCheck == true)
                    {
                        returnThis = tempArray[i];
                        spaceCheck = false;
                    }
                    else
                    {
                        returnThis = returnThis + " " + tempArray[i];
                    }
                } 
            }





            return returnThis;
        }
        /// <summary>
        /// Returns the actor's name.
        /// </summary>
        /// <param name="includeTitle">true/false</param>
        /// <returns>requested version of the name</returns>
        public string GetName (bool includeTitle)
        {
            if (includeTitle)
            {
                return _Name + " " + _Title;
            }
            else
            {
                return _Name;
            }
        }

        /// <summary>
        /// moves the actor in the specified direction by 1 unit, whatever that ends up being.
        /// </summary>
        /// <param name="direction">direction enum</param>
        virtual public void Move (Direction direction)
        {
            if (direction == Direction.up)
            {
                //up, increase in y

                _PosX--;

            }else if(direction == Direction.down)
            {
                //down, decrease of y
                _PosX++;

            }else if(direction == Direction.left)
            {
                //left, decrease of x
                _PosY--;

            }else if(direction == Direction.right)
            {
                //right, increase of x

                _PosY++;
            }
        }
        /// <summary>
        /// method when actor takes damage
        /// </summary>
        /// <param name="amount">amount to damage character</param>
        public void TakeDamage(double amount)
        {
            double pHolder = _CurrentHP - amount;

            if (pHolder >= 0)
            {
                _CurrentHP = pHolder;
            } else
            {
                _CurrentHP = 0;

                _IsAlive = false;
            }
        }
        /// <summary>
        /// method when actor gets healed 
        /// </summary>
        /// <param name="amount">amount to heal</param>
        public void GetHealed (double amount)
        {
            double pHolder = _CurrentHP + amount;

            if (pHolder <= MaxHP)
            {
                _CurrentHP = pHolder;
            }else
            {
                _CurrentHP = MaxHP;
            }
        }
        
    }
}
