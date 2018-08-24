namespace SMLEDIControl
{
    partial class _ediReceive
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._selectAllButton = new MyLib.ToolStripMyButton();
            this._selectNoneButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new MyLib.ToolStripMyButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._docGrid = new MyLib._myGrid();
            this.textShowData = new System.Windows.Forms.TextBox();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._importButton = new MyLib.VistaButton();
            this._reloadButton = new MyLib.VistaButton();
            this._detailGrid = new MyLib._myGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._icTransScreenTopControl1 = new SMLInventoryControl._icTransScreenTopControl();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectAllButton,
            this._selectNoneButton,
            this.toolStripSeparator1,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(920, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _selectAllButton
            // 
            this._selectAllButton.Image = global::SMLEDIControl.Properties.Resources.preferences1;
            this._selectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Padding = new System.Windows.Forms.Padding(1);
            this._selectAllButton.ResourceName = "เลือกทั้งหมด";
            this._selectAllButton.Size = new System.Drawing.Size(84, 22);
            this._selectAllButton.Text = "เลือกทั้งหมด";
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _selectNoneButton
            // 
            this._selectNoneButton.Image = global::SMLEDIControl.Properties.Resources.delete2;
            this._selectNoneButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectNoneButton.Name = "_selectNoneButton";
            this._selectNoneButton.Padding = new System.Windows.Forms.Padding(1);
            this._selectNoneButton.ResourceName = "ไม่เลือกทั้งหมด";
            this._selectNoneButton.Size = new System.Drawing.Size(96, 22);
            this._selectNoneButton.Text = "ไม่เลือกทั้งหมด";
            this._selectNoneButton.Click += new System.EventHandler(this._selectNoneButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLEDIControl.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "Close";
            this._closeButton.Size = new System.Drawing.Size(58, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this._myFlowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._detailGrid);
            this.splitContainer1.Size = new System.Drawing.Size(920, 956);
            this.splitContainer1.SplitterDistance = 523;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textShowData);
            this.splitContainer2.Size = new System.Drawing.Size(521, 918);
            this.splitContainer2.SplitterDistance = 610;
            this.splitContainer2.TabIndex = 2;
            // 
            // _docGrid
            // 
            this._docGrid._extraWordShow = true;
            this._docGrid._selectRow = -1;
            this._docGrid.BackColor = System.Drawing.SystemColors.Window;
            this._docGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._docGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._docGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._docGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._docGrid.Location = new System.Drawing.Point(0, 0);
            this._docGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._docGrid.Name = "_docGrid";
            this._docGrid.Size = new System.Drawing.Size(521, 545);
            this._docGrid.TabIndex = 0;
            this._docGrid.TabStop = false;
            // 
            // textShowData
            // 
            this.textShowData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textShowData.Location = new System.Drawing.Point(0, 0);
            this.textShowData.Multiline = true;
            this.textShowData.Name = "textShowData";
            this.textShowData.Size = new System.Drawing.Size(521, 304);
            this.textShowData.TabIndex = 0;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._importButton);
            this._myFlowLayoutPanel1.Controls.Add(this._reloadButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 918);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(521, 36);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // _importButton
            // 
            this._importButton._drawNewMethod = false;
            this._importButton.BackColor = System.Drawing.Color.Transparent;
            this._importButton.ButtonText = "Import";
            this._importButton.Location = new System.Drawing.Point(424, 3);
            this._importButton.Name = "_importButton";
            this._importButton.Size = new System.Drawing.Size(94, 27);
            this._importButton.TabIndex = 0;
            this._importButton.Text = "Import";
            this._importButton.UseVisualStyleBackColor = false;
            this._importButton.Click += new System.EventHandler(this._importButton_Click);
            // 
            // _reloadButton
            // 
            this._reloadButton._drawNewMethod = false;
            this._reloadButton.BackColor = System.Drawing.Color.Transparent;
            this._reloadButton.ButtonText = "Reload";
            this._reloadButton.Location = new System.Drawing.Point(324, 3);
            this._reloadButton.Name = "_reloadButton";
            this._reloadButton.Size = new System.Drawing.Size(94, 27);
            this._reloadButton.TabIndex = 1;
            this._reloadButton.Text = "Reload";
            this._reloadButton.UseVisualStyleBackColor = false;
            this._reloadButton.Click += new System.EventHandler(this._reloadButton_Click);
            // 
            // _detailGrid
            // 
            this._detailGrid._extraWordShow = true;
            this._detailGrid._selectRow = -1;
            this._detailGrid.BackColor = System.Drawing.SystemColors.Window;
            this._detailGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._detailGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._detailGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._detailGrid.Location = new System.Drawing.Point(0, 0);
            this._detailGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._detailGrid.Name = "_detailGrid";
            this._detailGrid.Size = new System.Drawing.Size(391, 954);
            this._detailGrid.TabIndex = 1;
            this._detailGrid.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // _icTransScreenTopControl1
            // 
            this._icTransScreenTopControl1._icTransControlType = _g.g._transControlTypeEnum.ว่าง;
            this._icTransScreenTopControl1._isChange = false;
            this._icTransScreenTopControl1.BackColor = System.Drawing.Color.Transparent;
            this._icTransScreenTopControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this._icTransScreenTopControl1.Location = new System.Drawing.Point(0, 0);
            this._icTransScreenTopControl1.Name = "_icTransScreenTopControl1";
            this._icTransScreenTopControl1.Size = new System.Drawing.Size(521, 68);
            this._icTransScreenTopControl1.TabIndex = 8;
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
            this.splitContainer3.Panel1.Controls.Add(this._icTransScreenTopControl1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this._docGrid);
            this.splitContainer3.Size = new System.Drawing.Size(521, 610);
            this.splitContainer3.SplitterDistance = 61;
            this.splitContainer3.TabIndex = 9;
            // 
            // _ediReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_ediReceive";
            this.Size = new System.Drawing.Size(920, 981);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myGrid _docGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _closeButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _importButton;
        private MyLib.VistaButton _reloadButton;
        private MyLib._myGrid _detailGrid;
        private MyLib.ToolStripMyButton _selectAllButton;
        private MyLib.ToolStripMyButton _selectNoneButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox textShowData;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private SMLInventoryControl._icTransScreenTopControl _icTransScreenTopControl1;
        private System.Windows.Forms.SplitContainer splitContainer3;
    }
}
