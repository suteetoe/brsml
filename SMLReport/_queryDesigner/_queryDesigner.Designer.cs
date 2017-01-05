namespace SMLReport._design
{
    partial class _queryDesigner
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._tableGrid = new MyLib._myGrid();
            this._labelTableList = new MyLib._myShadowLabel(this.components);
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this._myLinkList = new SMLReport._design._linkList();
            this._fieldGrid = new MyLib._myGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._clearButton = new System.Windows.Forms.ToolStripButton();
            this._queryTextBox = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._executeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._saveButton = new System.Windows.Forms.ToolStripButton();
            this._cancelButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tableGrid);
            this.splitContainer1.Panel1.Controls.Add(this._labelTableList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(699, 553);
            this.splitContainer1.SplitterDistance = 189;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // _tableGrid
            // 
            this._tableGrid._extraWordShow = true;
            this._tableGrid._selectRow = -1;
            this._tableGrid.AllowDrop = true;
            this._tableGrid.BackColor = System.Drawing.SystemColors.Window;
            this._tableGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._tableGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._tableGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._tableGrid.DisplayRowNumber = false;
            this._tableGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._tableGrid.Location = new System.Drawing.Point(0, 23);
            this._tableGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._tableGrid.Name = "_tableGrid";
            this._tableGrid.Size = new System.Drawing.Size(189, 530);
            this._tableGrid.TabIndex = 5;
            this._tableGrid.TabStop = false;
            // 
            // _labelTableList
            // 
            this._labelTableList.Angle = 0F;
            this._labelTableList.AutoSize = true;
            this._labelTableList.Dock = System.Windows.Forms.DockStyle.Top;
            this._labelTableList.DrawGradient = false;
            this._labelTableList.EndColor = System.Drawing.Color.LightGray;
            this._labelTableList.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._labelTableList.ForeColor = System.Drawing.Color.Black;
            this._labelTableList.Location = new System.Drawing.Point(0, 0);
            this._labelTableList.Name = "_labelTableList";
            this._labelTableList.ShadowColor = System.Drawing.Color.LightGray;
            this._labelTableList.Size = new System.Drawing.Size(98, 23);
            this._labelTableList.StartColor = System.Drawing.Color.White;
            this._labelTableList.TabIndex = 6;
            this._labelTableList.Text = "Table list";
            this._labelTableList.XOffset = 1F;
            this._labelTableList.YOffset = 1F;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this._queryTextBox);
            this.splitContainer3.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer3.Size = new System.Drawing.Size(509, 553);
            this.splitContainer3.SplitterDistance = 448;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this._myLinkList);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this._fieldGrid);
            this.splitContainer4.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer4.Size = new System.Drawing.Size(509, 448);
            this.splitContainer4.SplitterDistance = 320;
            this.splitContainer4.SplitterWidth = 1;
            this.splitContainer4.TabIndex = 1;
            // 
            // _myLinkList
            // 
            this._myLinkList.BackColor = System.Drawing.Color.White;
            this._myLinkList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._myLinkList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myLinkList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLinkList.Location = new System.Drawing.Point(0, 0);
            this._myLinkList.Name = "_myLinkList";
            this._myLinkList.Size = new System.Drawing.Size(509, 320);
            this._myLinkList.TabIndex = 0;
            // 
            // _fieldGrid
            // 
            this._fieldGrid._extraWordShow = true;
            this._fieldGrid._selectRow = -1;
            this._fieldGrid.BackColor = System.Drawing.SystemColors.Window;
            this._fieldGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._fieldGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._fieldGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._fieldGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fieldGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._fieldGrid.Location = new System.Drawing.Point(0, 25);
            this._fieldGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._fieldGrid.Name = "_fieldGrid";
            this._fieldGrid.Size = new System.Drawing.Size(509, 102);
            this._fieldGrid.TabIndex = 0;
            this._fieldGrid.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._clearButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(509, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _clearButton
            // 
            this._clearButton.Image = global::SMLReport.Properties.Resources.garbage_empty;
            this._clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(54, 22);
            this._clearButton.Text = "Clear";
            this._clearButton.Click += new System.EventHandler(this._clearButton_Click);
            // 
            // _queryTextBox
            // 
            this._queryTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryTextBox.Location = new System.Drawing.Point(0, 25);
            this._queryTextBox.Multiline = true;
            this._queryTextBox.Name = "_queryTextBox";
            this._queryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._queryTextBox.Size = new System.Drawing.Size(509, 79);
            this._queryTextBox.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._executeButton,
            this.toolStripSeparator1,
            this._saveButton,
            this._cancelButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(509, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _executeButton
            // 
            this._executeButton.Image = global::SMLReport.Properties.Resources.flash;
            this._executeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._executeButton.Name = "_executeButton";
            this._executeButton.Size = new System.Drawing.Size(67, 22);
            this._executeButton.Text = "Execute";
            this._executeButton.Click += new System.EventHandler(this._executeButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLReport.Properties.Resources.check2;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(51, 22);
            this._saveButton.Text = "Save";
            // 
            // _cancelButton
            // 
            this._cancelButton.Image = global::SMLReport.Properties.Resources.delete2;
            this._cancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(63, 22);
            this._cancelButton.Text = "Cancel";
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _queryDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "_queryDesigner";
            this.Size = new System.Drawing.Size(699, 553);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private MyLib._myGrid _tableGrid;
        private MyLib._myShadowLabel _labelTableList;
        public _linkList _myLinkList;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private MyLib._myGrid _fieldGrid;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton _clearButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _executeButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _cancelButton;
        public System.Windows.Forms.ToolStripButton _saveButton;
        public System.Windows.Forms.TextBox _queryTextBox;





    }
}
