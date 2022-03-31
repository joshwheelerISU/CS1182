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
    /// Interaction logic for frmMonster.xaml
    /// </summary>
    public partial class frmMonster : Window
    {
        int col;
        int row;
        public frmMonster()
        {
            InitializeComponent();
            col = Game.OurMap.PlayerCharacter.Col;
            row = Game.OurMap.PlayerCharacter.Row;
            lblMonsterFullName.Content = Game.OurMap.GameBoard[col, row].ContainedMonster.GetName(true);
            OutputStats();
            
        }     
        /// <summary>
        /// attack click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BtnAttack_Click(object sender, RoutedEventArgs e)
        {
            if(Game.OurMap.PlayerCharacter + Game.OurMap.GameBoard[col, row].ContainedMonster == true)
            {
                OutputStats();

                if(Game.OurMap.GameBoard[col,row].ContainedMonster.IsAlive == false)
                {
                    Game.OurMap.GameBoard[col, row].ContainedMonster = null;
                    this.Close();
                }
            }
            else
            {
                Game.Trackstate = Game.GameState.Lost;
                frmGameOver g = new frmGameOver();
                g.ShowDialog();
                this.Close();
            }
        }
        /// <summary>
        /// output stats
        /// </summary>
        private void OutputStats()
        {
            Monster m = Game.OurMap.GameBoard[col, row].ContainedMonster;
            lblHeroName.Content = Game.OurMap.PlayerCharacter.GetName(false);
            lblMonsterName.Content = m.GetName(false);
            lblHeroHP.Content = Game.OurMap.PlayerCharacter.CurrentHP + "/" + Game.OurMap.PlayerCharacter.MaxHP;
            lblMonsterHP.Content = m.CurrentHP + "/" + m.MaxHP;

        }
        /// <summary>
        /// run away
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblRun_Click(object sender, RoutedEventArgs e)
        {
            Game.OurMap.PlayerCharacter.IsRunningAway = true;
            if(Game.OurMap.PlayerCharacter + Game.OurMap.GameBoard[col, row].ContainedMonster)
            {
                Game.OurMap.PlayerCharacter.IsRunningAway = false;
                this.Close();
            }
            else
            {
                Game.OurMap.PlayerCharacter.IsRunningAway = false;

                Game.Trackstate = Game.GameState.Lost;
                this.Close();
            }
        }
    }
}
