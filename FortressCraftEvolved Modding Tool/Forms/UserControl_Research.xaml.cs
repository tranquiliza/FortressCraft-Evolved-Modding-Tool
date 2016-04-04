using System;
using System.Windows.Controls;
using FortressCraftEvolved_Modding_Tool.Data;

namespace FortressCraftEvolved_Modding_Tool.Forms
{
    /// <summary>
    /// Interaction logic for UserControl_Research.xaml
    /// </summary>
    public partial class UserControl_Research : UserControl
    {
        public UserControl_Research()
        {
            InitializeComponent();
            listBox_ResearchEntries.Items.Clear();
            //Add all our entries to the listbox
            for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
            {
                listBox_ResearchEntries.Items.Add(DataHolder.ResearchEntries[i].Name);
            }
            //Make sure user doesn't see textBlocks!
            textBlock_Key.Text = "";
            textBlock_Name.Text = "";
            textBlock_IconName.Text = "";
            textBlock_ReseachCost.Text = "";
            textBlock_PreDesc.Text = "";
            textBlock_PostDesc.Text = "";
        }
        
        //When We select an item from the list:
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //When we select something, find the one we selected!
            for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
            {
                //This is in place to make sure we don't hit a NRE when we use the searchbar!
                if (listBox_ResearchEntries.SelectedItem == null)
                {
                    break;
                }
                //If the name matches:
                if (DataHolder.ResearchEntries[i].Name == listBox_ResearchEntries.SelectedItem.ToString())
                {
                    textBlock_Key.Text = DataHolder.ResearchEntries[i].Key;
                    textBlock_Name.Text = DataHolder.ResearchEntries[i].Name;
                    textBlock_IconName.Text = DataHolder.ResearchEntries[i].IconName;

                    if (DataHolder.ResearchEntries[i].ResearchCost > 0)
                    {
                        label_ResearchCost.Content = "Research Cost:";
                        textBlock_ReseachCost.Text = DataHolder.ResearchEntries[i].ResearchCost.ToString();
                    }
                    else
                    {
                        label_ResearchCost.Content = "";
                        textBlock_ReseachCost.Text = "Requires Lab";
                    }

                    textBlock_PreDesc.Text = DataHolder.ResearchEntries[i].PreDescription;
                    textBlock_PostDesc.Text = DataHolder.ResearchEntries[i].PostDescription;


                    //Clear the Listbox, so we dont get multiples
                    listBox_ResearchPods.Items.Clear();
                    for (int j = 0; j < DataHolder.ResearchEntries[i].LabResearchItems.Count; j++)
                    {
                        listBox_ResearchPods.Items.Add(DataHolder.ResearchEntries[i].LabResearchItems[j].Text());
                    }

                    //Clear the Listbox, so we dont get multiples
                    listBox_ResearchReq.Items.Clear();
                    for (int j = 0; j < DataHolder.ResearchEntries[i].ResearchRequirements.Count; j++)
                    {
                        for (int k = 0; k < DataHolder.ResearchEntries.Count; k++)
                        {
                            //A bit complex: Basicly shows the Friendly Name instead of the ResearchEntry <Key>Text</Key>
                            if (DataHolder.ResearchEntries[k].Key == DataHolder.ResearchEntries[i].ResearchRequirements[j])
                            {
                                listBox_ResearchReq.Items.Add(DataHolder.ResearchEntries[k].Name);
                            }
                        }
                        //(Debug Line)
                        //listBox_ResearchReq.Items.Add("   - Key:" + DataHolder.ResearchEntries[i].ResearchRequirements[j]);
                    }
                }
            }
        }


        //Search Bar
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Maker sure the selectedItem is set to null, to prevent crash!
            listBox_ResearchEntries.SelectedItem = null;

            //Clear the list
            listBox_ResearchEntries.Items.Clear();
            String UserSearchString = SearchBar.Text.ToLower();
            if (UserSearchString == "")
            {
                for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
                {
                    listBox_ResearchEntries.Items.Add(DataHolder.ResearchEntries[i].Name);
                }
            }
            else
            {
                for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
                {
                    if (UserSearchString == DataHolder.ResearchEntries[i].Name.ToLower())
                    {
                        listBox_ResearchEntries.Items.Add(DataHolder.ResearchEntries[i].Name);
                        break;
                    }
                    String[] SearchStrings = DataHolder.ResearchEntries[i].Name.Split(' ');
                    for (int j = 0; j < SearchStrings.Length; j++)
                    {
                        if (UserSearchString == SearchStrings[j].ToLower())
                        {
                            listBox_ResearchEntries.Items.Add(DataHolder.ResearchEntries[i].Name);
                        }
                    }
                }
            }
        }
    }
}
