namespace BRInterface.saletools
{
    partial class import_request_transfer
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
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(765, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._docGrid);
            this.splitContainer1.Panel1.Controls.Add(this._myFlowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._detailGrid);
            this.splitContainer1.Size = new System.Drawing.Size(765, 880);
            this.splitContainer1.SplitterDistance = 422;
            this.splitContainer1.TabIndex = 1;
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
            this._docGrid.Size = new System.Drawing.Size(763, 389);
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
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 389);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(763, 31);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // _importButton
            // 
            this._importButton._drawNewMethod = false;
            this._importButton.BackColor = System.Drawing.Color.Transparent;
            this._importButton.ButtonText = "Import";
            this._importButton.Location = new System.Drawing.Point(679, 3);
            this._importButton.myImage = global::BRInterface.Properties.Resources.flash;
            this._importButton.Name = "_importButton";
            this._importButton.Size = new System.Drawing.Size(81, 25);
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
            this._reloadButton.Location = new System.Drawing.Point(592, 3);
            this._reloadButton.myImage = global::BRInterface.Properties.Resources.refresh;
            this._reloadButton.Name = "_reloadButton";
            this._reloadButton.Size = new System.Drawing.Size(81, 25);
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
            this._detailGrid.Size = new System.Drawing.Size(763, 452);
            this._detailGrid.TabIndex = 1;
            this._detailGrid.TabStop = false;
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::BRInterface.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "Close";
            this._closeButton.Size = new System.Drawing.Size(58, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // import_request_transfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "import_request_transfer";
            this.Size = new System.Drawing.Size(765, 905);
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

        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _closeButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myGrid _docGrid;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myGrid _detailGrid;
        private MyLib.VistaButton _importButton;
        private MyLib.VistaButton _reloadButton;
    }
}
