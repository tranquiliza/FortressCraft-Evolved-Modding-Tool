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
            textBox_RefRec.Text = User.Default.RefineryXmlPath;
            textBox_WritePath.Text = User.Default.WritePath;
            textBox_DataPath.Text = "Example: 'E:\\SteamLibrary\\SteamApps\\common\\FortressCraft\\64\\Default\\Data'";
            textBox_ModAuthorID.Text = User.Default.AuthorID;
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            User.Default.AuthorID = textBox_ModAuthorID.Text;
            User.Default.Save();
            ResearchReader.ReadResearchXML(User.Default.ResearchXmlPath);
            ManufacturerRecipesReader.ReadManufactoringXML(User.Default.ManufactorerXmlPath);
            ItemsReader.ReadItems(User.Default.ItemsXmlPath);
            TerrainDataReader.ReadTerrainDataEntry(User.Default.TerrainDataXmlPath);
            ManufacturerRecipesReader.ReadRefineryRecipes(User.Default.RefineryXmlPath);
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

        private void button_RefRec_Click(object sender, EventArgs e)
        {
            openFileDialog_RefRec.ShowDialog();
            User.Default.RefineryXmlPath = openFileDialog_RefRec.FileName;
            textBox_RefRec.Text = User.Default.RefineryXmlPath;
        }

        private void button_WritePath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog_WritePath.ShowDialog();
            User.Default.WritePath = folderBrowserDialog_WritePath.SelectedPath + "\\";
            textBox_WritePath.Text = User.Default.WritePath;
        }

        private void button_DataPath_Click(object sender, EventArgs e)
        {
            //This should allow users to just select the folder rather than every file we need to read!
            folderBrowserDialog_GameData.ShowDialog();
            User.Default.GameData = folderBrowserDialog_GameData.SelectedPath + "\\";

            User.Default.ResearchXmlPath = User.Default.GameData + "Research.xml";
            User.Default.ItemsXmlPath = User.Default.GameData + "Items.xml";
            User.Default.ManufactorerXmlPath = User.Default.GameData + "ManufacturerRecipes.xml";
            User.Default.TerrainDataXmlPath = User.Default.GameData + "TerrainData.xml";
            User.Default.RefineryXmlPath = User.Default.GameData + "RefineryRecipes.xml";

            //Mostly for debuggin purposes!
            textBox_ResearchXML.Text = User.Default.ResearchXmlPath;
            textBox_Manufacturer.Text = User.Default.ManufactorerXmlPath;
            textBox_ItemsPath.Text = User.Default.ItemsXmlPath;
            textBox_DataEntries.Text = User.Default.TerrainDataXmlPath;
            textBox_RefRec.Text = User.Default.RefineryXmlPath;
            textBox_WritePath.Text = User.Default.WritePath;

            textBox_DataPath.Text = User.Default.GameData;
        }
    }
}
