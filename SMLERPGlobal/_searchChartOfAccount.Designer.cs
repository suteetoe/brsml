namespace _g
{
    partial class _searchChartOfAccount
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
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this._accountGrid = new MyLib._myGrid();
            this._toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolBar
            // 
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this._toolBar.Location = new System.Drawing.Point(0, 0);
            this._toolBar.Name = "_toolBar";
            this._toolBar.Size = new System.Drawing.Size(565, 25);
            this._toolBar.TabIndex = 0;
            this._toolBar.Text = "toolStrip1";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            // 
            // _accountGrid
            // 
            this._accountGrid._extraWordShow = true;
            this._accountGrid._selectRow = -1;
            this._accountGrid.BackColor = System.Drawing.SystemColors.Window;
            this._accountGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._accountGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._accountGrid.DisplayRowNumber = false;
            this._accountGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._accountGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._accountGrid.Location = new System.Drawing.Point(0, 25);
            this._accountGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._accountGrid.Name = "_accountGrid";
            this._accountGrid.Size = new System.Drawing.Size(565, 395);
            this._accountGrid.TabIndex = 1;
            this._accountGrid.TabStop = false;
            // 
            // _searchChartOfAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 420);
            this.Controls.Add(this._accountGrid);
            this.Controls.Add(this._toolBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_searchChartOfAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_searChartOfAccount";
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolBar;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        public MyLib._myGrid _accountGrid;
    }
}