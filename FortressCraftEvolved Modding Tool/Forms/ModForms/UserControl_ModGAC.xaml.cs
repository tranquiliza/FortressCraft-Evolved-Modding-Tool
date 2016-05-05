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
using Common;
using Common.ModLogics;
using Common.XmlLogic;
using Common.ModWriter;
using Common.Data;
using System.IO;

namespace FortressCraftEvolved_Modding_Tool.Forms.ModForms
{
    /// <summary>
    /// Interaction logic for UserControl_ModGAC.xaml
    /// </summary>
    public partial class UserControl_ModGAC : UserControl
    {
        bool mbEditingExisting = false;
        private GenericAutoCrafterDataEntry mActiveGAC = null;
        private string mGACPath = string.Empty;
        private string mXMLPath = string.Empty;
        public UserControl_ModGAC()
        {
            InitializeComponent();
            #region .TerrainDataInit.
            if (ModWriterDataHolder.TerrainDataEntries.Count < 1)
            {
                string TerrainDataXmlPath = string.Empty;
                string[] Split = User.Default.ConfigFilePath.Split('\\');
                TerrainDataXmlPath = string.Empty;
                for (int i = 0; i < Split.Length - 1; i++)
                {
                    TerrainDataXmlPath += Split[i] + "\\";
                }
                TerrainDataXmlPath += "Xml\\";
                TerrainDataXmlPath += "TerrainData.xml";
                try
                {
                    ModWriterDataHolder.TerrainDataEntries = XMLSerializer.Deserialize<List<TerrainDataEntry>>(File.ReadAllText(TerrainDataXmlPath));

                }
                catch (Exception x)
                {
                    Common.Error.Log("Error: Items modding interfaace was unable to Deserialize " + x);
                    //File.WriteAllText("ModItemsCreatorError.txt", x.ToString());
                }
            }
            #endregion
            Refresh();
            EditMode(false);
        }

