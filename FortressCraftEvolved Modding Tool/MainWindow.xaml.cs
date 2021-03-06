﻿using System;
using System.Windows;
using MahApps.Metro.Controls;
using FortressCraftEvolved_Modding_Tool.Forms;
using Common.XmlLogic;
using System.IO;
using Common.ModWriter;
using MahApps.Metro.Controls.Dialogs;
using Version = Common.Data.Version;

namespace FortressCraftEvolved_Modding_Tool
{
    public partial class MainWindow : MetroWindow
    {
        UserControl_Research ResearchWindow;
        UserControl_Manufacturer ManufacturerWindow;
        UserControl_Items ItemsWindow;
        UserControl_TerrainData TerrainDataWindow;
        UserControl_GAC GACWindow;
        UserControl_ModInterface ModWindow;

        public MainWindow()
        {
            InitializeComponent();
            //Uncomment to Reset users information! (This is for debugging)
            //User.Default.Reset();
            //---
            textBlock_Welcome.Text += " " + Version.Value;
            textBlock_Welcome.Text += "\n Browse the application by using the buttons below!";
            //textBlock_Welcome.Text += "\n Use F5 to reset this window!";

            Common.Error.Log("Program Initiated!");
        }

        private async void MainWindow_OnLoaded(Object sender, RoutedEventArgs e)
        {
            var pathStructure = Path.Combine("FortressCraft", "{0}", "Default", "Data");
            if (!User.Default.GameData.Contains(string.Format(pathStructure, "64")) &&
                !User.Default.GameData.Contains(string.Format(pathStructure, "32"))  )//|| User.Default.AuthorID == "Tranq")
            {
                var dialog = new PathSelectorDialog();
                await this.Dispatcher.Invoke(async () =>
                {
                    await this.ShowMetroDialogAsync(dialog);
                    await dialog.Task;
                    await this.HideMetroDialogAsync(dialog);
                });

                User.Default.GameData = dialog.GamePath;// + Path.DirectorySeparatorChar;

                User.Default.ResearchXmlPath = Path.Combine(dialog.GamePath, "Research.xml");
                User.Default.ItemsXmlPath = Path.Combine(dialog.GamePath, "Items.xml");
                User.Default.ManufactorerXmlPath = Path.Combine(dialog.GamePath, "ManufacturerRecipes.xml");
                User.Default.TerrainDataXmlPath = Path.Combine(dialog.GamePath, "TerrainData.xml");
                User.Default.RefineryXmlPath = Path.Combine(dialog.GamePath, "RefineryRecipes.xml");
                User.Default.AuthorID = dialog.AuthorId;
                User.Default.WritePath = dialog.DataPath;
                User.Default.Save();
            }

            Common.Error.Log("Reading Game Data!");
            ResearchReader.ReadResearchXML(User.Default.ResearchXmlPath);
            ManufacturerRecipesReader.ReadManufactoringXML(User.Default.ManufactorerXmlPath);
            ItemsReader.ReadItems(User.Default.ItemsXmlPath);
            TerrainDataReader.ReadTerrainDataEntry(User.Default.TerrainDataXmlPath);
            ManufacturerRecipesReader.ReadRefineryRecipes(User.Default.RefineryXmlPath);
            Common.Error.Log("Reading Game Data! --- COMPLETE!");
        }

