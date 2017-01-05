namespace SMLERPICInfo
{
    partial class _stkBalanceByLot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_stkBalanceByLot));
            this._conditionScreen = new SMLERPICInfo._stkConditionScreen();
            this._resultGrid = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._movementGrid = new MyLib._myGrid();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _conditionScreen
            // 
            this._conditionScreen._isChange = false;
            this._conditionScreen._stkConditionType = SMLERPICInfo._stkConditionControlEnum.สินค้าคงเหลือตามLOT;
            this._conditionScreen.AutoSize = true;
            this._conditionScreen.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._conditionScreen.Location = new System.Drawing.Point(0, 0);
            this._conditionScreen.Name = "_conditionScreen";
            this._conditionScreen.Size = new System.Drawing.Size(805, 72);
            this._conditionScreen.TabIndex = 0;
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
            this._resultGrid.Location = new System.Drawing.Point(0, 0);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(805, 141);
            this._resultGrid.TabIndex = 10;
            this._resultGrid.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processButton,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 72);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(805, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 97);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._resultGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._movementGrid);
            this.splitContainer1.Size = new System.Drawing.Size(805, 276);
            this.splitContainer1.SplitterDistance = 141;
            this.splitContainer1.TabIndex = 11;
            // 
            // _movementGrid
            // 
            this._movementGrid._extraWordShow = true;
            this._movementGrid._selectRow = -1;
            this._movementGrid.BackColor = System.Drawing.SystemColors.Window;
            this._movementGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._movementGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._movementGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._movementGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._movementGrid.IsEdit = false;
            this._movementGrid.Location = new System.Drawing.Point(0, 0);
            this._movementGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._movementGrid.Name = "_movementGrid";
            this._movementGrid.Size = new System.Drawing.Size(805, 131);
            this._movementGrid.TabIndex = 11;
            this._movementGrid.TabStop = false;
            // 
            // _stkBalanceByLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._conditionScreen);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_stkBalanceByLot";
            this.Size = new System.Drawing.Size(805, 373);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private _stkConditionScreen _conditionScreen;
        private MyLib._myGrid _resultGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _processButton;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myGrid _movementGrid;
    }
}
