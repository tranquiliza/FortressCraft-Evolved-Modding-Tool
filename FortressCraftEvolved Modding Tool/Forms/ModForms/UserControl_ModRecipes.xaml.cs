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
using System.IO;
using Common.ModWriter;
using Common.XmlLogic;
using Common.Data;

namespace FortressCraftEvolved_Modding_Tool.Forms.ModForms
{
    /// <summary>
    /// Interaction logic for UserControl_ModRecipes.xaml
    /// </summary>
    public partial class UserControl_ModRecipes : UserControl
    {
        CraftData ActiveRecipe = null;
        CraftData SelectedRecipe = null;
        bool mbEditing;
        bool mbIsOverride;
        bool mbIsSelfCraft = false;
        //Holders for Research Values and Scan Values
        private List<string> TempResearchReq;
        private List<string> TempScanReq;
        private List<string> TempRemoveResearchReq;
        private List<string> TempRemoveScanReq;
        private List<CraftCost> TempCraftCost;
        string ManufacturerXmlPath;
        public UserControl_ModRecipes()
        {
            InitializeComponent();
            string[] Split = User.Default.ConfigFilePath.Split('\\');
            for (int i = 0; i < Split.Length - 1; i++)
            {
                ManufacturerXmlPath += Split[i] + "\\";
            }
            ManufacturerXmlPath += "Xml\\";

            ManufacturerXmlPath += "ManufacturerRecipes.xml";
            try
            {
                ModWriterDataHolder.ManufacturerEntries = XMLSerializer.Deserialize <List<CraftData>>(File.ReadAllText(ManufacturerXmlPath));
            }
            catch (Exception x)
            {
                File.WriteAllText("ModRecipeCreatorError.txt", x.ToString());
            }
            RefreshItemsLists();

            //Textblocks:
            textBlock_IsOverride.Text = "";
            textBlock_Key.Text = "";
            textBlock_Tier.Text = "";
            textBlock_CraftedKey.Text = "";
            textBlock_CraftedAmount.Text = "";
            textBlock_CraftTime.Text = "";
            textBlock_ResearchCost.Text = "";
            textBlock_Desc.Text = "";
            textBlock_Hint.Text = "";


            //Textboxes
            textBox_Key.Text = "";
            textBox_CraftTime.Text = "";
            textBox_Desc.Text = "";
            textBox_Hint.Text = "";

            //ComboBox Values:
            comboBox_ResearchCost.Items.Clear();
            for (int i = 0; i < 41; i++)
            {
                comboBox_ResearchCost.Items.Add(i);
            }
            comboBox_Tier.Items.Clear();
            for (int i = 0; i < 8; i++)
            {
                comboBox_Tier.Items.Add(i);
            }
            comboBox_CraftedAmount.Items.Clear();
            for (int i = 1; i < 101; i++)
            {
                comboBox_CraftedAmount.Items.Add(i);
            }
            comboBox_CraftCostAmount.Items.Clear();
            for (int i = 1; i < 301; i++)
            {
                comboBox_CraftCostAmount.Items.Add(i);
            }
            comboBox_CraftCostAmount.SelectedItem = 1;
            comboBox_CraftedAmount.SelectedItem = 1;
            Refresh();
            EditMode(false);
            //Special Things, because we dont use these at all?? 

            textBlock_CraftTime.Visibility = Visibility.Hidden;
            label_CraftTime.Visibility = Visibility.Hidden;
            textBox_CraftTime.Visibility = Visibility.Hidden;
            comboBox_Key.Visibility = Visibility.Hidden;
            //Not sure if we need this button anymore
            button_DeleteResearch.Visibility = Visibility.Hidden;
        }

        private void Refresh()
        {
            comboBox_Category.Items.Clear();
            foreach (Category enumValue in Enum.GetValues(typeof(Category)))
            {
                comboBox_Category.Items.Add(enumValue);
            }
            comboBox_Category.SelectedItem = Category.mining;

            comboBox_Research.Items.Clear();
            for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
            {
                comboBox_Research.Items.Add(DataHolder.ResearchEntries[i].Key);
            }
            for (int i = 0; i < ModWriterDataHolder.ResearchEntires.Count; i++)
            {
                comboBox_Research.Items.Add(DataHolder.ResearchEntries[i].Key);
            }
            comboBox_Scan.Items.Clear();
            for (int i = 0; i < DataHolder.TerrainDataEntries.Count; i++)
            {
                if (DataHolder.TerrainDataEntries[i].Values != null)
                {
                    for (int j = 0; j < DataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        comboBox_Scan.Items.Add(DataHolder.TerrainDataEntries[i].Values[j].Key);
                    }
                }
                else
                {
                    comboBox_Scan.Items.Add(DataHolder.TerrainDataEntries[i].Key);
                }
            }
            for (int i = 0; i < ModWriterDataHolder.TerrainDataEntries.Count; i++)
            {
                if (ModWriterDataHolder.TerrainDataEntries[i].Values != null)
                {
                    for (int j = 0; j < ModWriterDataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        comboBox_Scan.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Values[j].Key);
                    }
                }
                else
                {
                    comboBox_Scan.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Key);
                }
            }

