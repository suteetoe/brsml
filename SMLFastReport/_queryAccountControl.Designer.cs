namespace SMLFastReport
{
    partial class _queryAccountControl
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
            this._accountGrid = new MyLib._myGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._myToolStrip1 = new MyLib._myToolStrip();
            this._accountButtonNewLine = new MyLib.ToolStripMyButton();
            this._accountScreen = new MyLib._myScreen();
            this._accountColumnGrid = new MyLib._myGrid();
            this._myTab = new MyLib._myTabControl();
            this._accountConditionTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this._conditionScreen = new MyLib._myScreen();
            this._accountDetailTabPage = new System.Windows.Forms.TabPage();
            this._myToolStrip2 = new MyLib._myToolStrip();
            this._previewButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this._myToolStrip1.SuspendLayout();
            this._myTab.SuspendLayout();
            this._accountConditionTabPage.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this._accountDetailTabPage.SuspendLayout();
            this._myToolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _accountGrid
            // 
            this._accountGrid._extraWordShow = true;
            this._accountGrid._selectRow = -1;
            this._accountGrid.BackColor = System.Drawing.SystemColors.Window;
            this._accountGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._accountGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._accountGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._accountGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._accountGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._accountGrid.Location = new System.Drawing.Point(0, 25);
            this._accountGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._accountGrid.Name = "_accountGrid";
            this._accountGrid.Size = new System.Drawing.Size(558, 270);
            this._accountGrid.TabIndex = 0;
            this._accountGrid.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._accountScreen);
            this.splitContainer1.Size = new System.Drawing.Size(915, 627);
            this.splitContainer1.SplitterDistance = 297;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._accountGrid);
            this.splitContainer2.Panel2.Controls.Add(this._myToolStrip1);
            this.splitContainer2.Size = new System.Drawing.Size(915, 297);
            this.splitContainer2.SplitterDistance = 351;
            this.splitContainer2.TabIndex = 2;
            // 
            // _myToolStrip1
            // 
            this._myToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._accountButtonNewLine});
            this._myToolStrip1.Location = new System.Drawing.Point(0, 0);
            this._myToolStrip1.Name = "_myToolStrip1";
            this._myToolStrip1.Size = new System.Drawing.Size(558, 25);
            this._myToolStrip1.TabIndex = 1;
            this._myToolStrip1.Text = "_myToolStrip1";
            // 
            // _accountButtonNewLine
            // 
            this._accountButtonNewLine.Image = global::SMLFastReport.Properties.Resources.document_add;
            this._accountButtonNewLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._accountButtonNewLine.Name = "_accountButtonNewLine";
            this._accountButtonNewLine.Padding = new System.Windows.Forms.Padding(1);
            this._accountButtonNewLine.ResourceName = "";
            this._accountButtonNewLine.Size = new System.Drawing.Size(78, 22);
            this._accountButtonNewLine.Text = "New Line";
            // 
            // _accountScreen
            // 
            this._accountScreen._isChange = false;
            this._accountScreen.BackColor = System.Drawing.Color.Transparent;
            this._accountScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._accountScreen.Location = new System.Drawing.Point(0, 0);
            this._accountScreen.Name = "_accountScreen";
            this._accountScreen.Size = new System.Drawing.Size(913, 324);
            this._accountScreen.TabIndex = 0;
            // 
            // _accountColumnGrid
            // 
            this._accountColumnGrid._extraWordShow = true;
            this._accountColumnGrid._selectRow = -1;
            this._accountColumnGrid.BackColor = System.Drawing.SystemColors.Window;
            this._accountColumnGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._accountColumnGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._accountColumnGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._accountColumnGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._accountColumnGrid.Location = new System.Drawing.Point(0, 0);
            this._accountColumnGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._accountColumnGrid.Name = "_accountColumnGrid";
            this._accountColumnGrid.Size = new System.Drawing.Size(476, 600);
            this._accountColumnGrid.TabIndex = 1;
            this._accountColumnGrid.TabStop = false;
            // 
            // _myTab
            // 
            this._myTab.Controls.Add(this._accountConditionTabPage);
            this._myTab.Controls.Add(this._accountDetailTabPage);
            this._myTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTab.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTab.Location = new System.Drawing.Point(0, 25);
            this._myTab.Multiline = true;
            this._myTab.Name = "_myTab";
            this._myTab.SelectedIndex = 0;
            this._myTab.Size = new System.Drawing.Size(929, 635);
            this._myTab.TabIndex = 1;
            this._myTab.TableName = "";
            // 
            // _accountConditionTabPage
            // 
            this._accountConditionTabPage.Controls.Add(this.splitContainer3);
            this._accountConditionTabPage.Location = new System.Drawing.Point(4, 23);
            this._accountConditionTabPage.Name = "_accountConditionTabPage";
            this._accountConditionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._accountConditionTabPage.Size = new System.Drawing.Size(921, 608);
            this._accountConditionTabPage.TabIndex = 0;
            this._accountConditionTabPage.Text = "Condition";
            this._accountConditionTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this._conditionScreen);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this._accountColumnGrid);
            this.splitContainer3.Size = new System.Drawing.Size(915, 602);
            this.splitContainer3.SplitterDistance = 433;
            this.splitContainer3.TabIndex = 0;
            // 
            // _conditionScreen
            // 
            this._conditionScreen._isChange = false;
            this._conditionScreen.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._conditionScreen.Location = new System.Drawing.Point(0, 0);
            this._conditionScreen.Name = "_conditionScreen";
            this._conditionScreen.Size = new System.Drawing.Size(431, 600);
            this._conditionScreen.TabIndex = 0;
            // 
            // _accountDetailTabPage
            // 
            this._accountDetailTabPage.Controls.Add(this.splitContainer1);
            this._accountDetailTabPage.Location = new System.Drawing.Point(4, 23);
            this._accountDetailTabPage.Name = "_accountDetailTabPage";
            this._accountDetailTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._accountDetailTabPage.Size = new System.Drawing.Size(921, 633);
            this._accountDetailTabPage.TabIndex = 1;
            this._accountDetailTabPage.Text = "Details";
            this._accountDetailTabPage.UseVisualStyleBackColor = true;
            // 
            // _myToolStrip2
            // 
            this._myToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._previewButton});
            this._myToolStrip2.Location = new System.Drawing.Point(0, 0);
            this._myToolStrip2.Name = "_myToolStrip2";
            this._myToolStrip2.Size = new System.Drawing.Size(929, 25);
            this._myToolStrip2.TabIndex = 1;
            this._myToolStrip2.Text = "_myToolStrip2";
            // 
            // _previewButton
            // 
            this._previewButton.Image = global::SMLFastReport.Properties.Resources.flash;
            this._previewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._previewButton.Name = "_previewButton";
            this._previewButton.Size = new System.Drawing.Size(68, 22);
            this._previewButton.Text = "Preview";
            // 
            // _queryAccountControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myTab);
            this.Controls.Add(this._myToolStrip2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_queryAccountControl";
            this.Size = new System.Drawing.Size(929, 660);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this._myToolStrip1.ResumeLayout(false);
            this._myToolStrip1.PerformLayout();
            this._myTab.ResumeLayout(false);
            this._accountConditionTabPage.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this._accountDetailTabPage.ResumeLayout(false);
            this._myToolStrip2.ResumeLayout(false);
            this._myToolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myGrid _accountGrid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myToolStrip _myToolStrip1;
        private MyLib.ToolStripMyButton _accountButtonNewLine;
        private MyLib._myScreen _accountScreen;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private MyLib._myGrid _accountColumnGrid;
        private MyLib._myTabControl _myTab;
        private System.Windows.Forms.TabPage _accountConditionTabPage;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private MyLib._myScreen _conditionScreen;
        private System.Windows.Forms.TabPage _accountDetailTabPage;
        private MyLib._myToolStrip _myToolStrip2;
        private System.Windows.Forms.ToolStripButton _previewButton;

    }
}
