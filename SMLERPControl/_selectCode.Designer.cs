namespace SMLERPControl
{
    partial class _selectCode
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
            this.components = new System.ComponentModel.Container();
            this._dataGrid = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonRefresh = new MyLib.ToolStripMyButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonSelectAll = new MyLib.ToolStripMyButton();
            this._buttonRemoveAll = new MyLib.ToolStripMyButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _dataGrid
            // 
            this._dataGrid.BackColor = System.Drawing.SystemColors.Window;
            this._dataGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._dataGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataGrid.IsEdit = false;
            this._dataGrid.Location = new System.Drawing.Point(0, 25);
            this._dataGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._dataGrid.Name = "_dataGrid";
            this._dataGrid.Size = new System.Drawing.Size(626, 414);
            this._dataGrid.TabIndex = 1;
            this._dataGrid.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::SMLERPControl.Properties.Resources.bt03;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonRefresh,
            this.toolStripSeparator2,
            this._buttonSelectAll,
            this._buttonRemoveAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(626, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonRefresh
            // 
            this._buttonRefresh.Image = global::SMLERPControl.Properties.Resources.refresh;
            this._buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonRefresh.Name = "_buttonRefresh";
            this._buttonRefresh.ResourceName = "เรียกข้อมูลใหม่";
            this._buttonRefresh.Padding = new System.Windows.Forms.Padding(1);
            this._buttonRefresh.Size = new System.Drawing.Size(98, 22);
            this._buttonRefresh.Text = "เรียกข้อมูลใหม่";
            this._buttonRefresh.Click += new System.EventHandler(this._buttonRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonSelectAll
            // 
            this._buttonSelectAll.Image = global::SMLERPControl.Properties.Resources.check;
            this._buttonSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSelectAll.Name = "_buttonSelectAll";
            this._buttonSelectAll.ResourceName = "เลือกทั้งหมด";
            this._buttonSelectAll.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSelectAll.Size = new System.Drawing.Size(88, 22);
            this._buttonSelectAll.Text = "เลือกทั้งหมด";
            this._buttonSelectAll.Click += new System.EventHandler(this._buttonSelectAll_Click);
            // 
            // _buttonRemoveAll
            // 
            this._buttonRemoveAll.Image = global::SMLERPControl.Properties.Resources.delete;
            this._buttonRemoveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonRemoveAll.Name = "_buttonRemoveAll";
            this._buttonRemoveAll.ResourceName = "ไม่เลือกทั้งหมด";
            this._buttonRemoveAll.Padding = new System.Windows.Forms.Padding(1);
            this._buttonRemoveAll.Size = new System.Drawing.Size(94, 22);
            this._buttonRemoveAll.Text = "ยกเลิกทั้งหมด";
            this._buttonRemoveAll.Click += new System.EventHandler(this._buttonRemoveAll_Click);
            // 
            // _selectCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(626, 439);
            this.Controls.Add(this._dataGrid);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_selectCode";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_selectCode";
            this.Load += new System.EventHandler(this._selectCode_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _buttonSelectAll;
        private MyLib.ToolStripMyButton _buttonRemoveAll;
        public MyLib._myGrid _dataGrid;
        private MyLib.ToolStripMyButton _buttonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}