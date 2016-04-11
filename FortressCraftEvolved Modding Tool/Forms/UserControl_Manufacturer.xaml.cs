using System.Windows.Controls;
using Common.Data;
using Common.GameLogics;


namespace FortressCraftEvolved_Modding_Tool.Forms
{
    /// <summary>
    /// Interaction logic for UserControl_Manufacturer.xaml
    /// </summary>
    public partial class UserControl_Manufacturer : UserControl
    {
        bool CanCraftAnywhere = false;
        int Tier = -1;
        string SearchString = "";
        public UserControl_Manufacturer()
        {
            InitializeComponent();
            listBox_ManufacturerEntries.Items.Clear();
            for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
            {
                listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedKey);
            }
            textBlock_Category.Text = "";
            textBlock_CraftAnywhere.Text = "";
            textBlock_CraftedAmount.Text = "";
            textBlock_CraftedName.Text = "";
            textBlock_Desc.Text = "";
            textBlock_Hint.Text = "";
            textBlock_Key.Text = "";
            textBlock_ResearchCost.Text = "";
            textBlock_Tier.Text = "";
        }

        private void SearchFunction()
        {
            listBox_ManufacturerEntries.Items.Clear();
            listBox_ManufacturerEntries.SelectedItem = null;
            if (SearchString == "" || SearchString == "search")
            {
                for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
                {
                    if (DataHolder.ManufacturerEntries[i].CanCraftAnywhere == CanCraftAnywhere)
                    {
                        if (DataHolder.ManufacturerEntries[i].Tier == Tier)
                        {
                            listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedKey);
                        }
                        else
                        {
                            if (Tier == -1)
                            {
                                listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedKey);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
                {
                    if (SearchString == DataHolder.ManufacturerEntries[i].CraftedKey.ToLower()) //SearchString == Items[i].CraftedName.ToLower()
                    {
                        if (DataHolder.ManufacturerEntries[i].CanCraftAnywhere == CanCraftAnywhere)
                        {
                            if (DataHolder.ManufacturerEntries[i].Tier == Tier)
                            {
                                listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedKey);
                            }
                            else
                            {
                                if (Tier == -1)
                                {
                                    listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedKey);
                                }
                            }
                        }
                        break;
                    }
                    string[] SearchStrings = DataHolder.ManufacturerEntries[i].CraftedKey.Split(' ');
                    for (int j = 0; j < SearchStrings.Length; j++)
                    {
                        if (SearchString.ToLower() == SearchStrings[j].ToLower()) //SearchString.ToLower() == SearchStrings[j].ToLower()
                        {
                            if (DataHolder.ManufacturerEntries[i].CanCraftAnywhere == CanCraftAnywhere)
                            {
                                if (DataHolder.ManufacturerEntries[i].Tier == Tier)
                                {
                                    listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedKey);
                                }
                                else
                                {
                                    if (Tier == -1)
                                    {
                                        listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedKey);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //Search Bar
        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchString = Searchbar.Text.ToLower();
            SearchFunction();
        }

        private void listBox_ManufacturerEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CraftData SelectedEntry = null;
            if (listBox_ManufacturerEntries.SelectedItem == null)
            {
                return;
            }

            for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
            {
                if (DataHolder.ManufacturerEntries[i].CraftedKey == listBox_ManufacturerEntries.SelectedItem.ToString())
                {
                    if (DataHolder.ManufacturerEntries[i].CanCraftAnywhere == CanCraftAnywhere)
                    {
                        SelectedEntry = DataHolder.ManufacturerEntries[i];
                    }
                }
            }
            if (SelectedEntry == null)
            {
                return;
            }
            //Textblock stuff!
            if (SelectedEntry.Category != null)
            {
                textBlock_Category.Text = SelectedEntry.Category;
            }
            else
            {
                textBlock_Category.Text = "";
            }

            textBlock_CraftAnywhere.Text = SelectedEntry.CanCraftAnywhere.ToString();
            textBlock_CraftedAmount.Text = SelectedEntry.CraftedAmount.ToString();
            textBlock_CraftedName.Text = SelectedEntry.CraftedKey;
            textBlock_Desc.Text = SelectedEntry.Description;
            textBlock_Hint.Text = SelectedEntry.Hint;
            textBlock_Key.Text = SelectedEntry.Key;

            if (SelectedEntry.ResearchCost == 0)
            {
                textBlock_ResearchCost.Text = "Free";
                label_ResearchReq.Content = "";
            }
            else
            {
                textBlock_ResearchCost.Text = SelectedEntry.ResearchCost.ToString();
                label_ResearchReq.Content = "Research Cost:";
            }

            textBlock_Tier.Text = SelectedEntry.Tier.ToString();
            //Clear list and Add ResearchReqs
            if (SelectedEntry.ResearchRequirements.Count == 0)
            {
                label_ResearchReq.Content = "";
            }
            else
            {
                label_ResearchReq.Content = "Research Requirements:";
            }
            listBox_ResearchReq.Items.Clear();
            for (int i = 0; i < SelectedEntry.ResearchRequirements.Count; i++)
            {
                listBox_ResearchReq.Items.Add(SelectedEntry.ResearchRequirements[i]);
            }

            //Clear list and Add ScanReqs
            if (SelectedEntry.ScanRequirements.Count == 0)
            {
                label_ScanReq.Content = "";
            }
            else
            {
                label_ScanReq.Content = "Scan Requirements:";
            }
            listBox_ScanReq.Items.Clear();
            for (int i = 0; i < SelectedEntry.ScanRequirements.Count; i++)
            {
                listBox_ScanReq.Items.Add(SelectedEntry.ScanRequirements[i]);
            }

            //Clear List and Add crafting costs
            if (SelectedEntry.Costs.Count == 0)
            {
                label_CraftingCost.Content = "";
            }
            else
            {
                label_CraftingCost.Content = "Crafting Costs:";
            }
            listBox_CraftingCost.Items.Clear();
            for (int i = 0; i < SelectedEntry.Costs.Count; i++)
            {
                listBox_CraftingCost.Items.Add(SelectedEntry.Costs[i].Text());
            }
        }

        private void radioButton_ManufacturerPlant_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            CanCraftAnywhere = false;
            SearchFunction();
        }

        private void radioButton_CraftAnywhere_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            CanCraftAnywhere = true;
            SearchFunction();
        }

        private void comboBox_TierSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_TierSelect.SelectedItem == null)
            {
                return;
            }
            switch (comboBox_TierSelect.SelectedIndex)
            {
                case 0:
                    Tier = 0;
                    SearchFunction();
                    break;
                case 1:
                    Tier = 1;
                    SearchFunction();
                    break;
                case 2:
                    Tier = 2;
                    SearchFunction();
                    break;
                case 3:
                    Tier = 3;
                    SearchFunction();
                    break;
                case 4:
                    Tier = 4;
                    SearchFunction();
                    break;
                case 5:
                    Tier = -1;
                    SearchFunction();
                    break;
                default:
                    Tier = -1;
                    SearchFunction();
                    break;
            }
        }
    }
}
