namespace BRInterfaceControl.SOS
{
    partial class _singhaOnlineOrderImport
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._docGrid = new MyLib._myGrid();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._importButton = new MyLib.VistaButton();
            this._reloadButton = new MyLib.VistaButton();
            this._detailGrid = new MyLib._myGrid();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._selectAllButton = new MyLib.ToolStripMyButton();
            this._selectNoneButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
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
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._docGrid);
            this.splitContainer1.Panel1.Controls.Add(this._myFlowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._detailGrid);
            this.splitContainer1.Size = new System.Drawing.Size(920, 956);
            this.splitContainer1.SplitterDistance = 497;
            this.splitContainer1.TabIndex = 5;
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
            this._docGrid.Size = new System.Drawing.Size(495, 921);
            this._docGrid.TabIndex = 0;
            this._docGrid.TabStop = false;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._importButton);
            this._myFlowLayoutPanel1.Controls.Add(this._reloadButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 921);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(495, 33);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // _importButton
            // 
            this._importButton._drawNewMethod = false;
            this._importButton.BackColor = System.Drawing.Color.Transparent;
            this._importButton.ButtonText = "Import";
            this._importButton.Location = new System.Drawing.Point(398, 3);
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
            this._reloadButton.Location = new System.Drawing.Point(298, 3);
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
            this._detailGrid.Size = new System.Drawing.Size(417, 954);
            this._detailGrid.TabIndex = 1;
            this._detailGrid.TabStop = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _selectAllButton
            // 
            this._selectAllButton.Image = global::BRInterfaceControl.Properties.Resources.preferences1;
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
            this._selectNoneButton.Image = global::BRInterfaceControl.Properties.Resources.delete2;
            this._selectNoneButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectNoneButton.Name = "_selectNoneButton";
            this._selectNoneButton.Padding = new System.Windows.Forms.Padding(1);
            this._selectNoneButton.ResourceName = "ไม่เลือกทั้งหมด";
            this._selectNoneButton.Size = new System.Drawing.Size(96, 22);
            this._selectNoneButton.Text = "ไม่เลือกทั้งหมด";
            this._selectNoneButton.Click += new System.EventHandler(this._selectNoneButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::BRInterfaceControl.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "Close";
            this._closeButton.Size = new System.Drawing.Size(58, 22);
            this._closeButton.Text = "Close";
            // 
            // _singhaOnlineOrderImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_singhaOnlineOrderImport";
            this.Size = new System.Drawing.Size(920, 981);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this._myFlowLayoutPanel1.ResumeLayout(false);
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
    }
}
