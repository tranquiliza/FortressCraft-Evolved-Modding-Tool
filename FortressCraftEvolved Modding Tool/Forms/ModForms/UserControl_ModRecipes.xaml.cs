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
            listBox_Recipes.IsEnabled = !lbIsCrafting;
            checkBox_Delete.IsEnabled = lbIsCrafting;
            checkBox_IsSelfCraft.IsEnabled = lbIsCrafting;
            listBox_CraftCost.IsEnabled = lbIsCrafting;
            listBox_ResearchReq.IsEnabled = lbIsCrafting;
            listBox_RemoveResearchReq.IsEnabled = lbIsCrafting;
            listBox_ScanReq.IsEnabled = lbIsCrafting;
            listBox_RemoveScanReq.IsEnabled = lbIsCrafting;
            checkBox_IsOverride.IsEnabled = lbIsCrafting;
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
            

            textBox_CraftedAmount.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            //textBox_CraftTime.Visibility = Visibility.Visible; // lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            textBox_Desc.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            textBox_Hint.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            comboBox_ResearchCost.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            comboBox_CraftedKey.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            comboBox_Tier.Visibility = lbIsCrafting ? Visibility.Visible : Visibility.Hidden;
            
            // This was hard coded to the false statement only it appears, not sure why.
            if (!lbIsCrafting) {
                textBox_Key.Visibility = Visibility.Hidden; //We dont do this automaticly, different depending wither its an override or not!
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
