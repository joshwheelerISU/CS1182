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
    /// Interaction logic for Inventoruy.xaml
    /// </summary>
    public partial class Inventoruy : Window
    {
        private int selectedIndex;


        public Inventoruy()
        {
            InitializeComponent();

            FillInventory();
            btnApply.Visibility = Visibility.Hidden;
            btnDestroy.Visibility = Visibility.Hidden;

        }



        private void FillInventory()
        {

            int i = 0;

            if (Game.OurMap.PlayerCharacter.Inventory.Count != 0)
            {
                foreach (Item I in Game.OurMap.PlayerCharacter.Inventory)
                {
                    Button curButton = new Button();
                    curButton.Content = I.Name;
                    curButton.Name = "btn_" + i.ToString();
                    i++;
                    curButton.Click += new RoutedEventHandler(this.btnClick);
                    stkInventory.Children.Add(curButton);
                }
            }
            else
            {
                Label lbl = new Label();
                lbl.Content = "Sorry, Your \r\n inventory is Empty!";
                stkInventory.Children.Add(lbl);

            }

        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            Button clickedBtn = (Button)sender;
            string[] temp = clickedBtn.Name.Split('_');
            int curIndex = Int32.Parse(temp[1]);

            selectedIndex = curIndex;

            lblSelectedName.Content = "Name: " + Game.OurMap.PlayerCharacter.Inventory[curIndex].Name;

            string affectValue;

            if(Game.OurMap.PlayerCharacter.Inventory[curIndex].GetType() == typeof(Potion))
            {
                affectValue = "Heals " + Game.OurMap.PlayerCharacter.Inventory[curIndex].AffectValue + " HP.";

            }
            else if(Game.OurMap.PlayerCharacter.Inventory[curIndex].GetType() == typeof(Weapon))
            {
                affectValue = "Damage: " + Game.OurMap.PlayerCharacter.Inventory[curIndex].AffectValue;
            }
            else
            {
                affectValue = "Affect Value Unreachable";
            }

            lblSelectedStats.Content = affectValue;

            btnApply.Visibility = Visibility.Visible;
            btnDestroy.Visibility = Visibility.Visible;


        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
        
          
            Game.OurMap.PlayerCharacter.ApplyItem(Game.OurMap.PlayerCharacter.Inventory[selectedIndex]);  
            Game.OurMap.PlayerCharacter.Inventory.Remove(Game.OurMap.PlayerCharacter.Inventory[selectedIndex]);

            
            selectedIndex = 0;

            this.Close();

        }

        private void BtnDestroy_Click(object sender, RoutedEventArgs e)
        {
            Game.OurMap.PlayerCharacter.Inventory.Remove(Game.OurMap.PlayerCharacter.Inventory[selectedIndex]);
            this.Close();
        }
    }
}
