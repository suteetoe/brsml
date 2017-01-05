namespace SMLLanguage
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonLoadFromTextFile = new System.Windows.Forms.ToolStripButton();
            this._buttonIncludeThai = new System.Windows.Forms.ToolStripButton();
            this._enCompareButton = new System.Windows.Forms.ToolStripButton();
            this._buttonLoadCompare = new System.Windows.Forms.ToolStripButton();
            this._buttonGenXml = new System.Windows.Forms.ToolStripButton();
            this._buttonWriteXML = new System.Windows.Forms.ToolStripButton();
            this._duplicateCheckButton = new System.Windows.Forms.ToolStripButton();
            this._nextDuplicateButton = new System.Windows.Forms.ToolStripButton();
            this._dataGrid = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this._databaseDesignCompare = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonLoadFromTextFile,
            this._buttonIncludeThai,
            this._enCompareButton,
            this._buttonLoadCompare,
            this._buttonGenXml,
            this._buttonWriteXML,
            this._duplicateCheckButton,
            this._nextDuplicateButton,
            this._databaseDesignCompare});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1174, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonLoadFromTextFile
            // 
            this._buttonLoadFromTextFile.Image = ((System.Drawing.Image)(resources.GetObject("_buttonLoadFromTextFile.Image")));
            this._buttonLoadFromTextFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonLoadFromTextFile.Name = "_buttonLoadFromTextFile";
            this._buttonLoadFromTextFile.Size = new System.Drawing.Size(127, 22);
            this._buttonLoadFromTextFile.Text = "Load from Text File";
            this._buttonLoadFromTextFile.Click += new System.EventHandler(this._buttonLoadFromTextFile_Click);
            // 
            // _buttonIncludeThai
            // 
            this._buttonIncludeThai.Image = ((System.Drawing.Image)(resources.GetObject("_buttonIncludeThai.Image")));
            this._buttonIncludeThai.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonIncludeThai.Name = "_buttonIncludeThai";
            this._buttonIncludeThai.Size = new System.Drawing.Size(92, 22);
            this._buttonIncludeThai.Text = "Include Thai";
            this._buttonIncludeThai.Click += new System.EventHandler(this._buttonIncludeThai_Click);
            // 
            // _enCompareButton
            // 
            this._enCompareButton.Image = ((System.Drawing.Image)(resources.GetObject("_enCompareButton.Image")));
            this._enCompareButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._enCompareButton.Name = "_enCompareButton";
            this._enCompareButton.Size = new System.Drawing.Size(117, 22);
            this._enCompareButton.Text = "Compare English";
            this._enCompareButton.Click += new System.EventHandler(this._enCompareButton_Click);
            // 
            // _buttonLoadCompare
            // 
            this._buttonLoadCompare.Image = ((System.Drawing.Image)(resources.GetObject("_buttonLoadCompare.Image")));
            this._buttonLoadCompare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonLoadCompare.Name = "_buttonLoadCompare";
            this._buttonLoadCompare.Size = new System.Drawing.Size(98, 22);
            this._buttonLoadCompare.Text = "Compare Lao";
            this._buttonLoadCompare.Click += new System.EventHandler(this._buttonLoadCompare_Click);
            // 
            // _buttonGenXml
            // 
            this._buttonGenXml.Image = ((System.Drawing.Image)(resources.GetObject("_buttonGenXml.Image")));
            this._buttonGenXml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonGenXml.Name = "_buttonGenXml";
            this._buttonGenXml.Size = new System.Drawing.Size(75, 22);
            this._buttonGenXml.Text = "Gen XML";
            this._buttonGenXml.Click += new System.EventHandler(this._buttonGenXml_Click);
            // 
            // _buttonWriteXML
            // 
            this._buttonWriteXML.Image = ((System.Drawing.Image)(resources.GetObject("_buttonWriteXML.Image")));
            this._buttonWriteXML.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonWriteXML.Name = "_buttonWriteXML";
            this._buttonWriteXML.Size = new System.Drawing.Size(82, 22);
            this._buttonWriteXML.Text = "Write XML";
            this._buttonWriteXML.Click += new System.EventHandler(this._buttonWriteXML_Click);
            // 
            // _duplicateCheckButton
            // 
            this._duplicateCheckButton.Image = ((System.Drawing.Image)(resources.GetObject("_duplicateCheckButton.Image")));
            this._duplicateCheckButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._duplicateCheckButton.Name = "_duplicateCheckButton";
            this._duplicateCheckButton.Size = new System.Drawing.Size(113, 22);
            this._duplicateCheckButton.Text = "Check Duplicate";
            this._duplicateCheckButton.Click += new System.EventHandler(this._duplicateCheckButton_Click);
            // 
            // _nextDuplicateButton
            // 
            this._nextDuplicateButton.Image = ((System.Drawing.Image)(resources.GetObject("_nextDuplicateButton.Image")));
            this._nextDuplicateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._nextDuplicateButton.Name = "_nextDuplicateButton";
            this._nextDuplicateButton.Size = new System.Drawing.Size(130, 22);
            this._nextDuplicateButton.Text = "Next Duplicate Row";
            this._nextDuplicateButton.Click += new System.EventHandler(this._nextDuplicateButton_Click);
            // 
            // _dataGrid
            // 
            this._dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGrid.Location = new System.Drawing.Point(0, 25);
            this._dataGrid.Name = "_dataGrid";
            this._dataGrid.Size = new System.Drawing.Size(1174, 541);
            this._dataGrid.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // _databaseDesignCompare
            // 
            this._databaseDesignCompare.Image = ((System.Drawing.Image)(resources.GetObject("_databaseDesignCompare.Image")));
            this._databaseDesignCompare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._databaseDesignCompare.Name = "_databaseDesignCompare";
            this._databaseDesignCompare.Size = new System.Drawing.Size(166, 22);
            this._databaseDesignCompare.Text = "Compare Database Design";
            this._databaseDesignCompare.Click += new System.EventHandler(this._databaseDesignCompare_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 566);
            this.Controls.Add(this._dataGrid);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "Form1";
            this.Text = "SML Language Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _buttonLoadFromTextFile;
        private System.Windows.Forms.DataGridView _dataGrid;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripButton _buttonLoadCompare;
        private System.Windows.Forms.ToolStripButton _buttonIncludeThai;
        private System.Windows.Forms.ToolStripButton _buttonGenXml;
        private System.Windows.Forms.ToolStripButton _buttonWriteXML;
        private System.Windows.Forms.ToolStripButton _enCompareButton;
        private System.Windows.Forms.ToolStripButton _duplicateCheckButton;
        private System.Windows.Forms.ToolStripButton _nextDuplicateButton;
        private System.Windows.Forms.ToolStripButton _databaseDesignCompare;
    }
}

