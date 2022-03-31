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
    /// Interaction logic for frmFoundIt.xaml
    /// </summary>
    public partial class frmFoundIt : Window
    {
        public frmFoundIt()
        {
            InitializeComponent();
            lblItem.Content = Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem.Name;
           
        }

        private void LblClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// adds item to player's inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblAddToInv_Click(object sender, RoutedEventArgs e)
        {

            if(Game.OurMap.PlayerCharacter.Inventory.Count == 7)
            {
                lblItem.Content = "You can't fit anything more into your pockets!";
            }
            else
            {

                if (Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem.GetType() == typeof(Weapon))
                {
                    Weapon placeholder = (Weapon)Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem;
                    Game.OurMap.PlayerCharacter.AddToInventory(placeholder.CreateCopy());

                    Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem = null;
                }
                else if (Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem.GetType() == typeof(Potion))
                {
                    Potion placeholder = (Potion)Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem;
                    Game.OurMap.PlayerCharacter.AddToInventory(placeholder.CreateCopy());

                    Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem = null;

                }
            }



            this.Close();
        }
        /// <summary>
        /// applies item directly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUse_Click(object sender, RoutedEventArgs e)
        {
            
            Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem = Game.OurMap.PlayerCharacter.ApplyItem(Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem);


        }

        private void BtnUse_Click_1(object sender, RoutedEventArgs e)
        {
            Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem = Game.OurMap.PlayerCharacter.ApplyItem(Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem);

        }

        private void BtnEquip_Click(object sender, RoutedEventArgs e)
        {
            Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem = Game.OurMap.PlayerCharacter.ApplyItem(Game.OurMap.GameBoard[Game.OurMap.PlayerCharacter.Col, Game.OurMap.PlayerCharacter.Row].ContainedItem);
            this.Close();
        }
    }
}

