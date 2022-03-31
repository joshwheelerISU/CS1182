using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TempGameClasses;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Microsoft.Win32;




namespace Deliv7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Game.ResetGame(Game.Height, Game.Width, false);
            //populate the grid with mapcells
            DrawMap();
            gdMap.ShowGridLines = true;
            //show dialogue
            Dialogue d = new Dialogue();
            d.ShowDialog();

            Game.GetHighScore();
            lblHighScore.Content = "All-Time High Score: " + Game.HighScore.ToString();

        }
        /// <summary>
        /// creates the map on button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreateMap_Click(object sender, RoutedEventArgs e)
        {
            
        }
        /// <summary>
        /// what happens when a square is clicked. currently nothing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, RoutedEventArgs e)
        {
            //do nothing for now;
        }
        /// <summary>
        /// draws the map on a grid. 
        /// </summary>
        public void DrawMap()
        {

            //prep and output map stats
            gdMap.RowDefinitions.Clear();
            gdMap.ColumnDefinitions.Clear();
            gdMap.Children.Clear();
            lblPercentage.Content = "Percent Discovered: " + Game.OurMap.PercentDiscovered.ToString("P");
            lblMonsters.Content = "Monsters on Map: " + Game.OurMap.MonsterCount.ToString();
            lblItems.Content = "Items on Map: " + Game.OurMap.ItemCount.ToString();
            lblAveragePot.Content = "Average Potion Effect: " + Game.OurMap.AverageHealValue.ToString();
            lblMaxHp.Content = "Max Monster HP: " + Game.OurMap.HighestHpMonster.ToString();
            lblLowestDamage.Content = "Min Weapon Damage: " + Game.OurMap.LeastWeapon.ToString();


            //fill grid with buttons that relate to the mapcells
            for (int i = 0; i < Game.OurMap.GameBoard.GetLength(0); i++)
            {
                gdMap.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < Game.OurMap.GameBoard.GetLength(1); i++)
            {
                gdMap.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < Game.OurMap.GameBoard.GetLength(0); i++)
            {

                for (int j = 0; j < Game.OurMap.GameBoard.GetLength(1); j++)
                {

                    //place content
                    Button thisButton = new Button();
                    thisButton.Name = "btn_" + i.ToString() + "_" + j.ToString();
                    Item myItem = Game.OurMap.GameBoard[i, j].ContainedItem;
                    thisButton.Background = new SolidColorBrush(Colors.Black);
                    SolidColorBrush seenButton = new SolidColorBrush(Colors.Gray);
                    if (Game.OurMap.GameBoard[j, i].HasBeenSeen == true)
                    {
                        //empty cell
                        thisButton.Background = seenButton;
                        //thisButton.Content = "x";

                    }
                    if (myItem != null)
                    {
                        //name of item
                        if (Game.OurMap.GameBoard[j, i].HasBeenSeen == true)
                        {
                            thisButton.Content = myItem.Name;
                        }
                    }

                    Monster currentMonster = Game.OurMap.GameBoard[i, j].ContainedMonster;
                    if (currentMonster != null)
                    {

                        ///name of monster
                        if (Game.OurMap.GameBoard[j, i].HasBeenSeen == true)
                        {
                            thisButton.Content = currentMonster.Name;
                        }
                    }

                    //draw where playercharacter is
                    if (Game.OurMap.PlayerCharacter.Row == j && Game.OurMap.PlayerCharacter.Col == i)
                    {

                        thisButton.Background = seenButton;
                        thisButton.Content = "YOU";
                        Game.OurMap.GameBoard[j, i].HasBeenSeen = true;
                    }

                    //actually adding the button
                    thisButton.Click += new RoutedEventHandler(this.btnMap_Click);
                    Grid.SetColumn(thisButton, i);
                    Grid.SetRow(thisButton, j);
                    gdMap.Children.Add(thisButton);
                    //lblPercentage.Content = "Percent Discovered: " + Game.OurMap.PercentDiscovered.ToString();
                }
            }
            UpdateHeroStats();
        }
        /// <summary>
        /// move player up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Up_Click(object sender, RoutedEventArgs e)
        {
            if (Game.OurMap.MoveHero(Actor.Direction.up))
            {
                UnifiedMoveButtonBehavior();
            }
            DrawMap();

        }
        /// <summary>
        /// move player down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Down_Click(object sender, RoutedEventArgs e)
        {
            if (Game.OurMap.MoveHero(Actor.Direction.down))
            {
                UnifiedMoveButtonBehavior();
            }
            DrawMap();
        }
        /// <summary>
        /// move player left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Left_Click(object sender, RoutedEventArgs e)
        {
            if (Game.OurMap.MoveHero(Actor.Direction.left))
            {
                UnifiedMoveButtonBehavior();
            }
            DrawMap();
        }
        /// <summary>
        /// move player right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Right_Click(object sender, RoutedEventArgs e)
        {
            if (Game.OurMap.MoveHero(Actor.Direction.right))
            {
                UnifiedMoveButtonBehavior();
            }
            DrawMap();
        }
        /// <summary>
        /// check for keypresses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyPress(object sender, KeyEventArgs e)
        {


            if (e.Key == Key.Up)
            {
                btn_Up.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.Down)
            {
                btn_Down.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.Left)
            {
                btn_Left.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.Right)
            {
                btn_Right.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }

        }
        /// <summary>
        /// not needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpKey(object sender, KeyEventArgs e)
        {

        }
        /// <summary>
        /// serializes classes such to make a save game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs = null;
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Game Files | *.game";
                if(sfd.ShowDialog() == true)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    fs = new FileStream(sfd.FileName, FileMode.Create);
                    bf.Serialize(fs, Game.OurMap);
                }
                else
                {
                    //error somewhere
                }
            }
            catch
            {

            }
            finally
            {
                if(fs != null)
                {
                    fs.Close();
                }
            }
        }
        /// <summary>
        /// updates player stats
        /// </summary>
        private void UpdateHeroStats()
        {
            lblHPOut.Content = Game.OurMap.PlayerCharacter.CurrentHP.ToString() + "/" + Game.OurMap.PlayerCharacter.MaxHP.ToString();
            lblNameOutput.Content = Game.OurMap.PlayerCharacter.GetName(true);
            lblInventoryOutput.Content = "Held Weapon: ";

            if(Game.OurMap.PlayerCharacter.HeldWeapon != null)
            {
                lblInventoryOutput.Content += Game.OurMap.PlayerCharacter.HeldWeapon.Name;

            }
            lblInventoryOutput.Content += "\r\nHeld Key: ";
            if (Game.OurMap.PlayerCharacter.HeldKey != null)
            {
                lblInventoryOutput.Content += Game.OurMap.PlayerCharacter.HeldKey.Name;
            }

            lblCurScore.Content = "Current Score: " + Game.Score.ToString();

            Game.GetHighScore();
            lblHighScore.Content = "All-Time High Score: " + Game.HighScore.ToString();
        }
        /// <summary>
        /// checks to see what's going on in the game
        /// </summary>
        private void CheckGameState()
        {
            if(Game.Trackstate == Game.GameState.Lost)
            {
                //lost
            }else if(Game.Trackstate == Game.GameState.Won)
            {
                //won
            }
            else
            {
                //business as usual
            }
        }

        /// <summary>
        /// makes the move button methods more clean by seperating out common code amongst them
        /// </summary>
        private void UnifiedMoveButtonBehavior()
        {   
            if (Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedMonster != null)
            {
                //monster window
                Window frmMonster = new frmMonster();
                DrawMap();

                frmMonster.ShowDialog();
            }
            else
            {
                if (Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem.GetType() == typeof(Door))
                {
                    //issa door
                    DrawMap();

                    frmGameOver g = new frmGameOver();

                    g.ShowDialog();
                }else if(Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem.GetType() == typeof(DoorKey))
                {
                    //issa key
                    Game.OurMap.PlayerCharacter.HeldKey = (DoorKey)Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem;

                    Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem = null;

                    frmFoundKey fk = new frmFoundKey();
                    fk.ShowDialog();
                }
                else
                {
                    //item window
                    Window frmFoundIt = new frmFoundIt();
                    DrawMap();

                    frmFoundIt.ShowDialog();
                }
            }
        }
        /// <summary>
        /// opens inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInventory_Click(object sender, RoutedEventArgs e)
        {
            Inventoruy inv = new Inventoruy(); //misspelled, cant change it. 

            inv.Show();
        }
    }
}
