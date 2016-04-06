namespace FortressCraftEvolved_Modding_Tool.Forms
{
    partial class Form_PathSelector
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
            this.button_SelectResearch = new System.Windows.Forms.Button();
            this.button_Manufacturer = new System.Windows.Forms.Button();
            this.textBox_ResearchXML = new System.Windows.Forms.TextBox();
            this.textBox_Manufacturer = new System.Windows.Forms.TextBox();
            this.label_ResearchXML = new System.Windows.Forms.Label();
            this.label_Manufacturer = new System.Windows.Forms.Label();
            this.button_Close = new System.Windows.Forms.Button();
            this.openFileDialog_Research = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog_Manufacturer = new System.Windows.Forms.OpenFileDialog();
            this.label_Items = new System.Windows.Forms.Label();
            this.textBox_ItemsPath = new System.Windows.Forms.TextBox();
            this.button_SelectItems = new System.Windows.Forms.Button();
            this.openFileDialog_Items = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // button_SelectResearch
            // 
            this.button_SelectResearch.Location = new System.Drawing.Point(480, 23);
            this.button_SelectResearch.Name = "button_SelectResearch";
            this.button_SelectResearch.Size = new System.Drawing.Size(75, 23);
            this.button_SelectResearch.TabIndex = 0;
            this.button_SelectResearch.Text = "Select";
            this.button_SelectResearch.UseVisualStyleBackColor = true;
            this.button_SelectResearch.Click += new System.EventHandler(this.button_SelectResearch_Click);
            // 
            // button_Manufacturer
            // 
            this.button_Manufacturer.Location = new System.Drawing.Point(480, 62);
            this.button_Manufacturer.Name = "button_Manufacturer";
            this.button_Manufacturer.Size = new System.Drawing.Size(75, 23);
            this.button_Manufacturer.TabIndex = 1;
            this.button_Manufacturer.Text = "Select";
            this.button_Manufacturer.UseVisualStyleBackColor = true;
            this.button_Manufacturer.Click += new System.EventHandler(this.button_Manufacturer_Click);
            // 
            // textBox_ResearchXML
            // 
            this.textBox_ResearchXML.Location = new System.Drawing.Point(12, 25);
            this.textBox_ResearchXML.Name = "textBox_ResearchXML";
            this.textBox_ResearchXML.ReadOnly = true;
            this.textBox_ResearchXML.Size = new System.Drawing.Size(462, 20);
            this.textBox_ResearchXML.TabIndex = 2;
            // 
            // textBox_Manufacturer
            // 
            this.textBox_Manufacturer.Location = new System.Drawing.Point(12, 64);
            this.textBox_Manufacturer.Name = "textBox_Manufacturer";
            this.textBox_Manufacturer.ReadOnly = true;
            this.textBox_Manufacturer.Size = new System.Drawing.Size(462, 20);
            this.textBox_Manufacturer.TabIndex = 3;
            // 
            // label_ResearchXML
            // 
            this.label_ResearchXML.AutoSize = true;
            this.label_ResearchXML.Location = new System.Drawing.Point(12, 9);
            this.label_ResearchXML.Name = "label_ResearchXML";
            this.label_ResearchXML.Size = new System.Drawing.Size(78, 13);
            this.label_ResearchXML.TabIndex = 4;
            this.label_ResearchXML.Text = "Research Path";
            // 
            // label_Manufacturer
            // 
            this.label_Manufacturer.AutoSize = true;
            this.label_Manufacturer.Location = new System.Drawing.Point(12, 48);
            this.label_Manufacturer.Name = "label_Manufacturer";
            this.label_Manufacturer.Size = new System.Drawing.Size(95, 13);
            this.label_Manufacturer.TabIndex = 5;
            this.label_Manufacturer.Text = "Manufactorer Path";
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(480, 130);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 6;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // openFileDialog_Research
            // 
            this.openFileDialog_Research.FileName = "Research";
            // 
            // openFileDialog_Manufacturer
            // 
            this.openFileDialog_Manufacturer.FileName = "ManufacturerRecipes";
            // 
            // label_Items
            // 
            this.label_Items.AutoSize = true;
            this.label_Items.Location = new System.Drawing.Point(12, 87);
            this.label_Items.Name = "label_Items";
            this.label_Items.Size = new System.Drawing.Size(57, 13);
            this.label_Items.TabIndex = 8;
            this.label_Items.Text = "Items Path";
            // 
            // textBox_ItemsPath
            // 
            this.textBox_ItemsPath.Location = new System.Drawing.Point(12, 103);
            this.textBox_ItemsPath.Name = "textBox_ItemsPath";
            this.textBox_ItemsPath.ReadOnly = true;
            this.textBox_ItemsPath.Size = new System.Drawing.Size(462, 20);
            this.textBox_ItemsPath.TabIndex = 7;
            // 
            // button_SelectItems
            // 
            this.button_SelectItems.Location = new System.Drawing.Point(480, 101);
            this.button_SelectItems.Name = "button_SelectItems";
            this.button_SelectItems.Size = new System.Drawing.Size(75, 23);
            this.button_SelectItems.TabIndex = 9;
            this.button_SelectItems.Text = "Select";
            this.button_SelectItems.UseVisualStyleBackColor = true;
            this.button_SelectItems.Click += new System.EventHandler(this.button_SelectItems_Click);
            // 
            // openFileDialog_Items
            // 
            this.openFileDialog_Items.FileName = "Items";
            // 
            // Form_PathSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(567, 205);
            this.ControlBox = false;
            this.Controls.Add(this.button_SelectItems);
            this.Controls.Add(this.label_Items);
            this.Controls.Add(this.textBox_ItemsPath);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.label_Manufacturer);
            this.Controls.Add(this.label_ResearchXML);
            this.Controls.Add(this.textBox_Manufacturer);
            this.Controls.Add(this.textBox_ResearchXML);
            this.Controls.Add(this.button_Manufacturer);
            this.Controls.Add(this.button_SelectResearch);
            this.Name = "Form_PathSelector";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Path Selector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_SelectResearch;
        private System.Windows.Forms.Button button_Manufacturer;
        private System.Windows.Forms.TextBox textBox_ResearchXML;
        private System.Windows.Forms.TextBox textBox_Manufacturer;
        private System.Windows.Forms.Label label_ResearchXML;
        private System.Windows.Forms.Label label_Manufacturer;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Research;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Manufacturer;
        private System.Windows.Forms.Label label_Items;
        private System.Windows.Forms.TextBox textBox_ItemsPath;
        private System.Windows.Forms.Button button_SelectItems;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Items;
    }
}