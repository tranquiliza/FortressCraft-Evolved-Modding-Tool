using System;
using System.Windows.Forms;
using Common.XmlLogic;

namespace FortressCraftEvolved_Modding_Tool.Forms
{
    public partial class Form_PathSelector : Form
    {
        public Form_PathSelector()
        {
            InitializeComponent();
            textBox_ResearchXML.Text = User.Default.ResearchXmlPath;
            textBox_Manufacturer.Text = User.Default.ManufactorerXmlPath;
            textBox_ItemsPath.Text = User.Default.ItemsXmlPath;
            textBox_DataEntries.Text = User.Default.TerrainDataXmlPath;
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            User.Default.Save();
            ResearchReader.ReadResearchXML(User.Default.ResearchXmlPath);
            this.Close();
        }

        private void button_SelectResearch_Click(object sender, EventArgs e)
        {
            openFileDialog_Research.ShowDialog();
            User.Default.ResearchXmlPath = openFileDialog_Research.FileName;
            textBox_ResearchXML.Text = User.Default.ResearchXmlPath;
        }

        private void button_Manufacturer_Click(object sender, EventArgs e)
        {
            openFileDialog_Manufacturer.ShowDialog();
            User.Default.ManufactorerXmlPath = openFileDialog_Manufacturer.FileName;
            textBox_Manufacturer.Text = User.Default.ManufactorerXmlPath;

        }

        private void button_SelectItems_Click(object sender, EventArgs e)
        {
            openFileDialog_Items.ShowDialog();
            User.Default.ItemsXmlPath = openFileDialog_Items.FileName;
            textBox_ItemsPath.Text = User.Default.ItemsXmlPath;
        }

        private void button_DataEntrySelect_Click(object sender, EventArgs e)
        {
            openFileDialog_DataEntry.ShowDialog();
            User.Default.TerrainDataXmlPath = openFileDialog_DataEntry.FileName;
            textBox_DataEntries.Text = User.Default.TerrainDataXmlPath;
        }
    }
}
