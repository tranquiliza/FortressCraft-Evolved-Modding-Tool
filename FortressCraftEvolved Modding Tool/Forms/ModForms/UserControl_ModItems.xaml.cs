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

namespace FortressCraftEvolved_Modding_Tool.Forms.ModForms
{
    /// <summary>
    /// Interaction logic for UserControl_ModItems.xaml
    /// </summary>
    public partial class UserControl_ModItems : UserControl
    {
        ItemEntry ActiveItem = null;
        bool Editing;
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

            string ItemsXmlPath = XmlFilePath + "Items.xml";
            try
            {
                ModWriterDataHolder.Items = XMLSerializer.Deserialize<List<ItemEntry>>(File.ReadAllText(ItemsXmlPath));

            }
            catch (Exception x)
            {
                File.WriteAllText("ModItemsCreatorError.txt", x.ToString());
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
            for (int i = 0; i < ModWriterDataHolder.Sprites().Length; i++)
            {
                comboBox_Sprites.Items.Add(ModWriterDataHolder.Sprites()[i]);
            }

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
                textBlock_IsOverride.Text = SelectedItem.IsOverride;
                textBlock_ItemId.Text = SelectedItem.ItemID.ToString();
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

                listBox_ResearchReq.Items.Clear();
                for (int i = 0; i < SelectedItem.ResearchRequirements.Count; i++)
                {
                    listBox_ResearchReq.Items.Add(SelectedItem.ResearchRequirements[i]);
                }

                listBox_ScanReq.Items.Clear();
                for (int i = 0; i < SelectedItem.ScanRequirements.Count; i++)
                {
                    listBox_ScanReq.Items.Add(SelectedItem.ScanRequirements[i]);
                }

                listBox_RemoveResearchReq.Items.Clear();
                for (int i = 0; i < SelectedItem.RemoveResearchRequirements.Count; i++)
                {
                    listBox_RemoveResearchReq.Items.Add(SelectedItem.RemoveResearchRequirements[i]);
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
                listBox_Items.Visibility = Visibility.Hidden;
                //textBlock_IsOverride.Visibility = Visibility.Hidden;
                //textBlock_ItemId.Visibility = Visibility.Hidden;
                //textBlock_Key.Visibility = Visibility.Hidden;
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

                button_Confirm.Visibility = Visibility.Visible;
                button_EditItem.Visibility = Visibility.Hidden;
                button_AddItem.Visibility = Visibility.Hidden;

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
                //Visibility
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
            }
        }

        private void button_AddItem_Click(object sender, RoutedEventArgs e)
        {
            Editing = false;
            listBox_Items.Visibility = Visibility.Hidden;
            textBlock_IsOverride.Visibility = Visibility.Hidden;
            textBlock_ItemId.Visibility = Visibility.Hidden;
            textBlock_Key.Visibility = Visibility.Hidden;
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

            button_Confirm.Visibility = Visibility.Visible;
            button_EditItem.Visibility = Visibility.Hidden;
            button_AddItem.Visibility = Visibility.Hidden;
        }


        //Confirm button for Both new Item and Edit of an existing Item!
        private void button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (Editing == true)
            {
                //Before we do this, add values to ActiveItem
                for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
                {
                    if (ModWriterDataHolder.Items[i].Key == ActiveItem.Key)
                    {
                        //ModWriterDataHolder.Items[i] = ActiveItem;
                    }
                }
            }
            else
            {
                //Before we do this create new ItemEntry instance, and add values to this!
                //ModWriterDataHolder.Items.Add();
            }

            listBox_Items.Visibility = Visibility.Visible;
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
        }
    }
}
