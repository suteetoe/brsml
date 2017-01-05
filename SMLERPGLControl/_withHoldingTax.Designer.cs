namespace SMLERPGLControl
{
    partial class _withHoldingTax
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
            this._mainGrid = new MyLib._myGrid();
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._addButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonDelete = new MyLib.ToolStripMyButton();
            this.printSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._printWithHoldingTagButton = new MyLib.ToolStripMyButton();
            this._myPanel1 = new MyLib._myPanel();
            this._detailGrid = new MyLib._myGrid();
            this._detailScreen = new MyLib._myScreen();
            this._printSelectRecordButton = new MyLib.ToolStripMyButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._myToolbar.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._mainGrid);
            this.splitContainer1.Panel1.Controls.Add(this._myToolbar);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._myPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(643, 532);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.TabIndex = 0;
            // 
            // _mainGrid
            // 
            this._mainGrid._extraWordShow = true;
            this._mainGrid._selectRow = -1;
            this._mainGrid.BackColor = System.Drawing.SystemColors.Window;
            this._mainGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._mainGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._mainGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._mainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mainGrid.Location = new System.Drawing.Point(0, 25);
            this._mainGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._mainGrid.Name = "_mainGrid";
            this._mainGrid.Size = new System.Drawing.Size(643, 152);
            this._mainGrid.TabIndex = 0;
            this._mainGrid.TabStop = false;
            this._mainGrid.Load += new System.EventHandler(this._mainGrid_Load);
            // 
            // _myToolbar
            // 
            this._myToolbar.BackColor = System.Drawing.Color.Transparent;
            this._myToolbar.BackgroundImage = global::SMLERPGLControl.Properties.Resources.bt03;
            this._myToolbar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._addButton,
            this.toolStripSeparator1,
            this._buttonDelete,
            this.printSeparator,
            this._printWithHoldingTagButton,
            this._printSelectRecordButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(643, 25);
            this._myToolbar.TabIndex = 7;
            this._myToolbar.Text = "toolStrip1";
            // 
            // _addButton
            // 
            this._addButton.Image = global::SMLERPGLControl.Properties.Resources.add;
            this._addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._addButton.Name = "_addButton";
            this._addButton.Padding = new System.Windows.Forms.Padding(1);
            this._addButton.ResourceName = "เพิ่ม";
            this._addButton.Size = new System.Drawing.Size(47, 22);
            this._addButton.Text = "เพิ่ม";
            this._addButton.Click += new System.EventHandler(this._addButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonDelete
            // 
            this._buttonDelete.Image = global::SMLERPGLControl.Properties.Resources.delete;
            this._buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonDelete.Name = "_buttonDelete";
            this._buttonDelete.Padding = new System.Windows.Forms.Padding(1);
            this._buttonDelete.ResourceName = "ลบ";
            this._buttonDelete.Size = new System.Drawing.Size(44, 22);
            this._buttonDelete.Text = "ลบ";
            this._buttonDelete.Click += new System.EventHandler(this._buttonDelete_Click);
            // 
            // printSeparator
            // 
            this.printSeparator.Name = "printSeparator";
            this.printSeparator.Size = new System.Drawing.Size(6, 25);
            this.printSeparator.Visible = false;
            // 
            // _printWithHoldingTagButton
            // 
            this._printWithHoldingTagButton.Image = global::SMLERPGLControl.Properties.Resources.printer;
            this._printWithHoldingTagButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._printWithHoldingTagButton.Name = "_printWithHoldingTagButton";
            this._printWithHoldingTagButton.Padding = new System.Windows.Forms.Padding(1);
            this._printWithHoldingTagButton.ResourceName = "พิมพ์ทั้งหมด";
            this._printWithHoldingTagButton.Size = new System.Drawing.Size(87, 22);
            this._printWithHoldingTagButton.Text = "พิมพ์ทั้งหมด";
            this._printWithHoldingTagButton.Visible = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._detailGrid);
            this._myPanel1.Controls.Add(this._detailScreen);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(643, 351);
            this._myPanel1.TabIndex = 2;
            // 
            // _detailGrid
            // 
            this._detailGrid._extraWordShow = true;
            this._detailGrid._selectRow = -1;
            this._detailGrid.BackColor = System.Drawing.SystemColors.Window;
            this._detailGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._detailGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._detailGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._detailGrid.Location = new System.Drawing.Point(0, 0);
            this._detailGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._detailGrid.Name = "_detailGrid";
            this._detailGrid.Size = new System.Drawing.Size(643, 351);
            this._detailGrid.TabIndex = 1;
            this._detailGrid.TabStop = false;
            // 
            // _detailScreen
            // 
            this._detailScreen._isChange = false;
            this._detailScreen.AutoSize = true;
            this._detailScreen.BackColor = System.Drawing.Color.Transparent;
            this._detailScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._detailScreen.Location = new System.Drawing.Point(0, 0);
            this._detailScreen.Name = "_detailScreen";
            this._detailScreen.Size = new System.Drawing.Size(643, 0);
            this._detailScreen.TabIndex = 0;
            // 
            // _printSelectRecordButton
            // 
            this._printSelectRecordButton.Enabled = false;
            this._printSelectRecordButton.Image = global::SMLERPGLControl.Properties.Resources.printer;
            this._printSelectRecordButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._printSelectRecordButton.Name = "_printSelectRecordButton";
            this._printSelectRecordButton.Padding = new System.Windows.Forms.Padding(1);
            this._printSelectRecordButton.ResourceName = "พิมพ์รายการที่เลือก";
            this._printSelectRecordButton.Size = new System.Drawing.Size(120, 22);
            this._printSelectRecordButton.Text = "พิมพ์รายการที่เลือก";
            this._printSelectRecordButton.Visible = false;
            // 
            // _withHoldingTax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_withHoldingTax";
            this.Size = new System.Drawing.Size(643, 532);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myGrid _detailGrid;
        private MyLib._myScreen _detailScreen;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton _addButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public MyLib.ToolStripMyButton _buttonDelete;
        public MyLib._myGrid _mainGrid;
        public MyLib.ToolStripMyButton _printWithHoldingTagButton;
        public System.Windows.Forms.ToolStripSeparator printSeparator;
        public MyLib.ToolStripMyButton _printSelectRecordButton;
    }
}
