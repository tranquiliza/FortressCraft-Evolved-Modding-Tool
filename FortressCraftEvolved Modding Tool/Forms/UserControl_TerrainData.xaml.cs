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
using Common.Data;

namespace FortressCraftEvolved_Modding_Tool.Forms
{
    /// <summary>
    /// Interaction logic for UserControl_TerrainData.xaml
    /// </summary>
    public partial class UserControl_TerrainData : UserControl
    {
        //This form has issues with low resolutions!
        string SearchString;
        public UserControl_TerrainData()
        {
            InitializeComponent();
            textBlock_Key.Text = "";
            textBlock_Name.Text = "";
            textBlock_ResearchValue.Text = "";
            textBlock_LayerType.Text = "";
            textBlock_TopTexture.Text = "";
            textBlock_SideTexture.Text = "";
            textBlock_BottomTexture.Text = "";
            textBlock_GUITexture.Text = "";
            textBlock_isSolid.Text = "";
            textBlock_isTransparant.Text = "";
            textBlock_isHollow.Text = "";
            textBlock_isGlass.Text = "";
            textBlock_isPassable.Text = "";
            textBlock_hasObject.Text = "";
            textBlock_hasEntity.Text = "";
            textBlock_AudioWalkType.Text = "";
            textBlock_AudioBuildType.Text = "";
            textBlock_AudioDestroyType.Text = "";
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchString = Searchbar.Text.ToLower();
            //Clear Selection: 
            listBox_TerrainData.SelectedItem = null;
            listBox_TerrainData.Items.Clear();

            if (SearchString == "" || SearchString == "search")
            {
                for (int i = 0; i < DataHolder.TerrainDataEntries.Count; i++)
                {
                    listBox_TerrainData.Items.Add(DataHolder.TerrainDataEntries[i].Name);
                }
            }
            else
            {
                for (int i = 0; i < DataHolder.TerrainDataEntries.Count; i++)
                {
                    if (SearchString == DataHolder.TerrainDataEntries[i].Name.ToLower())
                    {
                        listBox_TerrainData.Items.Add(DataHolder.TerrainDataEntries[i].Name);
                        break;
                    }
                    string[] ItemStrings = DataHolder.TerrainDataEntries[i].Name.Split(' ');
                    for (int j = 0; j < ItemStrings.Length; j++)
                    {
                        if (SearchString == ItemStrings[j].ToLower())
                        {
                            listBox_TerrainData.Items.Add(DataHolder.TerrainDataEntries[i].Name);
                            break;
                        }
                    }
                }
            }
        }

        private void listBox_TerrainData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_TerrainData.SelectedItem == null)
            {
                return;
            }
            for (int i = 0; i < DataHolder.TerrainDataEntries.Count; i++)
            {
                if (DataHolder.TerrainDataEntries[i].Name == listBox_TerrainData.SelectedItem.ToString())
                {
                    textBlock_Key.Text = DataHolder.TerrainDataEntries[i].Key;
                    textBlock_Name.Text = DataHolder.TerrainDataEntries[i].Name;
                    textBlock_ResearchValue.Text = DataHolder.TerrainDataEntries[i].ResearchValue.ToString();
                    textBlock_LayerType.Text = DataHolder.TerrainDataEntries[i].LayerType.ToString();
                    textBlock_TopTexture.Text = DataHolder.TerrainDataEntries[i].TopTexture.ToString();
                    textBlock_SideTexture.Text = DataHolder.TerrainDataEntries[i].SideTexture.ToString();
                    textBlock_BottomTexture.Text = DataHolder.TerrainDataEntries[i].BottomTexture.ToString();
                    textBlock_GUITexture.Text = DataHolder.TerrainDataEntries[i].GUITexture.ToString();
                    textBlock_isSolid.Text = DataHolder.TerrainDataEntries[i].isSolid.ToString();
                    textBlock_isTransparant.Text = DataHolder.TerrainDataEntries[i].isTransparent.ToString();
                    textBlock_isHollow.Text = DataHolder.TerrainDataEntries[i].isHollow.ToString();
                    textBlock_isGlass.Text = DataHolder.TerrainDataEntries[i].isGlass.ToString();
                    textBlock_isPassable.Text = DataHolder.TerrainDataEntries[i].isPassable.ToString();
                    textBlock_hasObject.Text = DataHolder.TerrainDataEntries[i].hasObject.ToString();
                    textBlock_hasEntity.Text = DataHolder.TerrainDataEntries[i].hasEntity.ToString();
                    textBlock_AudioWalkType.Text = DataHolder.TerrainDataEntries[i].AudioWalkType.ToString();
                    textBlock_AudioBuildType.Text = DataHolder.TerrainDataEntries[i].AudioBuildType.ToString();
                    textBlock_AudioDestroyType.Text = DataHolder.TerrainDataEntries[i].AudioDestroyType.ToString();

                    listBox_tags.Items.Clear();
                    for (int j = 0; j < DataHolder.TerrainDataEntries[i].tags.Count; j++)
                    {
                        listBox_tags.Items.Add(DataHolder.TerrainDataEntries[i].tags[j].ToString());
                    }
                    listBox_Values.Items.Clear();
                    for (int j = 0; j < DataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        listBox_Values.Items.Add(DataHolder.TerrainDataEntries[i].Values[j].Text());
                    }
                }
            }
        }
    }
}
