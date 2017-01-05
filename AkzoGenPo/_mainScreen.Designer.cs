namespace AkzoGenPo
{
    partial class _mainScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_mainScreen));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._testPrintPDF = new MyLib.ToolStripMyButton();
            this._myGroupBox1 = new MyLib._myGroupBox();
            this._myGrid1 = new MyLib._myGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._formdesignButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._exitButton = new System.Windows.Forms.ToolStripMenuItem();
            this._poConfigToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this._myGroupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackgroundImage = global::AkzoGenPo.Properties.Resources.bt03;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._testPrintPDF});
            this.statusStrip1.Location = new System.Drawing.Point(0, 700);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1221, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _testPrintPDF
            // 
            this._testPrintPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._testPrintPDF.Image = ((System.Drawing.Image)(resources.GetObject("_testPrintPDF.Image")));
            this._testPrintPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._testPrintPDF.Name = "_testPrintPDF";
            this._testPrintPDF.Padding = new System.Windows.Forms.Padding(1);
            this._testPrintPDF.ResourceName = "";
            this._testPrintPDF.Size = new System.Drawing.Size(23, 22);
            this._testPrintPDF.Text = "toolStripMyButton1";
            this._testPrintPDF.Visible = false;
            this._testPrintPDF.Click += new System.EventHandler(this._testPrintPDF_Click);
            // 
            // _myGroupBox1
            // 
            this._myGroupBox1.Controls.Add(this._myGrid1);
            this._myGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGroupBox1.Location = new System.Drawing.Point(0, 24);
            this._myGroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGroupBox1.Name = "_myGroupBox1";
            this._myGroupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGroupBox1.ResourceName = "Detail";
            this._myGroupBox1.Size = new System.Drawing.Size(1221, 676);
            this._myGroupBox1.TabIndex = 2;
            this._myGroupBox1.TabStop = false;
            this._myGroupBox1.Text = "Detail";
            // 
            // _myGrid1
            // 
            this._myGrid1._extraWordShow = true;
            this._myGrid1._selectRow = -1;
            this._myGrid1.BackColor = System.Drawing.SystemColors.Window;
            this._myGrid1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGrid1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGrid1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGrid1.Location = new System.Drawing.Point(3, 20);
            this._myGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGrid1.Name = "_myGrid1";
            this._myGrid1.Size = new System.Drawing.Size(1215, 652);
            this._myGrid1.TabIndex = 0;
            this._myGrid1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImage = global::AkzoGenPo.Properties.Resources.bt03;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1221, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._poConfigToolstrip,
            this._formdesignButton,
            this.toolStripSeparator1,
            this._exitButton});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // _formdesignButton
            // 
            this._formdesignButton.Name = "_formdesignButton";
            this._formdesignButton.Size = new System.Drawing.Size(152, 22);
            this._formdesignButton.Text = "Form Design";
            this._formdesignButton.Click += new System.EventHandler(this._formdesignButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // _exitButton
            // 
            this._exitButton.Name = "_exitButton";
            this._exitButton.Size = new System.Drawing.Size(152, 22);
            this._exitButton.Text = "Exit";
            this._exitButton.Click += new System.EventHandler(this._exitButton_Click);
            // 
            // _poConfigToolstrip
            // 
            this._poConfigToolstrip.Name = "_poConfigToolstrip";
            this._poConfigToolstrip.Size = new System.Drawing.Size(152, 22);
            this._poConfigToolstrip.Text = "PO Config";
            this._poConfigToolstrip.Click += new System.EventHandler(this._poConfigToolstrip_Click);
            // 
            // _mainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 722);
            this.Controls.Add(this._myGroupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_mainScreen";
            this.Text = "AKZO GEN PO";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this._myGroupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private MyLib._myGroupBox _myGroupBox1;
        private MyLib._myGrid _myGrid1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _formdesignButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _exitButton;
        private MyLib.ToolStripMyButton _testPrintPDF;
        private System.Windows.Forms.ToolStripMenuItem _poConfigToolstrip;
    }
}

