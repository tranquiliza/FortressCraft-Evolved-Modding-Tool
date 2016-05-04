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
using Common.ModWriter;
using Common.XmlLogic;
using Common.ModLogics;
using Common.Data;
using System.IO;

namespace FortressCraftEvolved_Modding_Tool.Forms.ModForms
{
    /// <summary>
    /// Interaction logic for UserControl_ModResearch.xaml
    /// </summary>
    public partial class UserControl_ModResearch : UserControl
    {
        ResearchDataEntry mSelectedResearch = null;
        ResearchDataEntry mActiveResearch = null;
        List<string> mTempResearchReq = null;
        List<string> mTempRemoveResearchReq = null;
        List<string> mTempScanReq = null;
        List<string> mTempRemoveScanReq = null;
        List<ProjectItemRequirement> mTempProjectItemReq = null;

        bool mbEditing;
        bool mbIsOverride;
        
        string ResearchXmlPath;
        public UserControl_ModResearch()
        {
            InitializeComponent();
            string[] Split = User.Default.ConfigFilePath.Split('\\');
            for (int i = 0; i < Split.Length - 1; i++)
            {
                ResearchXmlPath += Split[i] + "\\";
            }
            ResearchXmlPath += "Xml\\";
            ResearchXmlPath += "Research.xml";
            try
            {
                ModWriterDataHolder.ResearchEntires = XMLSerializer.Deserialize<List<ResearchDataEntry>>(File.ReadAllText(ResearchXmlPath));
            }
            catch (Exception x)
            {
                Common.Error.Log("Error: ResearchEntry Creator did not find file: " + ResearchXmlPath + x);
                //File.WriteAllText("ModRecipeCreatorError.txt", x.ToString());
            }
            RefreshResearchList();
            RefreshComboBoxes();
            comboBox_Key.Visibility = Visibility.Hidden;
            EditMode(false);
            mbEditing = false;
        }

