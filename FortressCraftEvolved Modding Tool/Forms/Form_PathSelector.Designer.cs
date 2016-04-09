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
            this.label_DataEntriesPath = new System.Windows.Forms.Label();
            this.textBox_DataEntries = new System.Windows.Forms.TextBox();
            this.button_DataEntrySelect = new System.Windows.Forms.Button();
            this.openFileDialog_DataEntry = new System.Windows.Forms.OpenFileDialog();
            this.button_RefRec = new System.Windows.Forms.Button();
            this.label_RefPath = new System.Windows.Forms.Label();
            this.textBox_RefRec = new System.Windows.Forms.TextBox();
            this.openFileDialog_RefRec = new System.Windows.Forms.OpenFileDialog();
            this.button_WritePath = new System.Windows.Forms.Button();
            this.label_WritePath = new System.Windows.Forms.Label();
            this.textBox_WritePath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog_WritePath = new System.Windows.Forms.FolderBrowserDialog();
            this.button_DataPath = new System.Windows.Forms.Button();
            this.label_DataPath = new System.Windows.Forms.Label();
            this.textBox_DataPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog_GameData = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // button_SelectResearch
            // 
            this.button_SelectResearch.Location = new System.Drawing.Point(480, 136);
            this.button_SelectResearch.Name = "button_SelectResearch";
            this.button_SelectResearch.Size = new System.Drawing.Size(75, 23);
            this.button_SelectResearch.TabIndex = 0;
            this.button_SelectResearch.Text = "Select";
            this.button_SelectResearch.UseVisualStyleBackColor = true;
            this.button_SelectResearch.Visible = false;
            this.button_SelectResearch.Click += new System.EventHandler(this.button_SelectResearch_Click);
            // 
            // button_Manufacturer
            // 
            this.button_Manufacturer.Location = new System.Drawing.Point(480, 175);
            this.button_Manufacturer.Name = "button_Manufacturer";
            this.button_Manufacturer.Size = new System.Drawing.Size(75, 23);
            this.button_Manufacturer.TabIndex = 1;
            this.button_Manufacturer.Text = "Select";
            this.button_Manufacturer.UseVisualStyleBackColor = true;
            this.button_Manufacturer.Visible = false;
            this.button_Manufacturer.Click += new System.EventHandler(this.button_Manufacturer_Click);
            // 
            // textBox_ResearchXML
            // 
            this.textBox_ResearchXML.Location = new System.Drawing.Point(12, 138);
            this.textBox_ResearchXML.Name = "textBox_ResearchXML";
            this.textBox_ResearchXML.ReadOnly = true;
            this.textBox_ResearchXML.Size = new System.Drawing.Size(462, 20);
            this.textBox_ResearchXML.TabIndex = 2;
            this.textBox_ResearchXML.Visible = false;
            // 
            // textBox_Manufacturer
            // 
            this.textBox_Manufacturer.Location = new System.Drawing.Point(12, 177);
            this.textBox_Manufacturer.Name = "textBox_Manufacturer";
            this.textBox_Manufacturer.ReadOnly = true;
            this.textBox_Manufacturer.Size = new System.Drawing.Size(462, 20);
            this.textBox_Manufacturer.TabIndex = 3;
            this.textBox_Manufacturer.Visible = false;
            // 
            // label_ResearchXML
            // 
            this.label_ResearchXML.AutoSize = true;
            this.label_ResearchXML.Location = new System.Drawing.Point(12, 122);
            this.label_ResearchXML.Name = "label_ResearchXML";
            this.label_ResearchXML.Size = new System.Drawing.Size(78, 13);
            this.label_ResearchXML.TabIndex = 4;
            this.label_ResearchXML.Text = "Research Path";
            this.label_ResearchXML.Visible = false;
            // 
            // label_Manufacturer
            // 
            this.label_Manufacturer.AutoSize = true;
            this.label_Manufacturer.Location = new System.Drawing.Point(12, 161);
            this.label_Manufacturer.Name = "label_Manufacturer";
            this.label_Manufacturer.Size = new System.Drawing.Size(95, 13);
            this.label_Manufacturer.TabIndex = 5;
            this.label_Manufacturer.Text = "Manufactorer Path";
            this.label_Manufacturer.Visible = false;
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(480, 91);
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
            this.label_Items.Location = new System.Drawing.Point(12, 200);
            this.label_Items.Name = "label_Items";
            this.label_Items.Size = new System.Drawing.Size(57, 13);
            this.label_Items.TabIndex = 8;
            this.label_Items.Text = "Items Path";
            this.label_Items.Visible = false;
            // 
            // textBox_ItemsPath
            // 
            this.textBox_ItemsPath.Location = new System.Drawing.Point(12, 216);
            this.textBox_ItemsPath.Name = "textBox_ItemsPath";
            this.textBox_ItemsPath.ReadOnly = true;
            this.textBox_ItemsPath.Size = new System.Drawing.Size(462, 20);
            this.textBox_ItemsPath.TabIndex = 7;
            this.textBox_ItemsPath.Visible = false;
            // 
            // button_SelectItems
            // 
            this.button_SelectItems.Location = new System.Drawing.Point(480, 214);
            this.button_SelectItems.Name = "button_SelectItems";
            this.button_SelectItems.Size = new System.Drawing.Size(75, 23);
            this.button_SelectItems.TabIndex = 9;
            this.button_SelectItems.Text = "Select";
            this.button_SelectItems.UseVisualStyleBackColor = true;
            this.button_SelectItems.Visible = false;
            this.button_SelectItems.Click += new System.EventHandler(this.button_SelectItems_Click);
            // 
            // openFileDialog_Items
            // 
            this.openFileDialog_Items.FileName = "Items";
            // 
            // label_DataEntriesPath
            // 
            this.label_DataEntriesPath.AutoSize = true;
            this.label_DataEntriesPath.Location = new System.Drawing.Point(12, 239);
            this.label_DataEntriesPath.Name = "label_DataEntriesPath";
            this.label_DataEntriesPath.Size = new System.Drawing.Size(87, 13);
            this.label_DataEntriesPath.TabIndex = 11;
            this.label_DataEntriesPath.Text = "DataEntries Path";
            this.label_DataEntriesPath.Visible = false;
            // 
            // textBox_DataEntries
            // 
            this.textBox_DataEntries.Location = new System.Drawing.Point(12, 255);
            this.textBox_DataEntries.Name = "textBox_DataEntries";
            this.textBox_DataEntries.ReadOnly = true;
            this.textBox_DataEntries.Size = new System.Drawing.Size(462, 20);
            this.textBox_DataEntries.TabIndex = 10;
            this.textBox_DataEntries.Visible = false;
            // 
            // button_DataEntrySelect
            // 
            this.button_DataEntrySelect.Location = new System.Drawing.Point(480, 253);
            this.button_DataEntrySelect.Name = "button_DataEntrySelect";
            this.button_DataEntrySelect.Size = new System.Drawing.Size(75, 23);
            this.button_DataEntrySelect.TabIndex = 12;
            this.button_DataEntrySelect.Text = "Select";
            this.button_DataEntrySelect.UseVisualStyleBackColor = true;
            this.button_DataEntrySelect.Visible = false;
            this.button_DataEntrySelect.Click += new System.EventHandler(this.button_DataEntrySelect_Click);
            // 
            // openFileDialog_DataEntry
            // 
            this.openFileDialog_DataEntry.FileName = "TerrainData";
            // 
            // button_RefRec
            // 
            this.button_RefRec.Location = new System.Drawing.Point(480, 292);
            this.button_RefRec.Name = "button_RefRec";
            this.button_RefRec.Size = new System.Drawing.Size(75, 23);
            this.button_RefRec.TabIndex = 15;
            this.button_RefRec.Text = "Select";
            this.button_RefRec.UseVisualStyleBackColor = true;
            this.button_RefRec.Visible = false;
            this.button_RefRec.Click += new System.EventHandler(this.button_RefRec_Click);
            // 
            // label_RefPath
            // 
            this.label_RefPath.AutoSize = true;
            this.label_RefPath.Location = new System.Drawing.Point(12, 278);
            this.label_RefPath.Name = "label_RefPath";
            this.label_RefPath.Size = new System.Drawing.Size(108, 13);
            this.label_RefPath.TabIndex = 14;
            this.label_RefPath.Text = "Refinery Recipe Path";
            this.label_RefPath.Visible = false;
            // 
            // textBox_RefRec
            // 
            this.textBox_RefRec.Location = new System.Drawing.Point(12, 294);
            this.textBox_RefRec.Name = "textBox_RefRec";
            this.textBox_RefRec.ReadOnly = true;
            this.textBox_RefRec.Size = new System.Drawing.Size(462, 20);
            this.textBox_RefRec.TabIndex = 13;
            this.textBox_RefRec.Visible = false;
            // 
            // openFileDialog_RefRec
            // 
            this.openFileDialog_RefRec.FileName = "RefineryRecipes";
            // 
            // button_WritePath
            // 
            this.button_WritePath.Location = new System.Drawing.Point(480, 23);
            this.button_WritePath.Name = "button_WritePath";
            this.button_WritePath.Size = new System.Drawing.Size(75, 23);
            this.button_WritePath.TabIndex = 18;
            this.button_WritePath.Text = "Select";
            this.button_WritePath.UseVisualStyleBackColor = true;
            this.button_WritePath.Click += new System.EventHandler(this.button_WritePath_Click);
            // 
            // label_WritePath
            // 
            this.label_WritePath.AutoSize = true;
            this.label_WritePath.Location = new System.Drawing.Point(12, 9);
            this.label_WritePath.Name = "label_WritePath";
            this.label_WritePath.Size = new System.Drawing.Size(99, 13);
            this.label_WritePath.TabIndex = 17;
            this.label_WritePath.Text = "Program Write Path";
            // 
            // textBox_WritePath
            // 
            this.textBox_WritePath.Location = new System.Drawing.Point(12, 25);
            this.textBox_WritePath.Name = "textBox_WritePath";
            this.textBox_WritePath.ReadOnly = true;
            this.textBox_WritePath.Size = new System.Drawing.Size(462, 20);
            this.textBox_WritePath.TabIndex = 16;
            // 
            // folderBrowserDialog_WritePath
            // 
            this.folderBrowserDialog_WritePath.Description = "The path the program will write files to";
            // 
            // button_DataPath
            // 
            this.button_DataPath.Location = new System.Drawing.Point(480, 62);
            this.button_DataPath.Name = "button_DataPath";
            this.button_DataPath.Size = new System.Drawing.Size(75, 23);
            this.button_DataPath.TabIndex = 21;
            this.button_DataPath.Text = "Select";
            this.button_DataPath.UseVisualStyleBackColor = true;
            this.button_DataPath.Click += new System.EventHandler(this.button_DataPath_Click);
            // 
            // label_DataPath
            // 
            this.label_DataPath.AutoSize = true;
            this.label_DataPath.Location = new System.Drawing.Point(12, 48);
            this.label_DataPath.Name = "label_DataPath";
            this.label_DataPath.Size = new System.Drawing.Size(110, 13);
            this.label_DataPath.TabIndex = 20;
            this.label_DataPath.Text = "Game Data Files Path";
            // 
            // textBox_DataPath
            // 
            this.textBox_DataPath.Location = new System.Drawing.Point(12, 64);
            this.textBox_DataPath.Name = "textBox_DataPath";
            this.textBox_DataPath.ReadOnly = true;
            this.textBox_DataPath.Size = new System.Drawing.Size(462, 20);
            this.textBox_DataPath.TabIndex = 19;
            // 
            // folderBrowserDialog_GameData
            // 
            this.folderBrowserDialog_GameData.Description = "Select the Games Data folder";
            // 
            // Form_PathSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(570, 121);
            this.ControlBox = false;
            this.Controls.Add(this.button_DataPath);
            this.Controls.Add(this.label_DataPath);
            this.Controls.Add(this.textBox_DataPath);
            this.Controls.Add(this.button_WritePath);
            this.Controls.Add(this.label_WritePath);
            this.Controls.Add(this.textBox_WritePath);
            this.Controls.Add(this.button_RefRec);
            this.Controls.Add(this.label_RefPath);
            this.Controls.Add(this.textBox_RefRec);
            this.Controls.Add(this.button_DataEntrySelect);
            this.Controls.Add(this.label_DataEntriesPath);
            this.Controls.Add(this.textBox_DataEntries);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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
        private System.Windows.Forms.Label label_DataEntriesPath;
        private System.Windows.Forms.TextBox textBox_DataEntries;
        private System.Windows.Forms.Button button_DataEntrySelect;
        private System.Windows.Forms.OpenFileDialog openFileDialog_DataEntry;
        private System.Windows.Forms.Button button_RefRec;
        private System.Windows.Forms.Label label_RefPath;
        private System.Windows.Forms.TextBox textBox_RefRec;
        private System.Windows.Forms.OpenFileDialog openFileDialog_RefRec;
        private System.Windows.Forms.Button button_WritePath;
        private System.Windows.Forms.Label label_WritePath;
        private System.Windows.Forms.TextBox textBox_WritePath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_WritePath;
        private System.Windows.Forms.Button button_DataPath;
        private System.Windows.Forms.Label label_DataPath;
        private System.Windows.Forms.TextBox textBox_DataPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_GameData;
    }
}