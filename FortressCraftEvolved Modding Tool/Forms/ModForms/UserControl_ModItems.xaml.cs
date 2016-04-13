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
            ModWriterDataHolder.Items = XMLSerializer.Deserialize<List<ItemEntry>>(File.ReadAllText(ItemsXmlPath));
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
            textBlock_UnknownHint.Text = "";
            textBlock_MaxDurability.Text = "";
            textBlock_MaxStack.Text = "";
            textBlock_ItemAction.Text = "";
            textBlock_ActionParameter.Text = "";
            textBlock_DecomposeValue.Text = "";
        }

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
                textBlock_UnknownHint.Text = SelectedItem.UnknownHint;
                textBlock_MaxDurability.Text = SelectedItem.MaxDurability.ToString();
                textBlock_MaxStack.Text = SelectedItem.MaxStack.ToString();
                textBlock_ItemAction.Text = SelectedItem.ItemAction.ToString();
                textBlock_ActionParameter.Text = SelectedItem.ActionParameter;
                textBlock_DecomposeValue.Text = SelectedItem.DecomposeValue.ToString();
            }
        }
    }
}
