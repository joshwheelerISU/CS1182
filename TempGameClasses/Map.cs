using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TempGameClasses
{
    [Serializable]
    public class Map
    {
    

    //fields
        private int _Height;
        private int _Width;

        private MapCell[,] _GameBoard;
        private List<Item> _Items = new List<Item>();
        private List<Monster> _PossibleMonsters = new List<Monster>();
        private Hero _PlayerCharacter;
        private int _MonsterHPModifier = 0;
        private int _MonsterAttackModifier = 0;

        //properties

        public List<Monster> PossibleMonsters
        {
            get { return _PossibleMonsters; }
            set { _PossibleMonsters = value; }
        }

        public MapCell[,] GameBoard
        {
            get { return _GameBoard; }
        }
        public Hero PlayerCharacter
        {
            get { return _PlayerCharacter; }
            set { _PlayerCharacter = value; }
        }
        public int MonsterHPModifier
        {
            get { return _MonsterHPModifier; }
            set { _MonsterHPModifier = value; }
        }
        public int MonsterAttackModifier
        {
            get { return _MonsterAttackModifier; }
            set { _MonsterAttackModifier = value; }
        }
        /// <summary>
        /// gives total number of monsters present on the gameboard
        /// </summary>
        public int MonsterCount
        {
            get
            {
                int mCount = 0;
                foreach (MapCell m in _GameBoard)
                {
                    if(m.ContainedMonster != null)
                    {
                        mCount++;
                    }
                }
                return mCount;
            }
        }
        /// <summary>
        /// gives total number of items present on gameboard
        /// </summary>
        public int ItemCount
        {
            get
            {
                int iCount = 0;
                foreach (MapCell m in _GameBoard)
                {
                    if (m.ContainedItem != null)
                    {
                        iCount++;
                    }
                }
                return iCount;
            }
        }
        /// <summary>
        /// gives the percentage of the board discovered
        /// </summary>
        public double PercentDiscovered
        {
            get
            {
                double dCount = 0;//discovered cells
                double tCount = 0;//total cells
                foreach(MapCell m in _GameBoard)
                {
                    if(m.HasBeenSeen == true)
                    {
                        dCount++;
                    }
                    tCount++;
                }

                return (dCount / tCount);
            }
        }
        /// <summary>
        /// returns the max hp of the highest hp monster in _possiblemonsters
        /// </summary>
        public int HighestHpMonster
        {
            get
            {
                var hpArrayL =
                    from mo in _PossibleMonsters
                    select mo.CurrentHP;
                return (int)hpArrayL.Max();
                
            }
        }
        /// <summary>
        /// returns the lowest affect value of weapons
        /// 
        /// .OfType help found at https://stackoverflow.com/questions/3842714/linq-selection-by-type-of-an-object
        /// </summary>
        public int LeastWeapon
        { 
            get
            {
                var weaponArray = _Items.OfType<Weapon>();
                var weaponAtkArray =
                    from we in weaponArray
                    select we.AffectValue;
                return (int)weaponAtkArray.Min();
            }
        }
        /// <summary>
        /// returns the average heal value of potions in the items array
        /// 
        /// .OfType help found at https://stackoverflow.com/questions/3842714/linq-selection-by-type-of-an-object
        /// </summary>
        public int AverageHealValue
        {
            get
            {
                var potArray = _Items.OfType<Potion>();
                var potHealArray =
                    from pot in potArray
                    select pot.AffectValue;
                return (int)potHealArray.Average();
            }
        }
     //constructor
        public Map(int row, int col, Hero PC)
        {
            _Height = row;
            _Width = col;
            FillMonsters();
            FillPotions();
            FillWeapons();
            FillMap();
            PopulateMap();
            PlayerCharacter = PC;
        }
    //methods
    /// <summary>
    /// populates the map randomly
    /// </summary>
        public void PopulateMap()
        {
            Random rng = new Random();
            //door and key
            int fir = rng.Next(0, GameBoard.GetLength(0));
            int sec = rng.Next(0, GameBoard.GetLength(1));
            int kefir = rng.Next(0, GameBoard.GetLength(0));
            int kesec = rng.Next(0, GameBoard.GetLength(1));
            

            GameBoard[fir, sec].ContainedItem = new Door("Door", 2, "345");

            //key / door place logic
            if(GameBoard[kefir, kesec].ContainedItem == null)
            {
            GameBoard[kefir, kesec].ContainedItem = new DoorKey("Key", 2, "345");
            }else
            {
                GameBoard[kefir++, kesec].ContainedItem = new DoorKey("Key", 2, "345");
            }


            //monsters and items
            for(int i = 0; i < GameBoard.GetLength(0); i++)
            {

                for(int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    if(GameBoard[i,j].ContainedItem == null && GameBoard[i,j].ContainedMonster ==null)
                    {
                    int reference = rng.Next(0,101);
                        if(reference > 90)
                        {
                        //monster               
                          GameBoard[i,j].ContainedMonster = (Monster)_PossibleMonsters[rng.Next(0,_PossibleMonsters.Count)].CreateCopy();
                       }

                        if(reference > 75 && reference < 90)    
                        {
                            //item
                           GameBoard[i,j].ContainedItem = _Items[rng.Next(0,_Items.Count - 1)];
                        }
                        else
                        {
                           //blank tile       
                        }  
                    }

                }

            }                                                          

        }
        /// <summary>
        /// fills monsters
        /// </summary>
        private void FillMonsters()
        {
            _PossibleMonsters = new List<Monster>();
            _PossibleMonsters.Add(new Monster("Skeleton", "The Spooky", 9 + MonsterHPModifier, 6 + MonsterAttackModifier, 0, 0, 3 + MonsterAttackModifier));
            _PossibleMonsters.Add(new Monster("Orc", "of Orthanc", 8 + MonsterHPModifier , 2 + MonsterAttackModifier, 0, 0, 2 + MonsterAttackModifier));
            _PossibleMonsters.Add(new Monster("Raider", "of death", 7 + MonsterHPModifier, 2 + MonsterAttackModifier, 0, 0, 1 + MonsterAttackModifier));
            _PossibleMonsters.Add(new Monster("Mimic", "The Sneaky", 9 + MonsterHPModifier, 2 + MonsterAttackModifier, 0, 0, 2 + MonsterAttackModifier));
            _PossibleMonsters.Add(new Monster("Wraith", "the invisible", 56 + MonsterHPModifier,10 + MonsterAttackModifier, 0, 0, 3 + MonsterAttackModifier));
        }
        /// <summary>
        /// fills potions
        /// </summary>
        private void FillPotions()
        {
            _Items.Add(new Potion("Potion of Healing", 4, Potion.Colors.Blue));
            _Items.Add(new Potion("Potion of Plentiful Healing", 5, Potion.Colors.Red));
            _Items.Add(new Potion("Potion of Minor Regeneration", 2, Potion.Colors.Green));
            _Items.Add(new Potion("Potion of Incredible Fortitude", 6, Potion.Colors.Orange));



        }
        /// <summary>
        /// fills weapons
        /// </summary>
        private void FillWeapons()
        {
            _Items.Add(new Weapon(2, "Longsword", 3));
            _Items.Add(new Weapon(1, "Shortsword", 1));
            _Items.Add(new Weapon(3, "Mace", 8));
            _Items.Add(new Weapon(2, "Greatsword", 4));
            _Items.Add(new Weapon(1, "Shortbow", 3));
            _Items.Add(new Weapon(3, "Spear", 4));



        }
        /// <summary>
        /// fills the map
        /// </summary>
        private void FillMap()
        {
            _GameBoard = new MapCell[_Height, _Width];

            for(int i = 0; i < _GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < _GameBoard.GetLength(1); j++)
                {
                    _GameBoard[i, j] = new MapCell();
                }
            }
        }


        /// <summary>
        /// returns hero's location
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public MapCell FindHero(Hero h)
        {
            return _GameBoard[(int)h.Col, (int)h.Row];
        }
        /// <summary>
        /// Moves the hero around the gameboard, if permitted
        /// </summary>
        /// <param name="dir"></param>
        public bool MoveHero(Actor.Direction dir)
        {
            //ease of access current position of hero.
            int curX = PlayerCharacter.Row;
            int curY = PlayerCharacter.Col;

            if(dir == Actor.Direction.left)
            {
                if(curY == 0)
                {
                    //cant move
                }
                else
                {
                    //can move
                    PlayerCharacter.Move(Actor.Direction.left);
                }
            }else if(dir == Actor.Direction.right)
            {
                if (curY == GameBoard.GetLength(0) - 1)
                {
                    //cant move
                }
                else
                {
                    //can move
                    PlayerCharacter.Move(Actor.Direction.right);

                }
            }
            else if(dir == Actor.Direction.up)
            {
                if (curX == 0)
                {
                    //cant move
                }
                else
                {
                    //can move
                    PlayerCharacter.Move(Actor.Direction.up);

                }
            }
            else if(dir == Actor.Direction.down)
            {
                if (curX == GameBoard.GetLength(1) - 1)
                {
                    //cant move
                }
                else
                {
                    //can move
                    PlayerCharacter.Move(Actor.Direction.down);

                }
            }

            //sets mapcell to has been seen
            _GameBoard[PlayerCharacter.Row, PlayerCharacter.Col].HasBeenSeen = true;

            if(_GameBoard[PlayerCharacter.Col, PlayerCharacter.Row].ContainedMonster != null || _GameBoard[PlayerCharacter.Col, PlayerCharacter.Row].ContainedItem != null)
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
