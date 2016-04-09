namespace FortressCraftEvolved_Modding_Tool.Forms
{
    partial class Form_GACSelector
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
            this.button_GACPathSelect = new System.Windows.Forms.Button();
            this.textBox_GACPath = new System.Windows.Forms.TextBox();
            this.label_GACPath = new System.Windows.Forms.Label();
            this.openFileDialog_GAC = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // button_GACPathSelect
            // 
            this.button_GACPathSelect.Location = new System.Drawing.Point(724, 23);
            this.button_GACPathSelect.Name = "button_GACPathSelect";
            this.button_GACPathSelect.Size = new System.Drawing.Size(75, 23);
            this.button_GACPathSelect.TabIndex = 0;
            this.button_GACPathSelect.Text = "Select";
            this.button_GACPathSelect.UseVisualStyleBackColor = true;
            this.button_GACPathSelect.Click += new System.EventHandler(this.button_GACPathSelect_Click);
            // 
            // textBox_GACPath
            // 
            this.textBox_GACPath.Location = new System.Drawing.Point(12, 25);
            this.textBox_GACPath.Name = "textBox_GACPath";
            this.textBox_GACPath.ReadOnly = true;
            this.textBox_GACPath.Size = new System.Drawing.Size(706, 20);
            this.textBox_GACPath.TabIndex = 1;
            // 
            // label_GACPath
            // 
            this.label_GACPath.AutoSize = true;
            this.label_GACPath.Location = new System.Drawing.Point(12, 9);
            this.label_GACPath.Name = "label_GACPath";
            this.label_GACPath.Size = new System.Drawing.Size(54, 13);
            this.label_GACPath.TabIndex = 2;
            this.label_GACPath.Text = "GAC Path";
            // 
            // openFileDialog_GAC
            // 
            this.openFileDialog_GAC.FileName = "Generic AutoCrafter";
            // 
            // Form_GACSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(811, 67);
            this.ControlBox = false;
            this.Controls.Add(this.label_GACPath);
            this.Controls.Add(this.textBox_GACPath);
            this.Controls.Add(this.button_GACPathSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_GACSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generic AutoCrafter Selector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_GACPathSelect;
        private System.Windows.Forms.TextBox textBox_GACPath;
        private System.Windows.Forms.Label label_GACPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog_GAC;
    }
}