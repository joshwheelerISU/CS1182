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
using System.Windows.Shapes;
using TempGameClasses;

namespace Deliv7
{
    /// <summary>
    /// Interaction logic for frmGameOver.xaml
    /// </summary>
    public partial class frmGameOver : Window
    {
        //fields




        //properties
        
        public frmGameOver()
        {
            InitializeComponent();

            
            if(Game.Trackstate == Game.GameState.Running)
            {
                btnReset.Visibility = Visibility.Collapsed;
                btnQuit.Visibility = Visibility.Collapsed;

                if(Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem != null)
                {
                    if(Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem.GetType() == typeof(Door))
                    {
                        lblChapter.Content = "You've found the door...";
                    }
                }

                
            }else if(Game.Trackstate == Game.GameState.Lost)
            {
                btnContinue.Visibility = Visibility.Collapsed;
                btnOpenDoor.Visibility = Visibility.Collapsed;

                lblChapter.Content = "You have been slain...";
                txtOut.Text = "You were slain in battle. Your essense fades. An eternity passes. You feel sunshine on your face, wind through your hair. Your open your eyes... \r\n\r\n(Game Over! Try again or Exit!)";
                
            }




        }
        /// <summary>
        /// continues the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// uses the key to open the door
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenDoor_Click(object sender, RoutedEventArgs e)
        {
            Door temp = (Door)Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem;

            if(Game.OurMap.PlayerCharacter.HeldKey != null)
            {
                if (temp.IsKey(Game.OurMap.PlayerCharacter.HeldKey))
                {
                    //next floor

                    Game.OurMap.PlayerCharacter.HeldKey = null; 
                    Game.Width += Game.MapSizeDifference;
                    Game.Height += Game.MapSizeDifference;

                    Game.ResetGame(Game.Width, Game.Height, true);

                    Dialogue dg = new Dialogue();

                    dg.ShowDialog();

                    Game.ScoreUp();

                    this.Close();

                }
                else
                {
                    //not the right key!
                }
            }
            else
            {
                //no key
                txtOut.Text = "You wiggle the handle, it won't give. Gonna need a key... It's gotta be around here somewhere...";
                btnOpenDoor.Visibility = Visibility.Hidden;
           
            }

        }
        /// <summary>
        /// resets the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {

            Game.SaveHighScore();

            Game.Width = 5;
            Game.Height = 5;
            Game.ResetGame();

            this.Close();

            
        }
        /// <summary>
        /// kills the whole game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            Game.SaveHighScore();
            Application.Current.Shutdown();
            
        }
        
    }
}
