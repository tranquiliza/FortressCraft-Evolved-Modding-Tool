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
using FortressCraftEvolved_Modding_Tool.Data;
using FortressCraftEvolved_Modding_Tool.GameLogics;

namespace FortressCraftEvolved_Modding_Tool.Forms
{
    /// <summary>
    /// Interaction logic for UserControl_Items.xaml
    /// </summary>
    public partial class UserControl_Items : UserControl
    {
        private string SearchString;
        private ItemEntry SelectedItem = null;
        public UserControl_Items()
        {
            InitializeComponent();
            //for (int i = 0; i < DataHolder.ItemEntries.Count; i++)
            //{
            //    listBox_ItemEntries.Items.Add(DataHolder.ItemEntries[i].Name);
            //}
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchString = Searchbar.Text.ToLower();
            //Clear Selection: 
            listBox_ItemEntries.SelectedItem = null;
            listBox_ItemEntries.Items.Clear();

            if (SearchString == "" || SearchString == "search")
            {
                for (int i = 0; i < DataHolder.ItemEntries.Count; i++)
                {
                    listBox_ItemEntries.Items.Add(DataHolder.ItemEntries[i].Name);
                }
            }
            else
            {
                for (int i = 0; i < DataHolder.ItemEntries.Count; i++)
                {
                    if (SearchString == DataHolder.ItemEntries[i].Name.ToLower())
                    {
                        listBox_ItemEntries.Items.Add(DataHolder.ItemEntries[i].Name);
                        break;
                    }
                    string[] ItemStrings = DataHolder.ItemEntries[i].Name.Split(' ');
                    for (int j = 0; j < ItemStrings.Length; j++)
                    {
                        if (SearchString == ItemStrings[j].ToLower())
                        {
                            listBox_ItemEntries.Items.Add(DataHolder.ItemEntries[i].Name);
                            break;
                        }
                    }
                }
            }
        }

        private void listBox_ItemEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_ItemEntries.SelectedItem == null)
            {
                return;
            }
            //Selected Item:
            for (int i = 0; i < DataHolder.ItemEntries.Count; i++)
            {
                if (DataHolder.ItemEntries[i].Name == listBox_ItemEntries.SelectedItem.ToString())
                {
                    SelectedItem = DataHolder.ItemEntries[i];
                }
            }

            textBlock_ItemId.Text = SelectedItem.ItemID.ToString();
            textBlock_Key.Text = SelectedItem.Key.ToString();
            textBlock_Name.Text = SelectedItem.Name;
            textBlock_Plural.Text = SelectedItem.Plural;
            textBlock_Type.Text = SelectedItem.Type;
            textBlock_Hidden.Text = SelectedItem.Hidden.ToString();
            textBlock_Object.Text = SelectedItem.Object;
            textBlock_Sprite.Text = SelectedItem.Sprite;
            textBlock_Category.Text = SelectedItem.Category;


            listBox_ResearchReq.Items.Clear();
            for (int i = 0; i < SelectedItem.ResearchRequirements.Count; i++)
            {
                listBox_ResearchReq.Items.Add(SelectedItem.ResearchRequirements[i]);
            }

            listBox_ScanReq.Items.Clear();
            for (int i = 0; i < SelectedItem.ScanRequirements.Count; i++)
            {
                listBox_ScanReq.Items.Add(SelectedItem.ScanRequirements[i].Value);
            }
        }
    }
}
