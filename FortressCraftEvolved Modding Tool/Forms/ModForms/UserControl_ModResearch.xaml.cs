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

            #region .ComboBoxes.
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
            #endregion

            RefreshResearchList();
            RefreshComboBoxes();

            comboBox_Key.Visibility = Visibility.Hidden;

            EditMode(false);
            mbEditing = false;
        }

        private void RefreshComboBoxes()
        {
            comboBox_Key.Items.Clear();
            for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
            {
                comboBox_Key.Items.Add(DataHolder.ResearchEntries[i].Key);
            }
            for (int i = 0; i < ModWriterDataHolder.ResearchEntires.Count; i++)
            {
                comboBox_Key.Items.Add(ModWriterDataHolder.ResearchEntires[i].Key);
            }
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

        }

        private void button_AddResearch_Click(object sender, RoutedEventArgs e)
        {
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

        }

        private void RefreshNewSelection()
        {
            listBox_ResearchReq.Items.Clear();
            listBox_RemoveResearchReq.Items.Clear();
            listBox_ScanReq.Items.Clear();
            listBox_RemoveScanReq.Items.Clear();

            if (mbIsOverride)
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
                    listBox_ProjectItemReqs.Items.Clear();
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
            else
            {
                mTempResearchReq = null;
                mTempRemoveResearchReq = null;
                mTempScanReq = null;
                mTempRemoveScanReq = null;
                //TODO
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
                for (int i = 0; i < mSelectedResearch.ResearchRequirements.Count; i++)
                {
                    listBox_ResearchReq.Items.Add(mSelectedResearch.ResearchRequirements[i]);
                }
                for (int i = 0; i < mSelectedResearch.RemoveResearchRequirements.Count; i++)
                {
                    listBox_RemoveResearchReq.Items.Add(mSelectedResearch.RemoveResearchRequirements[i]);
                }
                for (int i = 0; i < mSelectedResearch.ScanRequirements.Count; i++)
                {
                    listBox_ScanReq.Items.Add(mSelectedResearch.ScanRequirements[i]);
                }
                for (int i = 0; i < mSelectedResearch.RemoveScanRequirements.Count; i++)
                {
                    listBox_RemoveScanReq.Items.Add(mSelectedResearch.RemoveScanRequirements[i]);
                }

            }
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            EditMode(false);
            mbEditing = false;
            ResetForms();
            checkBox_Delete.Visibility = Visibility.Visible;
        }

        private void button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            EditMode(false);
            mbEditing = false;
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

        private void comboBox_Key_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         //   ResetForms();
            if (comboBox_Key.SelectedItem != null)
            {
                for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
                {
                    if (comboBox_Key.SelectedItem.ToString() == DataHolder.ResearchEntries[i].Key)
                    {
                        mActiveResearch = new ResearchDataEntry();
                        //mActiveResearch = DataHolder.ResearchEntries[i];
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
    }
}
