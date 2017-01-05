namespace SMLERPControl
{
    partial class _selectWarehouseAndLocationForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._closeButton = new MyLib.VistaButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._whGrid = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._whSelectAllButton = new System.Windows.Forms.ToolStripButton();
            this._whDeSelectAllButton = new System.Windows.Forms.ToolStripButton();
            this._locationGrid = new MyLib._myGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._locationSelectAllButton = new System.Windows.Forms.ToolStripButton();
            this._locationDeSelectAllButton = new System.Windows.Forms.ToolStripButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 527);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(896, 32);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // _closeButton
            // 
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(827, 3);
            this._closeButton.myImage = global::SMLERPControl.Properties.Resources.filesave;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(64, 24);
            this._closeButton.TabIndex = 0;
            this._closeButton.Text = "Close";
            this._closeButton.UseVisualStyleBackColor = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._whGrid);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._locationGrid);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(896, 527);
            this.splitContainer1.SplitterDistance = 511;
            this.splitContainer1.TabIndex = 1;
            // 
            // _whGrid
            // 
            this._whGrid._extraWordShow = true;
            this._whGrid._selectRow = -1;
            this._whGrid.BackColor = System.Drawing.SystemColors.Window;
            this._whGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._whGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._whGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._whGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._whGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._whGrid.Location = new System.Drawing.Point(0, 0);
            this._whGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._whGrid.Name = "_whGrid";
            this._whGrid.Size = new System.Drawing.Size(509, 500);
            this._whGrid.TabIndex = 0;
            this._whGrid.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._whSelectAllButton,
            this._whDeSelectAllButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 500);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(509, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _whSelectAllButton
            // 
            this._whSelectAllButton.Image = global::SMLERPControl.Properties.Resources.checks;
            this._whSelectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._whSelectAllButton.Name = "_whSelectAllButton";
            this._whSelectAllButton.Size = new System.Drawing.Size(84, 22);
            this._whSelectAllButton.Text = "เลือกทั้งหมด";
            // 
            // _whDeSelectAllButton
            // 
            this._whDeSelectAllButton.Image = global::SMLERPControl.Properties.Resources.delete2;
            this._whDeSelectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._whDeSelectAllButton.Name = "_whDeSelectAllButton";
            this._whDeSelectAllButton.Size = new System.Drawing.Size(97, 22);
            this._whDeSelectAllButton.Text = "ไม่เลือกทั้งหมด";
            // 
            // _locationGrid
            // 
            this._locationGrid._extraWordShow = true;
            this._locationGrid._selectRow = -1;
            this._locationGrid.BackColor = System.Drawing.SystemColors.Window;
            this._locationGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._locationGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._locationGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._locationGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._locationGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._locationGrid.Location = new System.Drawing.Point(0, 0);
            this._locationGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._locationGrid.Name = "_locationGrid";
            this._locationGrid.Size = new System.Drawing.Size(379, 500);
            this._locationGrid.TabIndex = 1;
            this._locationGrid.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._locationSelectAllButton,
            this._locationDeSelectAllButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 500);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(379, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _locationSelectAllButton
            // 
            this._locationSelectAllButton.Image = global::SMLERPControl.Properties.Resources.checks;
            this._locationSelectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._locationSelectAllButton.Name = "_locationSelectAllButton";
            this._locationSelectAllButton.Size = new System.Drawing.Size(84, 22);
            this._locationSelectAllButton.Text = "เลือกทั้งหมด";
            // 
            // _locationDeSelectAllButton
            // 
            this._locationDeSelectAllButton.Image = global::SMLERPControl.Properties.Resources.delete2;
            this._locationDeSelectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._locationDeSelectAllButton.Name = "_locationDeSelectAllButton";
            this._locationDeSelectAllButton.Size = new System.Drawing.Size(97, 22);
            this._locationDeSelectAllButton.Text = "ไม่เลือกทั้งหมด";
            // 
            // _selectWarehouseAndLocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 559);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_selectWarehouseAndLocationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เลือกคลัง/ที่เก็บสินค้า";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public MyLib._myGrid _whGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _whSelectAllButton;
        private System.Windows.Forms.ToolStripButton _whDeSelectAllButton;
        public MyLib._myGrid _locationGrid;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton _locationSelectAllButton;
        private System.Windows.Forms.ToolStripButton _locationDeSelectAllButton;
        public MyLib.VistaButton _closeButton;
    }
}