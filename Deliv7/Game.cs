using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempGameClasses;
using System.IO;

namespace Deliv7
{
    static class Game
    {
        //enum
        public enum GameState
        {
            Running,Lost,Won
        }

        //fields
        private static GameState _TrackState;
        private static Map _OurMap;
        private static int _Height = 5;
        private static int _Width = 5;
        private static int _DialogueProgression = 0;
        private static int _MapSizeDifference = 3;
        private static int _Score;
        private static int _HighScore = 0;
        private static string _passedCharacterName = "Dave";


        //properties
        public static GameState Trackstate
        {
            get {
                if(_OurMap.PlayerCharacter.IsAlive == false)
                {
                    _TrackState = GameState.Lost;
                }
                return _TrackState;
            }
            set { _TrackState = value; }
        }
        public static int MapSizeDifference
        {
            get { return _MapSizeDifference; }
            set { _MapSizeDifference = value; }
        }
        public static int Score
        {
            get { return _Score; }
            set { value = _Score; }
        }
        public static string PassedCharacterName
        {
            get { return _passedCharacterName; }
            set { _passedCharacterName = value; }
        }
        public static int HighScore
        {
            get { return _HighScore; }
        }
        public static int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        public static int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        public static int DialogueProgression
        {
            get { return _DialogueProgression; }
            set { _DialogueProgression = value; }
        }

        public static Map OurMap
        {
            get { return _OurMap; }
            set { _OurMap = value; }
        }
        /// <summary>
        /// resets game
        /// </summary>
        /// <param name="x">width</param>
        /// <param name="y">height</param>
        public static void ResetGame(int x, int y, bool keepCharacter)
        {
            Trackstate = GameState.Running;
            Hero PC;
            if (keepCharacter)
            {
                PC = _OurMap.PlayerCharacter;
            }
            else
            {
                PC = new Hero(_passedCharacterName, "of the beyond", 12, 5, 0, 0);
            }



            _OurMap = new Map(x, y, PC);

            Random rng = new Random();

            //places hero in unoccupied cell.
            for(bool pass = false; pass == false;)
            {
                int Rx = rng.Next(0, _OurMap.GameBoard.GetLength(0));
                int Ry = rng.Next(0, _OurMap.GameBoard.GetLength(1));

                if(_OurMap.GameBoard[Rx,Ry].ContainedMonster != null || _OurMap.GameBoard[Rx, Ry].ContainedItem != null)
                {
                    //do nothing
                }
                else
                {
                    if (keepCharacter != true)
                    {
                        _OurMap.PlayerCharacter = new Hero(_passedCharacterName, "of the beyond", 12, 5, Ry, Rx);
                        pass = true;
                    }
                    else
                    {
                        _OurMap.PlayerCharacter.Col = Ry;
                        _OurMap.PlayerCharacter.Row = Rx;
                        pass = true;
                    }
                }

            }

        }
        /// <summary>
        /// see above resetgame method
        /// </summary>
        public static void ResetGame()
        {
            ResetGame(Height, Width, false);
        }
        /// <summary>
        /// adds to score. Score is number of levels traversed. 
        /// </summary>
        public static void ScoreUp()
        {
            _Score = Score + 1;
        }

        public static void GetHighScore()
        {
            int hs = 0;
            try
            {
                hs = Int32.Parse(File.ReadAllLines("hs.txt")[0]);

            }
            catch
            {

            }
            finally
            {
                _HighScore = hs;
            }
        }

        public static void SaveHighScore()
        {

            if(Score > HighScore)
            {
                _HighScore = _Score;
            }
            File.WriteAllText("hs.txt", _HighScore.ToString());
        }
    }
}
