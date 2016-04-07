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
        string SearchString;
        public UserControl_TerrainData()
        {
            InitializeComponent();
            //for (int i = 0; i < DataHolder.TerrainDataEntries.Count; i++)
            //{
            //    listBox_TerrainData.Items.Add(DataHolder.TerrainDataEntries[i].Name);
            //}
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
    }
}
