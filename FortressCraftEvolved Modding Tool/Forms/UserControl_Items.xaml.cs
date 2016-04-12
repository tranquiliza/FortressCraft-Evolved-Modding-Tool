using System.Windows.Controls;
using Common.Data;
using Common.GameLogics;

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
            textBlock_ItemId.Text = "";
            textBlock_Key.Text = "";
            textBlock_Name.Text = "";
            textBlock_Plural.Text = "";
            textBlock_Type.Text = "";
            textBlock_Hidden.Text = "";
            textBlock_Object.Text = "";
            textBlock_Sprite.Text = "";
            textBlock_Category.Text = "";
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

            if (SelectedItem.Plural == null)
            {
                label_Plural.Content = "";
            }
            else
            {
                label_Plural.Content = "Plural:";
            }
            textBlock_Plural.Text = SelectedItem.Plural;

            textBlock_Type.Text = SelectedItem.Type.ToString();
            textBlock_Hidden.Text = SelectedItem.Hidden.ToString();
            textBlock_Object.Text = SelectedItem.Object.ToString();
            textBlock_Sprite.Text = SelectedItem.Sprite;
            textBlock_Category.Text = SelectedItem.Category.ToString();


            if (SelectedItem.ResearchRequirements.Count == 0)
            {
                label_ResearchReq.Content = "";
            }
            else
            {
                label_ResearchReq.Content = "Research Requirements:";
            }
            listBox_ResearchReq.Items.Clear();
            for (int i = 0; i < SelectedItem.ResearchRequirements.Count; i++)
            {
                listBox_ResearchReq.Items.Add(SelectedItem.ResearchRequirements[i]);
            }

            if (SelectedItem.ScanRequirements.Count == 0)
            {
                label_ScanReq.Content = "";
            }
            else
            {
                label_ScanReq.Content = "Scan Requirements";
            }
            listBox_ScanReq.Items.Clear();
            for (int i = 0; i < SelectedItem.ScanRequirements.Count; i++)
            {
                listBox_ScanReq.Items.Add(SelectedItem.ScanRequirements[i]);
            }


            listBox_ItemUsedIn.Items.Clear();
            for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
            {
                for (int j = 0; j < DataHolder.ManufacturerEntries[i].Costs.Count; j++)
                {
                    if (SelectedItem.Key == DataHolder.ManufacturerEntries[i].Costs[j].Key)
                    {
                        listBox_ItemUsedIn.Items.Add(DataHolder.ManufacturerEntries[i].CraftedKey);
                    }
                }
            }
        }
    }
}
