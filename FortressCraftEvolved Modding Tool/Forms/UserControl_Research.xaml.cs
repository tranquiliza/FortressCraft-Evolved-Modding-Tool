using System;
using System.Windows.Controls;
using Common.Data;
using Common.GameLogics;

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
            ResearchDataEntry SelectedEntry = null; //In hindsight this should have been used everywhere!!
            
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
                    //Using this later for Unlocked items!
                    SelectedEntry = DataHolder.ResearchEntries[i];
                }
            }

            //UI Work Woo!
            if (SelectedEntry == null)
            {
                return;
            }

            textBlock_Key.Text = SelectedEntry.Key;
            textBlock_Name.Text = SelectedEntry.Name;
            textBlock_IconName.Text = SelectedEntry.IconName;
            if (SelectedEntry.ResearchCost > 0)
            {
                label_ResearchCost.Content = "Research Cost:";
                textBlock_ReseachCost.Text = SelectedEntry.ResearchCost.ToString();
            }
            else
            {
                label_ResearchCost.Content = "";
                textBlock_ReseachCost.Text = "Requires Lab";
            }
            textBlock_PreDesc.Text = SelectedEntry.PreDescription;
            textBlock_PostDesc.Text = SelectedEntry.PostDescription;

            if (SelectedEntry.ProjectItemRequirements.Count == 0)
            {
                label_PodRequirements.Content = "";
            }
            else
            {
                label_PodRequirements.Content = "Pod Requirements:";
            }

            //Pod Requirements
            //Clear the Listbox, so we dont get multiples
            listBox_ResearchPods.Items.Clear();
            for (int i = 0; i < SelectedEntry.ProjectItemRequirements.Count; i++)
            {
                listBox_ResearchPods.Items.Add(SelectedEntry.ProjectItemRequirements[i].Readable());
            }

            if (SelectedEntry.ResearchRequirements.Count == 0)
            {
                label_ResearchReq.Content = "";
            }
            else
            {
                label_ResearchReq.Content = "Research Requirements:";
            }

            //Research Req
            //Clear the Listbox, so we dont get multiples
            listBox_ResearchReq.Items.Clear();
            for (int j = 0; j < SelectedEntry.ResearchRequirements.Count; j++)
            {
                for (int k = 0; k < DataHolder.ResearchEntries.Count; k++)
                {
                    //A bit complex: Basicly shows the Friendly Name instead of the ResearchEntry <Key>Text</Key>
                    if (DataHolder.ResearchEntries[k].Key == SelectedEntry.ResearchRequirements[j])
                    {
                        listBox_ResearchReq.Items.Add(DataHolder.ResearchEntries[k].Name);
                    }
                }
                //(Debug Line)
                //listBox_ResearchReq.Items.Add("   - Key:" + DataHolder.ResearchEntries[i].ResearchRequirements[j]);
            }


            //Scan requirements
            if (SelectedEntry.ScanRequirements.Count == 0)
            {
                label_ScanReq.Content = "";
            }
            else
            {
                label_ScanReq.Content = "Scan Requirements:";
            }

            //Clear the list.
            listBox_ScanReq.Items.Clear();
            for (int i = 0; i < SelectedEntry.ScanRequirements.Count; i++)
            {
                listBox_ScanReq.Items.Add(SelectedEntry.ScanRequirements[i]);
            }


            //Unlocked Items;
            //Clear the list:
            listBox_ItemsUnlocked.Items.Clear();
            for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
            {
                for (int j = 0; j < DataHolder.ManufacturerEntries[i].ResearchRequirement.Count; j++)
                {
                    if (DataHolder.ManufacturerEntries[i].ResearchRequirement[j] == SelectedEntry.Key)
                    {
                        listBox_ItemsUnlocked.Items.Add(DataHolder.ManufacturerEntries[i].CraftedName);
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
            if (UserSearchString == "" || UserSearchString == "search")
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
