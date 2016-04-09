using System.Windows;
using MahApps.Metro.Controls;
using FortressCraftEvolved_Modding_Tool.Forms;
using Common.XmlLogic;
using System.IO;
using System.Collections.Generic;
using Common.Data;

namespace FortressCraftEvolved_Modding_Tool
{
    public partial class MainWindow : MetroWindow
    {
        UserControl_Research ResearchWindow;
        UserControl_Manufacturer ManufacturerWindow;
        UserControl_Items ItemsWindow;
        UserControl_TerrainData TerrainDataWindow;
        UserControl_GAC GACWindow;

        public MainWindow()
        {
            InitializeComponent();
            //Uncomment to Reset users information! (This is for debugging)
            //User.Default.Reset();
            ResearchReader.ReadResearchXML(User.Default.ResearchXmlPath);
            ManufacturerRecipesReader.ReadManufactoringXML(User.Default.ManufactorerXmlPath);
            ItemsReader.ReadItems(User.Default.ItemsXmlPath);
            TerrainDataReader.ReadTerrainDataEntry(User.Default.TerrainDataXmlPath);
            ManufacturerRecipesReader.ReadRefineryRecipes(User.Default.RefineryXmlPath);

            //Let the user know to update the paths, mainly ownly shows on first time use!
            if (User.Default.GameData == "")
            {
                MessageBox.Show("Please go to settings at specify where the games data is located! \n Example: E:\\SteamLibrary\\SteamApps\\common\\FortressCraft\\64\\Default\\Data");
            }
            //if (User.Default.ResearchXmlPath == "")
            //{
            //    MessageBox.Show("Research.XML path not found, please check settings!");
            //}
            //if (User.Default.ManufactorerXmlPath == "")
            //{
            //    MessageBox.Show("ManufacturerRecipes.xml path not found, please check settings!");
            //}
            //if (User.Default.ItemsXmlPath == "")
            //{
            //    MessageBox.Show("Items.xml path not found, please check settings!");
            //}
            //if (User.Default.TerrainDataXmlPath == "")
            //{
            //    MessageBox.Show("TerrainData.xml path not found, please check settings!");
            //}
            //if (User.Default.RefineryXmlPath == "")
            //{
            //    MessageBox.Show("RefineryRecipes.xml path not found, Please check settings!");
            //}
            //if (User.Default.WritePath == "")
            //{
            //    MessageBox.Show("You have not selected a path for the program to write files to! \n You can do so in the settings!");
            //}
            //A nice welcome message! :D -> Could display version name here?!
            textBlock_Welcome.Text += Version.Value;
            textBlock_Welcome.Text += "\n Browse the application by using the buttons below!";
            textBlock_Welcome.Text += "\n Use F5 to reset this window!";
        }


        //When user clicks the settings button currently only shows path. Maybe add possible change of style (Dark / Light?)
        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            //Prompts people with the fileselector Windows Form. This is a windows form cause they have OpenFileDialog Controls!!
            Form_PathSelector Settings = new Form_PathSelector();
            Settings.ShowDialog();
        }

        private void button_LoadResearch_Click(object sender, RoutedEventArgs e)
        {
            if (ResearchWindow == null)
            {
                ResearchWindow = new UserControl_Research();
            }
            ContentMain.Content = ResearchWindow;
        }

        private void KeyPress(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F5)
            {
                ContentMain.Content = null;
                ResearchWindow = null;
                ManufacturerWindow = null;
                ItemsWindow = null;
                TerrainDataWindow = null;
                GACWindow = null;
            }
        }

        private void button_LoadManufacturer_Click(object sender, RoutedEventArgs e)
        {
            if (ManufacturerWindow == null)
            {
                ManufacturerWindow = new UserControl_Manufacturer();
            }
            ContentMain.Content = ManufacturerWindow;
        }

        private void button_LoadItems_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsWindow == null)
            {
                ItemsWindow = new UserControl_Items();
            }
            ContentMain.Content = ItemsWindow;
        }

        private void button_LoadTerrainData_Click(object sender, RoutedEventArgs e)
        {
            if (TerrainDataWindow == null)
            {
                TerrainDataWindow = new UserControl_TerrainData();
            }
            ContentMain.Content = TerrainDataWindow;
        }

        private void button_FeedMynock_Click(object sender, RoutedEventArgs e)
        {
            string url = "www.twitchalerts.com/donate/djarcas";
            System.Diagnostics.Process.Start(url);
        }
        private void button_LoadGAC_Click(object sender, RoutedEventArgs e)
        {
            if (GACWindow == null)
            {
                GACWindow = new UserControl_GAC();
            }
            ContentMain.Content = GACWindow;
        }
        private void button_GenerateCreativeSurvival_Click(object sender, RoutedEventArgs e)
        {
            if (User.Default.ManufactorerXmlPath == "")
            {
                MessageBox.Show("Cannot write Creative Survial without a ManufacturerRecipes.xml Path! \n Please check your settings!");
                return;
            }
            if (User.Default.ItemsXmlPath == "")
            {
                MessageBox.Show("Cannot write Creative Survial without a Items.xml Path! \n Please check your settings!");
                return;
            }
            Common.ModLogics.CreativeSurvivalMod.ConvertRecipes();
            Common.ModLogics.CreativeSurvivalMod.ConvertItems();
            Common.ModLogics.CreativeSurvivalMod.AddExtraRecipes(); //Currently only adds ores to ManufacturerPlant!

            string recipes = XMLSerializer.Serialize(Common.ModLogics.CreativeSurvivalMod.ModdedRecipes);
            string items = XMLSerializer.Serialize(Common.ModLogics.CreativeSurvivalMod.ModdedItems);

            File.WriteAllText(User.Default.WritePath + "ManufacturerRecipes.xml", recipes);
            File.WriteAllText(User.Default.WritePath + "Items.xml", items);
            string Message = "Generated Creative Survival Files in the root folder!";
            if (User.Default.WritePath != "")
            {
               Message = "Generated Creative Survival Files in: " + User.Default.WritePath + ".";
            }
            MessageBox.Show(Message);
        }

    }
}
