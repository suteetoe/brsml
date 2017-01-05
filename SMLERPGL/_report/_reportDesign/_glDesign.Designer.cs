namespace SMLERPGL._report._reportDesign
{
    partial class _glDesign
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_glDesign));
            this._toolStripFile = new System.Windows.Forms.ToolStrip();
            this._newButton = new System.Windows.Forms.ToolStripButton();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._saveAsButton = new System.Windows.Forms.ToolStripButton();
            this._loadButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._info = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this._tabTextPage = new System.Windows.Forms.TabPage();
            this._textBox = new System.Windows.Forms.TextBox();
            this._toolStripControl = new System.Windows.Forms.ToolStrip();
            this._formulaCheckBox = new MyLib.ToolStripCheckedBox();
            this._processButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._preview = new System.Windows.Forms.WebBrowser();
            this._tabControl = new System.Windows.Forms.TabControl();
            this._tabValues = new System.Windows.Forms.TabPage();
            this._valuesGrid = new MyLib._myGrid();
            this._tabChartOfAccount = new System.Windows.Forms.TabPage();
            this._accountGrid = new MyLib._myGrid();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this._toolStripFile.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this._tabTextPage.SuspendLayout();
            this._toolStripControl.SuspendLayout();
            this._tabControl.SuspendLayout();
            this._tabValues.SuspendLayout();
            this._tabChartOfAccount.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStripFile
            // 
            this._toolStripFile.BackgroundImage = global::SMLERPGL.Resource16x16.bt03;
            this._toolStripFile.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newButton,
            this._saveButton,
            this._saveAsButton,
            this._loadButton,
            this.toolStripSeparator2,
            this._closeButton,
            this.toolStripSeparator1,
            this._info});
            this._toolStripFile.Location = new System.Drawing.Point(0, 0);
            this._toolStripFile.Name = "_toolStripFile";
            this._toolStripFile.Size = new System.Drawing.Size(850, 25);
            this._toolStripFile.TabIndex = 1;
            this._toolStripFile.Text = "toolStrip1";
            // 
            // _newButton
            // 
            this._newButton.Image = ((System.Drawing.Image)(resources.GetObject("_newButton.Image")));
            this._newButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._newButton.Name = "_newButton";
            this._newButton.Size = new System.Drawing.Size(77, 22);
            this._newButton.Text = "สร้างงบใหม่";
            this._newButton.Click += new System.EventHandler(this._newButton_Click);
            // 
            // _saveButton
            // 
            this._saveButton.Font = new System.Drawing.Font("Tahoma", 9F);
            this._saveButton.Image = global::SMLERPGL.Resource16x16.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "";
            this._saveButton.Size = new System.Drawing.Size(60, 22);
            this._saveButton.Text = "บันทึก";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _saveAsButton
            // 
            this._saveAsButton.Image = ((System.Drawing.Image)(resources.GetObject("_saveAsButton.Image")));
            this._saveAsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveAsButton.Name = "_saveAsButton";
            this._saveAsButton.Size = new System.Drawing.Size(112, 22);
            this._saveAsButton.Text = "บันทึก (เป็นงบใหม่)";
            this._saveAsButton.Click += new System.EventHandler(this._saveAsButton_Click);
            // 
            // _loadButton
            // 
            this._loadButton.Font = new System.Drawing.Font("Tahoma", 9F);
            this._loadButton.Image = global::SMLERPGL.Resource16x16.folder;
            this._loadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._loadButton.Name = "_loadButton";
            this._loadButton.Padding = new System.Windows.Forms.Padding(1);
            this._loadButton.ResourceName = "";
            this._loadButton.Size = new System.Drawing.Size(74, 22);
            this._loadButton.Text = "ดึงงบเดิม";
            this._loadButton.Click += new System.EventHandler(this._loadButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _closeButton
            // 
            this._closeButton.Font = new System.Drawing.Font("Tahoma", 9F);
            this._closeButton.Image = global::SMLERPGL.Resource16x16.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(79, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _info
            // 
            this._info.Name = "_info";
            this._info.Size = new System.Drawing.Size(10, 22);
            this._info.Text = ".";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(850, 565);
            this.splitContainer1.SplitterDistance = 627;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer2.Panel1.Controls.Add(this._toolStripControl);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._preview);
            this.splitContainer2.Size = new System.Drawing.Size(627, 565);
            this.splitContainer2.SplitterDistance = 282;
            this.splitContainer2.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this._tabTextPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(625, 255);
            this.tabControl1.TabIndex = 6;
            // 
            // _tabTextPage
            // 
            this._tabTextPage.Controls.Add(this._textBox);
            this._tabTextPage.Location = new System.Drawing.Point(4, 23);
            this._tabTextPage.Name = "_tabTextPage";
            this._tabTextPage.Padding = new System.Windows.Forms.Padding(3);
            this._tabTextPage.Size = new System.Drawing.Size(617, 228);
            this._tabTextPage.TabIndex = 1;
            this._tabTextPage.Text = "Text";
            this._tabTextPage.UseVisualStyleBackColor = true;
            // 
            // _textBox
            // 
            this._textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textBox.Location = new System.Drawing.Point(3, 3);
            this._textBox.Multiline = true;
            this._textBox.Name = "_textBox";
            this._textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._textBox.Size = new System.Drawing.Size(611, 222);
            this._textBox.TabIndex = 5;
            // 
            // _toolStripControl
            // 
            this._toolStripControl.BackgroundImage = global::SMLERPGL.Resource16x16.bt03;
            this._toolStripControl.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStripControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._formulaCheckBox,
            this._processButton,
            this.toolStripSeparator3});
            this._toolStripControl.Location = new System.Drawing.Point(0, 0);
            this._toolStripControl.Name = "_toolStripControl";
            this._toolStripControl.Size = new System.Drawing.Size(625, 25);
            this._toolStripControl.TabIndex = 4;
            this._toolStripControl.Text = "toolStrip1";
            // 
            // _formulaCheckBox
            // 
            this._formulaCheckBox.BackColor = System.Drawing.Color.Transparent;
            this._formulaCheckBox.Font = new System.Drawing.Font("Tahoma", 9F);
            // 
            // _formulaCheckBox
            // 
            this._formulaCheckBox.MyCheckBox.AccessibleName = "_formulaCheckBox";
            this._formulaCheckBox.MyCheckBox.BackColor = System.Drawing.Color.Transparent;
            this._formulaCheckBox.MyCheckBox.Font = new System.Drawing.Font("Tahoma", 9F);
            this._formulaCheckBox.MyCheckBox.Location = new System.Drawing.Point(0, 1);
            this._formulaCheckBox.MyCheckBox.Name = "_formulaCheckBox";
            this._formulaCheckBox.MyCheckBox.Size = new System.Drawing.Size(72, 22);
            this._formulaCheckBox.MyCheckBox.TabIndex = 1;
            this._formulaCheckBox.MyCheckBox.Text = "แสดงสูตร";
            this._formulaCheckBox.MyCheckBox.UseVisualStyleBackColor = false;
            this._formulaCheckBox.Name = "_formulaCheckBox";
            this._formulaCheckBox.Size = new System.Drawing.Size(72, 22);
            this._formulaCheckBox.Text = "แสดงสูตร";
            // 
            // _processButton
            // 
            this._processButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._processButton.Image = ((System.Drawing.Image)(resources.GetObject("_processButton.Image")));
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Padding = new System.Windows.Forms.Padding(1);
            this._processButton.ResourceName = "";
            this._processButton.Size = new System.Drawing.Size(108, 22);
            this._processButton.Text = "ประมวลผล (F1)";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _preview
            // 
            this._preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._preview.Location = new System.Drawing.Point(0, 0);
            this._preview.MinimumSize = new System.Drawing.Size(20, 20);
            this._preview.Name = "_preview";
            this._preview.Size = new System.Drawing.Size(625, 277);
            this._preview.TabIndex = 0;
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this._tabValues);
            this._tabControl.Controls.Add(this._tabChartOfAccount);
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(217, 563);
            this._tabControl.TabIndex = 0;
            // 
            // _tabValues
            // 
            this._tabValues.Controls.Add(this._valuesGrid);
            this._tabValues.Location = new System.Drawing.Point(4, 23);
            this._tabValues.Name = "_tabValues";
            this._tabValues.Padding = new System.Windows.Forms.Padding(3);
            this._tabValues.Size = new System.Drawing.Size(209, 536);
            this._tabValues.TabIndex = 0;
            this._tabValues.Text = "Values";
            this._tabValues.UseVisualStyleBackColor = true;
            // 
            // _valuesGrid
            // 
            this._valuesGrid._extraWordShow = true;
            this._valuesGrid._selectRow = -1;
            this._valuesGrid.BackColor = System.Drawing.SystemColors.Window;
            this._valuesGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._valuesGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._valuesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._valuesGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._valuesGrid.Location = new System.Drawing.Point(3, 3);
            this._valuesGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._valuesGrid.Name = "_valuesGrid";
            this._valuesGrid.Size = new System.Drawing.Size(203, 530);
            this._valuesGrid.TabIndex = 0;
            this._valuesGrid.TabStop = false;
            // 
            // _tabChartOfAccount
            // 
            this._tabChartOfAccount.Controls.Add(this._accountGrid);
            this._tabChartOfAccount.Location = new System.Drawing.Point(4, 22);
            this._tabChartOfAccount.Name = "_tabChartOfAccount";
            this._tabChartOfAccount.Padding = new System.Windows.Forms.Padding(3);
            this._tabChartOfAccount.Size = new System.Drawing.Size(209, 537);
            this._tabChartOfAccount.TabIndex = 1;
            this._tabChartOfAccount.Text = "Account";
            this._tabChartOfAccount.UseVisualStyleBackColor = true;
            // 
            // _accountGrid
            // 
            this._accountGrid._extraWordShow = true;
            this._accountGrid._selectRow = -1;
            this._accountGrid.BackColor = System.Drawing.SystemColors.Window;
            this._accountGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._accountGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._accountGrid.DisplayRowNumber = false;
            this._accountGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._accountGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._accountGrid.Location = new System.Drawing.Point(3, 3);
            this._accountGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._accountGrid.Name = "_accountGrid";
            this._accountGrid.Size = new System.Drawing.Size(203, 531);
            this._accountGrid.TabIndex = 2;
            this._accountGrid.TabStop = false;
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            // 
            // _glDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._toolStripFile);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_glDesign";
            this.Size = new System.Drawing.Size(850, 590);
            this._toolStripFile.ResumeLayout(false);
            this._toolStripFile.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this._tabTextPage.ResumeLayout(false);
            this._tabTextPage.PerformLayout();
            this._toolStripControl.ResumeLayout(false);
            this._toolStripControl.PerformLayout();
            this._tabControl.ResumeLayout(false);
            this._tabValues.ResumeLayout(false);
            this._tabChartOfAccount.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolStripFile;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _loadButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private MyLib.ToolStripMyButton _closeButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStrip _toolStripControl;
        private MyLib.ToolStripMyButton _processButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.WebBrowser _preview;
        private System.Windows.Forms.TextBox _textBox;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage _tabValues;
        private MyLib._myGrid _valuesGrid;
        private System.Windows.Forms.TabPage _tabChartOfAccount;
        public MyLib._myGrid _accountGrid;
        private MyLib.ToolStripCheckedBox _formulaCheckBox;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage _tabTextPage;
        private System.Windows.Forms.ToolStripButton _saveAsButton;
        private System.Windows.Forms.ToolStripLabel _info;
        private System.Windows.Forms.ToolStripButton _newButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
