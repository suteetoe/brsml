namespace SMLERPGL
{
    partial class _glTransScreenControl
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
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._screenTop = new MyLib._myScreen();
            this._myPanel1 = new MyLib._myPanel();
            this._tab = new MyLib._myTabControl();
            this.tab_detail = new System.Windows.Forms.TabPage();
            this._gridDetail = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonProcess = new MyLib.ToolStripMyButton();
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this._myToolBar.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._tab.SuspendLayout();
            this.tab_detail.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolBar
            // 
            this._myToolBar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._myToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this._closeButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(926, 25);
            this._myToolBar.TabIndex = 18;
            this._myToolBar.Text = "toolStrip2";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLERPGL.Properties.Resources.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.ResourceName = "บันทึกข้อมูล (F12)";
            this._buttonSave.Size = new System.Drawing.Size(122, 22);
            this._buttonSave.Text = "บันทึกข้อมูล (F12)";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPGL.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดจอ";
            this._closeButton.Size = new System.Drawing.Size(58, 22);
            this._closeButton.Text = "ปิดจอ";
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.AutoSize = true;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(0, 25);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(926, 0);
            this._screenTop.TabIndex = 21;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._tab);
            this._myPanel1.Controls.Add(this._screenTop);
            this._myPanel1.Controls.Add(this._myToolBar);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(926, 1263);
            this._myPanel1.TabIndex = 22;
            // 
            // _tab
            // 
            this._tab.Controls.Add(this.tab_detail);
            this._tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tab.Font = new System.Drawing.Font("Tahoma", 9F);
            this._tab.Location = new System.Drawing.Point(0, 25);
            this._tab.Multiline = true;
            this._tab.Name = "_tab";
            this._tab.SelectedIndex = 0;
            this._tab.Size = new System.Drawing.Size(926, 1238);
            this._tab.TabIndex = 20;
            this._tab.TableName = "ic_trans";
            // 
            // tab_detail
            // 
            this.tab_detail.Controls.Add(this._gridDetail);
            this.tab_detail.Controls.Add(this.toolStrip1);
            this.tab_detail.Location = new System.Drawing.Point(4, 23);
            this.tab_detail.Margin = new System.Windows.Forms.Padding(0);
            this.tab_detail.Name = "tab_detail";
            this.tab_detail.Padding = new System.Windows.Forms.Padding(3);
            this.tab_detail.Size = new System.Drawing.Size(918, 1211);
            this.tab_detail.TabIndex = 0;
            this.tab_detail.Text = "tab_detail";
            this.tab_detail.UseVisualStyleBackColor = true;
            // 
            // _gridDetail
            // 
            this._gridDetail._extraWordShow = true;
            this._gridDetail._selectRow = -1;
            this._gridDetail.BackColor = System.Drawing.SystemColors.Window;
            this._gridDetail.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._gridDetail.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._gridDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridDetail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridDetail.Location = new System.Drawing.Point(3, 28);
            this._gridDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridDetail.Name = "_gridDetail";
            this._gridDetail.Size = new System.Drawing.Size(912, 1180);
            this._gridDetail.TabIndex = 18;
            this._gridDetail.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonProcess});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(912, 25);
            this.toolStrip1.TabIndex = 19;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonProcess
            // 
            this._buttonProcess.Image = global::SMLERPGL.Properties.Resources.flash;
            this._buttonProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonProcess.Name = "_buttonProcess";
            this._buttonProcess.Padding = new System.Windows.Forms.Padding(1);
            this._buttonProcess.ResourceName = "ประมวลผล";
            this._buttonProcess.Size = new System.Drawing.Size(76, 22);
            this._buttonProcess.Text = "ประมวลผล";
            this._buttonProcess.Click += new System.EventHandler(this._buttonProcess_Click);
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.Location = new System.Drawing.Point(110, 3);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(780, 25);
            this.miniToolStrip.TabIndex = 19;
            // 
            // _glTransScreenControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_glTransScreenControl";
            this.Size = new System.Drawing.Size(926, 1263);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._tab.ResumeLayout(false);
            this.tab_detail.ResumeLayout(false);
            this.tab_detail.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ToolStrip _myToolBar;
        public MyLib.ToolStripMyButton _buttonSave;
        public MyLib.ToolStripMyButton _closeButton;
        public MyLib._myScreen _screenTop;
        public MyLib._myPanel _myPanel1;
        public MyLib._myTabControl _tab;
        public System.Windows.Forms.TabPage tab_detail;
        public MyLib._myGrid _gridDetail;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public MyLib.ToolStripMyButton _buttonProcess;
        private System.Windows.Forms.ToolStrip miniToolStrip;
    }
}
