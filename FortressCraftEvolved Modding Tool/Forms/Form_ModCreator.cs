﻿using System;
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
        public Form_ModCreator()
        {
            InitializeComponent();
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            if (textBox_ModName.Text != "")
            {
                ModCreator.ModName = textBox_ModName.Text;
                ModCreator.Version = numericUpDown_ModVersion.Value.ToString();
            }
            this.Close();
        }
    }
}