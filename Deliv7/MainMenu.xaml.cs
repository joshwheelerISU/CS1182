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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Microsoft.Win32;
using TempGameClasses;


namespace Deliv7
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {

        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            

            if(txtNameInput.Text != null)
            {
                Game.PassedCharacterName = txtNameInput.Text;
            }
            else
            {
                Game.PassedCharacterName = "Frank";
            }
            MainWindow MainGame = new MainWindow();
            MainGame.Show();
            this.Close();
        }

        private void BtnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainGame = new MainWindow();
            FileStream fs = null;
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Game Files | *.game | All Files(*.*)|*.*";

                if(ofd.ShowDialog() == true)
                {
                    fs = File.Open(ofd.FileName, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    Game.OurMap = (Map)bf.Deserialize(fs);
                }
                else
                {
                    //failed
                }
            }
            catch
            {

            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            MainGame.Show();
            MainGame.DrawMap();
            this.Close();
            
        }
    }
}
