namespace SMLERPControl._ic
{
    partial class _icPriceFormulaDetail
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._templateComboBox = new System.Windows.Forms.ToolStripComboBox();
            this._grid = new MyLib._myGrid();
            this._gridResult = new MyLib._myGrid();
            this._copyAllButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._grid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._gridResult);
            this.splitContainer1.Size = new System.Drawing.Size(1165, 420);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.TabIndex = 1;
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._copyAllButton,
            this._templateComboBox});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(1165, 25);
            this._toolStrip.TabIndex = 2;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _templateComboBox
            // 
            this._templateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._templateComboBox.MaxDropDownItems = 15;
            this._templateComboBox.Name = "_templateComboBox";
            this._templateComboBox.Size = new System.Drawing.Size(321, 25);
            // 
            // _grid
            // 
            this._grid._extraWordShow = true;
            this._grid._selectRow = -1;
            this._grid.BackColor = System.Drawing.SystemColors.Window;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._grid.Location = new System.Drawing.Point(0, 0);
            this._grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(1165, 201);
            this._grid.TabIndex = 0;
            this._grid.TabStop = false;
            // 
            // _gridResult
            // 
            this._gridResult._extraWordShow = true;
            this._gridResult._selectRow = -1;
            this._gridResult.BackColor = System.Drawing.SystemColors.Window;
            this._gridResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridResult.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridResult.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridResult.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridResult.IsEdit = false;
            this._gridResult.Location = new System.Drawing.Point(0, 0);
            this._gridResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridResult.Name = "_gridResult";
            this._gridResult.Size = new System.Drawing.Size(1165, 215);
            this._gridResult.TabIndex = 1;
            this._gridResult.TabStop = false;
            // 
            // _copyAllButton
            // 
            this._copyAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._copyAllButton.Name = "_copyAllButton";
            this._copyAllButton.Size = new System.Drawing.Size(119, 22);
            this._copyAllButton.Text = "Formula Copy All";
            this._copyAllButton.Click += new System.EventHandler(this._copyAllButton_Click);
            // 
            // _icPriceFormulaDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._toolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icPriceFormulaDetail";
            this.Size = new System.Drawing.Size(1165, 445);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MyLib._myGrid _grid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public MyLib._myGrid _gridResult;
        private System.Windows.Forms.ToolStripButton _copyAllButton;
        private System.Windows.Forms.ToolStripComboBox _templateComboBox;
        public System.Windows.Forms.ToolStrip _toolStrip;
    }
}
