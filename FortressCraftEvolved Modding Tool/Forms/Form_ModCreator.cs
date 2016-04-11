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
    public partial class Form_ModCreator : Form
    {
        ModConfiguration NewMod = new ModConfiguration();
        public Form_ModCreator()
        {
            InitializeComponent();
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            if (textBox_ModName.Text != "")
            {
                NewMod.Name = textBox_ModName.Text;
                string KeyFriendlyName = NewMod.Name.Replace(" ", "");
                NewMod.Id = User.Default.AuthorID + "." + KeyFriendlyName;
                NewMod.Version = numericUpDown_ModVersion.Value.ToString();
                NewMod.IsLocalMod = checkBox_IsLocalMod.Checked;
                NewMod.IsServerOnlyMod = checkBox_IsServerOnlyMod.Checked;
            }
            ModWriterDataHolder.Config = NewMod;
            Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
