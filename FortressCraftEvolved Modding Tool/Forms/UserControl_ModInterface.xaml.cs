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
using Common.ModLogics;
using Common.ModWriter;
using Common.XmlLogic;
using System.IO;
using FortressCraftEvolved_Modding_Tool.Forms.ModForms;

namespace FortressCraftEvolved_Modding_Tool.Forms
{
    /// <summary>
    /// Interaction logic for UserControl_ModInterface.xaml
    /// </summary>
    public partial class UserControl_ModInterface : UserControl
    {
        ModConfiguration Config = null;

        UserControl_ModItems ModItemsWindow = null;
        public UserControl_ModInterface()
        {
            InitializeComponent();
            textBlock_SelectedMod.Text = "";
            button_Items.Visibility = Visibility.Hidden;
        }

        private void button_CreateMod_Click(object sender, RoutedEventArgs e)
        {
            Form_ModCreator popup = new Form_ModCreator();
            popup.ShowDialog();
            //Config is also used for making the directory of the mod! (Cause it contains all the values :D )
            Config = ModWriterDataHolder.Config;
            if (User.Default.AuthorID == "")
            {
                MessageBox.Show("No Mod AuthorID Found, please check the settings!");
                return;
            }
            if (Config == null)
            {
                return;
            }
            ModCreator.GenerateDirectory(User.Default.WritePath, Config);
            string configfilepath = System.IO.Path.Combine(User.Default.WritePath, Config.Id);
            configfilepath = System.IO.Path.Combine(configfilepath, Config.Version);
            configfilepath += "\\";
            string configFile = XMLSerializer.Serialize(Config, false);
            File.WriteAllText(configfilepath + "Mod.Config", configFile);



            string[] IdSplit = Config.Id.Split('.');
            textBlock_SelectedMod.Text = "Mod: " + Config.Name + ", By " + IdSplit[0] + ", Version: " + Config.Version;
        }

        private void button_SelectMod_Click(object sender, RoutedEventArgs e)
        {
            Form_ModSelector Popup = new Form_ModSelector();
            Popup.ShowDialog();
            if (User.Default.ConfigFilePath.Contains("Mod.Config"))
            {
                try
                {
                    Config = XMLSerializer.Deserialize<ModConfiguration>(File.ReadAllText(User.Default.ConfigFilePath));
                    string[] IdSplit = Config.Id.Split('.');
                    textBlock_SelectedMod.Text = "Mod: " + Config.Name + ", By " + IdSplit[0] + ", Version: " + Config.Version;
                }
                catch (Exception x)
                {
                    File.WriteAllText("ModCreaterError.txt", "Error: " + x);
                }
            }
            else
            {
                return;
            }
            button_Items.Visibility = Visibility.Visible;
        }

        private void button_Items_Click(object sender, RoutedEventArgs e)
        {
            if (ModItemsWindow == null)
            {
                ModItemsWindow = new UserControl_ModItems();
            }
            ContentMain.Content = ModItemsWindow;
        }
    }
}
