using System.Windows.Controls;
using FortressCraftEvolved_Modding_Tool.Data;


namespace FortressCraftEvolved_Modding_Tool.Forms
{
    /// <summary>
    /// Interaction logic for UserControl_Manufacturer.xaml
    /// </summary>
    public partial class UserControl_Manufacturer : UserControl
    {
        bool CanCraftAnywhere = false;
        int Tier = -1;
        public UserControl_Manufacturer()
        {
            InitializeComponent();
            for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
            {
                listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedName);
            }
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listBox_ManufacturerEntries.Items.Clear();
            string SearchString = Searchbar.Text.ToLower();
            if (SearchString == "" || SearchString == "search")
            {
                for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
                {
                    if (DataHolder.ManufacturerEntries[i].CanCraftAnywhere == CanCraftAnywhere)
                    {
                        if (DataHolder.ManufacturerEntries[i].Tier == Tier)
                        {
                            listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedName);
                        }
                        else
                        {
                            if (Tier == -1)
                            {
                                listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedName);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
                {
                    if (SearchString == DataHolder.ManufacturerEntries[i].CraftedName.ToLower()) //SearchString == Items[i].CraftedName.ToLower()
                    {
                        if (DataHolder.ManufacturerEntries[i].CanCraftAnywhere == CanCraftAnywhere)
                        {
                            if (DataHolder.ManufacturerEntries[i].Tier == Tier)
                            {
                                listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedName);
                            }
                            else
                            {
                                if (Tier == -1)
                                {
                                    listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedName);
                                }
                            }
                        }
                        break;
                    }
                    string[] SearchStrings = DataHolder.ManufacturerEntries[i].CraftedName.Split(' ');
                    for (int j = 0; j < SearchStrings.Length; j++)
                    {
                        if (SearchString.ToLower() == SearchStrings[j].ToLower()) //SearchString.ToLower() == SearchStrings[j].ToLower()
                        {
                            if (DataHolder.ManufacturerEntries[i].CanCraftAnywhere == CanCraftAnywhere)
                            {
                                if (DataHolder.ManufacturerEntries[i].Tier == Tier)
                                {
                                    listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedName);
                                }
                                else
                                {
                                    if (Tier == -1)
                                    {
                                        listBox_ManufacturerEntries.Items.Add(DataHolder.ManufacturerEntries[i].CraftedName);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
