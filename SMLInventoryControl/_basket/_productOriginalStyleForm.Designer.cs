namespace SMLInventoryControl._basket
{
    partial class _productOriginalStyleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_productOriginalStyleForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._dataList = new MyLib._myDataList();
            this._unitControl = new SMLERPControl._ic._icmainGridUnitControl();
            this._basketGrid = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._dataList);
            this.splitContainer1.Panel1.Controls.Add(this._unitControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._basketGrid);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(701, 756);
            this.splitContainer1.SplitterDistance = 438;
            this.splitContainer1.TabIndex = 0;
            // 
            // _dataList
            // 
            this._dataList._extraWhere = "";
            this._dataList._multiSelect = false;
            this._dataList.BackColor = System.Drawing.Color.WhiteSmoke;
            this._dataList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._dataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataList.Location = new System.Drawing.Point(0, 0);
            this._dataList.Margin = new System.Windows.Forms.Padding(0);
            this._dataList.Name = "_dataList";
            this._dataList.Size = new System.Drawing.Size(699, 329);
            this._dataList.TabIndex = 0;
            this._dataList.TabStop = false;
            // 
            // _unitControl
            // 
            this._unitControl._extraWordShow = true;
            this._unitControl._selectRow = -1;
            this._unitControl.BackColor = System.Drawing.SystemColors.Window;
            this._unitControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._unitControl.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._unitControl.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._unitControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._unitControl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._unitControl.Location = new System.Drawing.Point(0, 329);
            this._unitControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._unitControl.Name = "_unitControl";
            this._unitControl.Size = new System.Drawing.Size(699, 107);
            this._unitControl.TabIndex = 1;
            this._unitControl.TabStop = false;
            // 
            // _basketGrid
            // 
            this._basketGrid._extraWordShow = true;
            this._basketGrid._selectRow = -1;
            this._basketGrid.BackColor = System.Drawing.SystemColors.Window;
            this._basketGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._basketGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._basketGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._basketGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._basketGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._basketGrid.Location = new System.Drawing.Point(0, 25);
            this._basketGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._basketGrid.Name = "_basketGrid";
            this._basketGrid.Size = new System.Drawing.Size(699, 287);
            this._basketGrid.TabIndex = 0;
            this._basketGrid.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(699, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(126, 22);
            this.toolStripButton1.Text = "Move All to Screen";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton2.Text = "Clear All";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // _productOriginalStyleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 756);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_productOriginalStyleForm";
            this.Text = "Product Basket";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myDataList _dataList;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private SMLERPControl._ic._icmainGridUnitControl _unitControl;
        public MyLib._myGrid _basketGrid;
    }
}