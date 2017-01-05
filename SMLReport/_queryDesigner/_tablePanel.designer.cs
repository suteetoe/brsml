namespace SMLReport._design
{
    partial class _tablePanel
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
            this._grouper = new CodeVendor.Controls.Grouper();
            this._tableGrid = new MyLib._myGrid();
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this._buttonSelectAll = new System.Windows.Forms.ToolStripButton();
            this._buttonDeselectAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonRemove = new System.Windows.Forms.ToolStripButton();
            this._parentComboBox = new System.Windows.Forms.ToolStripComboBox();
            this._grouper.SuspendLayout();
            this._toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _grouper
            // 
            this._grouper.AutoSize = true;
            this._grouper.BackgroundColor = System.Drawing.Color.Azure;
            this._grouper.BackgroundGradientColor = System.Drawing.SystemColors.InactiveCaptionText;
            this._grouper.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.Vertical;
            this._grouper.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._grouper.BorderColor = System.Drawing.SystemColors.HotTrack;
            this._grouper.BorderThickness = 1F;
            this._grouper.Controls.Add(this._tableGrid);
            this._grouper.Controls.Add(this._toolBar);
            this._grouper.CustomGroupBoxColor = System.Drawing.Color.Black;
            this._grouper.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._grouper.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this._grouper.GroupImage = null;
            this._grouper.GroupTitle = "ทดสอบกรอบแบบกลุ่ม";
            this._grouper.Location = new System.Drawing.Point(0, 0);
            this._grouper.Name = "_grouper";
            this._grouper.Padding = new System.Windows.Forms.Padding(5, 30, 8, 10);
            this._grouper.PaintGroupBox = false;
            this._grouper.RoundCorners = 5;
            this._grouper.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper.ShadowControl = true;
            this._grouper.ShadowThickness = 4;
            this._grouper.Size = new System.Drawing.Size(261, 196);
            this._grouper.TabIndex = 4;
            // 
            // _tableGrid
            // 
            this._tableGrid._extraWordShow = true;
            this._tableGrid._selectRow = -1;
            this._tableGrid.BackColor = System.Drawing.SystemColors.Window;
            this._tableGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._tableGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._tableGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._tableGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._tableGrid.Location = new System.Drawing.Point(5, 55);
            this._tableGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._tableGrid.Name = "_tableGrid";
            this._tableGrid.Size = new System.Drawing.Size(248, 131);
            this._tableGrid.TabIndex = 0;
            this._tableGrid.TabStop = false;
            this._tableGrid.Load += new System.EventHandler(this._tableGrid_Load);
            // 
            // _toolBar
            // 
            this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSelectAll,
            this._buttonDeselectAll,
            this.toolStripSeparator1,
            this._buttonRefresh,
            this.toolStripSeparator2,
            this._buttonRemove,
            this._parentComboBox});
            this._toolBar.Location = new System.Drawing.Point(5, 30);
            this._toolBar.Name = "_toolBar";
            this._toolBar.Size = new System.Drawing.Size(248, 25);
            this._toolBar.TabIndex = 2;
            this._toolBar.Text = "Tool Bar";
            // 
            // _buttonSelectAll
            // 
            this._buttonSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonSelectAll.Image = global::SMLReport.Resource16x16.preferences;
            this._buttonSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSelectAll.Name = "_buttonSelectAll";
            this._buttonSelectAll.Size = new System.Drawing.Size(23, 22);
            this._buttonSelectAll.Text = "Select All";
            this._buttonSelectAll.Click += new System.EventHandler(this._buttonSelectAll_Click);
            // 
            // _buttonDeselectAll
            // 
            this._buttonDeselectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonDeselectAll.Image = global::SMLReport.Resource16x16.selection;
            this._buttonDeselectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonDeselectAll.Name = "_buttonDeselectAll";
            this._buttonDeselectAll.Size = new System.Drawing.Size(23, 22);
            this._buttonDeselectAll.Text = "Deselect All";
            this._buttonDeselectAll.Click += new System.EventHandler(this._buttonDeselectAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonRefresh
            // 
            this._buttonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonRefresh.Image = global::SMLReport.Resource16x16.refresh;
            this._buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonRefresh.Name = "_buttonRefresh";
            this._buttonRefresh.Size = new System.Drawing.Size(23, 22);
            this._buttonRefresh.Text = "Refresh";
            this._buttonRefresh.Click += new System.EventHandler(this._buttonRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonRemove
            // 
            this._buttonRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonRemove.Image = global::SMLReport.Resource16x16.garbage_empty;
            this._buttonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonRemove.Name = "_buttonRemove";
            this._buttonRemove.Size = new System.Drawing.Size(23, 22);
            this._buttonRemove.Text = "Remove windows";
            this._buttonRemove.Click += new System.EventHandler(this._buttonRemove_Click);
            // 
            // _parentComboBox
            // 
            this._parentComboBox.AutoSize = false;
            this._parentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._parentComboBox.Name = "_parentComboBox";
            this._parentComboBox.Size = new System.Drawing.Size(50, 23);
            // 
            // _tablePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this._grouper);
            this.DoubleBuffered = true;
            this.Name = "_tablePanel";
            this.Size = new System.Drawing.Size(261, 196);
            this._grouper.ResumeLayout(false);
            this._grouper.PerformLayout();
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public CodeVendor.Controls.Grouper _grouper;
        public MyLib._myGrid _tableGrid;
        private System.Windows.Forms.ToolStripButton _buttonSelectAll;
        public System.Windows.Forms.ToolStrip _toolBar;
        private System.Windows.Forms.ToolStripButton _buttonDeselectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _buttonRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripButton _buttonRefresh;
        public System.Windows.Forms.ToolStripComboBox _parentComboBox;
    }
}
