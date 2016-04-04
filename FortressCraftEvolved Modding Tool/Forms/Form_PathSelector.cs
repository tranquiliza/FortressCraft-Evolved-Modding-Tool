using System;
using System.Windows.Forms;
using FortressCraftEvolved_Modding_Tool.XmlLogic;

namespace FortressCraftEvolved_Modding_Tool.Forms
{
    public partial class Form_PathSelector : Form
    {
        public Form_PathSelector()
        {
            InitializeComponent();
            textBox_ResearchXML.Text = User.Default.ResearchXmlPath;
            textBox_Manufacturer.Text = User.Default.ManufactorerXmlPath;
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            User.Default.Save();
            ResearchReader.ReadResearchXML();
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
    }
}
