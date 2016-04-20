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
            textBox_CraftedAmount.Text = "1";
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
            comboBox_CraftedKey.Items.Clear();
            for (int i = 0; i < DataHolder.ItemEntries.Count; i++)
            {
                comboBox_CraftedKey.Items.Add(DataHolder.ItemEntries[i].Key);
            }
            for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
            {
                comboBox_CraftedKey.Items.Add(ModWriterDataHolder.Items[i].Key);
            }
            for (int i = 0; i < DataHolder.TerrainDataEntries.Count; i++)
            {
                if (DataHolder.TerrainDataEntries[i].Values != null)
                {
                    for (int j = 0; j < DataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        comboBox_CraftedKey.Items.Add(DataHolder.TerrainDataEntries[i].Values[j].Key);
                    }
                }
                else
                {
                    comboBox_CraftedKey.Items.Add(DataHolder.TerrainDataEntries[i].Key);
                }
            }
            for (int i = 0; i < ModWriterDataHolder.TerrainDataEntries.Count; i++)
            {
                if (ModWriterDataHolder.TerrainDataEntries[i].Values != null)
                {
                    for (int j = 0; j < ModWriterDataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        comboBox_CraftedKey.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Values[j].Key);
                    }
                }
                else
                {
                    comboBox_CraftedKey.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Key);
                }
            }


            Refresh();
            EditMode(false);


            //Special Things, because we dont use these at all?? 

            textBlock_CraftTime.Visibility = Visibility.Hidden;
            label_CraftTime.Visibility = Visibility.Hidden;
            textBox_CraftTime.Visibility = Visibility.Hidden;
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
        }

        private void EditMode(bool lbIsCrafting) //This function Controls VIS and interaction of the form
        {
            if (lbIsCrafting) //If we're editing an item:
            {
                listBox_Recipes.IsEnabled = false;
                checkBox_Delete.IsEnabled = true;
                checkBox_IsSelfCraft.IsEnabled = true;
                listBox_CraftCost.IsEnabled = true;
                listBox_ResearchReq.IsEnabled = true;
                listBox_RemoveResearchReq.IsEnabled = true;
                listBox_ScanReq.IsEnabled = true;
                listBox_RemoveScanReq.IsEnabled = true;
                checkBox_IsOverride.IsEnabled = true;
                textBlock_Key.Visibility = Visibility.Hidden;
                textBlock_Tier.Visibility = Visibility.Hidden;
                textBlock_CraftedKey.Visibility = Visibility.Hidden;
                textBlock_CraftedAmount.Visibility = Visibility.Hidden;
                //textBlock_CraftTime.Visibility = Visibility.Hidden;
                textBlock_ResearchCost.Visibility = Visibility.Hidden;
                textBlock_Desc.Visibility = Visibility.Hidden;
                textBlock_Hint.Visibility = Visibility.Hidden;
                button_Save.Visibility = Visibility.Hidden;
                button_DeleteRecipe.Visibility = Visibility.Hidden;
                button_Confirm.Visibility = Visibility.Visible;
                button_Cancel.Visibility = Visibility.Visible;
                comboBox_Research.Visibility = Visibility.Visible;
                comboBox_Scan.Visibility = Visibility.Visible;
                button_Cancel.Visibility = Visibility.Visible;
                button_Confirm.Visibility = Visibility.Visible;
                button_AddResearch.Visibility = Visibility.Visible;
                button_RemoveResearch.Visibility = Visibility.Visible;
                button_DeleteResearch.Visibility = Visibility.Visible;
                button_AddScan.Visibility = Visibility.Visible;
                button_RemoveScan.Visibility = Visibility.Visible;
                button_AddRecipe.Visibility = Visibility.Hidden;
                button_EditRecipe.Visibility = Visibility.Hidden;
                button_DeleteCost.Visibility = Visibility.Visible;
                button_AddCost.Visibility = Visibility.Visible;
                

                textBox_CraftedAmount.Visibility = Visibility.Visible;
                //textBox_CraftTime.Visibility = Visibility.Visible;
                textBox_Desc.Visibility = Visibility.Visible;
                textBox_Hint.Visibility = Visibility.Visible;
                comboBox_ResearchCost.Visibility = Visibility.Visible;
                comboBox_CraftedKey.Visibility = Visibility.Visible;
                comboBox_Tier.Visibility = Visibility.Visible;
            }
            else //If not editing item:
            {
                listBox_Recipes.IsEnabled = true;
                checkBox_Delete.IsEnabled = false;
                checkBox_IsSelfCraft.IsEnabled = true;
                listBox_CraftCost.IsEnabled = false;
                listBox_ResearchReq.IsEnabled = false;
                listBox_RemoveResearchReq.IsEnabled = false;
                listBox_ScanReq.IsEnabled = false;
                listBox_RemoveScanReq.IsEnabled = false;
                checkBox_IsOverride.IsEnabled = false;
                textBlock_Key.Visibility = Visibility.Visible;
                textBlock_Tier.Visibility = Visibility.Visible;
                textBlock_CraftedKey.Visibility = Visibility.Visible;
                textBlock_CraftedAmount.Visibility = Visibility.Visible;
                //textBlock_CraftTime.Visibility = Visibility.Visible;
                textBlock_ResearchCost.Visibility = Visibility.Visible;
                textBlock_Desc.Visibility = Visibility.Visible;
                textBlock_Hint.Visibility = Visibility.Visible;
                button_AddRecipe.Visibility = Visibility.Visible;
                button_EditRecipe.Visibility = Visibility.Visible;
                button_Save.Visibility = Visibility.Visible;
                button_DeleteRecipe.Visibility = Visibility.Visible;
                button_Confirm.Visibility = Visibility.Hidden;
                button_Cancel.Visibility = Visibility.Hidden;
                comboBox_Research.Visibility = Visibility.Hidden;
                comboBox_Scan.Visibility = Visibility.Hidden;
                button_Cancel.Visibility = Visibility.Hidden;
                button_Confirm.Visibility = Visibility.Hidden;
                button_AddResearch.Visibility = Visibility.Hidden;
                button_RemoveResearch.Visibility = Visibility.Hidden;
                button_DeleteResearch.Visibility = Visibility.Hidden;
                button_AddScan.Visibility = Visibility.Hidden;
                button_RemoveScan.Visibility = Visibility.Hidden;
                button_AddRecipe.Visibility = Visibility.Visible;
                button_EditRecipe.Visibility = Visibility.Visible;
                button_DeleteCost.Visibility = Visibility.Hidden;
                button_AddCost.Visibility = Visibility.Hidden;
                
                textBox_Key.Visibility = Visibility.Hidden; //We dont do this automaticly, different depending wither its an override or not!

                textBox_CraftedAmount.Visibility = Visibility.Hidden;
                //textBox_CraftTime.Visibility = Visibility.Hidden;
                textBox_Desc.Visibility = Visibility.Hidden;
                textBox_Hint.Visibility = Visibility.Hidden;
                comboBox_ResearchCost.Visibility = Visibility.Hidden;
                comboBox_CraftedKey.Visibility = Visibility.Hidden;
                comboBox_Tier.Visibility = Visibility.Hidden;
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
                checkBox_IsOverride.IsEnabled = false;
                if (mbIsOverride)
                {
                    checkBox_Delete.IsEnabled = true;
                }
                else
                {
                    checkBox_Delete.IsEnabled = false;
                }
                textBlock_Key.Visibility = Visibility.Visible;
                comboBox_CraftedKey.Visibility = Visibility.Hidden;
                textBlock_CraftedKey.Visibility = Visibility.Visible;
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
                mbIsOverride = Convert.ToBoolean(SelectedRecipe.IsOverride);

                if (SelectedRecipe.IsOverride == "true")
                {
                    checkBox_IsOverride.IsChecked = true;
                }
                else
                {
                    checkBox_IsOverride.IsChecked = false;
                }

                textBlock_Key.Text = SelectedRecipe.Key;
                textBlock_Tier.Text = SelectedRecipe.Tier.ToString();
                textBlock_CraftedKey.Text = SelectedRecipe.CraftedKey;
                textBlock_CraftedAmount.Text = SelectedRecipe.CraftedAmount.ToString();
                textBlock_CraftTime.Text = SelectedRecipe.CraftTime.ToString();
                textBlock_ResearchCost.Text = SelectedRecipe.ResearchCost.ToString();
                textBlock_Desc.Text = SelectedRecipe.Description;
                textBlock_Hint.Text = SelectedRecipe.Hint;
                //This might cause issues later.
                checkBox_Delete.IsChecked = SelectedRecipe.Delete;
                //checkBox_IsSelfCraft.IsChecked = SelectedRecipe.CanCraftAnywhere;

                listBox_CraftCost.Items.Clear();
                for (int i = 0; i < SelectedRecipe.Costs.Count; i++)
                {
                    listBox_CraftCost.Items.Add(SelectedRecipe.Costs[i].Readable());
                }

                listBox_RemoveResearchReq.Items.Clear();
                for (int i = 0; i < SelectedRecipe.RemoveResearchRequirements.Count; i++)
                {
                    listBox_RemoveResearchReq.Items.Add(SelectedRecipe.RemoveResearchRequirements[i]);
                }

                listBox_RemoveScanReq.Items.Clear();
                for (int i = 0; i < SelectedRecipe.RemoveScanRequirements.Count; i++)
                {
                    listBox_RemoveScanReq.Items.Add(SelectedRecipe.RemoveScanRequirements[i]);
                }

                listBox_ResearchReq.Items.Clear();
                for (int i = 0; i < SelectedRecipe.ResearchRequirements.Count; i++)
                {
                    listBox_ResearchReq.Items.Add(SelectedRecipe.ResearchRequirements[i]);
                }

                listBox_ScanReq.Items.Clear();
                for (int i = 0; i < SelectedRecipe.ScanRequirements.Count; i++)
                {
                    listBox_ScanReq.Items.Add(SelectedRecipe.ScanRequirements[i]);
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

            }
            else //we create new values and add to the list!
            {

            }
        }

        private void button_AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            EditMode(true);
            checkBox_IsOverride.IsChecked = false;
            checkBox_Delete.Visibility = Visibility.Hidden;
            textBox_Key.Visibility = Visibility.Visible;
        }

        private void checkBox_IsOverride_Checked(object sender, RoutedEventArgs e)
        {
            mbIsOverride = true;
            if (mbEditing == false)
            {
                comboBox_Key.Visibility = Visibility.Visible;
                textBox_Key.Visibility = Visibility.Hidden;
                checkBox_Delete.Visibility = Visibility.Visible;
            }
        }

        private void checkBox_IsOverride_Unchecked(object sender, RoutedEventArgs e)
        {
            mbIsOverride = false;
            if (mbEditing == false)
            {
                comboBox_Key.Visibility = Visibility.Hidden;
                textBox_Key.Visibility = Visibility.Visible;
                checkBox_Delete.Visibility = Visibility.Hidden;
            }
        }

        private void comboBox_Key_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //When we select a key, this happens! (For creating new items using Override!) We're going to change textboxes to the active items values
        }
    }
}
