namespace SMLERPICInfo
{
    partial class _stkProfit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_stkProfit));
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._resultGrid = new MyLib._myGrid();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _processButton
            // 
            this._processButton.Image = ((System.Drawing.Image)(resources.GetObject("_processButton.Image")));
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(67, 22);
            this._processButton.Text = "Process";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = ((System.Drawing.Image)(resources.GetObject("_closeButton.Image")));
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _toolStrip
            // 
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processButton,
            this._closeButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(713, 25);
            this._toolStrip.TabIndex = 6;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _resultGrid
            // 
            this._resultGrid._extraWordShow = true;
            this._resultGrid._selectRow = -1;
            this._resultGrid.BackColor = System.Drawing.SystemColors.Window;
            this._resultGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._resultGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._resultGrid.IsEdit = false;
            this._resultGrid.Location = new System.Drawing.Point(0, 25);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(713, 499);
            this._resultGrid.TabIndex = 8;
            this._resultGrid.TabStop = false;
            // 
            // _stkProfit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._resultGrid);
            this.Controls.Add(this._toolStrip);
            this.Name = "_stkProfit";
            this.Size = new System.Drawing.Size(713, 524);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton _processButton;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private MyLib._myGrid _resultGrid;
        public System.Windows.Forms.ToolStrip _toolStrip;
    }
}
