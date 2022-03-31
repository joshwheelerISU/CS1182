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
    /// Interaction logic for Dialogue.xaml
    /// </summary>
    public partial class Dialogue : Window
    {

        List<String> chapterTitles = new List<String>();
        List<String> chapterText = new List<String>();


        public Dialogue()
        {
            InitializeComponent();

            CreateDialogue();
            OutputDialogue();


            

        }
        /// <summary>
        /// creates dialogue for the game to use elsewhere
        /// </summary>
        private void CreateDialogue()
        {
            chapterTitles.Add("Alone and Afraid...");
            chapterTitles.Add("A Light in the Dark...");
            chapterTitles.Add("Growing Brighter...");
            chapterTitles.Add("At the End of the Tunnel...");
            chapterTitles.Add("Hope is a flickering candle...");

            chapterText.Add("You awaken in a dark room, with nothing but the clothes on your back. No matter how hard you try, you can't seem to remember how you got there." +
                " Desperation grows. You move forward and discover that the room is not actually dark, just permeated with thick fog that recedes as your move. " +
                "Your situation seems to be improving. You realize there's a piece of paper in your pocket. \r\n(Open your inventory...)");
            chapterText.Add("The door opens, leading you to a ladder. You climb it, only to find yourself in a room similar to the one you just left. Something feels different... Something about the echoes... " +
                "You come to the conclusion that this room is significantly larger than the last. Must be a door around here somewhere. \r\n(Repeat the process. Find key --> Open Door --> Advance.)");
            chapterText.Add("As you ascend the ladder, you feel... Different. Stronger. Ready for anything that comes your way. \r\n(Max HP Increased. You have been healed!)");
            chapterText.Add("You wonder if this is ever going to end. Are you trapped in an eternal climb? Is this Hell? Purgatory? You don't know. You hear a deep growl echoing throughout this room." +
                " \r\n(Your enemies have grown  stronger. Be careful, traveller.)");
            chapterText.Add("Your enemies grow stronger. The only way out is through them. You feel new power coursing through your veins. You WILL make it out of this. \r\n(The game will continue until you die.) \r\n(How far will you get before the darkness takes you?)");
        }
        /// <summary>
        /// outputs dialogue. does player / mob effects as the game progresses
        /// </summary>
        private void OutputDialogue()
        {
            int dIndex = Game.DialogueProgression;

            lblChapter.Content = chapterTitles[dIndex];
            txtOut.Text = chapterText[dIndex];

            if(dIndex == 2)
            {
                Game.OurMap.PlayerCharacter.MaxHP += 5;
                Game.OurMap.PlayerCharacter.CurrentHP = Game.OurMap.PlayerCharacter.MaxHP;
            }
            if(dIndex >= 3)
            {
                foreach(Monster M in Game.OurMap.PossibleMonsters)
                {
                    M.ATKValue += 3;
                    M.MaxHP += 3;
                    M.CurrentHP = M.MaxHP;
                }
                Game.OurMap.PlayerCharacter.MaxHP += 5;
            }

            if (dIndex == 4)
            {              
                //stop size increase

                Game.MapSizeDifference = 0;
                
            }
            else
            {
                Game.DialogueProgression++;
            }
        }
        /// <summary>
        /// closes window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
