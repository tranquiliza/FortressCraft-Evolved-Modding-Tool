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
using Common.XmlLogic;
using Common.ModLogics;
using System.IO;
using Common.ModWriter;
using Common.Data;

namespace FortressCraftEvolved_Modding_Tool.Forms.ModForms
{
    /// <summary>
    /// Interaction logic for UserControl_ModItems.xaml
    /// </summary>
    public partial class UserControl_ModItems : UserControl
    {
        ItemEntry ActiveItem = null;
        bool Editing;
        bool mbIsOverride;
        string ItemsXmlPath;

        List<string> NewResearchReq;
        List<string> NewScanReq;
        List<string> NewRemoveResearchReq;
        List<string> NewRemoveScanReq;



        public UserControl_ModItems()
        {
            InitializeComponent();

            string[] Split = User.Default.ConfigFilePath.Split('\\');
            string XmlFilePath = "";

            for (int i = 0; i < Split.Length-1; i++)
            {
                XmlFilePath += Split[i] + "\\";
            }
            XmlFilePath += "Xml\\";

            ItemsXmlPath = XmlFilePath + "Items.xml";
            try
            {
                ModWriterDataHolder.Items = XMLSerializer.Deserialize<List<ItemEntry>>(File.ReadAllText(ItemsXmlPath));

            }
            catch (Exception x)
            {
                Common.Error.Log("Error: Items modding interfaace was unable to Deserialize " + x);
                //File.WriteAllText("ModItemsCreatorError.txt", x.ToString());
            }
            for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
            {
                listBox_Items.Items.Add(ModWriterDataHolder.Items[i].Name);
            }


            //Textblock Control:
            textBlock_IsOverride.Text = "";
            textBlock_ItemId.Text = "";
            textBlock_Key.Text = "";
            textBlock_Name.Text = "";
            textBlock_Plural.Text = "";
            textBlock_Type.Text = "";
            textBlock_Hidden.Text = "";
            textBlock_Object.Text = "";
            textBlock_Sprite.Text = "";
            textBlock_Category.Text = "";
            textBlock_Desc.Text = "";
            textBlock_MaxDurability.Text = "";
            textBlock_MaxStack.Text = "";
            textBlock_ItemAction.Text = "";
            textBlock_ActionParameter.Text = "";
            textBlock_DecomposeValue.Text = "";

            textBox_Name.Text = "";
            textBox_Plural.Text = "";
            comboBox_Types.Text = "";
            checkBox_Hidden.IsChecked = false;
            comboBox_Object.Text = "";
            comboBox_Sprites.Text = "";
            comboBox_Category.Text = "";
            textBox_Desc.Text = "";
            textBox_MaxDurability.Text = "";
            textBox_MaxStack.Text = "";
            comboBox_ItemAction.Text = "";
            textBox_ActionParameter.Text = "";
            textBox_DecomposeValue.Text = "";
            textBox_Key.Text = "";



            //Add these to a function that is called upon startup, and then add a key specific input that calls them again for refresh?!
            comboBox_Types.Items.Clear();
            foreach (ItemType enumValue in Enum.GetValues(typeof(ItemType)))
            {
                comboBox_Types.Items.Add(enumValue);
            }

            comboBox_Object.Items.Clear();
            foreach (SpawnableObjectEnum enumValue in Enum.GetValues(typeof(SpawnableObjectEnum)))
            {
                comboBox_Object.Items.Add(enumValue);
            }

            comboBox_Sprites.Items.Clear();
            if (ModWriterDataHolder.Sprites() != null)
            {
                for (int i = 0; i < ModWriterDataHolder.Sprites().Length; i++)
                {
                    comboBox_Sprites.Items.Add(ModWriterDataHolder.Sprites()[i]);
                }
            }
            else
            {
                Common.Error.Log("Error: Sprites wasn't loaded, Modwriter Items");
                MessageBox.Show("Sprites Diddn't load!");
            }
            //This is possibly wrong. Maybe not.
            comboBox_Category.Items.Clear();
            foreach (MaterialCategories enumValue in Enum.GetValues(typeof(MaterialCategories)))
            {
                comboBox_Category.Items.Add(enumValue);
            }

            comboBox_ItemAction.Items.Clear();
            foreach (ItemActions enumValue in Enum.GetValues(typeof(ItemActions)))
            {
                comboBox_ItemAction.Items.Add(enumValue);
            }
            Refresh();
            EditMode(false);
        }