            //Crafted Key:
            comboBox_CraftedKey.Items.Clear();
            comboBox_CraftCostItem.Items.Clear();
            //Add items:
            for (int i = 0; i < DataHolder.ItemEntries.Count; i++)
            {
                comboBox_CraftedKey.Items.Add(DataHolder.ItemEntries[i].Key);
                comboBox_CraftCostItem.Items.Add(DataHolder.ItemEntries[i].Key);
            }
            //Mod items:
            for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
            {
                comboBox_CraftedKey.Items.Add(ModWriterDataHolder.Items[i].Key);
                comboBox_CraftCostItem.Items.Add(ModWriterDataHolder.Items[i].Key);
            }

            //Add Terrain Data
            for (int i = 0; i < DataHolder.TerrainDataEntries.Count; i++)
            {
                if (DataHolder.TerrainDataEntries[i].Values != null)
                {
                    for (int j = 0; j < DataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        comboBox_CraftedKey.Items.Add(DataHolder.TerrainDataEntries[i].Values[j].Key);
                        comboBox_CraftCostItem.Items.Add(DataHolder.TerrainDataEntries[i].Values[j].Key);
                    }
                }
                else
                {
                    comboBox_CraftedKey.Items.Add(DataHolder.TerrainDataEntries[i].Key);
                    comboBox_CraftCostItem.Items.Add(DataHolder.TerrainDataEntries[i].Key);
                }
            }

