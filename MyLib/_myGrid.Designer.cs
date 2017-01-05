namespace MyLib
{
    partial class _myGrid
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
            this._contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._deleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this._insertRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._gotoTop = new System.Windows.Forms.ToolStripMenuItem();
            this._gotoButtom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._copyAll = new System.Windows.Forms.ToolStripMenuItem();
            this._copyAllIncludeHeader = new System.Windows.Forms.ToolStripMenuItem();
            this._removeAll = new System.Windows.Forms.ToolStripSeparator();
            this._removeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._importFromTextFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._importFromTextFileFastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._vScrollBar1 = new MyLib._myVScrollBar();
            this._hScrollBar1 = new MyLib._myHScrollBar();
            this._contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contextMenuStrip
            // 
            this._contextMenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._contextMenuStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._deleteRow,
            this._insertRow,
            this.toolStripSeparator3,
            this._gotoTop,
            this._gotoButtom,
            this.toolStripSeparator1,
            this._copyAll,
            this._copyAllIncludeHeader,
            this._removeAll,
            this._removeAllToolStripMenuItem,
            this._importFromTextFileToolStripMenuItem,
            this._importFromTextFileFastToolStripMenuItem});
            this._contextMenuStrip.Name = "_contextMenuStrip";
            this._contextMenuStrip.Size = new System.Drawing.Size(285, 242);
            // 
            // _deleteRow
            // 
            this._deleteRow.Image = global::MyLib.Resource16x16.delete;
            this._deleteRow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._deleteRow.Name = "_deleteRow";
            this._deleteRow.Size = new System.Drawing.Size(284, 22);
            this._deleteRow.Text = "Delete Row";
            this._deleteRow.Click += new System.EventHandler(this._deleteRow_Click);
            // 
            // _insertRow
            // 
            this._insertRow.Image = global::MyLib.Resource16x16.add;
            this._insertRow.Name = "_insertRow";
            this._insertRow.Size = new System.Drawing.Size(284, 22);
            this._insertRow.Text = "Insert Row";
            this._insertRow.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(281, 6);
            // 
            // _gotoTop
            // 
            this._gotoTop.Image = global::MyLib.Resource16x16.nav_up_blue;
            this._gotoTop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._gotoTop.Name = "_gotoTop";
            this._gotoTop.Size = new System.Drawing.Size(284, 22);
            this._gotoTop.Text = "Goto Top";
            this._gotoTop.Click += new System.EventHandler(this._toolStripMenuGotoTop_Click);
            // 
            // _gotoButtom
            // 
            this._gotoButtom.Image = global::MyLib.Resource16x16.nav_down_blue;
            this._gotoButtom.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._gotoButtom.Name = "_gotoButtom";
            this._gotoButtom.Size = new System.Drawing.Size(284, 22);
            this._gotoButtom.Text = "Goto Bottom";
            this._gotoButtom.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(281, 6);
            // 
            // _copyAll
            // 
            this._copyAll.Image = global::MyLib.Resource16x16.copy;
            this._copyAll.Name = "_copyAll";
            this._copyAll.Size = new System.Drawing.Size(284, 22);
            this._copyAll.Text = "Copy All";
            this._copyAll.Click += new System.EventHandler(this._copy_Click);
            // 
            // _copyAllIncludeHeader
            // 
            this._copyAllIncludeHeader.Image = global::MyLib.Properties.Resources.copy;
            this._copyAllIncludeHeader.Name = "_copyAllIncludeHeader";
            this._copyAllIncludeHeader.Size = new System.Drawing.Size(284, 22);
            this._copyAllIncludeHeader.Text = "Copy All (Include Header)";
            this._copyAllIncludeHeader.Click += new System.EventHandler(this._copyAllIncludeHeader_Click);
            // 
            // _removeAll
            // 
            this._removeAll.Name = "_removeAll";
            this._removeAll.Size = new System.Drawing.Size(281, 6);
            // 
            // _removeAllToolStripMenuItem
            // 
            this._removeAllToolStripMenuItem.Enabled = false;
            this._removeAllToolStripMenuItem.Image = global::MyLib.Resource16x16.garbage_empty;
            this._removeAllToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._removeAllToolStripMenuItem.Name = "_removeAllToolStripMenuItem";
            this._removeAllToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this._removeAllToolStripMenuItem.Text = "Remove All";
            this._removeAllToolStripMenuItem.Click += new System.EventHandler(this.removeAllToolStripMenuItem_Click);
            // 
            // _importFromTextFileToolStripMenuItem
            // 
            this._importFromTextFileToolStripMenuItem.Image = global::MyLib.Properties.Resources.folder_into;
            this._importFromTextFileToolStripMenuItem.Name = "_importFromTextFileToolStripMenuItem";
            this._importFromTextFileToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this._importFromTextFileToolStripMenuItem.Text = "Import (Text File,Clipboard)";
            this._importFromTextFileToolStripMenuItem.Click += new System.EventHandler(this._importFromTextFileToolStripMenuItem_Click);
            // 
            // _importFromTextFileFastToolStripMenuItem
            // 
            this._importFromTextFileFastToolStripMenuItem.Image = global::MyLib.Properties.Resources.flash1;
            this._importFromTextFileFastToolStripMenuItem.Name = "_importFromTextFileFastToolStripMenuItem";
            this._importFromTextFileFastToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this._importFromTextFileFastToolStripMenuItem.Text = "Import (Text file,Clipboard Fast mode)";
            this._importFromTextFileFastToolStripMenuItem.Click += new System.EventHandler(this._importFromTextFileFastToolStripMenuItem_Click);
            // 
            // _vScrollBar1
            // 
            this._vScrollBar1.Location = new System.Drawing.Point(308, 36);
            this._vScrollBar1.Name = "_vScrollBar1";
            this._vScrollBar1.Size = new System.Drawing.Size(17, 80);
            this._vScrollBar1.TabIndex = 1;
            // 
            // _hScrollBar1
            // 
            this._hScrollBar1.Location = new System.Drawing.Point(24, 203);
            this._hScrollBar1.Name = "_hScrollBar1";
            this._hScrollBar1.Size = new System.Drawing.Size(80, 17);
            this._hScrollBar1.TabIndex = 2;
            // 
            // _myGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ContextMenuStrip = this._contextMenuStrip;
            this.Controls.Add(this._hScrollBar1);
            this.Controls.Add(this._vScrollBar1);
            this.Font = new System.Drawing.Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size); //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_myGrid";
            this.Size = new System.Drawing.Size(325, 223);
            this.Load += new System.EventHandler(this.myGrid_Load);
            this._contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip _contextMenuStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator _removeAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem _deleteRow;
		private System.Windows.Forms.ToolStripMenuItem _insertRow;
		private System.Windows.Forms.ToolStripMenuItem _gotoTop;
        private System.Windows.Forms.ToolStripMenuItem _gotoButtom;
        private System.Windows.Forms.ToolStripMenuItem _removeAllToolStripMenuItem;
        public _myVScrollBar _vScrollBar1;
        private _myHScrollBar _hScrollBar1;
        private System.Windows.Forms.ToolStripMenuItem _copyAll;
        public System.Windows.Forms.ToolStripMenuItem _importFromTextFileToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem _importFromTextFileFastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _copyAllIncludeHeader;

    }
}