        //When user clicks the settings button currently only shows path. Maybe add possible change of style (Dark / Light?)
        private async void Button_Settings(object sender, RoutedEventArgs e)
        {
            var dialog = new PathSelectorDialog();
            await this.Dispatcher.Invoke(async () =>
            {
                await this.ShowMetroDialogAsync(dialog);
                await dialog.Task;
                await this.HideMetroDialogAsync(dialog);
            });

            User.Default.GameData = dialog.GamePath;// + Path.DirectorySeparatorChar;

            User.Default.ResearchXmlPath = Path.Combine(dialog.GamePath, "Research.xml");
            User.Default.ItemsXmlPath = Path.Combine(dialog.GamePath, "Items.xml");
            User.Default.ManufactorerXmlPath = Path.Combine(dialog.GamePath, "ManufacturerRecipes.xml");
            User.Default.TerrainDataXmlPath = Path.Combine(dialog.GamePath, "TerrainData.xml");
            User.Default.RefineryXmlPath = Path.Combine(dialog.GamePath, "RefineryRecipes.xml");
            User.Default.AuthorID = dialog.AuthorId;
            User.Default.WritePath = dialog.DataPath;
            User.Default.Save();
            //Do nothing for now!
            //Prompts people with the fileselector Windows Form. This is a windows form cause they have OpenFileDialog Controls!!
            // Form_PathSelector Settings = new Form_PathSelector();
            // Settings.ShowDialog();
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
            if (e.Key == System.Windows.Input.Key.F1)
            {
                ModConfiguration CleanMod = new ModConfiguration();
                CleanMod.Id = User.Default.AuthorID + "." + "CleanMod";
                CleanMod.Name = "Clean Mod";
                CleanMod.Version = "1";
                Common.ModLogics.CleanRecipesMod.GenerateCleanMod();

                string CleanModConfig = XMLSerializer.Serialize(CleanMod, false);
                ModCreator.GenerateDirectory(User.Default.WritePath, CleanMod);

                //Mod.Config Path.
                string CleanModConfigPath = Path.Combine(User.Default.WritePath, CleanMod.Id);
                CleanModConfigPath = Path.Combine(CleanModConfigPath, CleanMod.Version);
                CleanModConfigPath += "\\Mod.Config";
                File.WriteAllText(CleanModConfigPath, CleanModConfig);

                //XML Save Path:
                string CleanModXmlPath = Path.Combine(User.Default.WritePath, CleanMod.Id);
                CleanModXmlPath = Path.Combine(CleanModXmlPath, CleanMod.Version);
                CleanModXmlPath = Path.Combine(CleanModXmlPath, ModCreator.Xml);
                CleanModXmlPath += "\\";

                string CleanModCraftData = XMLSerializer.Serialize(Common.ModLogics.CleanRecipesMod.ModdedRecipes, false);
                string CleanModResearchData = XMLSerializer.Serialize(Common.ModLogics.CleanRecipesMod.ModdedResearch, false);

                File.WriteAllText(CleanModXmlPath + "ManufacturerRecipes.xml", CleanModCraftData);
                File.WriteAllText(CleanModXmlPath + "Research.xml", CleanModResearchData);

                MessageBox.Show("Made a Cleanmod in " + User.Default.WritePath);
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
            //string DjArcasDonate = "www.twitchalerts.com/donate/djarcas";
            string TranqDonate = "www.paypal.me/tranquiliza";
            MessageBoxResult lResult = MessageBox.Show("This will take you to a website, where you can support the developer of this application!", "Notice", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (lResult == MessageBoxResult.OK)
            {
                System.Diagnostics.Process.Start(TranqDonate);
            }
            //System.Diagnostics.Process.Start(TranqDonate);
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
            if (User.Default.GameData == "")
            {
                MessageBox.Show("GameData path was empty. Please check your settings!");
                return;
            }

            Common.ModLogics.CreativeSurvivalMod.AuthorID = "Tranq";
            Common.ModLogics.CreativeSurvivalMod.ConvertRecipes();
            Common.ModLogics.CreativeSurvivalMod.ConvertItems();
            Common.ModLogics.CreativeSurvivalMod.AddExtraRecipes(); //Currently only adds ores to ManufacturerPlant!

            string recipes = XMLSerializer.Serialize(Common.ModLogics.CreativeSurvivalMod.ModdedRecipes, false);
            string items = XMLSerializer.Serialize(Common.ModLogics.CreativeSurvivalMod.ModdedItems, false);

            ModConfiguration CreativeSurvival = new ModConfiguration();
            CreativeSurvival.Id = Common.ModLogics.CreativeSurvivalMod.AuthorID + "." + "CreativeSurvival";
            CreativeSurvival.Name = "Creative Survival";
            CreativeSurvival.Version = "1";

            string ConfigFile = XMLSerializer.Serialize(CreativeSurvival, false);

            ModCreator.GenerateDirectory(User.Default.WritePath, CreativeSurvival);

            //This is where we save the xml files!
            string xmlfilepath = Path.Combine(User.Default.WritePath, CreativeSurvival.Id);
            xmlfilepath = Path.Combine(xmlfilepath, CreativeSurvival.Version);
            xmlfilepath = Path.Combine(xmlfilepath, ModCreator.Xml);
            xmlfilepath += "\\";
            //This is where we save the config file
            string configfilepath = Path.Combine(User.Default.WritePath, CreativeSurvival.Id);
            configfilepath = Path.Combine(configfilepath, CreativeSurvival.Version);
            configfilepath += "\\";
            

            File.WriteAllText(xmlfilepath + "ManufacturerRecipes.xml", recipes);
            File.WriteAllText(xmlfilepath + "Items.xml", items);
            File.WriteAllText(configfilepath + "Mod.Config", ConfigFile);
            
            Common.ModLogics.FreeDecorMod.AuthorID = "Tranq";
            Common.ModLogics.FreeDecorMod.CreateRecipes();
            string FreeDecorRecipes = XMLSerializer.Serialize(Common.ModLogics.FreeDecorMod.ModdedRecipes, false);

            ModConfiguration FreeDecor = new ModConfiguration();
            FreeDecor.Id = Common.ModLogics.FreeDecorMod.AuthorID + "." + "FreeDecorMod";
            FreeDecor.Name = "Free Decor Mod";
            FreeDecor.Version = "1";

            string FreeDecorConfig = XMLSerializer.Serialize(FreeDecor, false);
            ModCreator.GenerateDirectory(User.Default.WritePath, FreeDecor);

            //This is where we save the xml files!
            string FreeDecorXmlfilepath = Path.Combine(User.Default.WritePath, FreeDecor.Id);
            FreeDecorXmlfilepath = Path.Combine(FreeDecorXmlfilepath, FreeDecor.Version);
            FreeDecorXmlfilepath = Path.Combine(FreeDecorXmlfilepath, ModCreator.Xml);
            FreeDecorXmlfilepath += "\\";
           
            //This is where we save the config file
            string FreeDecorConfigfilepath = Path.Combine(User.Default.WritePath, FreeDecor.Id);
            FreeDecorConfigfilepath = Path.Combine(FreeDecorConfigfilepath, FreeDecor.Version);
            FreeDecorConfigfilepath += "\\";
            
            File.WriteAllText(FreeDecorXmlfilepath + "ManufacturerRecipes.xml", FreeDecorRecipes);
            File.WriteAllText(FreeDecorConfigfilepath + "Mod.Config", FreeDecorConfig);

            string Message = "Generated Creative Survival Files in the root folder!";
            if (User.Default.WritePath != "")
            {
               Message = "Generated Creative Survival and Free Decor Mod Files in: " + User.Default.WritePath + ".";
            }
            MessageBox.Show(Message);
        }

        private void button_CreateMod_Click(object sender, RoutedEventArgs e)
        {
            if (ModWindow == null)
            {
                ModWindow = new UserControl_ModInterface();
            }
            //else //This will probably be hooked up in the future! 
            //{
            //    MessageBoxResult lResult = MessageBox.Show("Reset mod interface?", "IMPORTANT", MessageBoxButton.YesNo);
            //    if (lResult == MessageBoxResult.Yes)
            //    {
            //        ModWindow = new UserControl_ModInterface();
            //    }
            //}
            ContentMain.Content = ModWindow;
        }
    }
}