        private void Refresh()
        {
            //Research Dropdown!
            comboBox_Research.Items.Clear();
            for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
            {
                comboBox_Research.Items.Add(DataHolder.ResearchEntries[i].Key);
            }
            for (int i = 0; i < ModWriterDataHolder.ResearchEntires.Count; i++)
            {
                comboBox_Research.Items.Add(ModWriterDataHolder.ResearchEntires[i].Key);
            }

            //Scan Dropdown!
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

        private void EditMode(bool IsEditing)
        {
            if (IsEditing)
            {
                listBox_Items.IsEnabled = false;
                comboBox_Scan.Visibility = Visibility.Visible;
                button_Delete.Visibility = Visibility.Hidden;
                button_Write.Visibility = Visibility.Hidden;
                button_DeleteResearch.Visibility = Visibility.Visible;
                //listBox_Items.Visibility = Visibility.Hidden;
                textBlock_Name.Visibility = Visibility.Hidden;
                textBlock_Plural.Visibility = Visibility.Hidden;
                textBlock_Type.Visibility = Visibility.Hidden;
                textBlock_Hidden.Visibility = Visibility.Hidden;
                textBlock_Object.Visibility = Visibility.Hidden;
                textBlock_Sprite.Visibility = Visibility.Hidden;
                textBlock_Category.Visibility = Visibility.Hidden;
                textBlock_Desc.Visibility = Visibility.Hidden;
                textBlock_MaxDurability.Visibility = Visibility.Hidden;
                textBlock_MaxStack.Visibility = Visibility.Hidden;
                textBlock_ItemAction.Visibility = Visibility.Hidden;
                textBlock_ActionParameter.Visibility = Visibility.Hidden;
                textBlock_DecomposeValue.Visibility = Visibility.Hidden;
                

                textBox_Name.Visibility = Visibility.Visible;
                textBox_Plural.Visibility = Visibility.Visible;
                comboBox_Types.Visibility = Visibility.Visible;
                checkBox_Hidden.Visibility = Visibility.Visible;
                comboBox_Object.Visibility = Visibility.Visible;
                comboBox_Sprites.Visibility = Visibility.Visible;
                comboBox_Category.Visibility = Visibility.Visible;
                textBox_Desc.Visibility = Visibility.Visible;
                textBox_MaxDurability.Visibility = Visibility.Visible;
                textBox_MaxStack.Visibility = Visibility.Visible;
                comboBox_ItemAction.Visibility = Visibility.Visible;
                textBox_ActionParameter.Visibility = Visibility.Visible;
                textBox_DecomposeValue.Visibility = Visibility.Visible;

                
                button_AddScan.Visibility = Visibility.Visible;
                button_RemoveScan.Visibility = Visibility.Visible;
                button_AddResearch.Visibility = Visibility.Visible;
                button_RemoveResearch.Visibility = Visibility.Visible;
                button_Confirm.Visibility = Visibility.Visible;
                button_Cancel.Visibility = Visibility.Visible;
                button_EditItem.Visibility = Visibility.Hidden;
                button_AddItem.Visibility = Visibility.Hidden;

                comboBox_Research.Visibility = Visibility.Visible;
            }
            else
            {
                listBox_Items.IsEnabled = true;
                comboBox_Scan.Visibility = Visibility.Hidden;
                button_Delete.Visibility = Visibility.Visible;
                button_Write.Visibility = Visibility.Visible;
                button_DeleteResearch.Visibility = Visibility.Hidden;
                //listBox_Items.Visibility = Visibility.Visible;
                textBlock_IsOverride.Visibility = Visibility.Visible;
                textBlock_ItemId.Visibility = Visibility.Visible;
                textBlock_Key.Visibility = Visibility.Visible;
                textBlock_Name.Visibility = Visibility.Visible;
                textBlock_Plural.Visibility = Visibility.Visible;
                textBlock_Type.Visibility = Visibility.Visible;
                textBlock_Hidden.Visibility = Visibility.Visible;
                textBlock_Object.Visibility = Visibility.Visible;
                textBlock_Sprite.Visibility = Visibility.Visible;
                textBlock_Category.Visibility = Visibility.Visible;
                textBlock_Desc.Visibility = Visibility.Visible;
                textBlock_MaxDurability.Visibility = Visibility.Visible;
                textBlock_MaxStack.Visibility = Visibility.Visible;
                textBlock_ItemAction.Visibility = Visibility.Visible;
                textBlock_ActionParameter.Visibility = Visibility.Visible;
                textBlock_DecomposeValue.Visibility = Visibility.Visible;

                button_Confirm.Visibility = Visibility.Hidden;
                button_Cancel.Visibility = Visibility.Hidden;
                button_EditItem.Visibility = Visibility.Visible;
                button_AddItem.Visibility = Visibility.Visible;

                //Editor Controls:
                textBox_Name.Visibility = Visibility.Hidden;
                textBox_Plural.Visibility = Visibility.Hidden;
                comboBox_Types.Visibility = Visibility.Hidden;
                checkBox_Hidden.Visibility = Visibility.Hidden;
                comboBox_Object.Visibility = Visibility.Hidden;
                comboBox_Sprites.Visibility = Visibility.Hidden;
                comboBox_Category.Visibility = Visibility.Hidden;
                textBox_Desc.Visibility = Visibility.Hidden;
                textBox_MaxDurability.Visibility = Visibility.Hidden;
                textBox_MaxStack.Visibility = Visibility.Hidden;
                comboBox_ItemAction.Visibility = Visibility.Hidden;
                textBox_ActionParameter.Visibility = Visibility.Hidden;
                textBox_DecomposeValue.Visibility = Visibility.Hidden;

                textBox_Key.Visibility = Visibility.Hidden;
                comboBox_Key.Visibility = Visibility.Hidden;
                checkBox_IsOverride.Visibility = Visibility.Hidden;

                button_AddScan.Visibility = Visibility.Hidden;
                button_RemoveScan.Visibility = Visibility.Hidden;
                button_AddResearch.Visibility = Visibility.Hidden;
                button_RemoveResearch.Visibility = Visibility.Hidden;
                
                comboBox_Research.Visibility = Visibility.Hidden;

                listBox_RemoveResearchReq.Visibility = Visibility.Visible;
                listBox_RemoveScanReq.Visibility = Visibility.Visible;
            }
        }

        //When we select an Item:
        private void listBox_Items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemEntry SelectedItem = null;
            if (listBox_Items.SelectedItem != null)
            {
                for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
                {
                    if (ModWriterDataHolder.Items[i].Name == listBox_Items.SelectedItem.ToString())
                    {
                        SelectedItem = ModWriterDataHolder.Items[i];
                    }
                }
                mbIsOverride = SelectedItem.IsOverride;
                textBlock_IsOverride.Text = SelectedItem.IsOverride.ToString();
                //textBlock_ItemId.Text = SelectedItem.ItemID.ToString();
                textBlock_Key.Text = SelectedItem.Key;
                textBlock_Name.Text = SelectedItem.Name;
                textBlock_Plural.Text = SelectedItem.Plural;
                textBlock_Type.Text = SelectedItem.Type.ToString();
                textBlock_Hidden.Text = SelectedItem.Hidden.ToString();
                textBlock_Object.Text = SelectedItem.Object.ToString();
                textBlock_Sprite.Text = SelectedItem.Sprite;
                textBlock_Category.Text = SelectedItem.Category.ToString();
                textBlock_Desc.Text = SelectedItem.Description;
                textBlock_MaxDurability.Text = SelectedItem.MaxDurability.ToString();
                textBlock_MaxStack.Text = SelectedItem.MaxStack.ToString();
                textBlock_ItemAction.Text = SelectedItem.ItemAction.ToString();
                textBlock_ActionParameter.Text = SelectedItem.ActionParameter;
                textBlock_DecomposeValue.Text = SelectedItem.DecomposeValue.ToString();


                //Resets the list when we choose to edit a new item!
                
                listBox_ResearchReq.Items.Clear();
                for (int i = 0; i < SelectedItem.ResearchRequirements.Count; i++)
                {
                    listBox_ResearchReq.Items.Add(SelectedItem.ResearchRequirements[i]);
                }

                listBox_RemoveResearchReq.Items.Clear();
                for (int i = 0; i < SelectedItem.RemoveResearchRequirements.Count; i++)
                {
                    listBox_RemoveResearchReq.Items.Add(SelectedItem.RemoveResearchRequirements[i]);
                }

                listBox_ScanReq.Items.Clear();
                for (int i = 0; i < SelectedItem.ScanRequirements.Count; i++)
                {
                    listBox_ScanReq.Items.Add(SelectedItem.ScanRequirements[i]);
                }
                
                listBox_RemoveScanReq.Items.Clear();
                for (int i = 0; i < SelectedItem.RemoveScanRequirements.Count; i++)
                {
                    listBox_RemoveScanReq.Items.Add(SelectedItem.RemoveScanRequirements[i]);
                }

            }
        }


