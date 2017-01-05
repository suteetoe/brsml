namespace SMLERPGL._display
{
    partial class _glListSum
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
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myPanel1 = new MyLib._myPanel();
            this._listGrid = new MyLib._myGrid();
            this._screenTop = new SMLERPGL._display._selectAccountAndPeriod();
            this._processButton = new MyLib.ToolStripMyButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._myPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPGL.Resource16x16.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "แสดงข้อมูล (F12)";
            this._closeButton.Size = new System.Drawing.Size(74, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._listGrid);
            this._myPanel1.Controls.Add(this._screenTop);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(4);
            this._myPanel1.Size = new System.Drawing.Size(720, 447);
            this._myPanel1.TabIndex = 3;
            // 
            // _listGrid
            // 
            this._listGrid._extraWordShow = true;
            this._listGrid._selectRow = -1;
            this._listGrid.BackColor = System.Drawing.SystemColors.Window;
            this._listGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._listGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._listGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._listGrid.Location = new System.Drawing.Point(4, 48);
            this._listGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._listGrid.Name = "_listGrid";
            this._listGrid.Size = new System.Drawing.Size(712, 395);
            this._listGrid.TabIndex = 1;
            this._listGrid.TabStop = false;
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.AutoSize = true;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(4, 4);
            this._screenTop.Name = "_screenTop";
            this._screenTop.ScreenType = SMLERPGL._display._selectAccountAndPeriodType.แยกประเภท_สรุป;
            this._screenTop.Size = new System.Drawing.Size(712, 44);
            this._screenTop.TabIndex = 2;
            // 
            // _processButton
            // 
            this._processButton.Image = global::SMLERPGL.Resource16x16.flash;
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Padding = new System.Windows.Forms.Padding(1);
            this._processButton.ResourceName = "แสดงข้อมูล (F12)";
            this._processButton.Size = new System.Drawing.Size(108, 22);
            this._processButton.Text = "แสดงข้อมูล (F12)";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processButton,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(720, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _glListSum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_glListSum";
            this.Size = new System.Drawing.Size(720, 472);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib.ToolStripMyButton _closeButton;
        private MyLib._myPanel _myPanel1;
        private MyLib._myGrid _listGrid;
        private _selectAccountAndPeriod _screenTop;
        private MyLib.ToolStripMyButton _processButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}
