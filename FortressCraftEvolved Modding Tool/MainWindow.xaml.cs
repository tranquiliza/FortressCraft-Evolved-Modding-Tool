using System.Windows;
using MahApps.Metro.Controls;
using FortressCraftEvolved_Modding_Tool.Forms;
using Common.XmlLogic;
using System.IO;
using System.Collections.Generic;

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

            ResearchReader.ReadResearchXML(User.Default.ResearchXmlPath);
            ManufacturerRecipesReader.ReadManufactoringXML(User.Default.ManufactorerXmlPath);
            ItemsReader.ReadItems(User.Default.ItemsXmlPath);
            TerrainDataReader.ReadTerrainDataEntry(User.Default.TerrainDataXmlPath);
            ManufacturerRecipesReader.ReadRefineryRecipes(User.Default.RefineryXmlPath);
            ResearchWindow = new UserControl_Research();
            ManufacturerWindow = new UserControl_Manufacturer();
            ItemsWindow = new UserControl_Items();
            TerrainDataWindow = new UserControl_TerrainData();
            GACWindow = new UserControl_GAC();

            //Let the user know to update the paths, mainly ownly shows on first time use!
            if (User.Default.ResearchXmlPath == "")
            {
                MessageBox.Show("Research.XML path not found, please check settings!");
            }
            if (User.Default.ManufactorerXmlPath == "")
            {
                MessageBox.Show("ManufacturerRecipes.xml path not found, please check settings!");
            }
            if (User.Default.ItemsXmlPath == "")
            {
                MessageBox.Show("Items.xml path not found, please check settings!");
            }
            if (User.Default.TerrainDataXmlPath == "")
            {
                MessageBox.Show("TerrainData.xml path not found, please check settings!");
            }
            if (User.Default.RefineryXmlPath == "")
            {
                MessageBox.Show("RefineryRecipes.xml path not found, Please check settings!");
            }
            //A nice welcome message! :D -> Could display version name here?!
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
            //This takes the UserControl_Research and displays it on the MainWindow! No need for millions of windows! Woot!
            ContentMain.Content = ResearchWindow;
        }

        private void KeyPress(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F5)
            {
                ContentMain.Content = null;
            }
        }

        private void button_LoadManufacturer_Click(object sender, RoutedEventArgs e)
        {
            ContentMain.Content = ManufacturerWindow;
        }

        private void button_LoadItems_Click(object sender, RoutedEventArgs e)
        {
            ContentMain.Content = ItemsWindow;
        }

        private void button_LoadTerrainData_Click(object sender, RoutedEventArgs e)
        {
            ContentMain.Content = TerrainDataWindow;
        }

        private void button_FeedMynock_Click(object sender, RoutedEventArgs e)
        {
            string url = "www.twitchalerts.com/donate/djarcas";
            System.Diagnostics.Process.Start(url);
        }
        private void button_LoadGAC_Click(object sender, RoutedEventArgs e)
        {
            ContentMain.Content = GACWindow;
        }
        private void button_GenerateCreativeSurvival_Click(object sender, RoutedEventArgs e)
        {
            Common.ModLogics.CreativeSurvivalMod.ConvertRecipes();
            Common.ModLogics.CreativeSurvivalMod.ConvertItems();

            string recipes = XMLSerializer.Serialize(Common.ModLogics.CreativeSurvivalMod.ModdedRecipes);
            string items = XMLSerializer.Serialize(Common.ModLogics.CreativeSurvivalMod.ModdedItems);

            File.WriteAllText("ManufacturerRecipes.xml", recipes);
            File.WriteAllText("Items.xml", items);

            MessageBox.Show("Generated Creative Survival!");
        }

    }
}
