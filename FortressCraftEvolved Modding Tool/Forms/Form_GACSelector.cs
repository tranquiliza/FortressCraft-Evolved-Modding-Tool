using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.XmlLogic;

namespace FortressCraftEvolved_Modding_Tool.Forms
{
    public partial class Form_GACSelector : Form
    {
        public Form_GACSelector()
        {
            InitializeComponent();
            textBox_GACPath.Text = User.Default.GACXmlPath;
        }

        private void button_GACPathSelect_Click(object sender, EventArgs e)
        {
            openFileDialog_GAC.ShowDialog();
            User.Default.GACXmlPath = openFileDialog_GAC.FileName;
            textBox_GACPath.Text = User.Default.GACXmlPath;
            GACReader.ReadGAC(User.Default.GACXmlPath);
            User.Default.Save();
            Close();
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            GACReader.ReadGAC(User.Default.GACXmlPath);
            User.Default.Save();
            this.Close();
        }
    }
}
