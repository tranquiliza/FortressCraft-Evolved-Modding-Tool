namespace FortressCraftEvolved_Modding_Tool.Forms
{
    partial class Form_ModCreator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Close = new System.Windows.Forms.Button();
            this.textBox_ModName = new System.Windows.Forms.TextBox();
            this.label_ModName = new System.Windows.Forms.Label();
            this.numericUpDown_ModVersion = new System.Windows.Forms.NumericUpDown();
            this.label_ModVersion = new System.Windows.Forms.Label();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.checkBox_IsLocalMod = new System.Windows.Forms.CheckBox();
            this.checkBox_IsServerOnlyMod = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ModVersion)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(140, 148);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 0;
            this.button_Close.Text = "Ok";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // textBox_ModName
            // 
            this.textBox_ModName.Location = new System.Drawing.Point(12, 25);
            this.textBox_ModName.Name = "textBox_ModName";
            this.textBox_ModName.Size = new System.Drawing.Size(203, 20);
            this.textBox_ModName.TabIndex = 1;
            // 
            // label_ModName
            // 
            this.label_ModName.AutoSize = true;
            this.label_ModName.Location = new System.Drawing.Point(12, 9);
            this.label_ModName.Name = "label_ModName";
            this.label_ModName.Size = new System.Drawing.Size(59, 13);
            this.label_ModName.TabIndex = 2;
            this.label_ModName.Text = "Mod Name";
            // 
            // numericUpDown_ModVersion
            // 
            this.numericUpDown_ModVersion.Location = new System.Drawing.Point(154, 51);
            this.numericUpDown_ModVersion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_ModVersion.Name = "numericUpDown_ModVersion";
            this.numericUpDown_ModVersion.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown_ModVersion.TabIndex = 3;
            this.numericUpDown_ModVersion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label_ModVersion
            // 
            this.label_ModVersion.AutoSize = true;
            this.label_ModVersion.Location = new System.Drawing.Point(12, 53);
            this.label_ModVersion.Name = "label_ModVersion";
            this.label_ModVersion.Size = new System.Drawing.Size(66, 13);
            this.label_ModVersion.TabIndex = 4;
            this.label_ModVersion.Text = "Mod Version";
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(12, 148);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 5;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // checkBox_IsLocalMod
            // 
            this.checkBox_IsLocalMod.AutoSize = true;
            this.checkBox_IsLocalMod.Location = new System.Drawing.Point(12, 69);
            this.checkBox_IsLocalMod.Name = "checkBox_IsLocalMod";
            this.checkBox_IsLocalMod.Size = new System.Drawing.Size(81, 17);
            this.checkBox_IsLocalMod.TabIndex = 6;
            this.checkBox_IsLocalMod.Text = "IsLocalMod";
            this.checkBox_IsLocalMod.UseVisualStyleBackColor = true;
            // 
            // checkBox_IsServerOnlyMod
            // 
            this.checkBox_IsServerOnlyMod.AutoSize = true;
            this.checkBox_IsServerOnlyMod.Location = new System.Drawing.Point(12, 92);
            this.checkBox_IsServerOnlyMod.Name = "checkBox_IsServerOnlyMod";
            this.checkBox_IsServerOnlyMod.Size = new System.Drawing.Size(107, 17);
            this.checkBox_IsServerOnlyMod.TabIndex = 7;
            this.checkBox_IsServerOnlyMod.Text = "IsServerOnlyMod";
            this.checkBox_IsServerOnlyMod.UseVisualStyleBackColor = true;
            // 
            // Form_ModCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 183);
            this.ControlBox = false;
            this.Controls.Add(this.checkBox_IsServerOnlyMod);
            this.Controls.Add(this.checkBox_IsLocalMod);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.label_ModVersion);
            this.Controls.Add(this.numericUpDown_ModVersion);
            this.Controls.Add(this.label_ModName);
            this.Controls.Add(this.textBox_ModName);
            this.Controls.Add(this.button_Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_ModCreator";
            this.Text = "ModData";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ModVersion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.TextBox textBox_ModName;
        private System.Windows.Forms.Label label_ModName;
        private System.Windows.Forms.NumericUpDown numericUpDown_ModVersion;
        private System.Windows.Forms.Label label_ModVersion;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.CheckBox checkBox_IsLocalMod;
        private System.Windows.Forms.CheckBox checkBox_IsServerOnlyMod;
    }
}