        private void Refresh()
        {
            comboBox_SpawnObject.Items.Clear();
            foreach (SpawnableObjectEnum enumValue in Enum.GetValues(typeof(SpawnableObjectEnum)))
            {
                comboBox_SpawnObject.Items.Add(enumValue);
            }
            comboBox_CraftingCostAmount.Items.Clear();
            for (int i = 1; i < 101; i++)
            {
                comboBox_CraftingCostAmount.Items.Add(i);
            }
            comboBox_CraftingCostAmount.SelectedItem = 1;
            comboBox_RecipeCraftedAmount.Items.Clear();
            for (int i = 1; i < 101; i++)
            {
                comboBox_RecipeCraftedAmount.Items.Add(i);
            }
            comboBox_RecipeCraftedAmount.SelectedItem = 1;

            //CraftedKeys for GAC Recipe:
            comboBox_RecipeCraftedKey.Items.Clear(); //Same items as in the craftcost..
            //Items:
            comboBox_CraftCostItem.Items.Clear();
            for (int i = 0; i < DataHolder.ItemEntries.Count; i++)
            {
                comboBox_CraftCostItem.Items.Add(DataHolder.ItemEntries[i].Key);
                comboBox_RecipeCraftedKey.Items.Add(DataHolder.ItemEntries[i].Key);
            }
            //Modded Items:
            for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
            {
                comboBox_CraftCostItem.Items.Add(ModWriterDataHolder.Items[i].Key);
                comboBox_RecipeCraftedKey.Items.Add(ModWriterDataHolder.Items[i].Key);
            }

            //Terrain Data:
            for (int i = 0; i < DataHolder.TerrainDataEntries.Count; i++)
            {
                if (DataHolder.TerrainDataEntries[i].Values != null)
                {
                    comboBox_CraftCostItem.Items.Add(DataHolder.TerrainDataEntries[i].Key);
                    comboBox_RecipeCraftedKey.Items.Add(DataHolder.TerrainDataEntries[i].Key);
                    for (int j = 0; j < DataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        comboBox_CraftCostItem.Items.Add(DataHolder.TerrainDataEntries[i].Values[j].Key);
                        comboBox_RecipeCraftedKey.Items.Add(DataHolder.TerrainDataEntries[i].Values[j].Key);
                    }
                }
                else
                {
                    comboBox_CraftCostItem.Items.Add(DataHolder.TerrainDataEntries[i].Key);
                    comboBox_RecipeCraftedKey.Items.Add(DataHolder.TerrainDataEntries[i].Key);
                }
            }
            //Modded Terrain Data
            for (int i = 0; i < ModWriterDataHolder.TerrainDataEntries.Count; i++)
            {
                if (ModWriterDataHolder.TerrainDataEntries[i].Values != null)
                {
                    comboBox_CraftCostItem.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Key);
                    comboBox_RecipeCraftedKey.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Key);
                    for (int j = 0; j < ModWriterDataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        comboBox_CraftCostItem.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Values[j].Key);
                        comboBox_RecipeCraftedKey.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Values[j].Key);
                    }
                }
                else
                {
                    comboBox_CraftCostItem.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Key);
                    comboBox_RecipeCraftedKey.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Key);
                }
            }

            //IconNames
            comboBox_Icon.Items.Clear();
            for (int i = 0; i < ModWriterDataHolder.Sprites().Length; i++)
            {
                comboBox_Icon.Items.Add(ModWriterDataHolder.Sprites()[i]);
            }
        }

        private void EditMode(bool lbIsEditing)
        {
            button_SelectGAC.Visibility = lbIsEditing ? Visibility.Hidden : Visibility.Visible;
            button_Edit.Visibility = lbIsEditing ? Visibility.Hidden : Visibility.Visible;
            button_AddCost.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            button_RemoveCost.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            comboBox_CraftCostItem.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            comboBox_CraftingCostAmount.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            button_Cancel.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            button_Confirm.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;

            textBox_FriendlyName.IsEnabled = lbIsEditing;
            comboBox_SpawnObject.IsEnabled = lbIsEditing;
            textBox_LoadingAnim.IsEnabled = lbIsEditing;
            textBox_WorkingAnim.IsEnabled = lbIsEditing;
            textBox_UnloadingAnim.IsEnabled = lbIsEditing;
            textBox_CraftingString.IsEnabled = lbIsEditing;
            textBox_Value.IsEnabled = lbIsEditing;
            textBox_PowerUsePerSec.IsEnabled = lbIsEditing;
            textBox_PowerTransferPerSec.IsEnabled = lbIsEditing;
            textBox_MaxPowerStorage.IsEnabled = lbIsEditing;
            textBox_CraftTime.IsEnabled = lbIsEditing;
            checkBox_OptionalIngredients.IsEnabled = lbIsEditing;
            comboBox_Icon.IsEnabled = lbIsEditing;

            //Recipe Boxes:
            textBox_RecipeKey.IsEnabled = lbIsEditing;
            comboBox_RecipeCraftedKey.IsEnabled = lbIsEditing;
            comboBox_RecipeCraftedAmount.IsEnabled = lbIsEditing;
            textBox_RecipeDesc.IsEnabled = lbIsEditing;
            listBox_RecipeCraftCost.IsEnabled = lbIsEditing;
        }
        private void NewGACSelection()
        {
            textBox_FriendlyName.Text = mActiveGAC.FriendlyName;
            comboBox_SpawnObject.SelectedItem = mActiveGAC.SpawnObject;
            textBox_LoadingAnim.Text = mActiveGAC.LoadingAnimation;
            textBox_WorkingAnim.Text = mActiveGAC.WorkingAnimation;
            textBox_UnloadingAnim.Text = mActiveGAC.UnloadingAnimation;
            textBox_CraftingString.Text = mActiveGAC.CraftingString;
            textBox_Value.Text = mActiveGAC.Value;
            textBox_PowerUsePerSec.Text = mActiveGAC.PowerUsePerSecond.ToString();
            textBox_PowerTransferPerSec.Text = mActiveGAC.PowerTransferPerSecond.ToString();
            textBox_MaxPowerStorage.Text = mActiveGAC.MaxPowerStorage.ToString();
            textBox_CraftTime.Text = mActiveGAC.CraftTime.ToString();
            checkBox_OptionalIngredients.IsChecked = mActiveGAC.OptionalIngredients;
            for (int i = 0; i < ModWriterDataHolder.TerrainDataEntries.Count; i++)
            {
                if (ModWriterDataHolder.TerrainDataEntries[i].Key == "GenericAssemblerMachine")
                {
                    if (ModWriterDataHolder.TerrainDataEntries[i].Values != null)
                    {
                        for (int j = 0; j < ModWriterDataHolder.TerrainDataEntries[i].Values.Count; j++)
                        {
                            if (ModWriterDataHolder.TerrainDataEntries[i].Values[j].Key == mActiveGAC.Value)
                            {
                                comboBox_Icon.SelectedItem = ModWriterDataHolder.TerrainDataEntries[i].Values[j].IconName;
                                break;
                            }
                        }
                    }
                }
            }

            //Recipe Boxes:
            if (mActiveGAC.Recipe == null)
            {
                mActiveGAC.Recipe = new CraftData();
            }
            textBox_RecipeKey.Text = mActiveGAC.Recipe.Key;
            comboBox_RecipeCraftedKey.SelectedItem = mActiveGAC.Recipe.CraftedKey;
            comboBox_RecipeCraftedAmount.SelectedItem = mActiveGAC.Recipe.CraftedAmount;
            textBox_RecipeDesc.Text = mActiveGAC.Recipe.Description;
            listBox_RecipeCraftCost.Items.Clear();
            if (mActiveGAC.Recipe.Costs != null)
            {
                for (int i = 0; i < mActiveGAC.Recipe.Costs.Count; i++)
                {
                    listBox_RecipeCraftCost.Items.Add(mActiveGAC.Recipe.Costs[i].ToString());
                }
            }
        }

        private void button_SelectGAC_Click(object sender, RoutedEventArgs e)
        {
            mGACPath = string.Empty;
            string[] Split = User.Default.ConfigFilePath.Split('\\');
            for (int i = 0; i < Split.Length - 1; i++)
            {
                mGACPath += Split[i] + "\\";
            }
            mGACPath += "Xml\\";
            mXMLPath = mGACPath;
            mGACPath += "GenericAutoCrafter";

            Microsoft.Win32.OpenFileDialog SelectPath = new Microsoft.Win32.OpenFileDialog();
            SelectPath.DefaultExt = ".xml";
            SelectPath.Filter = "XML Files (*.xml)|*.xml";

            SelectPath.InitialDirectory = mGACPath;

            bool? lResult = SelectPath.ShowDialog();

            if (lResult == true)
            {
                //Common.Error.Log(SelectPath.FileName);
                try
                {
                    mActiveGAC = XMLSerializer.Deserialize<GenericAutoCrafterDataEntry>(File.ReadAllText(SelectPath.FileName));
                    mGACPath = SelectPath.FileName;
                    //Common.Error.Log("GAC PATH IS:" + lGACPath);
                }
                catch (Exception x)
                {
                    Error.Log("Failed to load GAC in ModGAC" + x);
                }
                //Lets put all the values into the boxes here!
                NewGACSelection();
            }
        }

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (mActiveGAC != null)
            {
                EditMode(true);
                mbEditingExisting = true;
                textBox_Value.IsEnabled = false;
            }
        }

        private void button_RemoveCost_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_RecipeCraftCost.SelectedItem != null)
            {
                for (int i = 0; i < mActiveGAC.Recipe.Costs.Count; i++)
                {
                    if (mActiveGAC.Recipe.Costs[i].ToString() == listBox_RecipeCraftCost.SelectedItem.ToString())
                    {
                        mActiveGAC.Recipe.Costs.RemoveAt(i);
                    }
                }
            }
            RefreshSelection();
        }

        private void button_AddCost_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_CraftCostItem.SelectedItem != null && comboBox_CraftingCostAmount.SelectedItem != null)
            {
                CraftCost Holder = new CraftCost();
                Holder.Key = comboBox_CraftCostItem.SelectedItem.ToString();
                uint lAmount = 1;
                uint.TryParse(comboBox_CraftingCostAmount.SelectedItem.ToString(), out lAmount);
                Holder.Amount = lAmount;
                if (mActiveGAC.Recipe.Costs == null)
                {
                    mActiveGAC.Recipe.Costs = new List<CraftCost>();
                }
                mActiveGAC.Recipe.Costs.Add(Holder);
            }
            RefreshSelection();
        }

        private void RefreshSelection()
        {
            listBox_RecipeCraftCost.Items.Clear();
            for (int i = 0; i < mActiveGAC.Recipe.Costs.Count; i++)
            {
                listBox_RecipeCraftCost.Items.Add(mActiveGAC.Recipe.Costs[i].ToString());
            }
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            EditMode(false);
        }

        private void button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (mActiveGAC != null)
            {
                #region .GACXML.
                //ActiveGAC Starts:
                mActiveGAC.FriendlyName = textBox_FriendlyName.Text;
                if (comboBox_SpawnObject.SelectedItem != null)
                {
                    mActiveGAC.SpawnObject = (SpawnableObjectEnum)comboBox_SpawnObject.SelectedItem;
                }
                mActiveGAC.LoadingAnimation = textBox_LoadingAnim.Text;
                mActiveGAC.WorkingAnimation = textBox_WorkingAnim.Text;
                mActiveGAC.UnloadingAnimation = textBox_UnloadingAnim.Text;
                mActiveGAC.CraftingString = textBox_CraftingString.Text;
                if (mbEditingExisting)
                {
                    mActiveGAC.Value = textBox_Value.Text; // We do this for no reason, user cannot edit this field any ways..
                }
                else
                {
                    mActiveGAC.Value = User.Default.AuthorID + "." + textBox_Value.Text;
                }
                float lPowerUsePerSecond = 0f;
                float.TryParse(textBox_PowerUsePerSec.Text, out lPowerUsePerSecond);
                mActiveGAC.PowerUsePerSecond = lPowerUsePerSecond;

                float lPowerTransferPerSecond = 0f;
                float.TryParse(textBox_PowerTransferPerSec.Text, out lPowerTransferPerSecond);
                mActiveGAC.PowerTransferPerSecond = lPowerTransferPerSecond;

                float lMaxPowerStorage = 0f;
                float.TryParse(textBox_MaxPowerStorage.Text, out lMaxPowerStorage);
                mActiveGAC.MaxPowerStorage = lMaxPowerStorage;

                float lCraftTime = 10f;
                float.TryParse(textBox_CraftTime.Text, out lCraftTime);
                mActiveGAC.CraftTime = lCraftTime;
                //ActiveGAC Ends*

                //ActiveGAC Recipe starts:
                mActiveGAC.Recipe.Key = textBox_RecipeKey.Text;
                if (comboBox_RecipeCraftedKey.SelectedItem != null)
                {
                    mActiveGAC.Recipe.CraftedKey = comboBox_RecipeCraftedKey.SelectedItem.ToString();
                }
                if (comboBox_RecipeCraftedAmount.SelectedItem != null)
                {
                    int lCraftedAmount = 1;
                    int.TryParse(comboBox_RecipeCraftedAmount.SelectedItem.ToString(), out lCraftedAmount);
                    mActiveGAC.Recipe.CraftedAmount = lCraftedAmount;
                }
                mActiveGAC.Recipe.Description = textBox_RecipeDesc.Text;
                #endregion

                #region TerrainDataHandling

                TerrainDataEntry lTerrainEntry = null;
                TerrainDataValueEntry lTerrainDataValue = new TerrainDataValueEntry();
                if (mbEditingExisting)
                {
                    lTerrainDataValue.Key = textBox_Value.Text;
                }
                else
                {
                    lTerrainDataValue.Key = User.Default.AuthorID + "." + textBox_Value.Text;
                }
                lTerrainDataValue.Name = textBox_FriendlyName.Text;
                if (comboBox_Icon.SelectedItem != null)
                {
                    lTerrainDataValue.IconName = comboBox_Icon.SelectedItem.ToString();
                }
                else
                {
                    lTerrainDataValue.IconName = "NoIcon";
                }
                lTerrainDataValue.Description = textBox_RecipeDesc.Text;
                for (int i = 0; i < ModWriterDataHolder.TerrainDataEntries.Count; i++)
                {
                    if (ModWriterDataHolder.TerrainDataEntries[i].Key == "GenericAssemblerMachine")
                    {
                        lTerrainEntry = ModWriterDataHolder.TerrainDataEntries[i];
                        break;
                    }
                }
                if (lTerrainEntry == null)
                {
                    //We make an identical entry to the Games Entry. This should work.
                    lTerrainEntry = new TerrainDataEntry();
                    lTerrainEntry.IsOverride = true;
                    lTerrainEntry.Key = "GenericAssemblerMachine";
                    lTerrainEntry.Name = "Generic Assembler Machine";
                    lTerrainEntry.MaxStack = 100;
                    lTerrainEntry.DefaultValue = 0;
                    lTerrainEntry.LayerType = SegmentRendererLayerType.PrimaryTerrain;
                    lTerrainEntry.TopTexture = 192;
                    lTerrainEntry.SideTexture = 192;
                    lTerrainEntry.BottomTexture = 192;
                    lTerrainEntry.GUITexture = 192;
                    lTerrainEntry.isSolid = false;
                    lTerrainEntry.isTransparent = true;
                    lTerrainEntry.isHollow = false;
                    lTerrainEntry.isGlass = false;
                    lTerrainEntry.isPassable = false;
                    lTerrainEntry.hasObject = true;
                    lTerrainEntry.hasEntity = true;
                    lTerrainEntry.Category = MaterialCategories.Machine;
                    lTerrainEntry.Hardness = 50;
                    lTerrainEntry.AudioWalkType = eBlockWalkAudioType.Metal;
                    lTerrainEntry.AudioBuildType = eBlockBuildAudioType.Dirt;
                    lTerrainEntry.AudioDestroyType = eBlockDestroyAudioType.None;
                    lTerrainEntry.tags = new List<TerrainData.eTags>();
                    lTerrainEntry.tags.Add(TerrainData.eTags.Machinery);
                    lTerrainEntry.Values = new List<TerrainDataValueEntry>();
                    //This is the New Machine:
                    ModWriterDataHolder.TerrainDataEntries.Add(lTerrainEntry);
                }
                if (mbEditingExisting)
                {
                    for (int i = 0; i < ModWriterDataHolder.TerrainDataEntries.Count; i++)
                    {
                        if (ModWriterDataHolder.TerrainDataEntries[i].Key == "GenericAssemblerMachine")
                        {
                            if (ModWriterDataHolder.TerrainDataEntries[i].Values != null)
                            {
                                for (int j = 0; j < ModWriterDataHolder.TerrainDataEntries[i].Values.Count; j++)
                                {
                                    if (ModWriterDataHolder.TerrainDataEntries[i].Values[j].Key == mActiveGAC.Value)
                                    {
                                        ModWriterDataHolder.TerrainDataEntries[i].Values[j] = lTerrainDataValue;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < ModWriterDataHolder.TerrainDataEntries.Count; i++)
                    {
                        if (ModWriterDataHolder.TerrainDataEntries[i].Key == "GenericAssemblerMachine")
                        {
                            if (ModWriterDataHolder.TerrainDataEntries[i].Values == null)
                            {
                                throw new NotImplementedException();
                            }
                            if (ModWriterDataHolder.TerrainDataEntries[i].Values != null)
                            {
                                ModWriterDataHolder.TerrainDataEntries[i].Values.Add(lTerrainDataValue);
                            }
                        }
                    }
                }

                
                #endregion
                string TerrainDataXml = XMLSerializer.Serialize(ModWriterDataHolder.TerrainDataEntries, false);
                string GACMachine = XMLSerializer.Serialize(mActiveGAC, false);
                if (!mbEditingExisting)
                {
                    mGACPath = string.Empty;
                    string[] Split = User.Default.ConfigFilePath.Split('\\');
                    for (int i = 0; i < Split.Length - 1; i++)
                    {
                        mGACPath += Split[i] + "\\";
                    }
                    mGACPath += "Xml\\";
                    mXMLPath = mGACPath;
                    mGACPath += "GenericAutoCrafter";
                    mGACPath += "\\" + mActiveGAC.Value+ ".xml";
                }

                File.WriteAllText(mXMLPath + "TerrainData.xml", TerrainDataXml);
                File.WriteAllText(mGACPath, GACMachine);

                EditMode(false);
            }
        }

        private void button_NewGAC_Click(object sender, RoutedEventArgs e)
        {
            mActiveGAC = new GenericAutoCrafterDataEntry();
            mbEditingExisting = false;
            NewGACSelection();
            EditMode(true);
        }
    }
}