        //This is what happens when we select to edit an item:
        private void button_EditItem_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            Editing = true;
            if (listBox_Items.SelectedItem != null)
            {
                for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
                {
                    if (ModWriterDataHolder.Items[i].Name == listBox_Items.SelectedItem.ToString())
                    {
                        ActiveItem = ModWriterDataHolder.Items[i];
                    }
                }

                EditMode(true);

                //Editor Controls
                textBox_Name.Text = ActiveItem.Name;
                textBox_Plural.Text = ActiveItem.Plural;
                comboBox_Types.Text = ActiveItem.Type.ToString();
                checkBox_Hidden.IsChecked = ActiveItem.Hidden;
                comboBox_Object.Text = ActiveItem.Object.ToString();
                comboBox_Sprites.Text = ActiveItem.Sprite;
                comboBox_Category.Text = ActiveItem.Category.ToString();
                textBox_Desc.Text = ActiveItem.Description;
                textBox_MaxDurability.Text = ActiveItem.MaxDurability.ToString();
                textBox_MaxStack.Text = ActiveItem.MaxStack.ToString();
                comboBox_ItemAction.Text = ActiveItem.ItemAction.ToString();
                textBox_ActionParameter.Text = ActiveItem.ActionParameter;
                textBox_DecomposeValue.Text = ActiveItem.DecomposeValue.ToString();
            }
        }


