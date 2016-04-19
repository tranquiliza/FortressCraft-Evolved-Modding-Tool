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
            textBlock_IsOverride.Text = "";
            textBlock_Key.Text = "";
            textBlock_Tier.Text = "";
            textBlock_CraftedKey.Text = "";
            textBlock_CraftedAmount.Text = "";
            textBlock_CraftTime.Text = "";
            textBlock_ResearchCost.Text = "";
            textBlock_Desc.Text = "";
            textBlock_Hint.Text = "";
            Refresh();
            EditMode(false);
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
            if (lbIsCrafting)
            {
                listBox_Recipes.IsEnabled = false;
                checkBox_Delete.IsEnabled = true;
                checkBox_IsSelfCraft.IsEnabled = true;
                checkBox_CanBeAutomated.IsEnabled = true;
                listBox_CraftCost.IsEnabled = true;
                listBox_ResearchReq.IsEnabled = true;
                listBox_RemoveResearchReq.IsEnabled = true;
                listBox_ScanReq.IsEnabled = true;
                listBox_RemoveScanReq.IsEnabled = true;
                textBlock_IsOverride.Visibility = Visibility.Hidden;
                textBlock_Key.Visibility = Visibility.Hidden;
                textBlock_Tier.Visibility = Visibility.Hidden;
                textBlock_CraftedKey.Visibility = Visibility.Hidden;
                textBlock_CraftedAmount.Visibility = Visibility.Hidden;
                textBlock_CraftTime.Visibility = Visibility.Hidden;
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
            }
            else
            {
                listBox_Recipes.IsEnabled = true;
                checkBox_Delete.IsEnabled = false;
                checkBox_IsSelfCraft.IsEnabled = true;
                checkBox_CanBeAutomated.IsEnabled = false;
                listBox_CraftCost.IsEnabled = false;
                listBox_ResearchReq.IsEnabled = false;
                listBox_RemoveResearchReq.IsEnabled = false;
                listBox_ScanReq.IsEnabled = false;
                listBox_RemoveScanReq.IsEnabled = false;
                textBlock_IsOverride.Visibility = Visibility.Visible;
                textBlock_Key.Visibility = Visibility.Visible;
                textBlock_Tier.Visibility = Visibility.Visible;
                textBlock_CraftedKey.Visibility = Visibility.Visible;
                textBlock_CraftedAmount.Visibility = Visibility.Visible;
                textBlock_CraftTime.Visibility = Visibility.Visible;
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
            }
        }

        private void RefreshItemsLists() //Function refreshes the main list.
        {
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
            mbEditing = true;
            EditMode(true);
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            mbEditing = false;
            EditMode(false);
        }
    }
}
