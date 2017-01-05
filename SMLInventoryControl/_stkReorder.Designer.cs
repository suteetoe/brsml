namespace SMLInventoryControl
{
    partial class _stkReorder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_stkReorder));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._stkReorderConditionScreen1 = new SMLInventoryControl._stkReorderConditionScreen();
            this._resultGrid = new MyLib._myGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._purchaseInfoGrid = new MyLib._myGrid();
            this._buyInfoGrid = new MyLib._myGrid();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processButton,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(682, 25);
            this.toolStrip1.TabIndex = 7;
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
            // _stkReorderConditionScreen1
            // 
            this._stkReorderConditionScreen1._isChange = false;
            this._stkReorderConditionScreen1.AutoSize = true;
            this._stkReorderConditionScreen1.BackColor = System.Drawing.Color.Transparent;
            this._stkReorderConditionScreen1.Dock = System.Windows.Forms.DockStyle.Top;
            this._stkReorderConditionScreen1.Location = new System.Drawing.Point(0, 25);
            this._stkReorderConditionScreen1.Name = "_stkReorderConditionScreen1";
            this._stkReorderConditionScreen1.Size = new System.Drawing.Size(682, 23);
            this._stkReorderConditionScreen1.TabIndex = 8;
            // 
            // _resultGrid
            // 
            this._resultGrid._extraWordShow = true;
            this._resultGrid.BackColor = System.Drawing.SystemColors.Window;
            this._resultGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._resultGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._resultGrid.IsEdit = false;
            this._resultGrid.Location = new System.Drawing.Point(0, 0);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(682, 276);
            this._resultGrid.TabIndex = 9;
            this._resultGrid.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 48);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._resultGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(682, 553);
            this.splitContainer1.SplitterDistance = 276;
            this.splitContainer1.TabIndex = 10;
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
            this.splitContainer2.Panel1.Controls.Add(this._purchaseInfoGrid);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._buyInfoGrid);
            this.splitContainer2.Size = new System.Drawing.Size(682, 273);
            this.splitContainer2.SplitterDistance = 124;
            this.splitContainer2.TabIndex = 0;
            // 
            // _purchaseInfoGrid
            // 
            this._purchaseInfoGrid._extraWordShow = true;
            this._purchaseInfoGrid.BackColor = System.Drawing.SystemColors.Window;
            this._purchaseInfoGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._purchaseInfoGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._purchaseInfoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._purchaseInfoGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._purchaseInfoGrid.IsEdit = false;
            this._purchaseInfoGrid.Location = new System.Drawing.Point(0, 0);
            this._purchaseInfoGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._purchaseInfoGrid.Name = "_purchaseInfoGrid";
            this._purchaseInfoGrid.Size = new System.Drawing.Size(682, 124);
            this._purchaseInfoGrid.TabIndex = 10;
            this._purchaseInfoGrid.TabStop = false;
            // 
            // _buyInfoGrid
            // 
            this._buyInfoGrid._extraWordShow = true;
            this._buyInfoGrid.BackColor = System.Drawing.SystemColors.Window;
            this._buyInfoGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._buyInfoGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._buyInfoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buyInfoGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._buyInfoGrid.IsEdit = false;
            this._buyInfoGrid.Location = new System.Drawing.Point(0, 0);
            this._buyInfoGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._buyInfoGrid.Name = "_buyInfoGrid";
            this._buyInfoGrid.Size = new System.Drawing.Size(682, 145);
            this._buyInfoGrid.TabIndex = 11;
            this._buyInfoGrid.TabStop = false;
            // 
            // _stkReorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._stkReorderConditionScreen1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_stkReorder";
            this.Size = new System.Drawing.Size(682, 601);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _processButton;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private _stkReorderConditionScreen _stkReorderConditionScreen1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private MyLib._myGrid _purchaseInfoGrid;
        private MyLib._myGrid _buyInfoGrid;
        public MyLib._myGrid _resultGrid;
    }
}