            //Modded TerrainData:
            for (int i = 0; i < ModWriterDataHolder.TerrainDataEntries.Count; i++)
            {
                if (ModWriterDataHolder.TerrainDataEntries[i].Values != null)
                {
                    for (int j = 0; j < ModWriterDataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        comboBox_CraftedKey.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Values[j].Key);
                        comboBox_CraftCostItem.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Values[j].Key);
                    }
                }
                else
                {
                    comboBox_CraftedKey.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Key);
                    comboBox_CraftCostItem.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Key);
                }
            }
        }

        private void EditMode(bool lbIsCrafting) //This function Controls VIS and interaction of the form
        {
            listBox_Recipes.IsEnabled = !lbIsCrafting;
            checkBox_Delete.IsEnabled = lbIsCrafting;
            //checkBox_IsSelfCraft.IsEnabled = lbIsCrafting;
            listBox_CraftCost.IsEnabled = lbIsCrafting;
            listBox_ResearchReq.IsEnabled = lbIsCrafting;
            listBox_RemoveResearchReq.IsEnabled = lbIsCrafting;
            listBox_ScanReq.IsEnabled = lbIsCrafting;
            listBox_RemoveScanReq.IsEnabled = lbIsCrafting;
            checkBox_IsOverride.IsEnabled = lbIsCrafting;
            checkBox_IsOverride.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            textBlock_IsOverride.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Visible;
            textBlock_Key.Visibility = lbIsCrafting ? Visibility.Hidden : Visibility.Visible;
            textBlock_Tier.Visibility = lbIsCrafting ? Visibility.Hidden : Visibility.Visible;
            textBlock_CraftedKey.Visibility = lbIsCrafting ? Visibility.Hidden : Visibility.Visible;
            textBlock_CraftedAmount.Visibility = lbIsCrafting ? Visibility.Hidden : Visibility.Visible;
            //textBlock_CraftTime.Visibility = Visibility.Hidden; // lbIsCrafting ? Visibility.Hidden : Visibility.Visible
            textBlock_ResearchCost.Visibility = lbIsCrafting ? Visibility.Hidden : Visibility.Visible;
            textBlock_Desc.Visibility = lbIsCrafting ? Visibility.Hidden : Visibility.Visible;
            textBlock_Hint.Visibility = lbIsCrafting ? Visibility.Hidden : Visibility.Visible;
            button_Save.Visibility = lbIsCrafting ? Visibility.Hidden : Visibility.Visible;
            button_DeleteRecipe.Visibility = lbIsCrafting ? Visibility.Hidden : Visibility.Visible;
            button_Confirm.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            button_Cancel.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            comboBox_Research.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            comboBox_Scan.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            button_Cancel.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            button_Confirm.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            button_AddResearch.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            button_RemoveResearch.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            button_DeleteResearch.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            button_AddScan.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            button_RemoveScan.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            button_AddRecipe.Visibility = lbIsCrafting ? Visibility.Hidden : Visibility.Visible;
            button_EditRecipe.Visibility = lbIsCrafting ? Visibility.Hidden : Visibility.Visible;
            button_DeleteCost.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            button_AddCost.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;

            comboBox_CraftCostAmount.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            comboBox_CraftCostItem.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;


            comboBox_CraftedAmount.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            //textBox_CraftTime.Visibility = Visibility.Visible; // lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            textBox_Desc.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            textBox_Hint.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            comboBox_ResearchCost.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            comboBox_CraftedKey.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            comboBox_Tier.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            
            // This was hard coded to the false statement only it appears, not sure why.
            if (!lbIsCrafting) {
                textBox_Key.Visibility = Visibility.Hidden; //We dont do this automaticly, different depending wether its an override or not!
                comboBox_Key.Visibility = Visibility.Hidden;
            }
        }

        private void RefreshItemsLists() //Function refreshes the main list.
        {
            listBox_Recipes.SelectedItem = null;
            listBox_Recipes.Items.Clear();
            for (int i = 0; i < ModWriterDataHolder.ManufacturerEntries.Count; i++)
            {
                //By Default we only show ManufacturerRecipes
                if (ModWriterDataHolder.ManufacturerEntries[i].CanCraftAnywhere == mbIsSelfCraft)
                {
                    if (comboBox_Category.SelectedItem != null)
                    {
                        if (ModWriterDataHolder.ManufacturerEntries[i].Category == comboBox_Category.SelectedItem.ToString())
                        {
                            listBox_Recipes.Items.Add(ModWriterDataHolder.ManufacturerEntries[i].CraftedKey);
                        }
                    }
                }
            }

            comboBox_Key.Items.Clear();
            for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
            {
                if (DataHolder.ManufacturerEntries[i].CanCraftAnywhere == mbIsSelfCraft)
                {
                    if (comboBox_Category.SelectedItem != null)
                    {
                        if (DataHolder.ManufacturerEntries[i].Category == comboBox_Category.SelectedItem.ToString())
                        {
                            comboBox_Key.Items.Add(DataHolder.ManufacturerEntries[i].Key);
                        }
                    }
                }
            }
        }

        private void checkBox_IsSelfCraft_Checked(object sender, RoutedEventArgs e)
        {
            mbIsSelfCraft = true;
            RefreshItemsLists();
        }

        private void checkBox_IsSelfCraft_Unchecked(object sender, RoutedEventArgs e)
        {
            mbIsSelfCraft = false;
            RefreshItemsLists();
        }

        private void comboBox_Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshItemsLists();
        }

        private void button_EditRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRecipe != null)
            {
                ActiveRecipe = SelectedRecipe;
                mbEditing = true;
                EditMode(true);
                bool lIsOverride;
                bool.TryParse(ActiveRecipe.IsOverride, out lIsOverride);
                mbIsOverride = lIsOverride;
                checkBox_IsOverride.IsEnabled = false;
                checkBox_IsOverride.Visibility = Visibility.Hidden;
                if (mbIsOverride)
                {
                    checkBox_Delete.Visibility = Visibility.Visible;
                    checkBox_Delete.IsEnabled = false;
                }
                else
                {
                    checkBox_Delete.Visibility = Visibility.Hidden;
                    checkBox_Delete.IsEnabled = false;
                }
                textBlock_Key.Visibility = Visibility.Visible;
                comboBox_CraftedKey.Visibility = Visibility.Hidden;
                textBlock_CraftedKey.Visibility = Visibility.Visible;
                ActiveRecipe = SelectedRecipe;
                //Set the textboxes to point at the active items values, as well as comboboxes!
                textBox_Key.Text = ActiveRecipe.Key;
                comboBox_Category.SelectedItem = ActiveRecipe.Category;
                comboBox_Tier.SelectedItem = (int)ActiveRecipe.Tier;
                textBlock_CraftedKey.Text = ActiveRecipe.CraftedKey;
                comboBox_CraftedAmount.SelectedItem = ActiveRecipe.CraftedAmount;
                listBox_CraftCost.Items.Clear();
                for (int i = 0; i < ActiveRecipe.Costs.Count; i++)
                {
                    listBox_CraftCost.Items.Add(ActiveRecipe.Costs[i].ToString());
                }
                comboBox_ResearchCost.SelectedItem = ActiveRecipe.ResearchCost;
                textBox_Desc.Text = ActiveRecipe.Description;
                textBox_Hint.Text = ActiveRecipe.Hint;
            }
            else
            {
                return;
            }
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            mbEditing = false;
            EditMode(false);
        }

        private void listBox_Recipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_Recipes.SelectedItem != null)
            {
                for (int i = 0; i < ModWriterDataHolder.ManufacturerEntries.Count; i++)
                {
                    if (ModWriterDataHolder.ManufacturerEntries[i].CraftedKey == listBox_Recipes.SelectedItem.ToString())
                    {
                        SelectedRecipe = ModWriterDataHolder.ManufacturerEntries[i];
                    }
                }
                bool lIsOverride;
                bool.TryParse(SelectedRecipe.IsOverride, out lIsOverride);
                mbIsOverride = lIsOverride;

                bool lDelete;
                bool.TryParse(SelectedRecipe.Delete, out lDelete);
                checkBox_Delete.IsChecked = lDelete;

                textBlock_IsOverride.Text = SelectedRecipe.IsOverride;

                textBlock_Key.Text = SelectedRecipe.Key;
                textBlock_Tier.Text = SelectedRecipe.Tier.ToString();
                textBlock_CraftedKey.Text = SelectedRecipe.CraftedKey;
                textBlock_CraftedAmount.Text = SelectedRecipe.CraftedAmount.ToString();
                textBlock_CraftTime.Text = SelectedRecipe.CraftTime.ToString();
                textBlock_ResearchCost.Text = SelectedRecipe.ResearchCost.ToString();
                textBlock_Desc.Text = SelectedRecipe.Description;
                textBlock_Hint.Text = SelectedRecipe.Hint;

                bool lIsDelete = false;
                bool.TryParse(SelectedRecipe.Delete, out lIsDelete);
                checkBox_Delete.IsChecked = lIsDelete;

                listBox_CraftCost.Items.Clear();
                for (int i = 0; i < SelectedRecipe.Costs.Count; i++)
                {
                    listBox_CraftCost.Items.Add(SelectedRecipe.Costs[i].ToString());
                }

                listBox_RemoveResearchReq.Items.Clear();
                if (SelectedRecipe.RemoveResearchRequirements != null)
                {
                    for (int i = 0; i < SelectedRecipe.RemoveResearchRequirements.Count; i++)
                    {
                        listBox_RemoveResearchReq.Items.Add(SelectedRecipe.RemoveResearchRequirements[i]);
                    }
                }

                listBox_RemoveScanReq.Items.Clear();
                if (SelectedRecipe.RemoveScanRequirements != null)
                {
                    for (int i = 0; i < SelectedRecipe.RemoveScanRequirements.Count; i++)
                    {
                        listBox_RemoveScanReq.Items.Add(SelectedRecipe.RemoveScanRequirements[i]);
                    }
                }

                listBox_ResearchReq.Items.Clear();
                if (SelectedRecipe.ResearchRequirements != null)
                {
                    for (int i = 0; i < SelectedRecipe.ResearchRequirements.Count; i++)
                    {
                        listBox_ResearchReq.Items.Add(SelectedRecipe.ResearchRequirements[i]);
                    }
                }

                listBox_ScanReq.Items.Clear();
                if (SelectedRecipe.ScanRequirements != null)
                {
                    for (int i = 0; i < SelectedRecipe.ScanRequirements.Count; i++)
                    {
                        listBox_ScanReq.Items.Add(SelectedRecipe.ScanRequirements[i]);
                    }
                }
                if (mbIsOverride == true)
                {
                    checkBox_Delete.Visibility = Visibility.Visible;
                }
                else
                {
                    checkBox_Delete.Visibility = Visibility.Hidden;
                }
            }
        }

        private void button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (mbEditing) //Editing Already existing item, we override Active Item:
            {
                if (checkBox_Delete.IsChecked.Value)
                {
                    ActiveRecipe.Delete = checkBox_Delete.IsChecked.Value.ToString();
                }
                else
                {
                    ActiveRecipe.Delete = null;
                }
                if (comboBox_Category.SelectedItem != null)
                {
                    ActiveRecipe.Category = comboBox_Category.SelectedItem.ToString();
                }
                if (comboBox_Tier.SelectedItem != null)
                {
                    ushort lTier = 0;
                    ushort.TryParse(comboBox_Tier.SelectedItem.ToString(), out lTier);
                    ActiveRecipe.Tier = lTier;
                }
                if (comboBox_CraftedAmount.SelectedItem != null)
                {
                    int lCraftedAmount = 1;
                    int.TryParse(comboBox_CraftedAmount.SelectedItem.ToString(), out lCraftedAmount);
                    ActiveRecipe.CraftedAmount = lCraftedAmount;
                }
                if (comboBox_ResearchCost.SelectedItem != null)
                {
                    int lResearchCost = 0;
                    int.TryParse(comboBox_ResearchCost.SelectedItem.ToString(), out lResearchCost);
                    ActiveRecipe.ResearchCost = lResearchCost;
                }
                ActiveRecipe.Description = textBox_Desc.Text;
                ActiveRecipe.Hint = textBox_Hint.Text;
                ActiveRecipe.CanCraftAnywhere = checkBox_IsSelfCraft.IsChecked.Value;


                //for (int i = 0; i < ModWriterDataHolder.ManufacturerEntries.Count; i++)
                //{
                //    if (ModWriterDataHolder.ManufacturerEntries[i].Key == ActiveRecipe.Key)
                //    {
                //        ModWriterDataHolder.ManufacturerEntries[i] = ActiveRecipe;
                //    }
                //}


                EditMode(false);
                RefreshItemsLists();
            }
            else //we create new values and add to the list!
            {
                CraftData lNewRecipe = new CraftData();
                if (mbIsOverride)
                {
                    lNewRecipe.IsOverride = mbIsOverride.ToString();
                }
                else
                {
                    lNewRecipe.IsOverride = null;
                }
                if (mbIsOverride == true)
                {
                    if (comboBox_Key.SelectedItem != null)
                    {
                        lNewRecipe.Key = comboBox_Key.SelectedItem.ToString();
                        lNewRecipe.RemoveResearchRequirements = TempRemoveResearchReq;
                        lNewRecipe.RemoveScanRequirements = TempRemoveScanReq;
                        bool? lIsDelete = checkBox_Delete.IsChecked;
                        lNewRecipe.Delete = lIsDelete.Value.ToString();
                    }
                }
                else
                {
                    lNewRecipe.Key = User.Default.AuthorID + "." + textBox_Key.Text;
                }

                lNewRecipe.Category = comboBox_Category.SelectedItem.ToString();

                ushort lTier = 0;
                ushort.TryParse(comboBox_Tier.SelectedItem.ToString(), out lTier);
                lNewRecipe.Tier = lTier;
                if (mbIsOverride)
                {
                    lNewRecipe.CraftedKey = textBlock_CraftedKey.Text;
                }
                else
                {
                    if (comboBox_CraftedKey.SelectedItem != null)
                    {
                        lNewRecipe.CraftedKey = comboBox_CraftedKey.SelectedItem.ToString();
                    }
                }

                int lCraftedAmount = 1;
                int.TryParse(comboBox_CraftedAmount.SelectedItem.ToString(), out lCraftedAmount);
                lNewRecipe.CraftedAmount = lCraftedAmount;

                lNewRecipe.Costs = TempCraftCost;

                lNewRecipe.ResearchRequirements = TempResearchReq;
                lNewRecipe.ScanRequirements = TempScanReq;

                int lResearchCost = 0;
                int.TryParse(comboBox_ResearchCost.SelectedItem.ToString(), out lResearchCost);
                lNewRecipe.ResearchCost = lResearchCost;

                lNewRecipe.Description = textBox_Desc.Text;
                lNewRecipe.Hint = textBox_Hint.Text;

                lNewRecipe.CanCraftAnywhere = mbIsSelfCraft;
                //*****************
                ModWriterDataHolder.ManufacturerEntries.Add(lNewRecipe);
                RefreshItemsLists();
                EditMode(false);
            }
        }

        private void button_AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            EditMode(true);
            mbIsOverride = false;
            mbEditing = false;
            checkBox_IsOverride.IsChecked = mbIsOverride;
            checkBox_Delete.Visibility = Visibility.Hidden;
            checkBox_Delete.IsChecked = false;
            textBox_Key.Visibility = Visibility.Visible;
            textBlock_IsOverride.Visibility = Visibility.Hidden;

            TempCraftCost = new List<CraftCost>();
            TempResearchReq = new List<string>();
            TempScanReq = new List<string>();
            TempRemoveResearchReq = new List<string>();
            TempRemoveScanReq = new List<string>();

            textBlock_IsOverride.Text = "";
            textBlock_Key.Text = "";
            textBlock_Tier.Text = "";
            textBlock_CraftedKey.Text = "";
            textBlock_CraftedAmount.Text = "";
            textBlock_CraftTime.Text = "";
            textBlock_ResearchCost.Text = "";
            textBlock_Desc.Text = "";
            textBlock_Hint.Text = "";
            
            textBox_Key.Text = "";
            textBox_CraftTime.Text = "";
            textBox_Desc.Text = "";
            textBox_Hint.Text = "";

            comboBox_ResearchCost.SelectedItem = 0;
            comboBox_CraftCostAmount.SelectedItem = 1;
            comboBox_CraftedAmount.SelectedItem = 1;
            comboBox_Tier.SelectedItem = 0;

            comboBox_Scan.SelectedItem = null;
            comboBox_Research.SelectedItem = null;
            comboBox_CraftCostItem.SelectedItem = null;
            comboBox_CraftCostAmount.SelectedItem = 1;
            comboBox_CraftedKey.SelectedItem = null;

            listBox_CraftCost.Items.Clear();
            listBox_RemoveResearchReq.Items.Clear();
            listBox_RemoveScanReq.Items.Clear();
            listBox_ResearchReq.Items.Clear();
            listBox_ScanReq.Items.Clear();
        }

        private void checkBox_IsOverride_Checked(object sender, RoutedEventArgs e)
        {
            mbIsOverride = true;
            if (mbEditing == false)
            {
                comboBox_Key.Visibility = Visibility.Visible;
                textBox_Key.Visibility = Visibility.Hidden;
                checkBox_Delete.Visibility = Visibility.Visible;
                comboBox_CraftedKey.Visibility = Visibility.Hidden;
                textBlock_CraftedKey.Visibility = Visibility.Visible;
            }
        }

        private void checkBox_IsOverride_Unchecked(object sender, RoutedEventArgs e)
        {
            mbIsOverride = false;
            textBox_Key.Text = string.Empty;
            if (mbEditing == false)
            {
                comboBox_Key.Visibility = Visibility.Hidden;
                textBox_Key.Visibility = Visibility.Visible;
                checkBox_Delete.Visibility = Visibility.Hidden;
                comboBox_CraftedKey.Visibility = Visibility.Visible;
                textBlock_CraftedKey.Visibility = Visibility.Hidden;
            }
        }

        private void comboBox_Key_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //New Item: Override
            if (comboBox_Key.SelectedItem != null)
            {
                for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
                {
                    if (comboBox_Key.SelectedItem.ToString() == DataHolder.ManufacturerEntries[i].Key)
                    {
                        textBox_Key.Text = DataHolder.ManufacturerEntries[i].Key;
                        comboBox_Category.SelectedItem = DataHolder.ManufacturerEntries[i].Category;
                        comboBox_Tier.SelectedItem = (int)DataHolder.ManufacturerEntries[i].Tier;
                        //comboBox_CraftedKey.Items.Add(DataHolder.ManufacturerEntries[i].CraftedKey);
                        //comboBox_CraftedKey.SelectedItem = DataHolder.ManufacturerEntries[i].CraftedKey;
                        textBlock_CraftedKey.Text = DataHolder.ManufacturerEntries[i].CraftedKey;
                        comboBox_CraftedAmount.SelectedItem = DataHolder.ManufacturerEntries[i].CraftedAmount;
                        TempCraftCost = new List<CraftCost>();
                        for (int j = 0; j < DataHolder.ManufacturerEntries[i].Costs.Count; j++)
                        {
                            CraftCost Holder = new CraftCost();
                            Holder.Amount = DataHolder.ManufacturerEntries[i].Costs[j].Amount;
                            Holder.Key = DataHolder.ManufacturerEntries[i].Costs[j].Key;
                            if (mbIsOverride)
                            {
                                Holder.Delete = "false";
                            }
                            TempCraftCost.Add(Holder);
                        }
                        comboBox_ResearchCost.SelectedItem = DataHolder.ManufacturerEntries[i].ResearchCost;
                        textBox_Desc.Text = DataHolder.ManufacturerEntries[i].Description;
                        textBox_Hint.Text = DataHolder.ManufacturerEntries[i].Hint;

                        listBox_CraftCost.Items.Clear();
                        for (int j = 0; j < TempCraftCost.Count; j++)
                        {
                            listBox_CraftCost.Items.Add(TempCraftCost[j].ToString());
                        }

                        TempResearchReq = DataHolder.ManufacturerEntries[i].ResearchRequirements;
                        TempRemoveResearchReq = new List<string>();
                        TempScanReq = DataHolder.ManufacturerEntries[i].ScanRequirements;
                        TempRemoveScanReq = new List<string>();
                    }
                }

                listBox_CraftCost.Items.Clear();
                for (int i = 0; i < TempCraftCost.Count; i++)
                {
                    listBox_CraftCost.Items.Add(TempCraftCost[i].ToString());
                }

                listBox_ScanReq.Items.Clear();
                for (int i = 0; i < TempScanReq.Count; i++)
                {
                    listBox_ScanReq.Items.Add(TempScanReq[i]);
                }
                listBox_ResearchReq.Items.Clear();
                for (int i = 0; i < TempResearchReq.Count; i++)
                {
                    listBox_ResearchReq.Items.Add(TempResearchReq[i]);
                }
                listBox_RemoveScanReq.Items.Clear();
                for (int i = 0; i < TempRemoveScanReq.Count; i++)
                {
                    listBox_RemoveScanReq.Items.Add(TempRemoveScanReq[i]);
                }
                listBox_RemoveResearchReq.Items.Clear();
                for (int i = 0; i < TempRemoveResearchReq.Count; i++)
                {
                    listBox_RemoveResearchReq.Items.Add(TempRemoveResearchReq[i]);
                }
                //When we select a key, this happens! (For creating new items using Override!) We're going to change textboxes to the active items values
            }
        }

        private void button_DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_Recipes.SelectedItem != null)
            {
                for (int i = 0; i < ModWriterDataHolder.ManufacturerEntries.Count; i++)
                {
                    if (ModWriterDataHolder.ManufacturerEntries[i].CraftedKey == listBox_Recipes.SelectedItem.ToString())
                    {
                        ModWriterDataHolder.ManufacturerEntries.RemoveAt(i);
                        Refresh();
                        return;
                    }
                }
            }
        }

        private void button_DeleteCost_Click(object sender, RoutedEventArgs e)
        {
            if (mbEditing)//If we're editing an item:
            {
                if (mbIsOverride) //On override we just change it to be "Delete = "true" / "false"" instead of deleting the CraftCost.
                {
                    if (listBox_CraftCost.SelectedItem != null)
                    {
                        for (int i = 0; i < ActiveRecipe.Costs.Count; i++)
                        {
                            if (ActiveRecipe.Costs[i].ToString() == listBox_CraftCost.SelectedItem.ToString())
                            {
                                if (ActiveRecipe.Costs[i].Delete == "true")
                                {
                                    ActiveRecipe.Costs[i].Delete = "false";
                                }
                                else if (ActiveRecipe.Costs[i].Delete == null)
                                {
                                    ActiveRecipe.Costs.RemoveAt(i);
                                }
                                else
                                {
                                    ActiveRecipe.Costs[i].Delete = "true";
                                }
                            }
                        }
                    }
                }
                else //So if its not an override, we just delete the entry.
                {
                    for (int i = 0; i < ActiveRecipe.Costs.Count; i++)
                    {
                        if (ActiveRecipe.Costs[i].ToString() == listBox_CraftCost.SelectedItem.ToString())
                        {
                            ActiveRecipe.Costs.RemoveAt(i);
                        }
                    }
                }
                listBox_CraftCost.Items.Clear();
                for (int i = 0; i < ActiveRecipe.Costs.Count; i++)
                {
                    listBox_CraftCost.Items.Add(ActiveRecipe.Costs[i].ToString());
                }
            }
            else //New items:
            {
                if (mbIsOverride)
                {
                    if (listBox_CraftCost.SelectedItem != null)
                    {
                        for (int i = 0; i < TempCraftCost.Count; i++)
                        {
                            if (TempCraftCost[i].ToString() == listBox_CraftCost.SelectedItem.ToString())
                            {
                                if (TempCraftCost[i].Delete == "true")
                                {
                                    TempCraftCost[i].Delete = "false";
                                }
                                else
                                {
                                    TempCraftCost[i].Delete = "true";
                                }
                            }
                        }
                        listBox_CraftCost.Items.Clear();
                        for (int i = 0; i < TempCraftCost.Count; i++)
                        {
                            listBox_CraftCost.Items.Add(TempCraftCost[i].ToString());
                        }
                    }
                }
                else
                {
                    if (listBox_CraftCost.SelectedItem != null)
                    {
                        for (int i = 0; i < TempCraftCost.Count; i++)
                        {
                            if (TempCraftCost[i].ToString() == listBox_CraftCost.SelectedItem.ToString())
                            {
                                TempCraftCost.RemoveAt(i);
                            }
                        }
                        listBox_CraftCost.Items.Clear();
                        for (int i = 0; i < TempCraftCost.Count; i++)
                        {
                            listBox_CraftCost.Items.Add(TempCraftCost[i].ToString());
                        }
                    }
                }
            }
        }

        private void button_AddCost_Click(object sender, RoutedEventArgs e)
        {
            if (mbEditing)
            {
                if (comboBox_CraftCostItem.SelectedItem != null && comboBox_CraftCostAmount.SelectedItem != null)
                {
                    CraftCost lNewCost = new CraftCost();
                    lNewCost.Key = comboBox_CraftCostItem.SelectedItem.ToString();
                    uint lAmount = 1;
                    UInt32.TryParse(comboBox_CraftCostAmount.SelectedItem.ToString(), out lAmount);
                    lNewCost.Amount = lAmount;
                    lNewCost.IsNew = true;
                    ActiveRecipe.Costs.Add(lNewCost);
                    listBox_CraftCost.Items.Clear();
                    for (int i = 0; i < ActiveRecipe.Costs.Count; i++)
                    {
                        listBox_CraftCost.Items.Add(ActiveRecipe.Costs[i].ToString());
                    }
                }
            }
            else
            {
                CraftCost lNewCost = new CraftCost();
                if (comboBox_CraftCostItem.SelectedItem != null && comboBox_CraftCostAmount.SelectedItem != null)
                {
                    lNewCost.Key = comboBox_CraftCostItem.SelectedItem.ToString();
                    uint lAmount = 1;
                    UInt32.TryParse(comboBox_CraftCostAmount.SelectedItem.ToString(), out lAmount);
                    lNewCost.Amount = lAmount;
                    TempCraftCost.Add(lNewCost);
                }
                listBox_CraftCost.Items.Clear();
                for (int i = 0; i < TempCraftCost.Count; i++)
                {
                    listBox_CraftCost.Items.Add(TempCraftCost[i].ToString());
                }
            }
        }

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Writing to disk!");
            string AllRecipes = XMLSerializer.Serialize(ModWriterDataHolder.ManufacturerEntries, false);
            File.WriteAllText(ManufacturerXmlPath, AllRecipes);
        }

        //Research and Scan reqs + removal part
        private void RefreshRequirementsLists()
        {
            if (mbEditing)
            {
                listBox_RemoveResearchReq.Items.Clear();
                listBox_RemoveScanReq.Items.Clear();
                listBox_ResearchReq.Items.Clear();
                listBox_ScanReq.Items.Clear();
                if (ActiveRecipe.RemoveResearchRequirements != null)
                {
                    for (int i = 0; i < ActiveRecipe.RemoveResearchRequirements.Count; i++)
                    {
                        listBox_RemoveResearchReq.Items.Add(ActiveRecipe.RemoveResearchRequirements[i]);
                    }
                }
                if (ActiveRecipe.RemoveScanRequirements != null)
                {
                    for (int i = 0; i < ActiveRecipe.RemoveScanRequirements.Count; i++)
                    {
                        listBox_RemoveScanReq.Items.Add(ActiveRecipe.RemoveScanRequirements[i]);
                    }
                }
                if (ActiveRecipe.ResearchRequirements != null)
                {
                    for (int i = 0; i < ActiveRecipe.ResearchRequirements.Count; i++)
                    {
                        listBox_ResearchReq.Items.Add(ActiveRecipe.ResearchRequirements[i]);
                    }
                }
                if (ActiveRecipe.ScanRequirements != null)
                {
                    for (int i = 0; i < ActiveRecipe.ScanRequirements.Count; i++)
                    {
                        listBox_ScanReq.Items.Add(ActiveRecipe.ScanRequirements[i]);
                    }
                }
            }
            else
            {
                listBox_RemoveResearchReq.Items.Clear();
                listBox_RemoveScanReq.Items.Clear();
                listBox_ResearchReq.Items.Clear();
                listBox_ScanReq.Items.Clear();
                if (TempRemoveResearchReq != null)
                {
                    for (int i = 0; i < TempRemoveResearchReq.Count; i++)
                    {
                        listBox_RemoveResearchReq.Items.Add(TempRemoveResearchReq[i]);
                    }
                }
                if (TempRemoveScanReq != null)
                {
                    for (int i = 0; i < TempRemoveScanReq.Count; i++)
                    {
                        listBox_RemoveScanReq.Items.Add(TempRemoveScanReq[i]);
                    }
                }
                if (TempResearchReq != null)
                {
                    for (int i = 0; i < TempResearchReq.Count; i++)
                    {
                        listBox_ResearchReq.Items.Add(TempResearchReq[i]);
                    }
                }
                if (TempScanReq != null)
                {
                    for (int i = 0; i < TempScanReq.Count; i++)
                    {
                        listBox_ScanReq.Items.Add(TempScanReq[i]);
                    }
                }
            }
        }

        private void button_AddResearch_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_Research.SelectedItem != null)
            {
                if (mbEditing)
                {
                    ActiveRecipe.ResearchRequirements.Add(comboBox_Research.SelectedItem.ToString());
                }
                else
                {
                    TempResearchReq.Add(comboBox_Research.SelectedItem.ToString());
                }
                RefreshRequirementsLists();
            }
        }

        private void button_AddScan_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_Scan.SelectedItem != null)
            {
                if (mbEditing)
                {
                    ActiveRecipe.ScanRequirements.Add(comboBox_Scan.SelectedItem.ToString());
                }
                else
                {
                    TempScanReq.Add(comboBox_Scan.SelectedItem.ToString());
                }
                RefreshRequirementsLists();
            }
        }

        //We call this when we remove a ResearchReq
        private void button_RemoveResearch_Click(object sender, RoutedEventArgs e)
        {
            //If we have something selected in the ResearchReq List
            if (listBox_ResearchReq.SelectedItem != null)
            {
                //We're working with the Listbox for ResearchReq
                if (mbEditing) //Existing Item
                {
                    if (mbIsOverride) //Item is an Override of existing Recipe
                    {
                        ActiveRecipe.RemoveResearchRequirements.Add(listBox_ResearchReq.SelectedItem.ToString());
                        ActiveRecipe.ResearchRequirements.Remove(listBox_ResearchReq.SelectedItem.ToString());
                    }
                    else //Is a selfmade item (Should not need RemoveResearchReq
                    {
                        ActiveRecipe.ResearchRequirements.Remove(listBox_ResearchReq.SelectedItem.ToString());
                    }
                }
                else //New Item
                {
                    if (mbIsOverride) //New Item that is an override
                    {
                        TempRemoveResearchReq.Add(listBox_ResearchReq.SelectedItem.ToString());
                        TempResearchReq.Remove(listBox_ResearchReq.SelectedItem.ToString());
                    }
                    else //New item, shouldn't add  
                    {
                        TempResearchReq.Remove(listBox_ResearchReq.SelectedItem.ToString());
                    }
                }
            }

            //If we want to remove something from the RemoveResearchReq list:
            if (listBox_RemoveResearchReq.SelectedItem != null)
            {
                //Here we're working with the RemoveResearchReq
                if (mbEditing) //Existing Item
                {
                    if (mbIsOverride) //Item is an Override of existing Recipe
                    {
                        ActiveRecipe.ResearchRequirements.Add(listBox_RemoveResearchReq.SelectedItem.ToString());
                        ActiveRecipe.RemoveResearchRequirements.Remove(listBox_RemoveResearchReq.SelectedItem.ToString());
                    }
                    else //Is a selfmade item (Should not need RemoveResearchReq
                    {
                        ActiveRecipe.RemoveResearchRequirements.Remove(listBox_RemoveResearchReq.SelectedItem.ToString());
                    }
                }
                else //New Item
                {
                    if (mbIsOverride) //New Item that is an override
                    {
                        TempResearchReq.Add(listBox_RemoveResearchReq.SelectedItem.ToString());
                        TempRemoveResearchReq.Remove(listBox_RemoveResearchReq.SelectedItem.ToString());
                    }
                    else // This should never active?!
                    {
                        TempRemoveResearchReq.Remove(listBox_RemoveResearchReq.SelectedItem.ToString());
                    }
                }
            }
            RefreshRequirementsLists();
        }

        private void button_RemoveScan_Clicked(object sender, RoutedEventArgs e)
        {
            if (listBox_ScanReq.SelectedItem != null)
            {
                if (mbEditing)//Existing Item
                {
                    if (mbIsOverride) //Item is an Override of existing Recipe
                    {
                        ActiveRecipe.RemoveScanRequirements.Add(listBox_ScanReq.SelectedItem.ToString());
                        ActiveRecipe.ScanRequirements.Remove(listBox_ScanReq.SelectedItem.ToString());
                    }
                    else //Is a selfmade item (Should not need RemoveResearchReq
                    {
                        ActiveRecipe.ScanRequirements.Remove(listBox_ScanReq.SelectedItem.ToString());
                    }
                }
                else // New item
                {
                    if (mbIsOverride) //New Item that is an override
                    {
                        TempRemoveScanReq.Add(listBox_ScanReq.SelectedItem.ToString());
                        TempScanReq.Remove(listBox_ScanReq.SelectedItem.ToString());
                    }
                    else
                    {
                        TempScanReq.Remove(listBox_ScanReq.SelectedItem.ToString());
                    }
                }
            }
            //RemoveReq -> Req
            if (listBox_RemoveScanReq.SelectedItem != null)
            {
                if (mbEditing)
                {
                    if (mbIsOverride)
                    {
                        ActiveRecipe.ScanRequirements.Add(listBox_RemoveScanReq.SelectedItem.ToString());
                        ActiveRecipe.RemoveScanRequirements.Remove(listBox_RemoveScanReq.SelectedItem.ToString());
                    }
                    else
                    {
                        ActiveRecipe.RemoveScanRequirements.Remove(listBox_RemoveScanReq.SelectedItem.ToString());
                    }
                }
                else
                {
                    if (mbIsOverride)
                    {
                        TempScanReq.Add(listBox_RemoveResearchReq.SelectedItem.ToString());
                        TempRemoveScanReq.Remove(listBox_RemoveResearchReq.SelectedItem.ToString());
                    }
                    else
                    {
                        TempRemoveScanReq.Remove(listBox_RemoveResearchReq.SelectedItem.ToString());
                    }
                }
            }
            RefreshRequirementsLists();
        }
    }
}
