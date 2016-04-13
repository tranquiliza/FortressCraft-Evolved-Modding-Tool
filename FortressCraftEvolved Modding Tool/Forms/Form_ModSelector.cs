using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.ModWriter;

namespace FortressCraftEvolved_Modding_Tool.Forms
{
    public partial class Form_ModSelector : Form
    {
        public Form_ModSelector()
        {
            InitializeComponent();
        }

        private void button_Select_Click(object sender, EventArgs e)
        {
            openFileDialog_ModConfig.ShowDialog();
            User.Default.ConfigFilePath = openFileDialog_ModConfig.FileName;
            User.Default.Save();
            this.Close();
        }
    }
}