        //Add new items:
        private void button_AddItem_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            Editing = false;

            EditMode(true);

            button_DeleteResearch.Visibility = Visibility.Hidden;

            NewResearchReq = new List<string>();
            NewScanReq = new List<string>();
            NewRemoveResearchReq = new List<string>();
            NewRemoveScanReq = new List<string>();

            checkBox_IsOverride.IsChecked = false;

            listBox_ResearchReq.Items.Clear();
            for (int i = 0; i < NewResearchReq.Count; i++)
            {
                listBox_ResearchReq.Items.Add(NewResearchReq[i]);
            }
            listBox_RemoveResearchReq.Items.Clear();
            for (int i = 0; i < NewScanReq.Count; i++)
            {
                listBox_ScanReq.Items.Add(NewScanReq[i]);
            }

            listBox_RemoveScanReq.Items.Clear();
            listBox_ScanReq.Items.Clear();

            textBlock_IsOverride.Visibility = Visibility.Hidden;
            textBlock_ItemId.Visibility = Visibility.Hidden;
            textBlock_Key.Visibility = Visibility.Hidden;
            textBox_Key.Visibility = Visibility.Visible;
            checkBox_IsOverride.Visibility = Visibility.Visible;

            textBox_Name.Text = "";
            textBox_Plural.Text = "";
            comboBox_Types.Text = "";
            checkBox_Hidden.IsChecked = false;
            comboBox_Object.Text = "";
            comboBox_Sprites.Text = "";
            comboBox_Category.Text = "";
            textBox_Desc.Text = "";
            textBox_MaxDurability.Text = "";
            textBox_MaxStack.Text = "";
            comboBox_ItemAction.Text = "";
            textBox_ActionParameter.Text = "";
            textBox_DecomposeValue.Text = "";
            textBox_Key.Text = "";

        }


        //Confirm button for Both new Item and Edit of an existing Item!
        //TODO:
        private void button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (Editing == true)
            {
                ActiveItem.IsOverride = ActiveItem.IsOverride;
                ActiveItem.Name = textBox_Name.Text;
                ActiveItem.Plural = textBox_Plural.Text;
                ActiveItem.Type = (ItemType)comboBox_Types.SelectedItem;
                if (checkBox_Hidden.IsChecked == true)
                {
                    ActiveItem.Hidden = true;
                }
                else
                {
                    ActiveItem.Hidden = false;
                }
                ActiveItem.Object = (SpawnableObjectEnum)comboBox_Object.SelectedItem;
                ActiveItem.Sprite = comboBox_Sprites.SelectedItem.ToString();
                ActiveItem.Category = (MaterialCategories)comboBox_Category.SelectedItem;
                ActiveItem.Description = textBox_Desc.Text;
                int lnMaxDura = 1;
                Int32.TryParse(textBox_MaxDurability.Text, out lnMaxDura);
                ActiveItem.MaxDurability = lnMaxDura;
                int lnMaxStack = 100;
                Int32.TryParse(textBox_MaxStack.Text, out lnMaxStack);
                ActiveItem.MaxStack = lnMaxStack;
                ActiveItem.ItemAction = (ItemActions)comboBox_ItemAction.SelectedIndex;
                ActiveItem.ActionParameter = textBox_ActionParameter.Text;
                int lnDecomposeValue = 0;
                Int32.TryParse(textBox_DecomposeValue.Text, out lnDecomposeValue);
                ActiveItem.DecomposeValue = lnDecomposeValue;
                //Before we do this, add values to ActiveItem
                for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
                {
                    if (ModWriterDataHolder.Items[i].Key == ActiveItem.Key)
                    {
                        ModWriterDataHolder.Items[i] = ActiveItem;
                    }
                }
            }
            else
            {
                ItemEntry lNewItem = new ItemEntry();
                if (checkBox_IsOverride.IsChecked == true)
                {
                    lNewItem.Key = comboBox_Key.SelectedItem.ToString();
                    lNewItem.IsOverride = true;
                }
                else
                {
                    lNewItem.Key = User.Default.AuthorID + "." + textBox_Key.Text;
                    lNewItem.IsOverride = false;
                }

                lNewItem.Name = textBox_Name.Text;
                lNewItem.Plural = textBox_Plural.Text;
                lNewItem.Type = (ItemType)comboBox_Types.SelectedItem;
                if (checkBox_Hidden.IsChecked == true)
                {
                    lNewItem.Hidden = true;
                }
                else
                {
                    lNewItem.Hidden = false;
                }
                lNewItem.Object = (SpawnableObjectEnum)comboBox_Object.SelectedItem;
                lNewItem.Sprite = comboBox_Sprites.SelectedItem.ToString();
                lNewItem.Category = (MaterialCategories)comboBox_Category.SelectedItem;
                lNewItem.Description = textBox_Desc.Text;
                int lnMaxDura = 0;
                if (textBox_MaxDurability.Text != "")
                {
                    Int32.TryParse(textBox_MaxDurability.Text, out lnMaxDura);
                }
                lNewItem.MaxDurability = lnMaxDura;
                int lnMaxStack = 100;
                if (textBox_MaxStack.Text != "")
                {
                    Int32.TryParse(textBox_MaxStack.Text, out lnMaxStack);
                }
                lNewItem.MaxStack = lnMaxStack;
                lNewItem.ItemAction = (ItemActions)comboBox_ItemAction.SelectedIndex;
                lNewItem.ActionParameter = textBox_ActionParameter.Text;
                int lnDecomposeValue = 0;
                Int32.TryParse(textBox_DecomposeValue.Text, out lnDecomposeValue);
                lNewItem.DecomposeValue = lnDecomposeValue;

                //Lists yay!

                lNewItem.ResearchRequirements = NewResearchReq;
                lNewItem.RemoveResearchRequirements = NewRemoveResearchReq;
                lNewItem.ScanRequirements = NewScanReq;
                lNewItem.RemoveScanRequirements = NewRemoveScanReq;

                //Before we do this create new ItemEntry instance, and add values to this!
                ModWriterDataHolder.Items.Add(lNewItem);
            }

            listBox_Items.Items.Clear();
            for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
            {
                listBox_Items.Items.Add(ModWriterDataHolder.Items[i].Name);
            }

            EditMode(false);

        }

        private void checkBox_IsOverride_Checked(object sender, RoutedEventArgs e)
        {
            textBox_Key.Visibility = Visibility.Hidden;
            comboBox_Key.Visibility = Visibility.Visible;
            mbIsOverride = true;
            comboBox_Key.Items.Clear();
            for (int i = 0; i < DataHolder.ItemEntries.Count; i++)
            {
                comboBox_Key.Items.Add(DataHolder.ItemEntries[i].Key);
            }
        }

        private void checkBox_IsOverride_Unchecked(object sender, RoutedEventArgs e)
        {
            comboBox_Key.Visibility = Visibility.Hidden;
            textBox_Key.Visibility = Visibility.Visible;

            mbIsOverride = false;

            textBlock_ItemId.Text = "";
            textBox_Name.Text = "";
            textBox_Plural.Text = "";
            comboBox_Types.Text = "";
            checkBox_Hidden.IsChecked = false;
            comboBox_Object.Text = "";
            comboBox_Sprites.Text = "";
            comboBox_Category.Text = "";
            textBox_Desc.Text = "";
            textBox_MaxDurability.Text = "";
            textBox_MaxStack.Text = "";
            comboBox_ItemAction.Text = "";
            textBox_ActionParameter.Text = "";
            textBox_DecomposeValue.Text = "";
        }

        private void comboBox_Key_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Common.GameLogics.ItemEntry SelectedItem = null;
            if (comboBox_Key.SelectedItem != null)
            {
                for (int i = 0; i < DataHolder.ItemEntries.Count; i++)
                {
                    if (DataHolder.ItemEntries[i].Key == comboBox_Key.SelectedItem.ToString())
                    {
                        SelectedItem = DataHolder.ItemEntries[i];
                        NewResearchReq = DataHolder.ItemEntries[i].ResearchRequirements;
                        NewRemoveResearchReq = DataHolder.ItemEntries[i].RemoveResearchRequirements;
                        NewScanReq = DataHolder.ItemEntries[i].ScanRequirements;
                        NewRemoveScanReq = DataHolder.ItemEntries[i].RemoveScanRequirements;
                    }
                }
                textBlock_ItemId.Text = SelectedItem.ItemID.ToString();
                textBox_Name.Text = SelectedItem.Name;
                textBox_Plural.Text = SelectedItem.Plural;
                comboBox_Types.Text = SelectedItem.Type.ToString();
                checkBox_Hidden.IsChecked = SelectedItem.Hidden;
                comboBox_Object.Text = SelectedItem.Object.ToString();
                comboBox_Sprites.Text = SelectedItem.Sprite;
                comboBox_Category.Text = SelectedItem.Category.ToString();
                textBox_Desc.Text = SelectedItem.Description;
                textBox_MaxDurability.Text = SelectedItem.MaxDurability.ToString();
                textBox_MaxStack.Text = SelectedItem.MaxStack.ToString();
                comboBox_ItemAction.Text = SelectedItem.ItemAction.ToString();
                textBox_ActionParameter.Text = SelectedItem.ActionParameter;
                textBox_DecomposeValue.Text = SelectedItem.DecomposeValue.ToString();

                listBox_ResearchReq.Items.Clear();


                for (int i = 0; i < NewResearchReq.Count; i++)
                {
                    listBox_ResearchReq.Items.Add(NewResearchReq[i]);
                }

                listBox_ScanReq.Items.Clear();
                for (int i = 0; i < NewScanReq.Count; i++)
                {
                    listBox_ScanReq.Items.Add(NewScanReq[i]);
                }

                listBox_RemoveResearchReq.Items.Clear();
                for (int i = 0; i < NewRemoveResearchReq.Count; i++)
                {
                    listBox_RemoveResearchReq.Items.Add(NewRemoveResearchReq[i]);
                }

                listBox_RemoveScanReq.Items.Clear();
                for (int i = 0; i < NewRemoveScanReq.Count; i++)
                {
                    listBox_RemoveScanReq.Items.Add(NewRemoveScanReq[i]);
                }
            }
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            EditMode(false);
        }


        //Remove research, works for both Research Lists
        private void button_RemoveResearch_Click(object sender, RoutedEventArgs e)
        {
            //This updates the items instantly. (Not writing to disk)
            //ResearchReqs
            if (listBox_ResearchReq.SelectedItem != null)
            {
                if (mbIsOverride)
                {
                    if (Editing)
                    {
                        //Adding the Item to Remove List, so that the game will override!
                        ActiveItem.RemoveResearchRequirements.Add(listBox_ResearchReq.SelectedItem.ToString());
                        listBox_RemoveResearchReq.Items.Clear();
                        for (int i = 0; i < ActiveItem.RemoveResearchRequirements.Count; i++)
                        {
                            listBox_RemoveResearchReq.Items.Add(ActiveItem.RemoveResearchRequirements[i]);
                        }
                        //Removing the item.
                        ActiveItem.ResearchRequirements.Remove(listBox_ResearchReq.SelectedItem.ToString());
                        listBox_ResearchReq.Items.Clear();
                        for (int i = 0; i < ActiveItem.ResearchRequirements.Count; i++)
                        {
                            listBox_ResearchReq.Items.Add(ActiveItem.ResearchRequirements[i]);
                        }
                    }
                    else
                    {
                        //Adding the item to RemoveResearchList:
                        NewRemoveResearchReq.Add(listBox_ResearchReq.SelectedItem.ToString());

                        listBox_RemoveResearchReq.Items.Clear();
                        for (int i = 0; i < NewRemoveResearchReq.Count; i++)
                        {
                            listBox_RemoveResearchReq.Items.Add(NewRemoveResearchReq[i]);
                        }

                        //Removing The Item.
                        NewResearchReq.Remove(listBox_ResearchReq.SelectedItem.ToString());
                        listBox_ResearchReq.Items.Clear();
                        for (int i = 0; i < NewResearchReq.Count; i++)
                        {
                            listBox_ResearchReq.Items.Add(NewResearchReq[i]);
                        }
                    }
                }

                if(mbIsOverride == false)
                {
                    if (Editing)
                    {
                        ActiveItem.ResearchRequirements.Remove(listBox_ResearchReq.SelectedItem.ToString());
                        listBox_ResearchReq.Items.Clear();
                        for (int i = 0; i < ActiveItem.ResearchRequirements.Count; i++)
                        {
                            listBox_ResearchReq.Items.Add(ActiveItem.ResearchRequirements[i]);
                        }
                    }
                    else
                    {
                        NewResearchReq.Remove(listBox_ResearchReq.SelectedItem.ToString());
                        listBox_ResearchReq.Items.Clear();
                        for (int i = 0; i < NewResearchReq.Count; i++)
                        {
                            listBox_ResearchReq.Items.Add(NewResearchReq[i]);
                        }
                    }
                }
            }
            //Removing RemoveResearchReqs
            if (listBox_RemoveResearchReq.SelectedItem != null)
            {
                if (mbIsOverride)
                {

                    if (Editing)
                    {
                        //Add it back to ResearchReqs (Cause It's an Override, and we want to see this)
                        ActiveItem.ResearchRequirements.Add(listBox_RemoveResearchReq.SelectedItem.ToString());
                        listBox_ResearchReq.Items.Clear();
                        for (int i = 0; i < ActiveItem.ResearchRequirements.Count; i++)
                        {
                            listBox_ResearchReq.Items.Add(ActiveItem.ResearchRequirements[i]);
                        }
                        //Remove the Items
                        ActiveItem.RemoveResearchRequirements.Remove(listBox_RemoveResearchReq.SelectedItem.ToString());
                        listBox_RemoveResearchReq.Items.Clear();
                        for (int i = 0; i < ActiveItem.RemoveResearchRequirements.Count; i++)
                        {
                            listBox_RemoveResearchReq.Items.Add(ActiveItem.RemoveResearchRequirements[i]);
                        }
                    }
                    else
                    {
                        //Add the item to Research Reqs, on the new item!
                        NewResearchReq.Add(listBox_RemoveResearchReq.SelectedItem.ToString());
                        listBox_ResearchReq.Items.Clear();
                        for (int i = 0; i < NewResearchReq.Count; i++)
                        {
                            listBox_ResearchReq.Items.Add(NewResearchReq[i]);
                        }

                        //Remove the Item
                        NewRemoveResearchReq.Remove(listBox_RemoveResearchReq.SelectedItem.ToString());

                        listBox_RemoveResearchReq.Items.Clear();
                        for (int i = 0; i < NewRemoveResearchReq.Count; i++)
                        {
                            listBox_RemoveResearchReq.Items.Add(NewRemoveResearchReq[i]);
                        }
                    }
                }
                if(mbIsOverride == false)
                {
                    //We don't do anything, because we don't use remove on newly created items. This is only used on Overrides!
                }
            }
        }

        private void button_Write_Click(object sender, RoutedEventArgs e)
        {
            //This needs to be a Dialogbox that informs user about overwriting already existing file!
            MessageBox.Show("Writing to disk!");
            string AllItems = XMLSerializer.Serialize(ModWriterDataHolder.Items, false);
            File.WriteAllText(ItemsXmlPath, AllItems);
        }

        private void button_AddResearch_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_Research.SelectedItem != null)
            {
                if (Editing)
                {
                    ActiveItem.ResearchRequirements.Add(comboBox_Research.SelectedItem.ToString());
                    listBox_ResearchReq.Items.Clear();
                    for (int i = 0; i < ActiveItem.ResearchRequirements.Count; i++)
                    {
                        listBox_ResearchReq.Items.Add(ActiveItem.ResearchRequirements[i]);
                    }
                }
                else
                {
                    NewResearchReq.Add(comboBox_Research.SelectedItem.ToString());
                    listBox_ResearchReq.Items.Clear();
                    for (int i = 0; i < NewResearchReq.Count; i++)
                    {
                        listBox_ResearchReq.Items.Add(NewResearchReq[i]);
                    }
                }
            }
        }

        private void button_DeleteResearch_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_ResearchReq.SelectedItem != null)
            {
                if (Editing)
                {
                    ActiveItem.ResearchRequirements.Remove(listBox_ResearchReq.SelectedItem.ToString());
                    listBox_ResearchReq.Items.Clear();
                    for (int i = 0; i < ActiveItem.ResearchRequirements.Count; i++)
                    {
                        listBox_ResearchReq.Items.Add(ActiveItem.ResearchRequirements[i]);
                    }
                }
                else
                {
                    NewResearchReq.Remove(listBox_ResearchReq.SelectedItem.ToString());
                    listBox_ResearchReq.Items.Clear();
                    for (int i = 0; i < NewResearchReq.Count; i++)
                    {
                        listBox_ResearchReq.Items.Add(NewResearchReq[i]);
                    }
                }
            }
            if (listBox_RemoveResearchReq.SelectedItem != null)
            {
                if (Editing)
                {
                    ActiveItem.RemoveResearchRequirements.Remove(listBox_RemoveResearchReq.SelectedItem.ToString());
                    listBox_RemoveResearchReq.Items.Clear();
                    for (int i = 0; i < ActiveItem.RemoveResearchRequirements.Count; i++)
                    {
                        listBox_RemoveResearchReq.Items.Add(ActiveItem.RemoveResearchRequirements[i]);
                    }
                }
                else
                {

                }
            }
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_Items.SelectedItem != null)
            {
                for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
                {
                    if (ModWriterDataHolder.Items[i].Name == listBox_Items.SelectedItem.ToString())
                    {
                        ModWriterDataHolder.Items.RemoveAt(i);
                    }
                }
                listBox_Items.SelectedItem = null;
                listBox_Items.Items.Clear();
                for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
                {
                    listBox_Items.Items.Add(ModWriterDataHolder.Items[i].Name);
                }
            }
        }


        //This happens when we add a scanreq!
        private void button_AddScan_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_Scan.SelectedItem != null)
            {
                if (Editing)
                {
                    ActiveItem.ScanRequirements.Add(comboBox_Scan.SelectedItem.ToString());

                    listBox_ScanReq.Items.Clear();
                    for (int i = 0; i < ActiveItem.ScanRequirements.Count; i++)
                    {
                        listBox_ScanReq.Items.Add(ActiveItem.ScanRequirements[i]);
                    }
                }
                else
                {
                    NewScanReq.Add(comboBox_Scan.SelectedItem.ToString());

                    listBox_ScanReq.Items.Clear();
                    for (int i = 0; i < NewScanReq.Count; i++)
                    {
                        listBox_ScanReq.Items.Add(NewScanReq[i]);
                    }
                }
            }
        }


        //Removing Scans:
        private void button_RemoveScan_Click(object sender, RoutedEventArgs e)
        {
            //Removing Scan Reqs:
            if (listBox_ScanReq.SelectedItem != null)
            {
                if (mbIsOverride) //If the item is an override
                {
                    if (Editing) //If the item is an already existing item, that we edit!
                    {
                        //Adding the Item to Remove List, so that the game will override!
                        ActiveItem.RemoveScanRequirements.Add(listBox_ScanReq.SelectedItem.ToString());

                        listBox_RemoveScanReq.Items.Clear();
                        for (int i = 0; i < ActiveItem.RemoveResearchRequirements.Count; i++)
                        {
                            listBox_RemoveScanReq.Items.Add(ActiveItem.RemoveResearchRequirements[i]);
                        }

                        //Removing the item.
                        ActiveItem.ScanRequirements.Remove(listBox_ScanReq.SelectedItem.ToString());
                        listBox_ScanReq.Items.Clear();
                        for (int i = 0; i < ActiveItem.ScanRequirements.Count; i++)
                        {
                            listBox_ScanReq.Items.Add(ActiveItem.ScanRequirements[i]);
                        }
                    }
                    else //When the item is new we use this part:
                    {
                        //Adding the item to RemoveScanList:
                        NewRemoveScanReq.Add(listBox_ScanReq.SelectedItem.ToString());

                        listBox_RemoveScanReq.Items.Clear();
                        for (int i = 0; i < NewRemoveScanReq.Count; i++)
                        {
                            listBox_RemoveScanReq.Items.Add(NewRemoveScanReq[i]);
                        }

                        //Removing The Item.
                        NewScanReq.Remove(listBox_ScanReq.SelectedItem.ToString());
                        listBox_ScanReq.Items.Clear();
                        for (int i = 0; i < NewScanReq.Count; i++)
                        {
                            listBox_ScanReq.Items.Add(NewScanReq[i]);
                        }
                    }
                }
                
                //If its not an override :
                if (mbIsOverride == false)
                {
                    if (Editing) //Already existing Item:
                    {
                        ActiveItem.ScanRequirements.Remove(listBox_ScanReq.SelectedItem.ToString());
                        listBox_ScanReq.Items.Clear();
                        for (int i = 0; i < ActiveItem.ScanRequirements.Count; i++)
                        {
                            listBox_ScanReq.Items.Add(ActiveItem.ScanRequirements[i]);
                        }
                    }
                    else //If new item:
                    {
                        NewScanReq.Remove(listBox_ScanReq.SelectedItem.ToString());
                        listBox_ScanReq.Items.Clear();
                        for (int i = 0; i < NewScanReq.Count; i++)
                        {
                            listBox_ScanReq.Items.Add(NewScanReq[i]);
                        }
                    }
                }
            }


            //Removing RemoveScanReq -> When removing Removed Scan Entries:
            if (listBox_RemoveScanReq.SelectedItem != null)
            {
                if (mbIsOverride)
                {
                    if (Editing)
                    {
                        //Add it back to ResearchReqs (Cause It's an Override, and we want to see this)
                        ActiveItem.ScanRequirements.Add(listBox_RemoveScanReq.SelectedItem.ToString());
                        listBox_ScanReq.Items.Clear();
                        for (int i = 0; i < ActiveItem.ScanRequirements.Count; i++)
                        {
                            listBox_ScanReq.Items.Add(ActiveItem.ScanRequirements[i]);
                        }

                        //Remove the Items
                        ActiveItem.RemoveScanRequirements.Remove(listBox_RemoveScanReq.SelectedItem.ToString());
                        listBox_RemoveScanReq.Items.Clear();
                        for (int i = 0; i < ActiveItem.RemoveScanRequirements.Count; i++)
                        {
                            listBox_RemoveScanReq.Items.Add(ActiveItem.RemoveScanRequirements[i]);
                        }
                    }
                    else
                    {
                        //Add the item to Research Reqs, on the new item!
                        NewScanReq.Add(listBox_RemoveScanReq.SelectedItem.ToString());
                        listBox_ResearchReq.Items.Clear();
                        for (int i = 0; i < NewScanReq.Count; i++)
                        {
                            listBox_ResearchReq.Items.Add(NewScanReq[i]);
                        }

                        //Remove the Item
                        NewRemoveScanReq.Remove(listBox_RemoveScanReq.SelectedItem.ToString());

                        listBox_RemoveScanReq.Items.Clear();
                        for (int i = 0; i < NewRemoveScanReq.Count; i++)
                        {
                            listBox_RemoveScanReq.Items.Add(NewRemoveScanReq[i]);
                        }
                    }
                }
                if (mbIsOverride == false)
                {
                    //We don't do anything, because we don't use remove on newly created items. This is only used on Overrides!
                }
            }
        }
    }
}