        private void RefreshComboBoxes()
        {
            #region StaticComboBoxes
            comboBox_Key.Items.Clear();
            for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
            {
                comboBox_Key.Items.Add(DataHolder.ResearchEntries[i].Key);
            }
            for (int i = 0; i < ModWriterDataHolder.ResearchEntires.Count; i++)
            {
                comboBox_Key.Items.Add(ModWriterDataHolder.ResearchEntires[i].Key);
            }
            comboBox_ResearchCost.Items.Clear();
            for (int i = 0; i < 41; i++)
            {
                comboBox_ResearchCost.Items.Add(i);
            }
            string[] lcIconNames = ModWriterDataHolder.Sprites();
            comboBox_IconName.Items.Clear();
            for (int i = 0; i < lcIconNames.Length; i++)
            {
                comboBox_IconName.Items.Add(lcIconNames[i]);
            }
            comboBox_ProjectAmount.Items.Clear();
            for (int i = 1; i < 201; i++)
            {
                comboBox_ProjectAmount.Items.Add(i);
            }

            #endregion

            #region ResearchReq
            comboBox_ResearchReq.Items.Clear();
            for (int i = 0; i < ModWriterDataHolder.ResearchEntires.Count; i++)
            {
                comboBox_ResearchReq.Items.Add(ModWriterDataHolder.ResearchEntires[i].Name);
            }
            for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
            {
                comboBox_ResearchReq.Items.Add(DataHolder.ResearchEntries[i].Name);
            }
            #endregion

            #region ScanReqs
            comboBox_ScanReq.Items.Clear();
            for (int i = 0; i < ModWriterDataHolder.TerrainDataEntries.Count; i++)
            {
                if (ModWriterDataHolder.TerrainDataEntries[i].Values != null)
                {
                    comboBox_ScanReq.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Key);
                    for (int j = 0; j < ModWriterDataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        comboBox_ScanReq.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Values[j].Key);
                    }
                }
                else
                {
                    comboBox_ScanReq.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Key);
                }
            }
            for (int i = 0; i < DataHolder.TerrainDataEntries.Count; i++)
            {
                if (DataHolder.TerrainDataEntries[i].Values != null)
                {
                    comboBox_ScanReq.Items.Add(DataHolder.TerrainDataEntries[i].Key);
                    for (int j = 0; j < DataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        comboBox_ScanReq.Items.Add(DataHolder.TerrainDataEntries[i].Values[j].Key);
                    }
                }
                else
                {
                    comboBox_ScanReq.Items.Add(DataHolder.TerrainDataEntries[i].Key);
                }
            }
            #endregion

            #region ProjectItemsList

            comboBox_ProjectItems.Items.Clear();
            for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
            {
                comboBox_ProjectItems.Items.Add(ModWriterDataHolder.Items[i].Key);
            }
            for (int i = 0; i < DataHolder.ItemEntries.Count; i++)
            {
                comboBox_ProjectItems.Items.Add(DataHolder.ItemEntries[i].Key);
            }
            for (int i = 0; i < ModWriterDataHolder.TerrainDataEntries.Count; i++)
            {
                if (ModWriterDataHolder.TerrainDataEntries[i].Values != null)
                {
                    comboBox_ProjectItems.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Key);
                    for (int j = 0; j < ModWriterDataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        comboBox_ProjectItems.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Values[j].Key);
                    }
                }
                else
                {
                    comboBox_ProjectItems.Items.Add(ModWriterDataHolder.TerrainDataEntries[i].Key);
                }
            }
            for (int i = 0; i < DataHolder.TerrainDataEntries.Count; i++)
            {
                if (DataHolder.TerrainDataEntries[i].Values != null)
                {
                    comboBox_ProjectItems.Items.Add(DataHolder.TerrainDataEntries[i].Key);
                    for (int j = 0; j < DataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        comboBox_ProjectItems.Items.Add(DataHolder.TerrainDataEntries[i].Values[j].Key);
                    }
                }
                else
                {
                    comboBox_ProjectItems.Items.Add(DataHolder.TerrainDataEntries[i].Key);
                }
            }
            #endregion
        }

        private void RefreshResearchList()
        {
            listBox_Research.SelectedItem = null;
            listBox_Research.Items.Clear();
            for (int i = 0; i < ModWriterDataHolder.ResearchEntires.Count; i++)
            {
                listBox_Research.Items.Add(ModWriterDataHolder.ResearchEntires[i].Name);
            }
        }
        private void ResetForms()
        {
            checkBox_Delete.IsChecked = false;
            checkBox_IsOverride.IsChecked = false;
            textBox_Key.Text = string.Empty;
            textBox_Name.Text = string.Empty;
            comboBox_IconName.SelectedItem = null;
            comboBox_ResearchCost.SelectedItem = null;
            listBox_ProjectItemReqs.Items.Clear();
            textBox_PreDesc.Text = string.Empty;
            textBox_PostDesc.Text = string.Empty;
            listBox_ResearchReq.Items.Clear();
            listBox_RemoveResearchReq.Items.Clear();
            listBox_ScanReq.Items.Clear();
            listBox_RemoveScanReq.Items.Clear();
            comboBox_ProjectAmount.SelectedItem = 1;
            comboBox_ProjectItems.SelectedItem = null;
        }
        private void EditMode(bool lbIsEditing)
        {
            // lbIsEditing ? if True : if False;
            //These controls wether use can unlock and edit or not:
            listBox_Research.IsEnabled = lbIsEditing ? false : true;
            checkBox_Delete.IsEnabled = lbIsEditing ? true : false;
            checkBox_IsOverride.IsEnabled = lbIsEditing ? true : false;
            textBox_Key.IsEnabled = lbIsEditing ? true : false;
            textBox_Name.IsEnabled = lbIsEditing ? true : false;
            comboBox_IconName.IsEnabled = lbIsEditing ? true : false;
            comboBox_ResearchCost.IsEnabled = lbIsEditing ? true : false;
            listBox_ProjectItemReqs.IsEnabled = lbIsEditing ? true : false;
            textBox_PostDesc.IsEnabled = lbIsEditing ? true : false;
            textBox_PreDesc.IsEnabled = lbIsEditing ? true : false;
            listBox_ResearchReq.IsEnabled = lbIsEditing ? true : false;
            listBox_RemoveResearchReq.IsEnabled = lbIsEditing ? true : false;
            listBox_ScanReq.IsEnabled = lbIsEditing ? true : false;
            listBox_RemoveScanReq.IsEnabled = lbIsEditing ? true : false;

            //Controls for editing:
            button_Cancel.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            button_Confirm.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            button_AddResearch.Visibility = lbIsEditing ? Visibility.Hidden : Visibility.Visible;
            button_EditResearch.Visibility = lbIsEditing ? Visibility.Hidden : Visibility.Visible;
            button_DeleteResearch.Visibility = lbIsEditing ? Visibility.Hidden : Visibility.Visible;
            button_SaveToDisk.Visibility = lbIsEditing ? Visibility.Hidden : Visibility.Visible;

            comboBox_Key.Visibility = lbIsEditing ? Visibility.Hidden : Visibility.Hidden;

            comboBox_ResearchReq.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            comboBox_ScanReq.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;

            button_AddResearchReq.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            button_RemoveResearchReq.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            button_AddScanReq.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            button_RemoveScanReq.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;

            button_AddProjectItem.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            button_RemoveProjectItem.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            comboBox_ProjectItems.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;
            comboBox_ProjectAmount.Visibility = lbIsEditing ? Visibility.Visible : Visibility.Hidden;

        }

        private void button_AddResearch_Click(object sender, RoutedEventArgs e)
        {
            mbEditing = false;
            listBox_Research.SelectedItem = null;
            mTempResearchReq = new List<string>();
            mTempRemoveResearchReq = new List<string>();
            mTempScanReq = new List<string>();
            mTempRemoveScanReq = new List<string>();
            mTempProjectItemReq = new List<ProjectItemRequirement>();
            ResetForms();
            checkBox_Delete.Visibility = Visibility.Hidden;
            EditMode(true);
        }

        private void button_EditResearch_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_Research.SelectedItem != null)
            {
                EditMode(true);
                mbEditing = true;
                checkBox_Delete.Visibility = mbIsOverride ? Visibility.Visible : Visibility.Hidden;
                checkBox_IsOverride.IsEnabled = false;
                textBox_Key.IsEnabled = false;
                if (mbIsOverride)
                {
                    textBox_Key.IsEnabled = false;
                }
                mActiveResearch = mSelectedResearch;
            }
        }

        private void button_DeleteResearch_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_Research.SelectedItem != null)
            {
                for (int i = 0; i < ModWriterDataHolder.ResearchEntires.Count; i++)
                {
                    if (ModWriterDataHolder.ResearchEntires[i].Name == listBox_Research.SelectedItem.ToString())
                    {
                        ModWriterDataHolder.ResearchEntires.RemoveAt(i);
                    }
                }
            }
            RefreshResearchList();
            ResetForms();
        }

        private void button_SaveToDisk_Click(object sender, RoutedEventArgs e)
        {
            string ModdedResearch = XMLSerializer.Serialize(ModWriterDataHolder.ResearchEntires, false);
            File.WriteAllText(ResearchXmlPath, ModdedResearch);
            MessageBox.Show("Saved to disk!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RefreshNewSelection()
        {
            listBox_ResearchReq.Items.Clear();
            listBox_RemoveResearchReq.Items.Clear();
            listBox_ScanReq.Items.Clear();
            listBox_RemoveScanReq.Items.Clear();
            listBox_ProjectItemReqs.Items.Clear();

            if (mbIsOverride)
            {
                if (mbEditing)
                {
                    if (mActiveResearch.ProjectItemRequirements != null)
                    {
                        for (int i = 0; i < mActiveResearch.ProjectItemRequirements.Count; i++)
                        {
                            listBox_ProjectItemReqs.Items.Add(mActiveResearch.ProjectItemRequirements[i].ToString());
                        }
                    }
                    if (mActiveResearch.ResearchRequirements != null)
                    {
                        for (int i = 0; i < mActiveResearch.ResearchRequirements.Count; i++)
                        {
                            listBox_ResearchReq.Items.Add(mActiveResearch.ResearchRequirements[i]);
                        }
                    }
                    if (mActiveResearch.RemoveResearchRequirements != null)
                    {
                        for (int i = 0; i < mActiveResearch.RemoveResearchRequirements.Count; i++)
                        {
                            listBox_RemoveResearchReq.Items.Add(mActiveResearch.RemoveResearchRequirements[i]);
                        }
                    }
                    if (mActiveResearch.ScanRequirements != null)
                    {
                        for (int i = 0; i < mActiveResearch.ScanRequirements.Count; i++)
                        {
                            listBox_ScanReq.Items.Add(mActiveResearch.ScanRequirements[i]);
                        }
                    }
                    if (mActiveResearch.RemoveScanRequirements != null)
                    {
                        for (int i = 0; i < mActiveResearch.RemoveScanRequirements.Count; i++)
                        {
                            listBox_RemoveScanReq.Items.Add(mActiveResearch.RemoveScanRequirements[i]);
                        }
                    }
                }
                else
                {
                    if (mActiveResearch != null)
                    {
                        textBox_Key.Text = mActiveResearch.Key;
                        textBox_Name.Text = mActiveResearch.Name;
                        if (mActiveResearch.IconName != null)
                        {
                            comboBox_IconName.SelectedItem = mActiveResearch.IconName;
                        }
                        else
                        {
                            comboBox_IconName.SelectedItem = null;
                        }
                        comboBox_ResearchCost.SelectedItem = mActiveResearch.ResearchCost;
                        for (int i = 0; i < mActiveResearch.ProjectItemRequirements.Count; i++)
                        {
                            listBox_ProjectItemReqs.Items.Add(mActiveResearch.ProjectItemRequirements[i].ToString());
                        }
                        textBox_PreDesc.Text = mActiveResearch.PreDescription;
                        textBox_PostDesc.Text = mActiveResearch.PostDescription;

                        if (mActiveResearch.ResearchRequirements != null)
                        {
                            for (int i = 0; i < mActiveResearch.ResearchRequirements.Count; i++)
                            {
                                listBox_ResearchReq.Items.Add(mActiveResearch.ResearchRequirements[i]);
                            }
                        }
                        if (mActiveResearch.RemoveResearchRequirements != null)
                        {
                            for (int i = 0; i < mActiveResearch.RemoveResearchRequirements.Count; i++)
                            {
                                listBox_RemoveResearchReq.Items.Add(mActiveResearch.RemoveResearchRequirements[i]);
                            }
                        }
                        if (mActiveResearch.ScanRequirements != null)
                        {
                            for (int i = 0; i < mActiveResearch.ScanRequirements.Count; i++)
                            {
                                listBox_ScanReq.Items.Add(mActiveResearch.ScanRequirements[i]);
                            }
                        }
                        if (mActiveResearch.RemoveScanRequirements != null)
                        {
                            for (int i = 0; i < mActiveResearch.RemoveScanRequirements.Count; i++)
                            {
                                listBox_RemoveScanReq.Items.Add(mActiveResearch.RemoveScanRequirements[i]);
                            }
                        }
                    }
                }
                   
            }
            else
            {
                if (mbEditing)
                {
                    if (mActiveResearch.ProjectItemRequirements != null)
                    {
                        for (int i = 0; i < mActiveResearch.ProjectItemRequirements.Count; i++)
                        {
                            listBox_ProjectItemReqs.Items.Add(mActiveResearch.ProjectItemRequirements[i].ToString());
                        }
                    }

                    if (mActiveResearch.ResearchRequirements != null)
                    {
                        for (int i = 0; i < mActiveResearch.ResearchRequirements.Count; i++)
                        {
                            listBox_ResearchReq.Items.Add(mActiveResearch.ResearchRequirements[i]);
                        }
                    }
                    if (mActiveResearch.RemoveResearchRequirements != null)
                    {
                        for (int i = 0; i < mActiveResearch.RemoveResearchRequirements.Count; i++)
                        {
                            listBox_RemoveResearchReq.Items.Add(mActiveResearch.RemoveResearchRequirements[i]);
                        }
                    }
                    if (mActiveResearch.ScanRequirements != null)
                    {
                        for (int i = 0; i < mActiveResearch.ScanRequirements.Count; i++)
                        {
                            listBox_ScanReq.Items.Add(mActiveResearch.ScanRequirements[i]);
                        }
                    }
                    if (mActiveResearch.RemoveScanRequirements != null)
                    {
                        for (int i = 0; i < mActiveResearch.RemoveScanRequirements.Count; i++)
                        {
                            listBox_RemoveScanReq.Items.Add(mActiveResearch.RemoveScanRequirements[i]);
                        }
                    }
                }
                else
                {
                    if (mTempProjectItemReq != null)
                    {
                        for (int i = 0; i < mTempProjectItemReq.Count; i++)
                        {
                            listBox_ProjectItemReqs.Items.Add(mTempProjectItemReq[i].ToString());
                        }
                    }
                    if (mTempResearchReq != null)
                    {
                        for (int i = 0; i < mTempResearchReq.Count; i++)
                        {
                            listBox_ResearchReq.Items.Add(mTempResearchReq[i]);
                        }
                    }
                    if (mTempRemoveResearchReq != null)
                    {
                        for (int i = 0; i < mTempRemoveResearchReq.Count; i++)
                        {
                            listBox_RemoveResearchReq.Items.Add(mTempRemoveResearchReq[i]);
                        }
                    }
                    if (mTempScanReq != null)
                    {
                        for (int i = 0; i < mTempScanReq.Count; i++)
                        {
                            listBox_ScanReq.Items.Add(mTempScanReq[i]);
                        }
                    }
                    if (mTempRemoveScanReq != null)
                    {
                        for (int i = 0; i < mTempRemoveScanReq.Count; i++)
                        {
                            listBox_RemoveScanReq.Items.Add(mTempRemoveScanReq[i]);
                        }
                    }
                }
            }
        }

        private void listBox_Research_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_Research.SelectedItem != null)
            {
                for (int i = 0; i < ModWriterDataHolder.ResearchEntires.Count; i++)
                {
                    if (ModWriterDataHolder.ResearchEntires[i].Name == listBox_Research.SelectedItem.ToString())
                    {
                        mSelectedResearch = ModWriterDataHolder.ResearchEntires[i];
                    }
                }

                bool lbDelete;
                bool.TryParse(mSelectedResearch.Delete, out lbDelete);
                checkBox_Delete.IsChecked = lbDelete;

                mbIsOverride = mSelectedResearch.IsOverride;
                checkBox_IsOverride.IsChecked = mbIsOverride;
                comboBox_Key.Visibility = Visibility.Hidden;
                checkBox_Delete.Visibility = mbIsOverride ? Visibility.Visible : Visibility.Hidden;
                textBox_Key.Text = mSelectedResearch.Key;
                textBox_Name.Text = mSelectedResearch.Name;
                comboBox_IconName.SelectedItem = mSelectedResearch.IconName;
                comboBox_ResearchCost.SelectedItem = mSelectedResearch.ResearchCost;
                listBox_ProjectItemReqs.Items.Clear();
                for (int i = 0; i < mSelectedResearch.ProjectItemRequirements.Count; i++)
                {
                    listBox_ProjectItemReqs.Items.Add(mSelectedResearch.ProjectItemRequirements[i].ToString());
                }
                textBox_PreDesc.Text = mSelectedResearch.PreDescription;
                textBox_PostDesc.Text = mSelectedResearch.PostDescription;
                listBox_ResearchReq.Items.Clear();
                listBox_RemoveResearchReq.Items.Clear();
                listBox_ScanReq.Items.Clear();
                listBox_RemoveScanReq.Items.Clear();
                if (mSelectedResearch.ResearchRequirements != null)
                {
                    for (int i = 0; i < mSelectedResearch.ResearchRequirements.Count; i++)
                    {
                        listBox_ResearchReq.Items.Add(mSelectedResearch.ResearchRequirements[i]);
                    }
                }
                if (mSelectedResearch.RemoveResearchRequirements != null)
                {
                    for (int i = 0; i < mSelectedResearch.RemoveResearchRequirements.Count; i++)
                    {
                        listBox_RemoveResearchReq.Items.Add(mSelectedResearch.RemoveResearchRequirements[i]);
                    }
                }
                if (mSelectedResearch.ScanRequirements != null)
                {
                    for (int i = 0; i < mSelectedResearch.ScanRequirements.Count; i++)
                    {
                        listBox_ScanReq.Items.Add(mSelectedResearch.ScanRequirements[i]);
                    }
                }
                if (mSelectedResearch.RemoveScanRequirements != null)
                {
                    for (int i = 0; i < mSelectedResearch.RemoveScanRequirements.Count; i++)
                    {
                        listBox_RemoveScanReq.Items.Add(mSelectedResearch.RemoveScanRequirements[i]);
                    }
                }
            }
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            EditMode(false);
            mbEditing = false;
            ResetForms();
            listBox_Research.SelectedItem = null;
            checkBox_Delete.Visibility = Visibility.Visible;
        }

        private void button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (mbEditing) //We're editing something, doesn't matter if its an override or not.
            {
                if (mActiveResearch != null)
                {
                    mActiveResearch.IsOverride = mbIsOverride;
                    bool lbDelete = checkBox_Delete.IsChecked.Value;
                    if (lbDelete)
                    {
                        mActiveResearch.Delete = "true";
                    }
                    else
                    {
                        mActiveResearch.Delete = null;
                    }
                    mActiveResearch.Name = textBox_Name.Text;
                    if (comboBox_IconName.SelectedItem != null)
                    {
                        mActiveResearch.IconName = comboBox_IconName.SelectedItem.ToString();
                    }
                    if (comboBox_ResearchCost.SelectedItem != null)
                    {
                        int lnResearchCost = 0;
                        int.TryParse(comboBox_ResearchCost.SelectedItem.ToString(), out lnResearchCost);
                        mActiveResearch.ResearchCost = lnResearchCost;
                    }
                    mActiveResearch.PreDescription = textBox_PreDesc.Text;
                    mActiveResearch.PostDescription = textBox_PostDesc.Text;
                }
            }
            else // New Items:
            {
                if (mbIsOverride) //new item that is an Override 
                {
                    if (mActiveResearch != null)
                    {
                        mActiveResearch.IsOverride = mbIsOverride;
                        bool lbDelete = checkBox_Delete.IsChecked.Value;
                        if (lbDelete)
                        {
                            mActiveResearch.Delete = "true";
                        }
                        else
                        {
                            mActiveResearch.Delete = null;
                        }
                        mActiveResearch.Name = textBox_Name.Text;
                        if (comboBox_IconName.SelectedItem != null)
                        {
                            mActiveResearch.IconName = comboBox_IconName.SelectedItem.ToString();
                        }
                        if (comboBox_ResearchCost.SelectedItem != null)
                        {
                            int lnResearchCost = 0;
                            int.TryParse(comboBox_ResearchCost.SelectedItem.ToString(), out lnResearchCost);
                            mActiveResearch.ResearchCost = lnResearchCost;
                        }
                        mActiveResearch.PreDescription = textBox_PreDesc.Text;
                        mActiveResearch.PostDescription = textBox_PostDesc.Text;
                    }
                    ModWriterDataHolder.ResearchEntires.Add(mActiveResearch);
                }
                else // We make the new item, with the values!
                {
                    ResearchDataEntry Holder = new ResearchDataEntry();
                    Holder.IsOverride = false;
                    Holder.Delete = null;
                    Holder.Key = User.Default.AuthorID + "." + textBox_Key.Text;
                    Holder.Name = textBox_Name.Text;
                    if (comboBox_IconName.SelectedItem != null)
                    {
                        Holder.IconName = comboBox_IconName.SelectedItem.ToString();
                    }
                    if (comboBox_ResearchCost.SelectedItem != null)
                    {
                        int lnResearchCost = 0;
                        int.TryParse(comboBox_ResearchCost.SelectedItem.ToString(), out lnResearchCost);
                        Holder.ResearchCost = lnResearchCost;
                    }
                    Holder.PreDescription = textBox_PreDesc.Text;
                    Holder.PostDescription = textBox_PostDesc.Text;
                    Holder.ResearchRequirements = mTempResearchReq;
                    Holder.ScanRequirements = mTempScanReq;
                    Holder.ProjectItemRequirements = mTempProjectItemReq;
                    ModWriterDataHolder.ResearchEntires.Add(Holder);
                }
            }
            EditMode(false);
            mbEditing = false;
            listBox_Research.SelectedItem = null;
            RefreshResearchList();
        }

        private void checkBox_IsOverride_Checked(object sender, RoutedEventArgs e)
        {
            mbIsOverride = true;
            checkBox_Delete.Visibility = Visibility.Visible;
            comboBox_Key.Visibility = Visibility.Visible;
        }

        private void checkBox_IsOverride_Unchecked(object sender, RoutedEventArgs e)
        {
            mbIsOverride = false;
            checkBox_Delete.Visibility = Visibility.Hidden;
            comboBox_Key.Visibility = Visibility.Hidden;
            ResetForms();
        }

        private void comboBox_Key_SelectionChanged(object sender, SelectionChangedEventArgs e) //This is only visible when mbIsOverride is true. 
        {
         //   ResetForms();
            if (comboBox_Key.SelectedItem != null)
            {
                for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
                {
                    if (comboBox_Key.SelectedItem.ToString() == DataHolder.ResearchEntries[i].Key)
                    {
                        mActiveResearch = new ResearchDataEntry();
                        mActiveResearch.IsOverride = true; 
                        mActiveResearch.Key = DataHolder.ResearchEntries[i].Key;
                        mActiveResearch.Name = DataHolder.ResearchEntries[i].Name;
                        mActiveResearch.IconName = DataHolder.ResearchEntries[i].IconName;
                        mActiveResearch.ResearchCost = DataHolder.ResearchEntries[i].ResearchCost;
                        mActiveResearch.PreDescription = DataHolder.ResearchEntries[i].PreDescription;
                        mActiveResearch.PostDescription = DataHolder.ResearchEntries[i].PostDescription;
                        mActiveResearch.ScanRequirements = DataHolder.ResearchEntries[i].ScanRequirements;
                        mActiveResearch.ResearchRequirements = DataHolder.ResearchEntries[i].ResearchRequirements;
                        mActiveResearch.ProjectItemRequirements = new List<ProjectItemRequirement>();
                        for (int j = 0; j < DataHolder.ResearchEntries[i].ProjectItemRequirements.Count; j++)
                        {
                            ProjectItemRequirement Holder = new ProjectItemRequirement();
                            Holder.Amount = DataHolder.ResearchEntries[i].ProjectItemRequirements[j].Amount;
                            Holder.Key = DataHolder.ResearchEntries[i].ProjectItemRequirements[j].Key;
                            mActiveResearch.ProjectItemRequirements.Add(Holder);
                        }
                    }
                }
                for (int i = 0; i < ModWriterDataHolder.ResearchEntires.Count; i++)
                {
                    if (comboBox_Key.SelectedItem.ToString() == ModWriterDataHolder.ResearchEntires[i].Key)
                    {
                        mActiveResearch = ModWriterDataHolder.ResearchEntires[i];
                    }
                }
            }
            RefreshNewSelection();
        }

        private void button_AddResearchReq_Click(object sender, RoutedEventArgs e)
        {
            string lSelectedResearchKey = string.Empty;
            if (comboBox_ResearchReq.SelectedItem != null)
            {
                for (int i = 0; i < ModWriterDataHolder.ResearchEntires.Count; i++)
                {
                    if (ModWriterDataHolder.ResearchEntires[i].Name == comboBox_ResearchReq.SelectedItem.ToString())
                    {
                        lSelectedResearchKey = ModWriterDataHolder.ResearchEntires[i].Key;
                    }
                }
                for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
                {
                    if (DataHolder.ResearchEntries[i].Name == comboBox_ResearchReq.SelectedItem.ToString())
                    {
                        lSelectedResearchKey = DataHolder.ResearchEntries[i].Key;
                    }
                }
                if (mbEditing)
                {
                    mActiveResearch.ResearchRequirements.Add(lSelectedResearchKey);
                }
                else // New item:
                {
                    if (mbIsOverride)
                    {
                        mActiveResearch.ResearchRequirements.Add(lSelectedResearchKey);
                    }
                    else
                    {
                        mTempResearchReq.Add(lSelectedResearchKey);
                    }
                }
            }
            RefreshNewSelection();
        }

        private void button_AddScanReq_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_ScanReq.SelectedItem != null)
            {
                if (mbEditing)
                {
                    mActiveResearch.ScanRequirements.Add(comboBox_ScanReq.SelectedItem.ToString());
                }
                else
                {
                    if (mbIsOverride)
                    {
                        mActiveResearch.ScanRequirements.Add(comboBox_ScanReq.SelectedItem.ToString());
                    }
                    else
                    {
                        mTempScanReq.Add(comboBox_ScanReq.SelectedItem.ToString());
                    }
                }
                RefreshNewSelection();
            }
        }

        private void button_AddProjectItem_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_ProjectItems.SelectedItem != null && comboBox_ProjectAmount.SelectedItem != null)
            {
                ProjectItemRequirement Holder = new ProjectItemRequirement();
                Holder.Key = comboBox_ProjectItems.SelectedItem.ToString();
                int lAmount;
                int.TryParse(comboBox_ProjectAmount.SelectedItem.ToString(), out lAmount);
                Holder.Amount = lAmount;
                Holder.Delete = "false";
                if (mbEditing)
                {
                    mActiveResearch.ProjectItemRequirements.Add(Holder);
                }
                else //New Item:
                {
                    if (mbIsOverride)
                    {
                        mActiveResearch.ProjectItemRequirements.Add(Holder);
                    }
                    else
                    {
                        mTempProjectItemReq.Add(Holder);
                    }
                }
                RefreshNewSelection();
            }
        }

        private void button_RemoveProjectItem_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_ProjectItemReqs.SelectedItem != null)
            {
                if (mbEditing)
                {
                    if (mbIsOverride)
                    {
                        for (int i = 0; i < mActiveResearch.ProjectItemRequirements.Count; i++)
                        {
                            if (mActiveResearch.ProjectItemRequirements[i].ToString() == listBox_ProjectItemReqs.SelectedItem.ToString())
                            {
                                if (mActiveResearch.ProjectItemRequirements[i].Delete == "true")
                                {
                                    mActiveResearch.ProjectItemRequirements[i].Delete = "false";
                                }
                                else if (mActiveResearch.ProjectItemRequirements[i].Delete == null)
                                {
                                    mActiveResearch.ProjectItemRequirements.RemoveAt(i);
                                }
                                else
                                {
                                    mActiveResearch.ProjectItemRequirements[i].Delete = "true";
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < mActiveResearch.ProjectItemRequirements.Count; i++)
                        {
                            if (mActiveResearch.ProjectItemRequirements[i].ToString() == listBox_ProjectItemReqs.SelectedItem.ToString())
                            {
                                mActiveResearch.ProjectItemRequirements.RemoveAt(i);
                            }
                        }
                    }
                }
                else // New Item
                {
                    if (mbIsOverride)
                    {
                        for (int i = 0; i < mActiveResearch.ProjectItemRequirements.Count; i++)
                        {
                            if (mActiveResearch.ProjectItemRequirements[i].ToString() == listBox_ProjectItemReqs.SelectedItem.ToString())
                            {
                                if (mActiveResearch.ProjectItemRequirements[i].ToString() == listBox_ProjectItemReqs.SelectedItem.ToString())
                                {
                                    if (mActiveResearch.ProjectItemRequirements[i].Delete == "true")
                                    {
                                        mActiveResearch.ProjectItemRequirements[i].Delete = "false";
                                    }
                                    else
                                    {
                                        mActiveResearch.ProjectItemRequirements[i].Delete = "true";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < mTempProjectItemReq.Count; i++)
                        {
                            if (mTempProjectItemReq[i].Key == listBox_ProjectItemReqs.SelectedItem.ToString())
                            {
                                mTempProjectItemReq.RemoveAt(i);
                            }
                        }
                    }
                }
                RefreshNewSelection();
            }
        }

        private void button_RemoveResearchReq_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_ResearchReq.SelectedItem != null)
            {
                if (mbEditing)
                {
                    if (mbIsOverride)
                    {
                        mActiveResearch.RemoveResearchRequirements.Add(listBox_ResearchReq.SelectedItem.ToString());
                        mActiveResearch.ResearchRequirements.Remove(listBox_ResearchReq.SelectedItem.ToString());
                    }
                    else
                    {
                        mActiveResearch.ResearchRequirements.Remove(listBox_ResearchReq.SelectedItem.ToString());
                    }
                }
                else
                {
                    if (mbIsOverride)
                    {
                        mActiveResearch.RemoveResearchRequirements.Add(listBox_ResearchReq.SelectedItem.ToString());
                        mActiveResearch.ResearchRequirements.Remove(listBox_ResearchReq.SelectedItem.ToString());
                    }
                    else // New Item:
                    {
                        mTempResearchReq.Remove(listBox_ResearchReq.SelectedItem.ToString());
                    }
                }
            }

            if (listBox_RemoveResearchReq.SelectedItem != null)
            {
                if (mbEditing)
                {
                    if (mbIsOverride)
                    {
                        mActiveResearch.ResearchRequirements.Add(listBox_RemoveResearchReq.SelectedItem.ToString());
                        mActiveResearch.RemoveResearchRequirements.Remove(listBox_RemoveResearchReq.SelectedItem.ToString());
                    }
                    else
                    {
                        mActiveResearch.RemoveResearchRequirements.Remove(listBox_RemoveResearchReq.SelectedItem.ToString());
                    }
                }
                else
                {
                    if (mbIsOverride)
                    {
                        mActiveResearch.ResearchRequirements.Add(listBox_RemoveResearchReq.SelectedItem.ToString());
                        mActiveResearch.RemoveResearchRequirements.Remove(listBox_RemoveResearchReq.SelectedItem.ToString());
                    }
                    else
                    {
                        //This should never be needed, cause we never add an item to this!
                    }
                }
            }
            RefreshNewSelection();
        }

        private void button_RemoveScanReq_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_ScanReq.SelectedItem != null)
            {
                if (mbEditing)
                {
                    if (mbIsOverride)
                    {
                        mActiveResearch.RemoveScanRequirements.Add(listBox_ScanReq.SelectedItem.ToString());
                        mActiveResearch.ScanRequirements.Remove(listBox_ScanReq.SelectedItem.ToString());
                    }
                    else
                    {
                        mActiveResearch.ScanRequirements.Remove(listBox_ScanReq.SelectedItem.ToString());
                    }
                }
                else
                {
                    if (mbIsOverride)
                    {
                        mActiveResearch.RemoveScanRequirements.Add(listBox_ScanReq.SelectedItem.ToString());
                        mActiveResearch.ScanRequirements.Remove(listBox_ScanReq.SelectedItem.ToString());
                    }
                    else
                    {
                        mTempScanReq.Remove(listBox_ScanReq.SelectedItem.ToString());
                    }
                }
            }

            if (listBox_RemoveScanReq.SelectedItem != null)
            {
                if (mbEditing)
                {
                    if (mbIsOverride)
                    {
                        mActiveResearch.ScanRequirements.Add(listBox_RemoveScanReq.SelectedItem.ToString());
                        mActiveResearch.RemoveScanRequirements.Remove(listBox_RemoveScanReq.SelectedItem.ToString());
                    }
                    else
                    {
                        mTempRemoveScanReq.Remove(listBox_RemoveScanReq.SelectedItem.ToString());
                    }
                }
                else
                {
                    if (mbIsOverride)
                    {
                        mActiveResearch.ScanRequirements.Add(listBox_RemoveScanReq.SelectedItem.ToString());
                        mActiveResearch.RemoveScanRequirements.Remove(listBox_RemoveScanReq.SelectedItem.ToString());
                    }
                    else
                    {
                        //This should never be used?
                    }
                }
            }
            RefreshNewSelection();
        }
    }
}